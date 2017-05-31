using System;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace Bshkara.Mobile.Views.AgenciesView
{
    public partial class AgenciesView : BaseView
    {
        public AgenciesView()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var masterDetail = Application.Current.MainPage as MasterDetailPage;
            if (masterDetail != null)
                masterDetail.IsPresented = true;
        }
    }
}