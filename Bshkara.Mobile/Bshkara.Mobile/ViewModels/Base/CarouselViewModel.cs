using Bshkara.Mobile.Controls.CarouselLayout;
using Xamarin.Forms;

namespace Bshkara.Mobile.ViewModels.Base
{
    public class CarouselViewModel : BaseViewModel, IHeaderModel
    {
        public CarouselViewModel()
        {
        }

        public CarouselViewModel(BaseViewModel parentViewModel)
        {
            ParentViewModel = parentViewModel;
        }

        public BaseViewModel ParentViewModel { get; set; }

        public ContentPage ParentView { get; set; }

        public bool IsActive { get; set; }

        public string ActiveImage { get; set; }

        public string Image { get; set; }
    }
}