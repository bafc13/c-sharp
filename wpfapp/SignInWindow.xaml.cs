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

namespace WpfApp2
{
    public partial class SignInWindow : Window
    {
        private AppContext db;
        public SignInWindow()
        {
            InitializeComponent();
            db = new AppContext();
        }
        private void Button_SignUp_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();

        }
        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            TBlogin.Rules().MinCharacters(5).Validate();
            PBpassword.Rules().MinCharacters(5).Validate();
            if (TBlogin.Background.Equals(PBpassword.Background) 
                && TBlogin.Background.Equals(Background = Brushes.Green))
            {
                if (db.Logging(TBlogin.Text, PBpassword.Password))
                {
                    MessageBox.Show("Logging in ...");
                    ContentWindow contentWindow = new ContentWindow();
                    contentWindow.Show();
                } 
                else
                {
                    MessageBox.Show("erorr");
                }
            }
        }
    }
}
