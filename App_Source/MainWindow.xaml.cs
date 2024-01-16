using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Timers;
using PathfinderAInetLibrary;
using XboxGameBarApp;
using System.Net;
using System.Security.Policy;
using Accord.Extensions.Math.Geometry;

namespace TaskBar
{
   
    public partial class MainWindow : Window
    {
      
        public MainWindow()
        {
            InitializeComponent();
            // Set the window to be transparent and allow click-through
            
            this.AllowsTransparency = true;
            this.Background = Brushes.Transparent;
            this.WindowStyle = WindowStyle.None;
            this.Topmost = true;
           
            this.IsHitTestVisible = true;

            // Set the window position and size to match the screen
            this.Left = 0;
            this.Top = 0;
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = 43;
         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          Window1 window = new Window1();
           window.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
       
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Window2 window = new Window2();
            window.Show();
        }

        //open url
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
          string  edgePath = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
            string url = "https://pathfinderai.000webhostapp.com/";

            // start Edge
            Process.Start(edgePath, url);
        }
    }
}