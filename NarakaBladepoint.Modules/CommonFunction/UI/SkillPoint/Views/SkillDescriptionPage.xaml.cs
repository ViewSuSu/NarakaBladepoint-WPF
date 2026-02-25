using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Views
{
    /// <summary>
    /// SkillDescriptionPage.xaml 的交互逻辑
    /// </summary>
    public partial class SkillDescriptionPage : UserControl
    {
        private bool _isPlaying = false;

        public SkillDescriptionPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 视频打开事件，用于显示第一帧
        /// </summary>
        private void VideoPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            var mediaElement = sender as MediaElement;
            if (mediaElement != null && !_isPlaying)
            {
                try
                {
                    // 使用 ScrubbingEnabled 在暂停时也能显示帧
                    // 设置位置到第一帧但不播放
                    mediaElement.Position = TimeSpan.Zero;
                    mediaElement.Pause();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"显示第一帧失败: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 视频播放完毕时的事件处理
        /// </summary>
        private void VideoPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            var mediaElement = sender as MediaElement;
            if (mediaElement != null)
            {
                // 停止播放，重置位置
                mediaElement.Stop();
                mediaElement.Position = TimeSpan.Zero;
                _isPlaying = false;
                UpdatePlayPauseButton();
            }
        }

        /// <summary>
        /// 播放/暂停按钮点击事件
        /// </summary>
        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (VideoPlayer == null || VideoPlayer.Source == null)
            {
                System.Diagnostics.Debug.WriteLine("VideoPlayer 或 Source 为空");
                return;
            }

            try
            {
                System.Diagnostics.Debug.WriteLine($"点击播放按钮，当前播放状态: {_isPlaying}");

                if (_isPlaying)
                {
                    // 暂停播放
                    System.Diagnostics.Debug.WriteLine("执行暂停");
                    VideoPlayer.Pause();
                    _isPlaying = false;
                }
                else
                {
                    // 播放视频
                    System.Diagnostics.Debug.WriteLine($"执行播放，当前位置: {VideoPlayer.Position}");
                    VideoPlayer.Play();
                    _isPlaying = true;
                    System.Diagnostics.Debug.WriteLine("播放命令已发送");
                }

                UpdatePlayPauseButton();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"播放控制失败: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"堆栈跟踪: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// 更新播放/暂停按钮的显示状态
        /// </summary>
        private void UpdatePlayPauseButton()
        {
            if (PlayPauseButton != null && PlayPauseButton.Template != null)
            {
                var playSymbol = PlayPauseButton.Template.FindName("PlaySymbol", PlayPauseButton) as Path;
                if (playSymbol != null)
                {
                    if (_isPlaying)
                    {
                        // 显示暂停符号（两个竖条，居中）
                        playSymbol.Data = Geometry.Parse("M 8,5 L 8,30 L 12,30 L 12,5 Z M 23,5 L 23,30 L 27,30 L 27,5 Z");
                    }
                    else
                    {
                        // 显示播放三角形（躺着的三角形，居中）
                        playSymbol.Data = Geometry.Parse("M 5,2 L 30,17.5 L 5,33 Z");
                    }
                }
            }
        }

        /// <summary>
        /// 当 VideoSource 属性变化时，停止当前视频并重置
        /// </summary>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == VideoSourceProperty && VideoPlayer != null)
            {
                VideoPlayer.Stop();
                _isPlaying = false;
                UpdatePlayPauseButton();
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(SkillDescriptionPage),
                new PropertyMetadata(string.Empty));

        /// <summary>
        /// 标签集合
        /// </summary>
        public IEnumerable<string> Tags
        {
            get { return (IEnumerable<string>)GetValue(TagsProperty); }
            set { SetValue(TagsProperty, value); }
        }

        public static readonly DependencyProperty TagsProperty =
            DependencyProperty.Register(
                nameof(Tags),
                typeof(IEnumerable<string>),
                typeof(SkillDescriptionPage),
                new PropertyMetadata(null));

        /// <summary>
        /// 标签宽度
        /// </summary>
        public double TagWidth
        {
            get { return (double)GetValue(TagWidthProperty); }
            set { SetValue(TagWidthProperty, value); }
        }

        public static readonly DependencyProperty TagWidthProperty =
            DependencyProperty.Register(
                nameof(TagWidth),
                typeof(double),
                typeof(SkillDescriptionPage),
                new PropertyMetadata(double.NaN));

        /// <summary>
        /// 标签高度
        /// </summary>
        public double TagHeight
        {
            get { return (double)GetValue(TagHeightProperty); }
            set { SetValue(TagHeightProperty, value); }
        }

        public static readonly DependencyProperty TagHeightProperty =
            DependencyProperty.Register(
                nameof(TagHeight),
                typeof(double),
                typeof(SkillDescriptionPage),
                new PropertyMetadata(double.NaN));

        /// <summary>
        /// 技能描述内容（可以是字符串或复杂控件）
        /// </summary>
        public object Description
        {
            get { return GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(
                nameof(Description),
                typeof(object),
                typeof(SkillDescriptionPage),
                new PropertyMetadata(null));

        /// <summary>
        /// 高亮字符串
        /// </summary>
        public string HighlightText
        {
            get { return (string)GetValue(HighlightTextProperty); }
            set { SetValue(HighlightTextProperty, value); }
        }

        public static readonly DependencyProperty HighlightTextProperty =
            DependencyProperty.Register(
                nameof(HighlightText),
                typeof(string),
                typeof(SkillDescriptionPage),
                new PropertyMetadata(string.Empty));

        /// <summary>
        /// 描述高度
        /// </summary>
        public GridLength DescriptionHeight
        {
            get { return (GridLength)GetValue(DescriptionHeightProperty); }
            set { SetValue(DescriptionHeightProperty, value); }
        }

        public static readonly DependencyProperty DescriptionHeightProperty =
            DependencyProperty.Register(
                nameof(DescriptionHeight),
                typeof(GridLength),
                typeof(SkillDescriptionPage),
                new PropertyMetadata(new GridLength(1, GridUnitType.Auto)));

        /// <summary>
        /// 视频源
        /// </summary>
        public Uri VideoSource
        {
            get { return (Uri)GetValue(VideoSourceProperty); }
            set { SetValue(VideoSourceProperty, value); }
        }

        public static readonly DependencyProperty VideoSourceProperty =
            DependencyProperty.Register(
                nameof(VideoSource),
                typeof(Uri),
                typeof(SkillDescriptionPage),
                new PropertyMetadata(null));
    }
}
