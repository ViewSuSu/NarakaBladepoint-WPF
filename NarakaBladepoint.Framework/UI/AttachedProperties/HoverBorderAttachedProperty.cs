using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace NarakaBladepoint.Framework.UI.AttachedProperties
{
    /// <summary>
    /// 为任意 FrameworkElement 添加鼠标悬停时的白色突出边框（Adorner 实现）
    /// 参考 NarakaBladepoint.Controls.HighlightBorder 的实现。
    /// </summary>
    public static class HoverBorderAttachedProperty
    {
        public static readonly DependencyProperty EnableHoverBorderProperty =
            DependencyProperty.RegisterAttached(
                "EnableHoverBorder",
                typeof(bool),
                typeof(HoverBorderAttachedProperty),
                new PropertyMetadata(false, OnEnableHoverBorderChanged)
            );

        // 可选的画笔和线宽，默认为白色和 1
        public static readonly DependencyProperty HighlightBrushProperty =
            DependencyProperty.RegisterAttached(
                "HighlightBrush",
                typeof(Brush),
                typeof(HoverBorderAttachedProperty),
                new PropertyMetadata(Brushes.White)
            );

        public static readonly DependencyProperty HighlightThicknessProperty =
            DependencyProperty.RegisterAttached(
                "HighlightThickness",
                typeof(Thickness),
                typeof(HoverBorderAttachedProperty),
                new PropertyMetadata(new Thickness(1))
            );

        // 用于在元素上缓存已创建的 Adorner
        private static readonly DependencyProperty HoverAdornerProperty =
            DependencyProperty.RegisterAttached(
                "HoverAdorner",
                typeof(Adorner),
                typeof(HoverBorderAttachedProperty),
                new PropertyMetadata(null)
            );

        public static bool GetEnableHoverBorder(DependencyObject obj) =>
            (bool)obj.GetValue(EnableHoverBorderProperty);

        public static void SetEnableHoverBorder(DependencyObject obj, bool value) =>
            obj.SetValue(EnableHoverBorderProperty, value);

        public static Brush GetHighlightBrush(DependencyObject obj) =>
            (Brush)obj.GetValue(HighlightBrushProperty);

        public static void SetHighlightBrush(DependencyObject obj, Brush value) =>
            obj.SetValue(HighlightBrushProperty, value);

        public static Thickness GetHighlightThickness(DependencyObject obj) =>
            (Thickness)obj.GetValue(HighlightThicknessProperty);

        public static void SetHighlightThickness(DependencyObject obj, Thickness value) =>
            obj.SetValue(HighlightThicknessProperty, value);

        private static Adorner GetHoverAdorner(DependencyObject obj) =>
            (Adorner)obj.GetValue(HoverAdornerProperty);

        private static void SetHoverAdorner(DependencyObject obj, Adorner adorner) =>
            obj.SetValue(HoverAdornerProperty, adorner);

        private static void OnEnableHoverBorderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not UIElement element)
                return;

            // 先移除旧的处理器
            WeakEventManager<UIElement, MouseEventArgs>.RemoveHandler(
                element,
                nameof(UIElement.MouseEnter),
                OnMouseEnter
            );
            WeakEventManager<UIElement, MouseEventArgs>.RemoveHandler(
                element,
                nameof(UIElement.MouseLeave),
                OnMouseLeave
            );

            if ((bool)e.NewValue)
            {
                WeakEventManager<UIElement, MouseEventArgs>.AddHandler(
                    element,
                    nameof(UIElement.MouseEnter),
                    OnMouseEnter
                );
                WeakEventManager<UIElement, MouseEventArgs>.AddHandler(
                    element,
                    nameof(UIElement.MouseLeave),
                    OnMouseLeave
                );
            }
            else
            {
                // 移除并清理可能已存在的 adorner
                RemoveAdorner(element);
            }
        }

        private static void OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is not UIElement element)
                return;

            AddAdorner(element);
        }

        private static void OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is not UIElement element)
                return;

            RemoveAdorner(element);
        }

        private static void AddAdorner(UIElement element)
        {
            // 已存在则不重复添加
            if (GetHoverAdorner(element) != null)
                return;

            var brush = GetHighlightBrush(element) ?? Brushes.White;
            var thickness = GetHighlightThickness(element);

            var layer = AdornerLayer.GetAdornerLayer(element);
            if (layer == null)
            {
                // 如果尚未有 AdornerLayer，则在 Loaded 后重试一次
                if (element is FrameworkElement fe)
                {
                    void OnLoaded(object s, RoutedEventArgs args)
                    {
                        fe.Loaded -= OnLoaded;
                        AddAdorner(element);
                    }

                    fe.Loaded += OnLoaded;
                }
                return;
            }

            var adorner = new HoverHighlightAdorner(element, brush, thickness);
            layer.Add(adorner);
            SetHoverAdorner(element, adorner);
        }

        private static void RemoveAdorner(UIElement element)
        {
            var adorner = GetHoverAdorner(element);
            if (adorner == null)
                return;

            var layer = AdornerLayer.GetAdornerLayer(element);
            if (layer != null)
            {
                layer.Remove(adorner);
            }

            SetHoverAdorner(element, null);
        }

        private class HoverHighlightAdorner : Adorner
        {
            private readonly Brush _brush;
            private readonly double _thickness;

            public HoverHighlightAdorner(UIElement adornedElement, Brush brush, Thickness thickness)
                : base(adornedElement)
            {
                _brush = brush;
                _thickness = Math.Max(1.0, thickness.Left);
                IsHitTestVisible = false;
            }

            protected override void OnRender(DrawingContext drawingContext)
            {
                // 获取元素原始矩形（基于 RenderSize）
                Rect adornedElementRect = new Rect(this.AdornedElement.RenderSize);

                // 考虑元素可能存在的 LayoutTransform / RenderTransform / 任何 Visual 变换，
                // 将元素矩形转换到当前 Adorner 的坐标系中再绘制。
                Rect targetRect = adornedElementRect;
                try
                {
                    var gt = AdornedElement.TransformToVisual(this) as GeneralTransform;
                    if (gt != null)
                    {
                        targetRect = gt.TransformBounds(adornedElementRect);
                    }
                }
                catch
                {
                    // 发生任何异常时回退到未变换的矩形
                    targetRect = adornedElementRect;
                }

                var pen = new Pen(_brush, _thickness);
                pen.Freeze();

                double half = _thickness / 2.0;
                // 内缩半个线宽以使描边位于元素内部
                var innerRect = new Rect(
                    targetRect.Left + half,
                    targetRect.Top + half,
                    Math.Max(0.0, targetRect.Width - _thickness),
                    Math.Max(0.0, targetRect.Height - _thickness)
                );

                drawingContext.DrawRectangle(null, pen, innerRect);
            }
        }
    }
}
