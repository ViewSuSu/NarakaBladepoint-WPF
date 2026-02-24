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
        public SkillDescriptionPage()
        {
            InitializeComponent();
        }

        private void VideoPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (VideoPlayer != null)
            {
                VideoPlayer.Position = TimeSpan.Zero;
                VideoPlayer.Play();
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
