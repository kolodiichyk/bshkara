using System.Collections.ObjectModel;
using System.Windows.Input;
using Bshkara.Mobile.ViewModels.Base;
using Xamarin.Forms;

namespace Bshkara.Mobile.ViewModels
{
    public class ImageViewerViewModel : BaseViewModel
    {
        public bool NextVisibility { get; set; }

        public bool PreviousVisibility { get; set; }

        public ObservableCollection<CarouselViewModel> Views { get; set; }

        public CarouselViewModel SelectedItem { get; set; }

        public ICommand NextCommand => new Command(Next);

        public ICommand PreviousCommand => new Command(Previous);

        public string CountOf => $"{Views.IndexOf(SelectedItem) + 1} of {Views.Count}";

        private void Next()
        {
            var currentIndex = Views.IndexOf(SelectedItem);
            if (currentIndex + 1 <= Views.Count - 1)
                SelectedItem = Views[currentIndex + 1];
        }

        private void Previous()
        {
            var currentIndex = Views.IndexOf(SelectedItem);
            if (currentIndex - 1 >= 0)
                SelectedItem = Views[currentIndex - 1];
        }

        protected override void NotifyPropertyChanged(string propertyName = null)
        {
            base.NotifyPropertyChanged(propertyName);

            if (propertyName == nameof(SelectedItem) /*&& SelectedItem != null*/)
            {
                NotifyPropertyChanged(nameof(CountOf));

                PreviousVisibility = Views.IndexOf(SelectedItem) != 0;
                NextVisibility = Views.IndexOf(SelectedItem) != Views.Count - 1;
            }
        }
    }
}