using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EtheriumWatcher
{
    public partial class MainWindow : Window
    {
        CoinInfo ethCoin = new CoinInfo();
        int slideState = 0;

        public MainWindow()
        {
            InitializeComponent();
            GetCoinInfo(null, null);

            /* Temporary until the 'options' system is in place */
            stackPan_Settings1.Visibility = Visibility.Hidden;
            stackPan_Settings2.Visibility = Visibility.Hidden;


            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(GetCoinInfo);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }

        private void GetCoinInfo(object sender, EventArgs e)
        {
            string src;
            using (WebClient wc = new WebClient())
            {
                src = wc.DownloadString("http://coinmarketcap-nexuist.rhcloud.com/api/eth");
            }
            ethCoin.ParseJson(src);
            SetPrice(ethCoin, label_Currency.Content.ToString());


        }

        private void SetPrice(CoinInfo coin, string currency)
        {
            int rountTo = 2;
            
            string priceStr = "";
            switch (currency.ToLower())
            {
                case "usd":
                    priceStr = TruncateDecimal(ethCoin.PriceUSD, rountTo).ToString("C" + rountTo, CultureInfo.CreateSpecificCulture("en-US"));
                    break;
                case "eur":
                    priceStr = TruncateDecimal(ethCoin.PriceEUR, rountTo).ToString("C" + rountTo, CultureInfo.CreateSpecificCulture("fr-FR"));
                    break;
                case "cny":
                    priceStr = TruncateDecimal(ethCoin.PriceCNY, rountTo).ToString("C" + rountTo, CultureInfo.CreateSpecificCulture("zh-CN"));
                    break;
                case "cad":
                    priceStr = TruncateDecimal(ethCoin.PriceCAD, rountTo).ToString("C" + rountTo, CultureInfo.CreateSpecificCulture("en-CA"));
                    break;
                case "rub":
                    priceStr = TruncateDecimal(ethCoin.PriceRUB, rountTo).ToString().Replace('.',','); //Will be weird if price exceeds 1000rub
                    break;
                case "btc":
                    priceStr = "Ƀ" + TruncateDecimal(ethCoin.PriceBTC, rountTo);

                    break;
            }
            label_Price.Content = priceStr;
            label_Currency.Content = currency.ToUpper();
        }

        private string RemoveExtraZeros(string str)
        {
            bool prevWas0 = false;
            int index = 0;
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];

                if (!char.IsDigit(c)) continue;
                if (c == '0' && !prevWas0)
                {
                    prevWas0 = true;
                    index = i;
                }
                else if (c != '0') prevWas0 = false;
            }
            if (index == 0 || prevWas0 == false)
                return str;

            string finalStr = str.Substring(0, index);
            if (!char.IsDigit(str[str.Length - 1]))
            {
                finalStr += str[str.Length - 1];
            }
            return finalStr;
        }

        public decimal TruncateDecimal(decimal value, int precision)
        {
            decimal step = (decimal)Math.Pow(10, precision);
            int tmp = (int)Math.Truncate(step * value);
            return tmp / step;
        }

        private void testREc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 1)
            {
                this.DragMove();
            }
            else if (e.ClickCount == 2)
            {
                if(MainWindow1.WindowState != WindowState.Maximized)
                {
                    MainWindow1.WindowState = WindowState.Maximized;
                }
                else
                {
                    MainWindow1.WindowState = WindowState.Normal;
                }
            }

        }

        private void label_X_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void label_Min_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void FadeIn(object sender, MouseEventArgs e)
        {
            UIElement lab = (UIElement)e.OriginalSource;
            lab.Opacity = 1;
        }

        private void Fadeout(object sender, MouseEventArgs e)
        {
            UIElement lab = (UIElement)e.OriginalSource;
            lab.Opacity = .10;
        }


        private void MainWindow1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            int startHeight = 650;
            int startWidth = 550;
            int heightOffset = 300;

            if(slideState == 1)
            {
                grid_Main.RenderTransform = new TranslateTransform((int)(grid_Main.ActualWidth) * -1, 0);
            }
            else
            { 

            }

            if (MainWindow1.WindowState == WindowState.Maximized && (MainWindow1.Height <= startHeight || MainWindow1.Width <= startWidth))
            {
                image_EthLogo.VerticalAlignment = VerticalAlignment.Top;
                grid_Price.VerticalAlignment = VerticalAlignment.Stretch;
                grid_Price.Margin = new Thickness(0, startHeight - 100 - heightOffset, 0, 0);
                return;
            }




   
            if(MainWindow1.Height > startHeight - heightOffset)
            {
                image_EthLogo.VerticalAlignment = VerticalAlignment.Top;
                image_EthLogo.Margin = new Thickness(0, 0, 0, 0);
                grid_Price.VerticalAlignment = VerticalAlignment.Stretch;
                grid_Price.Margin = new Thickness(0, startHeight - 100 - heightOffset, 0, 0);

            }
            else
            {

                grid_Price.VerticalAlignment = VerticalAlignment.Bottom;
                grid_Price.Margin = new Thickness(0, 0, 0, 0);

            }
            if (MainWindow1.Height < startHeight - heightOffset + 100)
            {

                image_EthLogo.VerticalAlignment = VerticalAlignment.Bottom;
                image_EthLogo.Margin = new Thickness(0, 0, 0,  100);
            }

        }

        private void label_Currency_MouseUp(object sender, MouseButtonEventArgs e)
        {
            List<string> currencyList = new List<string> { "USD", "EUR", "CNY", "CAD", "RUB", "BTC" };
            string current = label_Currency.Content.ToString();

            int index = currencyList.IndexOf(current);
            if (index >= currencyList.Count - 1)
                index = -1;
            SetPrice(ethCoin, currencyList[index + 1]);

        }

        private void Slide(object sender, MouseButtonEventArgs e)
        {
            int mainWinX = (int)MainWindow1.PointToScreen(new Point(0, 0)).X;
            int mainWinWidth = (int)MainWindow1.ActualWidth;

  

            if (slideState == 0)
            {
                Slide(grid_Main, 0, (int)(grid_Main.ActualWidth) * -1);
                slideState = 1;
            }
            else
            {
                Slide(grid_Main, (int)(grid_Main.ActualWidth) * -1, 0);
              //  Slide(grid_Settings, 0, (int)MainWindow1.Width);
                slideState = 0;
            }




        }

        private void Slide(UIElement ele, int from, int to)
        {
            DoubleAnimation slideMainAnimation = new DoubleAnimation();
            slideMainAnimation.Duration = TimeSpan.FromSeconds(2);
            Console.WriteLine("Duration: " + Math.Abs((from - to)) * .003);
 
            slideMainAnimation.From = from;
            slideMainAnimation.To = to;

            BackEase ease = new BackEase();
            ease.Amplitude = .15;

            slideMainAnimation.EasingFunction = ease;


            ele.RenderTransform = new TranslateTransform();
            Storyboard.SetTarget(slideMainAnimation, ele);
            Storyboard.SetTargetProperty(slideMainAnimation, new PropertyPath("(0).(1)", new DependencyProperty[] { UIElement.RenderTransformProperty, TranslateTransform.XProperty }));
            Storyboard sb = new Storyboard();

           // sb.AccelerationRatio = 0.1;




            sb.Children.Add(slideMainAnimation);
            sb.Begin();
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
