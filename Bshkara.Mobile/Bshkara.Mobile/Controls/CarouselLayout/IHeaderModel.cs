using System.ComponentModel;

namespace Bshkara.Mobile.Controls.CarouselLayout
{
    public interface IHeaderModel : INotifyPropertyChanged
    {
        bool IsActive { get; set; }

        string ActiveImage { get; set; }

        string Image { get; set; }

        string Title { get; set; }
    }
}