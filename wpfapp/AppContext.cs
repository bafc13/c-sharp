using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows.Forms;

namespace WpfApp2
{
    internal class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppContext() : base("DefaultConnection") { }
        public bool Logging(string login, string password)
        {
            return Users.Any(user => user.Login == login && user.Password == password);
        }

        public bool IsRepeat(string instance, string value)
        {
            bool confirmer = true;
            if (instance == "login")
            {
                foreach (User user in Users)
                {
                    if (user.Login == value) { confirmer = true; break; }
                    else { confirmer = false; }
                }
            }
            if (instance == "email")
            {
                foreach (User user in Users)
                {
                    if (user.Email == value) { confirmer = true; break; }
                    else { confirmer = false; }
                }
            }
            return confirmer;
        }
    }
}
