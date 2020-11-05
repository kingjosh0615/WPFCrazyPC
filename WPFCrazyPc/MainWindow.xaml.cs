using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
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
using System.Text.RegularExpressions;

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
        string readLine1;
        string readLine2;
        double oldTime = 0;
        double newPlayerTime;
        string newPlayerName;
        string oldPlayerName = "";
        char[] charsToGetRidOf = { ':' };

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
            if (timesClicked != 30 && timesClicked > 0)
            {

            }
            else if (timesClicked == 0)
            {
                GameRunning();
            }
            else if (timesClicked == 30 && oldTime > newPlayerTime)
            {
                newPlayerName = xEnterPlayerName.Text;

                using (StreamWriter sw = new StreamWriter("highscore1.txt", append: false))
                {
                    sw.Write(currentTime);
                }

                using (StreamWriter sw = new StreamWriter("highscore2.txt", append: false))
                {
                    sw.Write(newPlayerName);
                }

                xEnterPlayerName.Visibility = Visibility.Hidden;
                xCurrentHighScore.Visibility = Visibility.Hidden;
                xStartButton.Visibility = Visibility.Hidden;
                xGamerTime.Visibility = Visibility.Hidden;
                xSelfHighScoreMessage.Text = "Congratulations new champion! You may press the exit button to leave.";
            }
            
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
                xChaserButton.FontSize = 80;
            }
            else if (timesClicked >= 10 && timesClicked < 15)
            {
                buttonColumnSpan = 1;
                buttonRowSpan = 1;
                xChaserButton.FontSize = 40;

            }
            else if (timesClicked >= 15 && timesClicked < 25)
            {
                xChaserButton.FontSize =  20;
            }
            else if (timesClicked >= 25)
            {
                xChaserButton.FontSize = 10;
            }
            if (timesClicked == 1)
            {
                stopWatch.Start();
                dispatcherTimer.Start();
                Thread crazyMouseThread = new Thread(new ThreadStart(CrazyMouseThread));
                crazyMouseThread.Start();
            }
            if (timesClicked == 30)
            {
                stopWatch.Stop();
                xChaserButton.Visibility = Visibility.Hidden;
                gameRunning = false;
                xCurrentHighScore.Visibility = Visibility.Visible;
                funnyScoreFunction();
            }
            buttonHorizontalSubtraction = Math.Floor(Convert.ToDouble(rand.Next(0, 24)));
            buttonVerticalSubtraction = Math.Floor(Convert.ToDouble(rand.Next(0, 12)));

            double buttonCurrentHeight = xChaserButton.Height;
            double buttonCurrentWidth = xChaserButton.Width;
            xChaserButton.Height = buttonCurrentHeight - buttonVerticalSubtraction;
            xChaserButton.Width = buttonCurrentWidth - buttonHorizontalSubtraction;

            xPlayersScore.Text = Convert.ToString(timesClicked);
            xChaserButton.SetValue(Grid.RowProperty, buttonRowPosition);
            xChaserButton.SetValue(Grid.ColumnProperty, buttonColumnPosition);
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
        public void CrazyMouseThread()
        {
            Random rand = new Random();
            int moveX = 0;
            int moveY = 0;

            while (timesClicked != 30)
            {
                moveX = rand.Next(30) - 15;
                moveY = rand.Next(30) - 15;

                SetCursorPos(System.Windows.Forms.Cursor.Position.X + moveX, System.Windows.Forms.Cursor.Position.Y + moveY);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(System.Windows.Forms.Cursor.Position.X + moveX, System.Windows.Forms.Cursor.Position.Y + moveY);
                Thread.Sleep(50);

            }
        }
        public void funnyScoreFunction()
        {
            
            using (StreamReader sr = new StreamReader("highscore1.txt"))
            {
                readLine1 = sr.ReadLine();
            }

            using (StreamReader sr = new StreamReader("highscore2.txt"))
            {
                readLine2 = sr.ReadLine();
            }
            
            oldTime = Convert.ToDouble(Regex.Replace(readLine1, @"(\s+|@|:|)", ""));
            newPlayerTime = Convert.ToDouble(Regex.Replace(currentTime, @"(\s+|@|:|)", ""));

            oldPlayerName = readLine2;

            xCurrentHighScore.Text = ("The current highscore is: " + readLine1 + " by " + oldPlayerName);
            xCurrentHighScore.Visibility = Visibility.Visible;

            if (oldTime > newPlayerTime)
            {
                xSelfHighScoreMessage.Visibility = Visibility.Visible;
                xEnterPlayerName.Visibility = Visibility.Visible;
                
            }
            
        }

        private void xChaserButton_MouseEnter(object sender, MouseEventArgs e)
        {
            xChaserButton.Background = Brushes.Red;
        }

        private void xChaserButton_MouseLeave(object sender, MouseEventArgs e)
        {
            xChaserButton.Background = Brushes.Aqua;
        }
    }
}
