using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace image.ViewModel
{
    public class MainViewModel : AbstractMainViewModel
    {
        #region WebClaents
        WebClient web = new WebClient();
        WebClient web1 = new WebClient();
        WebClient web2 = new WebClient();
        #endregion
        #region Переменные без привязки
        int ClickButton, IntermediateBarProgress, IntermediateBarProgress1, IntermediateBarProgress2;
        #endregion
        #region Команды
        protected override void StartExecute()
        {
            DownloadImage(Tb1);
        }
        protected override void StartExecute1()
        {
            DownloadImage1(Tb2);
        }
        protected override void StartExecute2()
        {
            DownloadImage2(Tb3);
        }
        protected override void StartAllExecute()
        {
            StartExecute();
            StartExecute1();
            StartExecute2();
        }
        protected override void StopExecute()
        {
            ClickButton = 1;
            web.CancelAsync();
        }
        protected override void StopExecute1()
        {
            ClickButton = 1;
            web1.CancelAsync();
        }
        protected override void StopExecute2()
        {
            ClickButton = 1;
            web2.CancelAsync();
        }
        #endregion
        #region объединение трех progressBar в один общий
        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            IntermediateBarProgress = e.ProgressPercentage;
            BarProgress = IntermediateBarProgress + IntermediateBarProgress1 + IntermediateBarProgress2;
            #region условия для общего progressBar
            if (IntermediateBarProgress > 0 && IntermediateBarProgress1 > 0 && IntermediateBarProgress2 > 0) BarMaxVall = 300;
            else if ((IntermediateBarProgress > 0 && IntermediateBarProgress1 > 0) || (IntermediateBarProgress > 0 && IntermediateBarProgress2 > 0) || (IntermediateBarProgress1 > 0 && IntermediateBarProgress2 > 0)) BarMaxVall = 200;
            else if (IntermediateBarProgress > 0 || IntermediateBarProgress1 > 0 || IntermediateBarProgress2 > 0) BarMaxVall = 100;
            IntermediateBarProgress = 0;
            #endregion
        }
        private void OnDownloadProgressChanged1(object sender, DownloadProgressChangedEventArgs e)
        {
            IntermediateBarProgress1 = e.ProgressPercentage;
            BarProgress = IntermediateBarProgress + IntermediateBarProgress1 + IntermediateBarProgress2;
            #region условия для общего progressBar
            if (IntermediateBarProgress > 0 && IntermediateBarProgress1 > 0 && IntermediateBarProgress2 > 0) BarMaxVall = 300;
            else if ((IntermediateBarProgress > 0 && IntermediateBarProgress1 > 0) || (IntermediateBarProgress > 0 && IntermediateBarProgress2 > 0) || (IntermediateBarProgress1 > 0 && IntermediateBarProgress2 > 0)) BarMaxVall = 200;
            else if (IntermediateBarProgress > 0 || IntermediateBarProgress1 > 0 || IntermediateBarProgress2 > 0) BarMaxVall = 100;
            IntermediateBarProgress1 = 0;
            #endregion
        }
        private void OnDownloadProgressChanged2(object sender, DownloadProgressChangedEventArgs e)
        {
            IntermediateBarProgress2 = e.ProgressPercentage;
            BarProgress = IntermediateBarProgress + IntermediateBarProgress1 + IntermediateBarProgress2;
            #region условия для общего progressBar
            if (IntermediateBarProgress > 0 && IntermediateBarProgress1 > 0 && IntermediateBarProgress2 > 0) BarMaxVall = 300;
            else if ((IntermediateBarProgress > 0 && IntermediateBarProgress1 > 0) || (IntermediateBarProgress > 0 && IntermediateBarProgress2 > 0) || (IntermediateBarProgress1 > 0 && IntermediateBarProgress2 > 0)) BarMaxVall = 200;
            else if (IntermediateBarProgress > 0 || IntermediateBarProgress1 > 0 || IntermediateBarProgress2 > 0) BarMaxVall = 100;
            IntermediateBarProgress2 = 0;
            #endregion
        }
        #endregion
        #region Загрузка картинок
        async private void DownloadImage(string url)
        {
            web.DownloadProgressChanged += OnDownloadProgressChanged;
            try { await web.DownloadFileTaskAsync(new Uri(url), "output1.jpg"); Img1 = Tb1; }
            catch { if (ClickButton == 0) MessageBox.Show("Неправильный url"); else ClickButton = 0; } 
        }
        async private void DownloadImage1(string url)
        {
            web1.DownloadProgressChanged += OnDownloadProgressChanged1;
            try { await web1.DownloadFileTaskAsync(new Uri(url), "output2.jpg"); Img2 = Tb2; }
            catch { if (ClickButton == 0) MessageBox.Show("Неправильный url"); else ClickButton = 0; }
        }
        async private void DownloadImage2(string url)
        {
            web2.DownloadProgressChanged += OnDownloadProgressChanged2;
            try { await web2.DownloadFileTaskAsync(new Uri(url), "output3.jpg"); Img3 = Tb3; }
            catch { if (ClickButton == 0) MessageBox.Show("Неправильный url"); else ClickButton = 0; }
        }
        #endregion
    }
}
