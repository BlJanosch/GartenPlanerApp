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
    /// Interaction logic for BeetControl.xaml
    /// </summary>
    public partial class BeetControl : UserControl
    {
        private int thickness = 5;

        [Category("Custom")]
        [Browsable(true)]
        public int Thickness
        {
            get { return thickness; }
            set {
                if (value >= 0)
                {
                    thickness = value;
                }
                
                // TODO: set thickness
                BorderLeft.Width = thickness;
            }
        }


        private Brush colorLeft = Brushes.Black;
        [Category("Custom")]
        [Browsable(true)]
        public Brush ColorLeft { 
            get 
            {
                return colorLeft; 
            } 
            set 
            {
                colorLeft = value;
                BorderLeft.Background = colorLeft;
            
            } 
        }

        private Brush colorRight = Brushes.Black;
        [Category("Custom")]
        [Browsable(true)]
        public Brush ColorRight
        {
            get
            {
                return colorRight;
            }
            set
            {
                colorRight = value;
                BorderRight.Background = colorRight;

            }
        }

        private Brush colorTop = Brushes.Black;
        [Category("Custom")]
        [Browsable(true)]
        public Brush ColorTop
        {
            get
            {
                return colorTop;
            }
            set
            {
                colorTop = value;
                BorderTop.Background = colorTop;

            }
        }

        private Brush colorBottom = Brushes.Black;
        [Category("Custom")]
        [Browsable(true)]
        public Brush ColorBottom
        {
            get
            {
                return colorBottom;
            }
            set
            {
                colorBottom = value;
                BorderBottom.Background = colorBottom;

            }
        }

        private Beet beet;
        private int col;
        private int row;

        public BeetControl()
        {
            InitializeComponent();
        }

        public BeetControl(Beet beet, int col, int row, string headingText, string imagePath, Brush colorLeft, Brush colorRight, Brush colorTop, Brush colorBottom)
        {
            InitializeComponent();

            this.row = row;
            this.col = col;
            this.beet = beet;
            ColorBottom = colorBottom;
            ColorLeft = colorLeft;
            ColorRight = colorRight;
            ColorTop = colorTop;
            LabelHeading.Content = headingText;
            SetImage(imagePath);
        }

        public void SetImage(string path)
        {
            Image image = new Image()
            {

                Height = 48,
                Width = 48,

            };

            image.Source = new BitmapImage(new Uri(path, UriKind.Relative));

            /* <Button ></Button>

            Button ButtonDelete = new Button()
            {
                Width = 16,
                Height = 16,
                
            };
            ButtonDelete.Click += ButtonDelete_Click;
            Canvas.SetBottom(ButtonDelete, 5);
            Canvas.SetRight(ButtonDelete, 5);
            */
            


            CanvasMain.Children.Add(image);
            
            Canvas.SetTop(image, 0);
            Canvas.SetLeft(image, 10);

        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {

            this.beet.DeleteElement(col, row);

        }
    }
}
