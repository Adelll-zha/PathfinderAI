using PathfinderAInetLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
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
    /// Logique d'interaction pour Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public string a = "BTCUSDT";
        string URL = "https://api.binance.com/api/v3/klines?symbol=BTCUSDT&interval=1d&limit=60";
    

        public Window2()
        {
            InitializeComponent();
            this.AllowsTransparency = true;
            this.Background = Brushes.Transparent;
            this.WindowStyle = WindowStyle.None;
            this.Topmost = true;


            this.IsHitTestVisible = true;


        }
        //console commande system
        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                try
                {
                 
                    if(codeTextBox.Text == "Set BTCUSDT")
                    {
                        a = "BTCUSDT";
                        resultTextBox.Text = "BTCUSDT";
                    }
                    if (codeTextBox.Text == "Set EURUSDT")
                    {
                        a = "EURUSDT";
                        resultTextBox.Text = "EURUSDT";
                    }
                    if (codeTextBox.Text == "Set ETHUSDT")
                    {
                        a = "ETHUSDT";
                        resultTextBox.Text = "ETHUSDT";
                    }
                    if(codeTextBox.Text  == "SOLUSDT")
                    {
                        a = "SOLUSDT";
                        resultTextBox.Text = "SOLUSDT";
                    }
                    if (codeTextBox.Text == "Set 0s/1h")
                    {
                        resultTextBox.Text = "0s/1h";
                        URL = $"https://api.binance.com/api/v3/klines?symbol={a}&interval=1m&limit=524";
                    }
                    if (codeTextBox.Text == "Set 1h/1d")
                    {
                        resultTextBox.Text = "1h/1d";
                        URL = $"https://api.binance.com/api/v3/klines?symbol={a}&interval=1d&limit=60";
                    }
                    if (codeTextBox.Text == "Set more 1d")
                    {
                        resultTextBox.Text = "more 1d";
                        URL = $"https://api.binance.com/api/v3/klines?symbol={a}&interval=1d&limit=524";
                    }
                    if(codeTextBox.Text == "Set 1h/2h")
                    {
                        resultTextBox.Text = "1h/2h";
                        URL = $"https://api.binance.com/api/v3/klines?symbol={a}&interval=1m&limit=60";
                    }
                    if (codeTextBox.Text == "Set 1h/2h")
                    {
                        PathfinderAI_PolynomialRegression polynomialRegression = new PathfinderAI_PolynomialRegression();
                        string result = polynomialRegression.Predict(URL);
                        resultTextBox.Text = result.ToString();

                    }
                    if (codeTextBox.Text == "MLR/PathfinderAI")
                    {
                        PathfinderAI pathfinderAI = new PathfinderAI();
                        string result = pathfinderAI.Predict(URL);
                        resultTextBox.Text = result.ToString();

                    }
                    if(codeTextBox.Text == "LogicalRegress/PathfinderAI")
                    {
                        PathfinderAI_LogisticRegression pathfinderAI_LogisticRegression = new PathfinderAI_LogisticRegression();
                        pathfinderAI_LogisticRegression.Run(URL);
                        resultTextBox.Text = pathfinderAI_LogisticRegression.strlogic;
                    }
                    if (codeTextBox.Text == "Poly/GETmov/PathfinderAI")
                    {
                        PathfinderAI_PolynomialRegression polynomialRegression = new PathfinderAI_PolynomialRegression();
                        PathfinderAI pathfinderAI = new PathfinderAI();
                        string result = polynomialRegression.Predict(URL);
                        string PriceURL = $"https://api.binance.com/api/v3/ticker/price?symbol={a}";
                        if (pathfinderAI.GetCurrentBitcoinPrice(PriceURL) < Convert.ToDouble(result))
                        {
                            resultTextBox.Text = $"Movements up ⬆️  \n {pathfinderAI.GetCurrentBitcoinPrice(PriceURL)} < {Convert.ToDouble(result)} ";
                        }
                        if (pathfinderAI.GetCurrentBitcoinPrice(PriceURL) > Convert.ToDouble(result))
                        {
                            resultTextBox.Text = $"Movements down ⬇️ \n {pathfinderAI.GetCurrentBitcoinPrice(PriceURL)} > {Convert.ToDouble(result)} ";
                        }

                    }
                    if (codeTextBox.Text == "Poly&&MLR=?/PathfinderAI")
                    {
                        PathfinderAI_PolynomialRegression polynomialRegression = new PathfinderAI_PolynomialRegression();
                        PathfinderAI pathfinderAI = new PathfinderAI();
                        string resultL = pathfinderAI.Predict(URL);
                        string resultP = polynomialRegression.Predict(URL);
                        string PriceURL = $"https://api.binance.com/api/v3/ticker/price?symbol={a}";
                        string current = pathfinderAI.GetCurrentBitcoinPrice(PriceURL).ToString();

                        double linearPrediction = double.Parse(resultL);
                        double polynomialPrediction = double.Parse(resultP);
                        double currentPrice = double.Parse(current);

                        bool isLinearPredictionHigher = linearPrediction > currentPrice;
                        bool isPolynomialPredictionHigher = polynomialPrediction > currentPrice;

                        if (isLinearPredictionHigher && isPolynomialPredictionHigher)
                        {
                            resultTextBox.Text = $"Both linear and polynomial predictions are higher than the current price. \n curent: ({currentPrice})\n MLR:{linearPrediction} \a Polynomial:{polynomialPrediction}";
                        }
                        else if (!isLinearPredictionHigher && !isPolynomialPredictionHigher)
                        {
                            resultTextBox.Text = $"Both linear and polynomial predictions are lower than the current price.  \n curent: ({currentPrice})\n MLR:{linearPrediction} \a Polynomial:{polynomialPrediction}";
                        }
                        else
                        {
                            resultTextBox.Text = $"⚠️ Warning! Linear and polynomial predictions are not both higher or lower than the current price.  \n curent: ({currentPrice})\n MLR:{linearPrediction} \a Polynomial:{polynomialPrediction}";

                        }
                    }
                    




                }
                catch (Exception ex)
                {
                    // Handle code execution errors here
                    resultTextBox.Text = $"Error: {ex.Message}";
                }


            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
               if (this.IsEnabled ==true)
                {
                    this.IsEnabled = false;
                }
                this.DragMove();
            this.IsEnabled = true;

        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
