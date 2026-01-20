using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Microsoft.Xaml.Behaviors;

namespace NarakaBladepoint.App.Shell.Behaviors
{
    /// <summary>
    /// ESC 键命令行为
    /// 
    /// 用途：全局监听 ESC 键按下，只在窗口处于激活状态时触发命令
    /// 使用 Windows 键盘钩子实现全局键盘监听
    /// </summary>
    public class EscCommandBehavior : Behavior<Window>
    {
        #region Windows API

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int VK_ESCAPE = 0x1B;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(
            int idHook,
            LowLevelKeyboardProc lpfn,
            IntPtr hMod,
            uint dwThreadId
        );

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(
            IntPtr hhk,
            int nCode,
            IntPtr wParam,
            IntPtr lParam
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion

        #region Dependency Properties

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(EscCommandBehavior),
            new PropertyMetadata(null)
        );

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                "CommandParameter",
                typeof(object),
                typeof(EscCommandBehavior),
                new PropertyMetadata(null)
            );

        #endregion

        #region Fields

        private IntPtr _hookId = IntPtr.Zero;
        private LowLevelKeyboardProc _hookProc;
        private Window _window;

        #endregion

        #region Behavior Lifecycle

        protected override void OnAttached()
        {
            base.OnAttached();

            _window = AssociatedObject;
            _hookProc = HookCallback;
            _hookId = SetHook(_hookProc);
        }

        protected override void OnDetaching()
        {
            if (_hookId != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookId);
                _hookId = IntPtr.Zero;
            }

            base.OnDetaching();
        }

        #endregion

        #region Hook Methods

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(
                    WH_KEYBOARD_LL,
                    proc,
                    GetModuleHandle(curModule.ModuleName),
                    0
                );
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if (vkCode == VK_ESCAPE)
                {
                    // 确保在 UI 线程上执行
                    Application.Current?.Dispatcher?.Invoke(() =>
                    {
                        // 只在窗口激活时触发命令
                        if (_window != null && _window.IsActive)
                        {
                            ExecuteCommand();
                        }
                    });
                }
            }

            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        private void ExecuteCommand()
        {
            if (Command != null && Command.CanExecute(CommandParameter))
            {
                Command.Execute(CommandParameter);
            }
        }

        #endregion
    }
}
