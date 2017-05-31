using System.Collections.Generic;
using System.Linq;
using Bashkra.ApiClient.Models;
using FFImageLoading.Cache;
using FFImageLoading.Forms;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace Bshkara.Mobile.Controls
{
    public class Skills : StackLayout
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource),
            typeof(IEnumerable<ApiMaidSkill>), typeof(Skills), null, BindingMode.TwoWay);

        private readonly ExtendedLabel _loadingLabel;

        private readonly Dictionary<string, CachedImage> _skillImages = new Dictionary<string, CachedImage>();

        public Skills()
        {
            Orientation = StackOrientation.Horizontal;
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            Spacing = 5;

            _loadingLabel = new ExtendedLabel
            {
                Text = "loading...",
                FontSize = 12,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = (Color) Application.Current.Resources["Accent"]
            };

            Children.Add(_loadingLabel);
        }

        public IEnumerable<ApiMaidSkill> ItemsSource
        {
            get { return (IEnumerable<ApiMaidSkill>) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private void CreateSkills()
        {
            Children.Clear();
            foreach (var item in ItemsSource.OrderBy(t => t.Skill.Name))
            {
                CachedImage image;
                if (!_skillImages.TryGetValue(item.Skill.Name, out image))
                {
                    image = new CachedImage
                    {
                        Source = item.Skill.Icon,
                        DownsampleToViewSize = true,
                        CacheType = CacheType.All,
                        Aspect = Aspect.AspectFit
                    };
                    image.WidthRequest = image.HeightRequest = 20;
                    image.Finish += ImageOnFinish;

                    _skillImages.Add(item.Skill.Name, image);
                }

                image.IsVisible = true;
                if (!Children.Contains(image))
                    Children.Add(image);
            }
        }

        private void ImageOnFinish(object sender, CachedImageEvents.FinishEventArgs finishEventArgs)
        {
            _loadingLabel.IsVisible = false;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if ((propertyName == ItemsSourceProperty.PropertyName) && (ItemsSource != null))
                CreateSkills();
        }
    }
}