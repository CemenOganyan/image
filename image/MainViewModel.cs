using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace image
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));  
        }
        private int _Clicks;
        public int Clicks
        {
            get { return _Clicks; }
            set
            {
                _Clicks = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Task.Delay(1000).Wait();
                    Clicks++;
                }
            });
        }
    }
}
