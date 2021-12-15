using System;
using System.Collections.Generic;
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
using System.Net;
using System.ComponentModel;
using System.Threading;

namespace image
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            barProgress.Value = e.ProgressPercentage;
        }
        int choice = 0;
        private void OnButtonSubmitClick(object sender, RoutedEventArgs e)
        {            
            DownloadImage(String.Copy(tb1.Text), "output.jpg");
            choice = 1;
        }
        private void OnButtonSubmitClick1(object sender, RoutedEventArgs e)
        {
            DownloadImage(String.Copy(tb2.Text), "output1.jpg");
            choice = 2;
        }
        private void OnButtonSubmitClick2(object sender, RoutedEventArgs e)
        {
            DownloadImage(String.Copy(tb3.Text), "output2.jpg");
            choice = 3;
        }
        private void OnButtonSubmitClick3(object sender, RoutedEventArgs e)
        {
            OnButtonSubmitClick(sender, e);
            OnButtonSubmitClick1(sender, e);
            OnButtonSubmitClick2(sender, e);
        }
        async private void DownloadImage(string url, string fileName)
        {
            WebClient web = new WebClient();
            web.DownloadFileCompleted += OnDownloadFileCompleted;
            web.DownloadProgressChanged += OnDownloadProgressChanged;
            web.DownloadFileAsync(new Uri(url), fileName); 
        }
        private void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var projectName = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            switch (choice)
            {
                case 1:
                    var imagePath = projectName + "\\output.jpg";
                    img1.Source = new BitmapImage(new Uri(imagePath));
                    break;
                case 2:
                    var imagePath1 = projectName + "\\output1.jpg";
                    img2.Source = new BitmapImage(new Uri(imagePath1));
                    break;
                case 3:
                    var imagePath2 = projectName + "\\output2.jpg";
                    img3.Source = new BitmapImage(new Uri(imagePath2));
                    break;
            }
        }
    }
}
