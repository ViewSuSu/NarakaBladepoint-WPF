using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

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

        [DllImport("user32", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLong(IntPtr hwnd, int nIndex, uint dwNewLong);

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong(IntPtr hwnd, int nIndex);

        public MainWindow()
        {
            InitializeComponent();
            this.SourceInitialized += delegate
            {
                IntPtr hwnd = new WindowInteropHelper(this).Handle;
                uint extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
            };
        }
    }
}
