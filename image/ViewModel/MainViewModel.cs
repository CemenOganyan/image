using image.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace image
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
        protected void Set<T>(ref T propertyFiled, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!object.Equals(propertyFiled, newValue))
            {
                T oldValue = propertyFiled;
                propertyFiled = newValue;
                OnPropertyChanged(propertyName);

                OnPropertyChanged(propertyName, oldValue, newValue);
            }
        }
        protected virtual void OnPropertyChanged(string propertyName, object oldValue, object newValue) { }
        Image _img1 = new Image();
        Image _img2 = new Image();
        Image _img3 = new Image();
        public Image Img1 { get => _img1; set => Set(ref _img1, value); }
        public Image Img2 { get => _img2; set => Set(ref _img2, value); }
        public Image Img3 { get => _img3; set => Set(ref _img3, value); }

        string _tb1;
        string _tb2;
        string _tb3;
        public string Tb1 { get => _tb1; set => Set(ref _tb1, value); }
        public string Tb2 { get => _tb2; set => Set(ref _tb2, value); }
        public string Tb3 { get => _tb3; set => Set(ref _tb3, value); }
        int _barProgress;
        public int BarProgress { get => _barProgress; set => Set(ref _barProgress, value); }

        WebClient web = new WebClient();
        WebClient web1 = new WebClient();
        WebClient web2 = new WebClient();
        int choice = 0;
        private Command addCommand;
        public Command AddCommand { get { return addCommand ?? (addCommand = new Command(obj => { DownloadImage(String.Copy(_tb1), "output.jpg"); choice = 1; })); } }
        private Command addCommand1;
        public Command AddCommand1 { get { return addCommand1 ?? (addCommand1 = new Command(obj => { DownloadImage(String.Copy(_tb2), "output1.jpg"); choice = 2; })); } }

        private Command addCommand2;
        public Command AddCommand2 { get { return addCommand2 ?? (addCommand2 = new Command(obj => { DownloadImage(String.Copy(_tb3), "output2.jpg"); choice = 3; })); } }

        private Command addCommand3;
        public Command AddCommand3 { get { return addCommand3 ?? (addCommand3= new Command(obj => { web.CancelAsync(); _barProgress = 0; })); } }
        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) { _barProgress = e.ProgressPercentage; }
        private void DownloadImage(string url, string fileName)
        {
            switch (choice)
            {
                case 1:
                    Task.Factory.StartNew(() => { web.DownloadFileCompleted += OnDownloadFileCompleted; web.DownloadProgressChanged += OnDownloadProgressChanged;
                        web.DownloadFileAsync(new Uri(url), fileName); });
                    break;
                case 2:
                    Task.Factory.StartNew(() => { web1.DownloadFileCompleted += OnDownloadFileCompleted; web1.DownloadProgressChanged += OnDownloadProgressChanged;
                        web1.DownloadFileAsync(new Uri(url), fileName); });
                    break;
                case 3:
                    Task.Factory.StartNew(() => { web2.DownloadFileCompleted += OnDownloadFileCompleted; web2.DownloadProgressChanged += OnDownloadProgressChanged;
                        web2.DownloadFileAsync(new Uri(url), fileName); });
                    break;
            }
        }
        async private void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var projectName = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            switch (choice)
            {
                case 1:
                    await Task.Factory.StartNew(() => { var imagePath = projectName + "\\output.jpg"; _img1.Source = new BitmapImage(new Uri(imagePath)); });
                    break;
                case 2:
                    await Task.Factory.StartNew(() => { var imagePath1 = projectName + "\\output1.jpg"; _img2.Source = new BitmapImage(new Uri(imagePath1)); }); 
                    break;
                case 3:
                    await Task.Factory.StartNew(() => { var imagePath2 = projectName + "\\output2.jpg"; _img3.Source = new BitmapImage(new Uri(imagePath2)); });
                    break;
            }
        } 
    }
}
