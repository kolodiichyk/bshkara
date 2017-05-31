using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Behaviors;
using XLabs.Forms.Controls;

namespace Bshkara.Mobile.ViewModels
{
    public class BaseHomeViewModel : BaseViewModel
    {
        public BaseHomeViewModel()
        {
            MaidGesture = new RelayGesture(OnMaidGesture);
            ShowMaidHint = IsShowActions = false;
        }

        public ICommand ShowPreviousViewCommand => new Command(ShowPreviousView);

        private void ShowPreviousView()
        {
            IsShowActions = false;
        }

        public bool ShowMaidHint { get; set; }

        public RelayGesture MaidGesture { get; set; }

        public bool IsShowActions { get; set; }

        private void OnMaidGesture(GestureResult gr, object obj)
        {
            switch (gr.GestureType)
            {
                case GestureType.SingleTap:
                    SingleTap();
                    break;
                case GestureType.LongPress:
                    LongPress();
                    break;
            }
        }

        private void SingleTap()
        {
            ShowMaidHint = true;
            Task.Run(async () =>
            {
                await Task.Delay(2000);
                Device.BeginInvokeOnMainThread(() => ShowMaidHint = false);
            });
        }

        private void LongPress()
        {
            IsShowActions = true;
        }
    }
}