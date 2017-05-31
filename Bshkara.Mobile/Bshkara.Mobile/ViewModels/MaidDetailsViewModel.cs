using System.Collections.ObjectModel;
using System.Linq;
using Bashkra.ApiClient.Models;
using Bshkara.Mobile.Helpers.Extenstions;
using Bshkara.Mobile.ViewModels.Base;

namespace Bshkara.Mobile.ViewModels
{
    public class MaidDetailsViewModel : BaseHomeViewModel
    {
        public ApiMaid Maid { get; set; }

        public ObservableCollection<ApiMaidDocument> Pictures { get; set; } =
            new ObservableCollection<ApiMaidDocument>();

        public ObservableCollection<ApiMaidDocument> Documents { get; set; } =
            new ObservableCollection<ApiMaidDocument>();

        public ApiMaidDocument SelectedPicture { get; set; }

        public ApiMaidDocument SelectedDocument { get; set; }

        protected override async void NotifyPropertyChanged(string propertyName = null)
        {
            base.NotifyPropertyChanged(propertyName);
            if ((propertyName == nameof(Maid)) && (Maid != null) && Maid.Documents.Any())
            {
                Pictures = Maid.Documents.Where(t => t.DocumentType.ShowAsPicture).ToObservableCollection();
                Documents = Maid.Documents.Where(t => !t.DocumentType.ShowAsPicture).ToObservableCollection();
            }

            if ((propertyName == nameof(SelectedPicture)) && (SelectedPicture != null))
            {
                var views =
                    Pictures.Select(t => new CarouselViewModel
                    {
                        Image = t.File,
                        IsActive = t == SelectedPicture
                    }).ToObservableCollection();

                var selectedView = views.FirstOrDefault(t => t.Image == SelectedPicture.File);

                await Navigation.For<ImageViewerViewModel>()
                    .WithParam(t => t.Views, views)
                    .WithParam(t => t.SelectedItem, selectedView)
                    .NavigateModal();
            }
        }
    }
}