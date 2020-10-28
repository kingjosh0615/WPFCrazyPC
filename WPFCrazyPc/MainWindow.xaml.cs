using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Effects;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Threading;

namespace WPFCrazyPc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);
        public static int timesClicked = 0;
        bool gameRunning = true;
        string currentTime = string.Empty;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(timer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            GameRunning();
        }

        private void ColorTheme_Click(object sender, RoutedEventArgs e)
        {

        }
        public void GameRunning()
        {


            if (timesClicked == 0)
            {
                xChaserButton.Visibility = Visibility.Visible;
                

            }
            Thread thr = new Thread(UpdateTime);
            thr.Start();

        }

        private void xChaserButtonClick(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();

            int buttonRowPosition = Convert.ToInt32(Math.Floor(Convert.ToDouble(rand.Next(0, 8))));
            int buttonColumnPosition = Convert.ToInt32(Math.Floor(Convert.ToDouble(rand.Next(0, 15))));
            int buttonRowSpan;
            int buttonColumnSpan;
            double buttonVerticalSubtraction = 0;
            double buttonHorizontalSubtraction = 0;


            timesClicked++;
            if (timesClicked > 1 && timesClicked < 5)
            {
                if (buttonColumnPosition < 15 && buttonColumnPosition > 12)
                {
                    buttonColumnSpan = Convert.ToInt32(Math.Floor(Convert.ToDouble(rand.Next(0, 4))));
                }
                else if (buttonColumnPosition < 15 && buttonColumnPosition > 13)
                {
                    buttonColumnSpan = Convert.ToInt32(Math.Floor(Convert.ToDouble(rand.Next(0, 3))));
                }
                else if (buttonColumnPosition <= 15 || buttonColumnPosition >= 14)
                {
                    buttonColumnSpan = 1;
                }

                if (buttonRowPosition < 7 && buttonRowPosition > 4)
                {
                    buttonRowSpan = Convert.ToInt32(Math.Floor(Convert.ToDouble(rand.Next(0, 4))));
                }
                else if (buttonRowPosition < 7 && buttonRowPosition > 5)
                {
                    buttonRowSpan = Convert.ToInt32(Math.Floor(Convert.ToDouble(rand.Next(0, 3))));
                }
                else if (buttonRowPosition <= 7 || buttonRowPosition >= 6)
                {
                    buttonRowSpan = 1;
                }
            }
            else if (timesClicked >= 5 && timesClicked < 10)
            {
                if (buttonColumnPosition < 15 && buttonColumnPosition > 13)
                {
                    buttonColumnSpan = Convert.ToInt32(Math.Floor(Convert.ToDouble(rand.Next(0, 3))));
                }
                else if (buttonColumnPosition <= 15 || buttonColumnPosition >= 14)
                {
                    buttonColumnSpan = 1;
                }

                if (buttonRowPosition < 7 && buttonRowPosition > 5)
                {
                    buttonRowSpan = Convert.ToInt32(Math.Floor(Convert.ToDouble(rand.Next(0, 3))));
                }
                else if (buttonRowPosition <= 7 || buttonRowPosition >= 6)
                {
                    buttonRowSpan = 1;
                }

            }
            else if (timesClicked >= 10 && timesClicked < 15)
            {
                buttonColumnSpan = 1;
                buttonRowSpan = 1;

                
            }
            else if (timesClicked >= 15 && timesClicked < 20)
            {

            }
            if (timesClicked == 1)
            {
                stopWatch.Start();
                dispatcherTimer.Start();
            }
            if (timesClicked == 20)
            {
                stopWatch.Stop();
                xChaserButton.Visibility = Visibility.Hidden;
                gameRunning = false;

            }

            buttonHorizontalSubtraction = Math.Floor(Convert.ToDouble(rand.Next(0, 48)));
            buttonVerticalSubtraction = Math.Floor(Convert.ToDouble(rand.Next(0, 24)));


            double buttonCurrentHeight = xChaserButton.Height;
            double buttonCurrentWidth = xChaserButton.Width;
            xChaserButton.Height = buttonCurrentHeight - buttonVerticalSubtraction;
            xChaserButton.Width = buttonCurrentWidth - buttonHorizontalSubtraction;



            xPlayersScore.Text = Convert.ToString(timesClicked);
            xChaserButton.SetValue(Grid.RowProperty, 4);
            xChaserButton.SetValue(Grid.ColumnProperty, 4);
        }
        public void UpdateTime()
        {
            this.Dispatcher.Invoke(() =>
            {

                var watch = new Stopwatch();
  
            });



        }
        void timer_Tick(object sender, EventArgs e)
        {

            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                xGamerTime.Text = ("Time: " + currentTime);
            }
        }
    }
}
