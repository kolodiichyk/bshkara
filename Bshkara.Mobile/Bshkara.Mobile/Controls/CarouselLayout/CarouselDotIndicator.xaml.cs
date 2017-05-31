using Xamarin.Forms;

namespace Bshkara.Mobile.Controls.CarouselLayout
{
    public partial class CarouselDotIndicator : ContentView
    {
        public CarouselDotIndicator()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
                HookBindingContext();
        }

        private void HookBindingContext()
        {
            var model = BindingContext as IHeaderModel;
            if (model != null)
            {
                var prop = nameof(IHeaderModel.IsActive);
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == prop)
                        SetLabelColor(model.IsActive);
                };
                SetLabelColor(model.IsActive);
            }
        }

        private void SetLabelColor(bool isActive)
        {
            DotImage.ScaleTo(isActive ? 2.3 : 1);
        }
    }
}