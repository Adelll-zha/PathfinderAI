using PathfinderAInetLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace XboxGameBarApp
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        // Créer un Timer avec un intervalle de 20 secondes
        private static Timer timer = new Timer(2500);
        public string url = "https://api.binance.com/api/v3/klines?symbol=BTCUSDT&interval=1d&limit=524";
        public string a = "BTCUSDT";
        public Window1()
        {
            InitializeComponent();
            this.AllowsTransparency = true;
            this.Background = Brushes.Transparent;
            this.WindowStyle = WindowStyle.None;
            this.Topmost = true;
            up_img.Visibility = Visibility.Hidden;
            down_img.Visibility = Visibility.Hidden;

            this.IsHitTestVisible = true;
            // call timer
            timer.Elapsed += OnTimerElapsed;

            // start timer
            timer.Start();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (timer.Enabled == true) 
            {

                timer.Enabled = false;
            }
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        public void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() => Actu());
        }


        // timer void
        public void Actu()
        {
    
            if(MoneyBox.SelectedItem != null)
            {
               if(MoneyBox.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: BTCUSDT")
                {
                    a = "BTCUSDT";
                }
                if (MoneyBox.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: ETHUSDT")
                {
                    a = "ETHUSDT";
                }
                if (MoneyBox.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: EURUSDT")
                {
                    a = "EURUSDT";
                }
                if(MoneyBox.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: SOLUSDT")
                {
                    a = "SOLUSDT";
                }
            }
            string PriceURL = $"https://api.binance.com/api/v3/ticker/price?symbol={a}";
            PathfinderAI pathfinder = new PathfinderAI();
            string current = Convert.ToString(pathfinder.GetCurrentBitcoinPrice(PriceURL));
            currentbox.Text = Convert.ToString( pathfinder.GetCurrentBitcoinPrice(PriceURL));
          
            if(_1h_pam.IsChecked == true)
            {
           
                _1d_pam.IsChecked = false;
                more_1d.IsChecked = false;
                 url = $"https://api.binance.com/api/v3/klines?symbol={a}&interval=1m&limit=524";
                string result = pathfinder.Predict(url);
                predictbox.Text = result;
                if(Convert.ToDouble(current) < Convert.ToDouble(result))
                {
                    down_img.Visibility = Visibility.Hidden;
                    up_img.Visibility = Visibility.Visible;
                }
                else if (Convert.ToDouble(current) > Convert.ToDouble(result))
                {
                    up_img.Visibility = Visibility.Hidden;
                    down_img.Visibility = Visibility.Visible;
                }
            }
            if(_1d_pam.IsChecked == true)
            {
              
                _1h_pam.IsChecked = false;
                more_1d.IsChecked = false;
                url = $"https://api.binance.com/api/v3/klines?symbol={a}&interval=1d&limit=60";
                string result = pathfinder.Predict(url);
                predictbox.Text = result;
                if (Convert.ToDouble(current) < Convert.ToDouble(result))
                {
                    down_img.Visibility = Visibility.Hidden;
                    up_img.Visibility = Visibility.Visible;
                }
                else if (Convert.ToDouble(current) > Convert.ToDouble(result))
                {
                    up_img.Visibility = Visibility.Hidden;
                    down_img.Visibility = Visibility.Visible;
                }
            }
            if(more_1d.IsChecked == true)
            {
            
                _1d_pam.IsChecked= false;
                _1d_pam.IsChecked = false;
                url = $"https://api.binance.com/api/v3/klines?symbol={a}&interval=1d&limit=524";
                string result = pathfinder.Predict(url);
                predictbox.Text = result;
                if (Convert.ToDouble(current) < Convert.ToDouble(result))
                {
                    down_img.Visibility = Visibility.Hidden;
                    up_img.Visibility = Visibility.Visible;
                }
                else if (Convert.ToDouble(current) > Convert.ToDouble(result))
                {
                    up_img.Visibility = Visibility.Hidden;
                    down_img.Visibility = Visibility.Visible;
                }
            }

        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
           if(timer.Enabled == false) { 
            
            timer.Enabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }
    }
}
