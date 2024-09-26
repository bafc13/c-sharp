using System.Windows;
using System.Windows.Media;
using System;
using System.Windows.Controls;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        private SignInWindow _signInWindow;
        private AppContext db;
        public MainWindow()
        {
            InitializeComponent();
            db = new AppContext();
        } 

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            TBlogin.Rules().MinCharacters(5).IsLoginRepeat().Validate();
            PBpassword.Rules().MinCharacters(5).Validate();
            if(PBcorrect_password.Password == PBpassword.Password)
            {
                PBcorrect_password.Rules().MinCharacters(5).Validate();
            }
            TBemail.Rules().MinCharacters(5).IsEmail().IsEmailRepeat().Validate();
            if (TBlogin.Background.Equals(PBcorrect_password.Background) && TBlogin.Background.Equals(TBemail.Background)
                && TBlogin.Background.Equals(Background = Brushes.Green))
            {
                MessageBox.Show("Registration succesfull");
                User user = new User(TBlogin.Text, TBemail.Text, PBpassword.Password);

                db.Users.Add(user);
                db.SaveChanges();
                ContentWindow contentWindow = new ContentWindow();
                contentWindow.Show();
            }
        }

        private void Button_SignIn_Click(object sender, RoutedEventArgs e)
        {
            _signInWindow = new SignInWindow();
            _signInWindow.Show();
            this.Close();
        }
    }
}
