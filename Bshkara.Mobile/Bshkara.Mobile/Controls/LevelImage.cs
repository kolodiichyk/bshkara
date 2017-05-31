using Bashkra.Shared.Enums;
using FFImageLoading.Forms;
using Xamarin.Forms;

namespace Bshkara.Mobile.Controls
{
    public class LevelImage : CachedImage
    {
        public static readonly BindableProperty LevelProperty = BindableProperty.Create(nameof(Level),
            typeof (Level), typeof (Skills), Level.Poor, BindingMode.TwoWay);

        public LevelImage()
        {
            Aspect = Aspect.AspectFit;
            Source = "level_poor.png";
        }

        public Level Level
        {
            get { return (Level) GetValue(LevelProperty); }
            set { SetValue(LevelProperty, value); }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(Level))
            {
                var source = "";
                switch (Level)
                {
                    case Level.Poor:
                        source = "level_poor.png";
                        break;
                    case Level.Little:
                        source = "level_little.png";
                        break;
                    case Level.Fair:
                        source = "level_fair.png";
                        break;
                    case Level.Fulent:
                        source = "level_fulent.png";
                        break;
                }
                Aspect = Aspect.AspectFit;
                Source = source;
            }
        }
    }
}