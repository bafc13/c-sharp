using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _2048remake
{
    internal class _2048ViewModel : INotifyPropertyChanged
    {
        private MainWindow Board
        {
            get { return Board as MainWindow; }
        set
            {
                Board = value;
                OnPropertyChanged(nameof(Board));
            }
        }

        public _2048ViewModel()
        {
            Board = new MainWindow();
        } 
        public void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
