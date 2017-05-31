using XLabs.Forms.Mvvm;

namespace Bshkara.Mobile.Views.ImageViewerView
{
    public partial class ImageViewerView : BaseView
    {
        public ImageViewerView()
        {
            InitializeComponent();
        }

        /*
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            var imageViewerViewModel = BindingContext as ImageViewerViewModel;

            if (imageViewerViewModel != null)
                foreach (var views in imageViewerViewModel.Views)
                {
                    CarouselSwipeLayout.Children.Add(new ImageView {BindingContext = views});
                    if (views.IsActive)
                        imageViewerViewModel.SelectedItem = views;
                }
        }*/
    }
}