using System.Collections.Generic;
using Xamarin.Forms;

namespace Bshkara.Mobile.Controls
{
    public class RatingStars : ContentView
    {
        public static BindableProperty RatingProperty =
            BindableProperty.Create(nameof(Rating), typeof(int), typeof(RatingStars), default(int), BindingMode.OneWay,
                null,
                (bindable, oldValue, newValue) =>
                {
                    var ratingStars = (RatingStars) bindable;
                    ratingStars.UpdateStarsDisplay();
                });

        public RatingStars()
        {
            GenerateDisplay();
        }

        private List<FontAwesomeIcon.FontAwesomeIcon> Icons { get; set; }

        //Rating is out of 10 
        public int Rating
        {
            get { return (int) GetValue(RatingProperty); }
            set { SetValue(RatingProperty, value); }
        }

        private void GenerateDisplay()
        {
            //Create Star Image Placeholders 
            Icons = new List<FontAwesomeIcon.FontAwesomeIcon>();
            for (var i = 0; i < 5; i++)
                Icons.Add(new FontAwesomeIcon.FontAwesomeIcon
                {
                    Icon = "fa-star-o",
                    TextColor = (Color) Application.Current.Resources["Primary"]
                });

            //Create Horizontal Stack containing stars and review count label 
            var starsStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Start,
                Padding = 0,
                Spacing = 5,
                Children =
                {
                    Icons[0],
                    Icons[1],
                    Icons[2],
                    Icons[3],
                    Icons[4]
                }
            };

            UpdateStarsDisplay();

            Content = starsStack;
        }

        //Set the correct images for the stars based on the rating 
        public void UpdateStarsDisplay()
        {
            for (var i = 0; i < Icons.Count; i++)
                Icons[i].Icon = GetStarFileName(i);
        }

        private string GetStarFileName(int position)
        {
            var currentStarMaxRating = position + 1;
            Icons[position].TextColor = (Color) Application.Current.Resources["Primary"];

            //Rating is out of 10 
            if (Rating >= currentStarMaxRating*2)
                return "fa-star";

            // Half star
            if ((Rating >= currentStarMaxRating*2 - 1) && (Rating < currentStarMaxRating*2))
                return "fa-star-half-o";


            Icons[position].TextColor = (Color) Application.Current.Resources["LightGray"];
            return "fa-star";
        }
    }

    public enum EmptyStarType
    {
        White = 0,
        Transparent = 1
    }
}