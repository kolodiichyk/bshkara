using Bshkara.Mobile.Services.PlatformService;
using Xamarin.Forms;
using XLabs.Ioc;

namespace Bshkara.Mobile.Views.HomeView
{
    public partial class MaidsGridView : ContentView
    {
        public MaidsGridView()
        {
            InitializeComponent();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            var parentView = Parent as View;
            if (parentView != null)
            {
                MaidsGrdView.ItemHeight =
                    MaidsGrdView.ItemWidth =
                        Resolver.Resolve<IPlatformService>()
                            .ConvertDpToPixels(parentView.Width/2 - MaidsGrdView.ColumnSpacing);
            }
        }
    }
}