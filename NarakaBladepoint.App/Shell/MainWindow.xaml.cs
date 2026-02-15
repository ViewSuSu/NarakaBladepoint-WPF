using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Animation;

namespace NarakaBladepoint.App.Shell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    ///
    /// 注意：此文件仅包含 UI 相关的逻辑（如窗口初始化、Windows API 调用等）
    /// 业务逻辑已提取到 Behavior 中处理
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int GWL_EXSTYLE = -20;
        private const double DesignWidth = 1280;
        private const double DesignHeight = 720;
        private const double AspectRatio = DesignWidth / DesignHeight; // 16:9

        private bool _isUpdatingSize = false;
        private double _lastWidth = DesignWidth;
        private double _lastHeight = DesignHeight;

        [DllImport("user32", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLong(IntPtr hwnd, int nIndex, uint dwNewLong);

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("user32")]
        private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32")]
        private static extern bool SetWindowPos(IntPtr hwnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        private const uint SWP_NOMOVE = 0x0002;

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public MainWindow()
        {
            InitializeComponent();
            // Use weak event pattern so the handler does not keep the window alive.
            WeakEventManager<Window, EventArgs>.AddHandler(
                this,
                nameof(SourceInitialized),
                OnSourceInitialized
            );

            // Start queuing animation when loaded
            Loaded += (s, e) =>
            {
                if (Resources["IsQueuingRotationStoryboard"] is Storyboard storyboard)
                {
                    storyboard.Begin(this, isControllable: true);
                }
            };

        }



        private void OnSourceInitialized(object? sender, EventArgs e)
        {
            if (sender is Window window)
            {
                IntPtr hwnd = new WindowInteropHelper(window).Handle;
                uint extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
            }
        }
    }
}
