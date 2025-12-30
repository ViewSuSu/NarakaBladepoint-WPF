using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Nakara.Controls.Converters
{
    internal class LTopLeftConverter : IValueConverter
    {
        public object Convert(object value, Type t, object p, CultureInfo c)
        {
            double l = System.Convert.ToDouble(value);

            var figure = new PathFigure
            {
                StartPoint = new Point(0, l),
                Segments =
                {
                    new LineSegment(new Point(0, 0), true),
                    new LineSegment(new Point(l, 0), true),
                },
            };

            return new PathGeometry { Figures = { figure } };
        }

        public object ConvertBack(object v, Type t, object p, CultureInfo c) => Binding.DoNothing;
    }

    internal class LTopRightConverter : IValueConverter
    {
        public object Convert(object value, Type t, object p, CultureInfo c)
        {
            double l = System.Convert.ToDouble(value);

            var figure = new PathFigure
            {
                StartPoint = new Point(0, 0),
                Segments =
                {
                    new LineSegment(new Point(l, 0), true),
                    new LineSegment(new Point(l, l), true),
                },
            };

            return new PathGeometry { Figures = { figure } };
        }

        public object ConvertBack(object v, Type t, object p, CultureInfo c) => Binding.DoNothing;
    }

    internal class LBottomLeftConverter : IValueConverter
    {
        public object Convert(object value, Type t, object p, CultureInfo c)
        {
            double l = System.Convert.ToDouble(value);

            var figure = new PathFigure
            {
                StartPoint = new Point(0, 0),
                Segments =
                {
                    new LineSegment(new Point(0, l), true),
                    new LineSegment(new Point(l, l), true),
                },
            };

            return new PathGeometry { Figures = { figure } };
        }

        public object ConvertBack(object v, Type t, object p, CultureInfo c) => Binding.DoNothing;
    }

    internal class LBottomRightConverter : IValueConverter
    {
        public object Convert(object value, Type t, object p, CultureInfo c)
        {
            double l = System.Convert.ToDouble(value);

            var figure = new PathFigure
            {
                StartPoint = new Point(l, 0),
                Segments =
                {
                    new LineSegment(new Point(l, l), true),
                    new LineSegment(new Point(0, l), true),
                },
            };

            return new PathGeometry { Figures = { figure } };
        }

        public object ConvertBack(object v, Type t, object p, CultureInfo c) => Binding.DoNothing;
    }
}
