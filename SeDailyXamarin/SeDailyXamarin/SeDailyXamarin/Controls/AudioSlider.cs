using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SeDailyXamarin.Controls
{
    public class AudioSlider : Slider
    {
        public static readonly BindableProperty HasThumbProperty =
            BindableProperty.Create(nameof(HasThumb), typeof(bool), typeof(HorizontalScrollView), true);

        public bool HasThumb
        {
            get { return (bool)GetValue(HasThumbProperty); }
            set { SetValue(HasThumbProperty, value); }
        }
    }
}
