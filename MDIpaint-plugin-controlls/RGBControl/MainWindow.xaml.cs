using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Accessibility;

namespace RGB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private byte _r;
        private byte _g;
        private byte _b;
        private bool _isSaved = false;

        public MainWindow() { }

        public MainWindow(byte r, byte g, byte b)
        {
            InitializeComponent();
            _r = r;
            _g = g;
            _b = b;
            redTextBox.Tag = (byte)_r;
            greenTextBox.Tag = (byte)_g;
            blueTextBox.Tag = (byte)_b;
            redTextBox.Text = _r.ToString();
            greenTextBox.Text = _g.ToString();
            blueTextBox.Text = _b.ToString();
            RectangleFill();   
        }

        public (int r, int g, int b, bool isSaved) ShowDialogRgb()
        {
            ShowDialog();
            return (_r, _g, _b, _isSaved);
        }

        void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (decRadioButton.IsChecked == true)
                if (!byte.TryParse(textBox.Text, out byte xResult))
                {
                    System.Media.SystemSounds.Exclamation.Play();
                    textBox.Text = "0";
                    textBox.Tag = (byte)0;
                }
                else
                {
                    textBox.Tag = xResult;
                }
            else
            {
                if (!byte.TryParse(textBox.Text, System.Globalization.NumberStyles.HexNumber, null, out byte xResult))
                {
                    System.Media.SystemSounds.Exclamation.Play();
                    textBox.Text = "0";
                    textBox.Tag = (byte)0;
                }
                else
                {
                    textBox.Tag = xResult;
                }
            }
            RectangleFill();
        }

        private void RectangleFill()
        {
            rectangle.Fill = new SolidColorBrush(Color.FromRgb((byte)redTextBox.Tag, (byte)greenTextBox.Tag, (byte)blueTextBox.Tag));
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            _r = (byte)redTextBox.Tag;
            _g = (byte)greenTextBox.Tag;
            _b = (byte)blueTextBox.Tag;
            _isSaved = true;
            Close();
        }

        private void decRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            redTextBox.Text = byte.Parse(redTextBox.Text, System.Globalization.NumberStyles.HexNumber).ToString();
            greenTextBox.Text = byte.Parse(greenTextBox.Text, System.Globalization.NumberStyles.HexNumber).ToString();
            blueTextBox.Text = byte.Parse(blueTextBox.Text, System.Globalization.NumberStyles.HexNumber).ToString();
        }

        private void hexRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            redTextBox.Text = ((byte)redTextBox.Tag).ToString("X");
            greenTextBox.Text = ((byte)greenTextBox.Tag).ToString("X");
            blueTextBox.Text = ((byte)blueTextBox.Tag).ToString("X");
        }

        private void RGBSetup_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (redTextBox.IsFocused)
                {
                    textBox_LostFocus(redTextBox, new RoutedEventArgs());
                }
                else if (greenTextBox.IsFocused)
                {
                    textBox_LostFocus(greenTextBox, new RoutedEventArgs());
                }
                else if (blueTextBox.IsFocused)
                {
                    textBox_LostFocus(blueTextBox, new RoutedEventArgs());
                }
            }
        }
    }
}
