using System;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace Bshkara.Mobile.Views.HomeView
{
    public partial class HomeView : BaseView
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var masterDetail = Application.Current.MainPage as MasterDetailPage;
            if (masterDetail != null)
            {
                masterDetail.IsPresented = true;
            }
        }
    }
}