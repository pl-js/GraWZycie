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
using System.Windows.Shapes;

namespace WPF_Gra
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        MainWindow chefRef;

        public Window1()
        {
            InitializeComponent();
            this.sizeField.Focusable.Equals(true);
            this.sizeField.Focus();
        }

        public Window1(MainWindow chef)
        {
            InitializeComponent();
            this.sizeField.Focusable.Equals(true);
            this.sizeField.Focus();
            this.chefRef = chef;
        }

        //handling the "ok" button press
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int size = 0;
            
                try
                {
                    size = int.Parse(this.sizeField.Text);

                    if ((size > 3) && (size < 16))
                    {
                        this.chefRef.Show();
                        this.Hide();
                        MainWindow.worldSize = size;
                        this.chefRef.NewBoard();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Improper world size!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        this.sizeField.Focus();
                        this.sizeField.SelectionStart = 0;
                        this.sizeField.SelectionLength = sizeField.Text.Length;
                    }
                }

                catch
                {
                    MessageBox.Show("Given value must be a value! ;)", "Something went wrong", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    this.sizeField.SelectAll();

                }
        }

        //handling close button press
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            chefRef.Show();
            MainWindow.logBox.Text = "No world to start game with!";
        }

        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
