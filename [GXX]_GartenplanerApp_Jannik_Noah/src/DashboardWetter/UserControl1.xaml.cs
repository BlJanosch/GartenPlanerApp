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

namespace DashboardWetter
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register("Percentage", typeof(double), typeof(UserControl1), new PropertyMetadata(0.0, OnPercentageChanged));

        public double Percentage
        {
            get { return (double)GetValue(PercentageProperty); }
            set { SetValue(PercentageProperty, value); }
        }

        public UserControl1()
        {
            InitializeComponent();
            UpdateProgress();
        }

        private static void OnPercentageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as UserControl1;
            control?.UpdateProgress();
        }

        private void UpdateProgress()
        {
            double angle = (Percentage / 100.0) * 360;

            // Define the size of the circle
            double radius = 90;
            Point center = new Point(Width / 2, Height / 2);

            // Calculate the endpoint of the arc
            double angleRad = (Math.PI / 180.0) * angle;
            Point endPoint = new Point(
                center.X + radius * Math.Cos(angleRad - Math.PI / 2),
                center.Y + radius * Math.Sin(angleRad - Math.PI / 2)
            );

            bool isLargeArc = angle > 180.0;

            // Create the path data for the arc
            PathFigure pathFigure = new PathFigure
            {
                StartPoint = new Point(center.X, center.Y - radius),
                IsClosed = false
            };

            ArcSegment arcSegment = new ArcSegment
            {
                Point = endPoint,
                Size = new Size(radius, radius),
                IsLargeArc = isLargeArc,
                SweepDirection = SweepDirection.Clockwise
            };

            pathFigure.Segments.Add(arcSegment);

            // Create a line segment to close the path back to the center
            LineSegment lineToCenter = new LineSegment
            {
                Point = center
            };

            pathFigure.Segments.Add(lineToCenter);

            // Create a line segment from the center back to the starting point
            LineSegment lineToStart = new LineSegment
            {
                Point = new Point(center.X, center.Y - radius)
            };

            pathFigure.Segments.Add(lineToStart);

            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);

            progressPath.Data = pathGeometry;
        }
    }
}
