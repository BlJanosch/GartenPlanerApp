using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ChemieControll.xaml
    /// </summary>
    public partial class ChemieControll : UserControl
    {
        [Category("Custom")]
        [Browsable(true)]

        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register("Percentage", typeof(int), typeof(ChemieControll), new PropertyMetadata(0, OnPercentageChanged));

        public int Percentage
        {
            get { return (int)GetValue(PercentageProperty); }
            set { SetValue(PercentageProperty, value); }
        }

        public ChemieControll()
        {
            InitializeComponent();
            UpdateProgress();
        }

        private static void OnPercentageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ChemieControll;
            control?.UpdateProgress();
        }

        private void UpdateProgress()
        {
            double angle = (Percentage / 100.0) * 360;

            // Define the size of the outer circle
            double radius = 100;
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

            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);

            progressPath.Data = pathGeometry;
            Loggerclass.log.Information($"Prozess in ChemieControll wurde erfolgreich geupdated.");
        }
    }
}
