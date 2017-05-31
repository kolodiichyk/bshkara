using Bashkra.ApiClient.Models;
using Xamarin.Forms;

namespace Bshkara.Mobile.Views.HomeView
{
    public partial class MaidViewCell : ViewCell
    {
        public MaidViewCell()
        {
            InitializeComponent();
        }

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			Photo.Source = null;
			var item = BindingContext as ApiMaid;
			if (item != null)
			{
				Photo.Source = item.Photo;
			}
		}
    }
}