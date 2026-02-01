using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace NarakaBladepoint.Controls
{
    public class HighlightBorder : Border
    {
        private AdornerLayer _adornerLayer;
        private HighlightAdorner _highlightAdorner;
        private MaskAdorner _maskAdorner;

        public HighlightBorder()
        {
            // Use weak event subscriptions to avoid memory leaks from event handlers
            System.Windows.WeakEventManager<
                UIElement,
                System.Windows.Input.MouseEventArgs
            >.AddHandler(
                this,
                nameof(MouseEnter),
                new EventHandler<System.Windows.Input.MouseEventArgs>(OnMouseEnter)
            );

            System.Windows.WeakEventManager<
                UIElement,
                System.Windows.Input.MouseEventArgs
            >.AddHandler(
                this,
                nameof(MouseLeave),
                new EventHandler<System.Windows.Input.MouseEventArgs>(OnMouseLeave)
            );

            System.Windows.WeakEventManager<FrameworkElement, RoutedEventArgs>.AddHandler(
                this,
                nameof(Unloaded),
                new EventHandler<RoutedEventArgs>(OnUnloaded)
            );
        }

        #region 依赖属性

        public static readonly DependencyProperty HighlightThicknessProperty =
            DependencyProperty.Register(
                "HighlightThickness",
                typeof(Thickness),
                typeof(HighlightBorder),
                new PropertyMetadata(new Thickness(1))
            );

        public Thickness HighlightThickness
        {
            get { return (Thickness)GetValue(HighlightThicknessProperty); }
            set { SetValue(HighlightThicknessProperty, value); }
        }

        public static readonly DependencyProperty HighlightBrushProperty =
            DependencyProperty.Register(
                "HighlightBrush",
                typeof(Brush),
                typeof(HighlightBorder),
                new PropertyMetadata(Brushes.White)
            );

        public Brush HighlightBrush
        {
            get { return (Brush)GetValue(HighlightBrushProperty); }
            set { SetValue(HighlightBrushProperty, value); }
        }

        public static readonly DependencyProperty EnableMaskOnHoverProperty =
            DependencyProperty.Register(
                "EnableMaskOnHover",
                typeof(bool),
                typeof(HighlightBorder),
                new PropertyMetadata(false)
            );

        public bool EnableMaskOnHover
        {
            get { return (bool)GetValue(EnableMaskOnHoverProperty); }
            set { SetValue(EnableMaskOnHoverProperty, value); }
        }

        #endregion 依赖属性

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            ShowHighlight();
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            HideHighlight();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            HideHighlight();
        }

        private void ShowHighlight()
        {
            if (_adornerLayer == null)
                _adornerLayer = AdornerLayer.GetAdornerLayer(this);

            if (_adornerLayer != null && _highlightAdorner == null)
            {
                _highlightAdorner = new HighlightAdorner(this, HighlightBrush, HighlightThickness);
                _adornerLayer.Add(_highlightAdorner);
            }

            // 如果启用了蒙版，则显示蒙版
            if (EnableMaskOnHover && _adornerLayer != null && _maskAdorner == null)
            {
                _maskAdorner = new MaskAdorner(this);
                _adornerLayer.Add(_maskAdorner);
            }
        }

        private void HideHighlight()
        {
            if (_adornerLayer != null && _highlightAdorner != null)
            {
                _adornerLayer.Remove(_highlightAdorner);
                _highlightAdorner = null;
            }

            if (_adornerLayer != null && _maskAdorner != null)
            {
                _adornerLayer.Remove(_maskAdorner);
                _maskAdorner = null;
            }
        }

        // 装饰器类
        private class HighlightAdorner : Adorner
        {
            private readonly Brush _brush;
            private readonly Thickness _thickness;

            public HighlightAdorner(UIElement adornedElement, Brush brush, Thickness thickness)
                : base(adornedElement)
            {
                _brush = brush;
                _thickness = thickness;
                IsHitTestVisible = false;
            }

            protected override void OnRender(DrawingContext drawingContext)
            {
                Rect adornedElementRect = new Rect(this.AdornedElement.RenderSize);

                // Use the provided thickness (use Left as scalar) and draw the border inside the element bounds
                double thickness = Math.Max(1.0, _thickness.Left);
                var pen = new Pen(_brush, thickness);
                pen.Freeze();

                // Inset the rectangle by half the pen thickness so the stroke stays inside the element
                double half = thickness / 2.0;
                var innerRect = new Rect(
                    adornedElementRect.Left + half,
                    adornedElementRect.Top + half,
                    Math.Max(0.0, adornedElementRect.Width - thickness),
                    Math.Max(0.0, adornedElementRect.Height - thickness)
                );

                drawingContext.DrawRectangle(null, pen, innerRect);
            }
        }

        // 蒙版装饰器类
        private class MaskAdorner : Adorner
        {
            private readonly Brush _maskBrush;

            public MaskAdorner(UIElement adornedElement)
                : base(adornedElement)
            {
                // 创建带透明度的白色朦胧蒙版（更淡）
                _maskBrush = new SolidColorBrush(Color.FromArgb(60, 255, 255, 255)); // 白色透明（更淡）
                _maskBrush.Freeze();

                IsHitTestVisible = false;
            }

            protected override void OnRender(DrawingContext drawingContext)
            {
                Rect adornedElementRect = new Rect(this.AdornedElement.RenderSize);

                // 绘制蒙版矩形（覆盖整个元素）
                drawingContext.DrawRectangle(_maskBrush, null, adornedElementRect);
            }
        }
    }
}
