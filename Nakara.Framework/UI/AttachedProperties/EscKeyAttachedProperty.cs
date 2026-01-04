using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace Nakara.Framework.UI.AttachedProperties
{
    /// <summary>
    /// Esc键附加属性
    /// </summary>
    public static class EscKeyAttachedProperty
    {
        #region Win32 Hook

        private static IntPtr _hookId = IntPtr.Zero;
        private static LowLevelKeyboardProc _proc = HookCallback;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(
            int idHook,
            LowLevelKeyboardProc lpfn,
            IntPtr hMod,
            uint dwThreadId
        );

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(
            IntPtr hhk,
            int nCode,
            IntPtr wParam,
            IntPtr lParam
        );

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion Win32 Hook

        #region 附加属性

        public static ICommand GetCommand(DependencyObject obj) =>
            (ICommand)obj.GetValue(CommandProperty);

        public static void SetCommand(DependencyObject obj, ICommand value) =>
            obj.SetValue(CommandProperty, value);

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(EscKeyAttachedProperty),
                new PropertyMetadata(null, OnCommandChanged)
            );

        public static object GetCommandParameter(DependencyObject obj) =>
            obj.GetValue(CommandParameterProperty);

        public static void SetCommandParameter(DependencyObject obj, object value) =>
            obj.SetValue(CommandParameterProperty, value);

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached(
                "CommandParameter",
                typeof(object),
                typeof(EscKeyAttachedProperty),
                new PropertyMetadata(null)
            );

        #endregion 附加属性

        #region 内部状态

        // 保存绑定的控件弱引用，防止内存泄漏
        private static readonly List<WeakReference<FrameworkElement>> _elements = new();

        #endregion 内部状态

        #region 附加属性回调

        private static void OnCommandChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (d is FrameworkElement element)
            {
                // 订阅 Unloaded 事件，控件卸载时移除弱引用
                element.Unloaded -= OnElementUnloaded;
                element.Unloaded += OnElementUnloaded;

                // 保存控件弱引用
                if (!_elements.Exists(wr => wr.TryGetTarget(out var target) && target == element))
                    _elements.Add(new WeakReference<FrameworkElement>(element));

                // 安装钩子
                InstallHook();
            }
        }

        private static void OnElementUnloaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                // 移除弱引用
                _elements.RemoveAll(wr => wr.TryGetTarget(out var target) && target == element);

                // 卸载钩子，如果没有控件绑定
                if (_elements.Count == 0)
                    UninstallHook();
            }
        }

        #endregion 附加属性回调

        #region 钩子安装/卸载

        private static void InstallHook()
        {
            if (_hookId == IntPtr.Zero)
            {
                using var curProcess = Process.GetCurrentProcess();
                using var curModule = curProcess.MainModule;
                IntPtr hMod = GetModuleHandle(curModule.ModuleName);
                _hookId = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, hMod, 0);
            }
        }

        private static void UninstallHook()
        {
            if (_hookId != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookId);
                _hookId = IntPtr.Zero;
            }
        }

        #endregion 钩子安装/卸载

        #region 钩子回调

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (vkCode == 0x1B) // VK_ESCAPE
                {
                    for (int i = _elements.Count - 1; i >= 0; i--)
                    {
                        if (_elements[i].TryGetTarget(out var element))
                        {
                            // 判断控件所在窗口是否激活
                            var window = Window.GetWindow(element);
                            if (
                                window != null
                                && window.IsActive
                                && element.IsVisible
                                && element.IsLoaded
                            )
                            {
                                var command = GetCommand(element);
                                var param = GetCommandParameter(element);
                                if (command != null && command.CanExecute(param))
                                    command.Execute(param);
                            }
                        }
                        else
                        {
                            _elements.RemoveAt(i);
                        }
                    }

                    if (_elements.Count == 0)
                        UninstallHook();
                }
            }

            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        #endregion 钩子回调
    }
}
