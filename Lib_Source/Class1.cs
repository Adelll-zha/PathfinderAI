using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Models.Regression.Linear;
using Newtonsoft.Json;
using Accord.Statistics.Models.Regression.Fitting;
using Accord.Statistics.Kernels;
using Accord.Math;
using Accord.Statistics.Models.Regression;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using System.IO;
using Accord.Neuro;
using Accord.Neuro.Learning;


namespace PathfinderAInetLibrary
{
    public class PathfinderAI
    {
        private const int DELAY = 2;
        private MultipleLinearRegression regression;
        private double[][] inputs;
        private double[] outputs;
        public string lasterror;
       
        public string Predict(string url)
        {
            using (WebClient client = new WebClient())
            {
                // data formatting
                string json = client.DownloadString(url);
                List<string[]> data = JsonConvert.DeserializeObject<List<string[]>>(json);
                CultureInfo culture = new CultureInfo("en-US");
                double[] prices = data.Select(x => double.Parse(x[4], culture)).ToArray();

                inputs = new double[prices.Length - 24 - DELAY][];
                outputs = new double[prices.Length - 24 - DELAY];

                for (int i = 0; i < inputs.Length; i++)
                {
                    inputs[i] = new double[24];
                    for (int j = 0; j < 24; j++)
                    {
                        inputs[i][j] = prices[i + j];
                    }
                    outputs[i] = prices[i + 24 + DELAY];
                }

                regression = new MultipleLinearRegression(24);
                regression.Regress(inputs, outputs);
                double[] predictions = regression.Transform(inputs);
                double prediction = predictions.Last();
                lasterror = new SquareLoss(outputs).Loss(predictions).ToString();
                return string.Format(prediction.ToString());

            }



        }
        public double GetCurrentBitcoinPrice(string PriceURL)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(PriceURL);
                    dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    double currentPrice = (double)data.price;
                    return currentPrice;
                }
            }
            catch (Exception ex)
            {
                // Managing price recovery errors
                Console.WriteLine("Erreur lors de la récupération du prix du Bitcoin : " + ex.Message);
                return 0.0; // You can manage this default value appropriately
            }
        }


    }
    public class PathfinderAI_LogisticRegression
    {
        public string strlogic;
        public void Run(string url)
        {
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                List<double[]> data = JsonConvert.DeserializeObject<List<double[]>>(json);

                double[][] inputs = data.Select(x => new double[] { x[0], x[1] }).ToArray();

                int[] outputs = data.Select((x, i) =>
                {
                    if (i == 0) 
                        return 0;
                    else if (x[2] > data[i - 1][2])
                        return 1;
                    else if (x[2] < data[i - 1][2]) 
                        return -1;
                    else 
                        return 0;
                }).ToArray();

              
                Gaussian gaussian = new Gaussian(0.1);

               
                var machine = new SupportVectorMachine<Gaussian>(inputs[0].Length, gaussian);

                var teacher = new SequentialMinimalOptimization<Gaussian>()
                {
                    Complexity = 100 
                };

          
                var svm = teacher.Learn(inputs, outputs);

                
                int[] predicted = inputs.Select(input => Math.Sign(svm.Compute(input))).ToArray();

                string predictedString = string.Join(", ", predicted);
                strlogic = predictedString;
            }
        }
    }
    public class PathfinderAI_PolynomialRegression
        {
            public string lasterror;
            private PolynomialRegression regression;
            private double[] inputs;
            private double[] outputs;
            private const int DELAY = 2;
            public string Polyminal_lasterror;
            public string Predict(string url)
            {
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);
                    List<string[]> data = JsonConvert.DeserializeObject<List<string[]>>(json);
                    CultureInfo culture = new CultureInfo("en-US");
                    double[] prices = data.Select(x => double.Parse(x[4], culture)).ToArray();

                    inputs = new double[prices.Length - 24 - DELAY];
                    outputs = new double[prices.Length - 24 - DELAY];

                    for (int i = 0; i < inputs.Length; i++)
                    {
                        inputs[i] = i;
                        outputs[i] = prices[i + 24 + DELAY];
                    }

                    // Use Ordinary Least Squares to learn the regression
                    OrdinaryLeastSquares ols = new OrdinaryLeastSquares();

                    // Create a new polynomial with 2 degree
                    var poly = new Polynomial(2);

                    // Use PolynomialLeastSquares to learn the regression
                    PolynomialLeastSquares pls = new PolynomialLeastSquares() { Degree = 2 };

                    // Use OLS to learn the multiple linear regression
                    this.regression = pls.Learn(inputs, outputs);

                    // Compute the output for a given input:
                    double y = regression.Transform(-1); // The answer will be 0

                    double[] predictions = inputs.Select(input => regression.Transform(input)).ToArray();
                    double prediction = predictions.Last();

                    lasterror = new SquareLoss(outputs).Loss(predictions).ToString();
                    Polyminal_lasterror = (100 - (double.Parse(lasterror) / outputs.Average()) * 100).ToString();
                    return string.Format(prediction.ToString());
                }
            }

    }
    
}


