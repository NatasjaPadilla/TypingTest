using FileCreator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace typingtest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double word = 0;
        double wrong = 0;
        int index = 0;
        string path = @"C:\Users\Natasja\source\repos\typingtest\words.csv";
        List<string> lines = new List<string>();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();
        string currentTime = string.Empty;
        private static string filePath = "";

        public MainWindow()
        {
            InitializeComponent();
            inputTB.IsEnabled = false;
            Class1.fileCreator();
        }

        private void CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(CB.SelectedIndex == 0)
            {
                MessageBox.Show("The screen will display one (1) word at a time.\n" +
                    "There will be fifty (50) words in total.");
                oneWord();
            }
            else if(CB.SelectedIndex == 1)
            {
                MessageBox.Show("You have one (1) minute to type as many words as you can.");
                timeBound();
            }

            startBTN.IsHitTestVisible = true;
        }

        private void startBTN_Click(object sender, RoutedEventArgs e)
        {
            inputTB.IsEnabled = true;
            if (CB.SelectedIndex == 0)
            {
                LB.SelectedIndex = 0;
                dispatcherTimer.Tick += new EventHandler(dt_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
                stopWatch.Start();
                dispatcherTimer.Start();

                extras();
            }
            else if (CB.SelectedIndex == 1)
            {
                DispatcherTimer timer = new DispatcherTimer(); ;
                TimeSpan time;
                time = TimeSpan.FromMinutes(1);

                timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                {
                    timerLBL.Content = time.ToString("c");
                    if (time == TimeSpan.Zero)
                    {
                        timer.Stop();
                        wpmLBL.Content = "WPM: " + word.ToString();
                        double accuracy = ((word - wrong) / word) * 100;
                        typoLBL.Content = "Typos: " + wrong.ToString();
                        accuracyLBL.Content = "Accuracy: " + Math.Round(accuracy, 2).ToString() + "%";
                        inputTB.IsEnabled = false;
                        saveBTN.IsHitTestVisible = true;
                    }
                    time = time.Add(TimeSpan.FromSeconds(-1));
                }, Application.Current.Dispatcher);

                timer.Start();

                LB.SelectedIndex = index;
            }

            startBTN.IsHitTestVisible = false;
        }

        private void saveBTN_Click(object sender, RoutedEventArgs e)
        {
            Class1.fileWriter(true, "Your Test Score");
            Class1.fileWriter(true, wpmLBL.Content.ToString());
            Class1.fileWriter(true, typoLBL.Content.ToString());
            Class1.fileWriter(true, accuracyLBL.Content.ToString());
            MessageBox.Show("Test results saved.");
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (CB.SelectedIndex == 0)
                {
                    if (LB.SelectedItem.ToString() != inputTB.Text)
                    {
                        wrong += 1;
                    }
                    extras();
                    LB.SelectedIndex = 0;

                    if (word == 50)
                    {
                        inputTB.IsEnabled = false;
                        LB.Items.Clear();
                        stopWatch.Stop();
                        double wpm = word / (double)minuteDec();
                        int WPM = (int)Math.Round(wpm);
                        wpmLBL.Content = "WPM: " + WPM.ToString();
                        double accuracy = ((word - wrong) / word) * 100;
                        typoLBL.Content = "Typos: " + wrong.ToString();
                        accuracyLBL.Content = "Accuracy: " + Math.Round(accuracy, 2).ToString() + "%";
                        saveBTN.IsHitTestVisible = true;
                    }
                }
                returnBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
                if(CB.SelectedIndex == 1)
                {
                    if (LB.SelectedItem.ToString() != inputTB.Text)
                    {
                        wrong += 1;
                    }
                    index += 1;
                    LB.SelectedIndex = index;
                }
                word += 1;
                inputTB.Text = "";
            }
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                shiftBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            if (e.Key == Key.Space)
            {
                spaceBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }

            if (e.Key == Key.A)
            {
                aBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.B)
            {
                bBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.C)
            {
                cBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.D)
            {
                dBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.E)
            {
                eBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.F)
            {
                fBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.G)
            {
                gBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.H)
            {
                hBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.I)
            {
                iBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.J)
            {
                jBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.K)
            {
                kBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.L)
            {
                lBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.M)
            {
                mBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.N)
            {
                nBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.O)
            {
                oBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.P)
            {
                pBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.Q)
            {
                qBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.R)
            {
                rBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.S)
            {
                sBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.T)
            {
                tBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.U)
            {
                uBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.V)
            {
                vBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.W)
            {
                wBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.X)
            {
                xBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.Y)
            {
                yBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
            else if (e.Key == Key.Z)
            {
                zBTN.Background = new SolidColorBrush(Color.FromRgb(205, 180, 219));
            }
        }

        private void OnKeyUpHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                returnBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                shiftBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            if (e.Key == Key.Space)
            {
                spaceBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }

            if (e.Key == Key.A)
            {
                aBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.B)
            {
                bBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.C)
            {
                cBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.D)
            {
                dBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.E)
            {
                eBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.F)
            {
                fBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.G)
            {
                gBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.H)
            {
                hBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.I)
            {
                iBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.J)
            {
                jBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.K)
            {
                kBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.L)
            {
                lBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.M)
            {
                mBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.N)
            {
                nBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.O)
            {
                oBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.P)
            {
                pBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.Q)
            {
                qBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.R)
            {
                rBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.S)
            {
                sBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.T)
            {
                tBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.U)
            {
                uBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.V)
            {
                vBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.W)
            {
                wBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.X)
            {
                xBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.Y)
            {
                yBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
            else if (e.Key == Key.Z)
            {
                zBTN.Background = new SolidColorBrush(Color.FromRgb(255, 175, 204));
            }
        }

        private void oneWord()
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line = "";

                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                lines = new List<string>();
            }
        }

        private void timeBound()
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("file does not exist");
            }
            else if (File.Exists(path))
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        LB.Items.Add(line);
                    }
                }
            }
        }

        private void extras()
        {
            Random r = new Random();
            int rando = r.Next(lines.Count);

            LB.Items.Clear();
            LB.Items.Add(lines[rando]);
        }

        private decimal minuteDec()
        {
            string[] timeDec = timerLBL.Content.ToString().Split(new string[] { ":" }, StringSplitOptions.None);
            decimal decMin = Math.Round((Convert.ToDecimal(timeDec[0])) + (Convert.ToDecimal(timeDec[1])) / 60, 1);
            return decMin;
        }

        void dt_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                timerLBL.Content = currentTime;
            }
        }

        private void LB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(CB.SelectedIndex == 1)
            {
                ((ListBox)sender).ScrollIntoView(e.AddedItems[0]);
            }
        }

        //public static void fileCreator()
        //{
        //    filePath = "TypingTestScore.txt";
        //    if (!File.Exists(filePath))
        //    {
        //        File.Create(filePath);
        //    }
        //}

        //public static void fileWriter(bool appendFlag, string message)
        //{
        //    using (StreamWriter sr = new StreamWriter(filePath, appendFlag))
        //    {
        //        sr.WriteLine(message);
        //    }
        //}
    }
}
