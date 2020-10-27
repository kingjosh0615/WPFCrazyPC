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
        public static string stopwtchTime;
        public static int timesClicked = 0;
        bool gameRunning = true;
        

        public MainWindow()
        {
            InitializeComponent();
            
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

            timesClicked++;
            if (timesClicked > 1 && timesClicked < 5)
            {

            }
            else if (timesClicked >= 5 && timesClicked < 10)
            {

            }
            else if (timesClicked >= 10 && timesClicked < 15)
            {

            }
            else if (timesClicked >= 15 && timesClicked < 20)
            {

            }  
            

            
            
            
            xPlayersScore.Text = Convert.ToString(timesClicked);
        }
        public void UpdateTime()
        {
            this.Dispatcher.Invoke(() =>
            {
                
             var watch = new Stopwatch();
                
               

                if (timesClicked == 1)
                {
                    watch.Start();
                }
                if (timesClicked == 20)
                {
                    watch.Stop();
                    xChaserButton.Visibility = Visibility.Hidden;
                    gameRunning = false;
                }



                TimeSpan ts = watch.Elapsed;
                stopwtchTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

                xGamerTime.Text = ("Time: " + stopwtchTime);
            });

            

        }
    }
}
