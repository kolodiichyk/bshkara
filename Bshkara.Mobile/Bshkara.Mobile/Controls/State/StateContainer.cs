using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Bshkara.Mobile.Controls.State
{
    [ContentProperty("Conditions")]
    [Preserve(AllMembers = true)]
    public class StateContainer : ContentView
    {
        public static readonly BindableProperty StateProperty =
            BindableProperty.Create<StateContainer, object>(x => x.State, null, propertyChanged: StateChanged);

        private StateCondition _current;

        public EventHandler<object> OnActivate;
        public EventHandler<object> OnDeActivate;

        public List<StateCondition> Conditions { get; set; } = new List<StateCondition>();

        public object State
        {
            get { return GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        public static void Init()
        {
            //for linker
        }

        private static void StateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null) return;

            var parent = bindable as StateContainer;
            parent?.ChooseStateProperty(newValue);
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);
            ChooseStateProperty(State);
        }

        private void ChooseStateProperty(object newValue)
        {
            foreach (var stateCondition in Conditions)
                if (stateCondition.Is != null)
                {
                    var splitIs = stateCondition.Is.ToString().Split(',');
                    foreach (var conditionStr in splitIs)
                        if (conditionStr.Equals(newValue.ToString()))
                        {
                            if (Content != null)
                                stateCondition.Disappearing?.Apply(Content);
                            if (_current != null)
                                OnDeActivate?.Invoke(this, _current);
                            Content = stateCondition.Content;
                            stateCondition.Appearing?.Apply(Content);
                            OnActivate?.Invoke(this, newValue);
                            _current = stateCondition;
                        }
                }
                else if (stateCondition.IsNot != null)
                {
                    if (!stateCondition.IsNot.ToString().Equals(newValue.ToString()))
                        Content = stateCondition.Content;
                }
        }
    }

    public abstract class AnimationBase
    {
        public abstract void Apply(View view);
    }

    public class FadeOutAnimation : AnimationBase
    {
        public override void Apply(View view)
        {
            Device.BeginInvokeOnMainThread(() => { view.FadeTo(0); });
        }
    }

    public class FadeInAnimation : AnimationBase
    {
        public override void Apply(View view)
        {
            Device.BeginInvokeOnMainThread(() => { view.FadeTo(1); });
        }
    }
}