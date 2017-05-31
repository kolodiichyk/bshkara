using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Bshkara.Mobile.Controls.State
{
    [ContentProperty("Content")]
    [Preserve(AllMembers = true)]
    public class StateCondition
    {
        public object Is { get; set; }
        public object IsNot { get; set; }
        public View Content { get; set; }


        public AnimationBase Appearing { get; set; }
        public AnimationBase Disappearing { get; set; }
    }
}