using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace _2048remake
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<TextBlock> plates;
        private int score = 0;
        public int SpawnCounter;
        private int rearrangmentCounter = 0;
        private int winCounter = 0;
        private readonly Random randomPlateNumGenerator;
        public event PropertyChangedEventHandler PropertyChanged;
        public MainWindow()
        {
            InitializeComponent();
            randomPlateNumGenerator = new Random();
            KeyDown += Board_KeyDown;

            InitializeList();
        }
        private void InitializeList()
        {
            plates = new List<TextBlock>();
            for (int i = 0; i < 16; i++)
            {
                plates.Add(new TextBlock() { Text = "" }); 
                Board.Children.Add(plates[i]);
                plates.ElementAt(i).SetValue(Grid.RowProperty, i / 4);
                plates.ElementAt(i).SetValue(Grid.ColumnProperty, i % 4);
            }
            plates[1].Text = "2";
            plates[2].Text = "2";
            plates[6].Text = "4";
        }
        void MoveRight()
        {
            for (int i = 1; i < 14; i += 4)
            {
                for (int j = -1; j < 3; ++j)
                {
                    for (int k = 2; k > j; --k)
                    {
                        TryMove(i + k, i + k - 1);
                    }
                }
            }
        }
        void MoveLeft()
        {
            for (int i = 1; i < 14; i += 4)
            {
                for (int j = 3; j > 0; --j)
                {
                    for (int k = 0; k < j; ++k)
                    {
                        TryMove(i + k - 1, i + k);
                    }
                }
            }
        }
        void MoveDown()
        {
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 12; j = j + 4)
                {
                    for (int k = 12; k > j; k = k - 4)
                    {
                        TryMove(i + k, i + k - 4);
                    }
                }
            }
        }
        void MoveUp()
        {
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 12; j > 0; j = j - 4)
                {
                    for (int k = 0; k < j; k = k + 4)
                    {
                        TryMove(i + k, i + k + 4);
                    }
                }
            }
        }
        void TryMove(int k, int j)
        {
            if (plates[j].Text != "" && plates[k].Text == "")
            {
                MoveWithoutAddition(k, j);
            }
            else if (plates[k].Text != "" && plates[j].Text != "" &&
                       plates[k].Text == plates[j].Text)
            {
                MoveWithAddition(k, j);
            }
        }
        private void MoveWithAddition(int k, int j)
        {
            int plateScore = int.Parse(plates[k].Text);
            int newScore = plateScore * 2;
            score += newScore;
            plates[k].Text = newScore.ToString();
            plates[j].Text = "";

            WinCheck();
            rearrangmentCounter++;
        }
        private void MoveWithoutAddition(int k, int j)
        {
            string temp = plates[j].Text;
            plates[k].Text = temp;
            plates[j].Text = "";
            WinCheck();
            rearrangmentCounter++;
        }
        private void WinCheck()
        {
            liveScore.Text = score.ToString();
            for (int i = 0; i < 16; i++)
            {
                if (plates[i].Text != "")
                {
                    if (plates[i].Text == "2048" && winCounter == 0)
                    {
                        MessageBox.Show("U won, continue playing, game endless");
                        winCounter++;
                    }
                }
            }
        }
        private void Board_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D || e.Key == Key.Right)
            {
                SpawnCounter = 0;
                rearrangmentCounter = 0;
                MoveRight();
                GameEnd();
                SpawnAllPlates();
            }
            if (e.Key == Key.S || e.Key == Key.Down) 
            { 
                SpawnCounter = 0;
                rearrangmentCounter = 0;
                MoveDown(); 
                GameEnd();
                SpawnAllPlates();
            }
            if(e.Key == Key.A || e.Key == Key.Left)
            {
                SpawnCounter = 0;
                rearrangmentCounter = 0;
                MoveLeft(); 
                GameEnd();
                SpawnAllPlates();
            }
            if (e.Key == Key.W || e.Key == Key.Up)
            { 
                SpawnCounter = 0;
                rearrangmentCounter = 0;
                MoveUp();
                GameEnd();
                SpawnAllPlates();
            }
        }
        private void SpawnAllPlates()
        {
            if (rearrangmentCounter > 0)
            {
                int k = randomPlateNumGenerator.Next(0,16);
                bool spawned = false;
                while (SpawnCounter == 0)
                {
                    if (plates[k].Text == "")
                    {
                        SpawnPlate(k);
                        spawned = true;
                    }
                    k = randomPlateNumGenerator.Next(0, 16);
                }
            }
        }
        private void SpawnPlate(int k, int value = 0)
        {
            int text;
            int temp = randomPlateNumGenerator.Next(0, 16);
            if (value == 0)
            {
                text = 2;
                if (temp < 8) 
                { 
                    text = 2; 
                } 
                else 
                { 
                    text = 4; 
                }
            }
             else 
            { 
                text = value; 
            }
            plates[k].Text = text.ToString();
            SpawnCounter++;
        }
        private void GameEnd()
        {
            if (rearrangmentCounter == 0 && IsEnd() == 1)
            {
                MessageBox.Show("U lost! restart da game");
            }
        }
        private int IsEnd()
        {
            int k = 0;
            for (int i = 0; i < 16; i++)
            {
                if (plates[i].Text != "")
                {
                    k++;
                }
            }
            if (k == 16) { return 1; }
            else
            {
                return 0;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < 16; i++)
            {
                plates[i].Text = "";
                SpawnAllPlates();
            }
        }
    }
}
