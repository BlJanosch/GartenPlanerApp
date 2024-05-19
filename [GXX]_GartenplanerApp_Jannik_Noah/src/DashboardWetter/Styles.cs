using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace DashboardWetter
{
    public class Styles
    {
        public static Style GetUserLoginButtonStyle()
        {
            Style buttonStyle = new Style(typeof(Button));

            buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(Color.FromRgb(38, 80, 38))));
            buttonStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.White));
            buttonStyle.Setters.Add(new Setter(Button.FontSizeProperty, 15.0));
            buttonStyle.Setters.Add(new Setter(Button.FontFamilyProperty, new FontFamily("Aharoni")));
            buttonStyle.Setters.Add(new Setter(Button.WidthProperty, 200.0));
            buttonStyle.Setters.Add(new Setter(Button.HeightProperty, 30.0));
            buttonStyle.Setters.Add(new Setter(Button.VerticalAlignmentProperty, VerticalAlignment.Center));
            buttonStyle.Setters.Add(new Setter(Button.MarginProperty, new Thickness(0, 20, 0, 0)));
            buttonStyle.Setters.Add(new Setter(Button.FontWeightProperty, FontWeights.Bold));

            return buttonStyle;
        }

        public static Style GetFontStyle(double FontSize)
        {
            Style labelStyle = new Style();
            labelStyle.Setters.Add(new Setter(Label.FontSizeProperty, FontSize));
            labelStyle.Setters.Add(new Setter(Label.FontFamilyProperty, new FontFamily("Aharoni")));
            labelStyle.Setters.Add(new Setter(Label.FontWeightProperty, FontWeights.Bold));
            labelStyle.Setters.Add(new Setter(Label.ForegroundProperty, Brushes.White));

            return labelStyle;
        }

        public static Style GetTextBoxStyle()
        {
            Style textboxStyle = new Style(typeof(TextBox));
            textboxStyle.Setters.Add(new Setter(TextBox.WidthProperty, 200.0));
            textboxStyle.Setters.Add(new Setter(TextBox.HeightProperty, 30.0));
            textboxStyle.Setters.Add(new Setter(TextBox.BackgroundProperty, new SolidColorBrush(Color.FromRgb(38, 38, 38))));
            textboxStyle.Setters.Add(new Setter(TextBox.FontSizeProperty, 15.0));
            textboxStyle.Setters.Add(new Setter(TextBox.TextAlignmentProperty, TextAlignment.Center));
            textboxStyle.Setters.Add(new Setter(TextBox.VerticalContentAlignmentProperty, VerticalAlignment.Center));
            textboxStyle.Setters.Add(new Setter(Label.FontSizeProperty, 15.0));
            textboxStyle.Setters.Add(new Setter(Label.FontFamilyProperty, new FontFamily("Aharoni")));
            textboxStyle.Setters.Add(new Setter(Label.FontWeightProperty, FontWeights.Bold));
            textboxStyle.Setters.Add(new Setter(Label.ForegroundProperty, Brushes.White));

            return textboxStyle;
        }
    }
}
