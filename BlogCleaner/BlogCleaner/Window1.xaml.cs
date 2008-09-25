using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlogCleaner
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            string oldText = Clipboard.GetText();
            textBox1.Text = oldText;

            StringBuilder builder = new StringBuilder(oldText);
            builder.Replace("<p>", string.Empty);
            builder.Replace("</p>", string.Empty);
            Clipboard.SetText(builder.ToString());
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }
    }
}
