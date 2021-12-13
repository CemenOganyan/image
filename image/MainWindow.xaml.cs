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

        private void OnButtonSubmitClick(object sender, RoutedEventArgs e)
        {
            DownloadImage(String.Copy(tb1.Text), "output.jpg");
        }

        private void DownloadImage(string url, string fileName)
        {
            WebClient web = new WebClient();
            web.DownloadProgressChanged += OnDownloadProgressChanged;
            web.DownloadFileCompleted += OnDownloadFileCompleted;
            web.DownloadFileAsync(new Uri(url), fileName);
        }

        private void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var projectName = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var imagePath = projectName + "\\output.jpg";
            img1.Source = new BitmapImage(new Uri(imagePath));
            MessageBox.Show("Загрузка файла завершена!");
        }
    }
}
