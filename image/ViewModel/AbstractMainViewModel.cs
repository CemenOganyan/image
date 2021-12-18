using Simplified;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace image.ViewModel
{
    public abstract class AbstractMainViewModel : BaseInp
    {
        #region Поля, хранящие значения свойств
        private BitmapImage? _img1;
        private BitmapImage? _img2;
        private BitmapImage? _img3;
        private string _tb1 = string.Empty;
        private string _tb2 = string.Empty;
        private string _tb3 = string.Empty;
        private int _barMaxVall = 0;
        private int _barProgress;
        #endregion

        #region Свойства
        public BitmapImage? Img1 { get => _img1; set => Set(ref _img1, value); }
        public BitmapImage? Img2 { get => _img2; set => Set(ref _img2, value); }
        public BitmapImage? Img3 { get => _img3; set => Set(ref _img3, value); }

        public string Tb1 { get => _tb1; set => Set(ref _tb1, value); }
        public string Tb2 { get => _tb2; set => Set(ref _tb2, value); }
        public string Tb3 { get => _tb3; set => Set(ref _tb3, value); }

        public int BarProgress { get => _barProgress; set => Set(ref _barProgress, value); }
        public int BarMaxVall { get => _barMaxVall; set => Set(ref _barMaxVall, value); }
        #endregion

        #region Команды (Это тоже свойства, но со специфическим функционалом)
        public RelayCommand StartCommand { get; }
        public RelayCommand StartCommand1 { get; }
        public RelayCommand StartCommand2 { get; }
        public RelayCommand StopCommand { get; }
        public RelayCommand StopCommand1 { get; }
        public RelayCommand StopCommand2 { get; }
        public RelayCommand StartCommandAll { get; }
        #endregion

        #region Конструктор и методы команд
        protected AbstractMainViewModel()
        {
            StartCommand = new RelayCommand(StartExecute);
            StartCommand1 = new RelayCommand(StartExecute1);
            StartCommand2 = new RelayCommand(StartExecute2);
            StopCommand = new RelayCommand(StopExecute);
            StopCommand1 = new RelayCommand(StopExecute1);
            StopCommand2 = new RelayCommand(StopExecute2);
            StartCommandAll = new RelayCommand(StartAllExecute);
        }
        protected abstract void StartExecute();
        protected abstract void StartExecute1();
        protected abstract void StartExecute2();
        protected abstract void StopExecute();
        protected abstract void StopExecute1();
        protected abstract void StopExecute2();
        protected abstract void StartAllExecute();
        #endregion
    }
}