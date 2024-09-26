using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp2
{
    static class ControlValidationExtension
    {
        public static Validator Rules(this TextBox control) => new Validator(control, control.Text);
        public static Validator Rules(this PasswordBox control) => new Validator(control, control.Password);
    }

    internal class Validator
    {
        private readonly Control _control;
        private readonly string _content;
        public  AppContext db = new AppContext();
        private bool _valid = true;

        public Validator(Control control, string content)
        {
            _control = control;
            _content = content;
        }
        public Validator MinCharacters(int count)
        {
            if (_content.Length < count)
            {
                _valid = false;
                _control.ToolTip = "Minimum 5 characters in line";
            }
            return this;
        }
        public Validator IsEmail()
        {
            if (!_content.Contains("@") || !_content.Contains("."))
            {
                _valid = false;
                _control.ToolTip = "Incorrect email";
            }
            return this;
        }
        public Validator IsEmailRepeat()
        {
            if (db.IsRepeat("email", _content))
            {
                _control.ToolTip = "This email has been registered";
                _valid = false;
            }
            return this;
        }
        public Validator IsLoginRepeat()
        {
            if (db.IsRepeat("login", _content))
            {
                _valid = false;
                _control.ToolTip = "This login has been registered";
            }
            return this;
        }
        public void Validate()
        {
            if (!_valid)
            {
                _control.Background = Brushes.Red;
            } else
            {
                _control.Background = Brushes.Green;
                _control.ToolTip = "";
            }
        }
    }
}
