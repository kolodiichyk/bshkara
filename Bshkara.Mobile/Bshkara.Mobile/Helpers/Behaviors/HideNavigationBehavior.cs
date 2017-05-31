using Xamarin.Forms;

namespace Bshkara.Mobile.Helpers.Behaviors
{
    public class HideNavigationBehavior : Behavior<Page>
    {
        protected override void OnAttachedTo(Page bindable)
        {
            base.OnAttachedTo(bindable);

            NavigationPage.SetHasNavigationBar(bindable, false);
        }
    }
}