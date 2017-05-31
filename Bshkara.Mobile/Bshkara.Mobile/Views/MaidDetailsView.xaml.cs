using Bashkra.ApiClient.Models;
using XLabs.Forms.Mvvm;

namespace Bshkara.Mobile.Views
{
    public partial class MaidDetailsView : BaseView
    {
        public MaidDetailsView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method that is called when the binding context changes.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var maid = BindingContext as ApiMaid;
            if (maid != null)
            {
                LanguagesListView.HeightRequest = LanguagesListView.RowHeight*maid.Languages.Count;
            }
        }
    }
}