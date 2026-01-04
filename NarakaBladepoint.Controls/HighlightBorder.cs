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

        public HighlightBorder()
        {
            this.MouseEnter += OnMouseEnter;
            this.MouseLeave += OnMouseLeave;
            this.Unloaded += OnUnloaded;
        }

        #region 依赖属性

        public static readonly DependencyProperty HighlightThicknessProperty =
            DependencyProperty.Register(
                "HighlightThickness",
                typeof(Thickness),
                typeof(HighlightBorder),
                new PropertyMetadata(new Thickness(2))
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
        }

        private void HideHighlight()
        {
            if (_adornerLayer != null && _highlightAdorner != null)
            {
                _adornerLayer.Remove(_highlightAdorner);
                _highlightAdorner = null;
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

                Pen pen = new Pen(_brush, 1);
                pen.Freeze();

                // 绘制四边
                drawingContext.DrawRectangle(null, pen, adornedElementRect);

                // 如果边框粗细大于1，绘制额外的线
                if (_thickness.Left > 1)
                {
                    for (int i = 1; i < _thickness.Left; i++)
                    {
                        drawingContext.DrawRectangle(
                            null,
                            pen,
                            new Rect(
                                adornedElementRect.Left - i,
                                adornedElementRect.Top - i,
                                adornedElementRect.Width + i * 2,
                                adornedElementRect.Height + i * 2
                            )
                        );
                    }
                }
            }
        }
    }
}
