﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeDailyXamarin.ViewModels
{
    public class PlayBackViewModel : INotifyPropertyChanged
    {
        double timeElapsed;

        public event PropertyChangedEventHandler PropertyChanged;

        public double TimeElapsed
        {
            set
            {
                if(timeElapsed != value)
                {
                    timeElapsed = value;
                    OnPropertChanged(timeElapsed.ToString());
                }
            }
            get
            {
                return timeElapsed;
            }
        }
        public double TimeRemaining
        {
            set
            {
                if (timeElapsed != value)
                {
                    timeElapsed = value;
                    OnPropertChanged(timeElapsed.ToString());
                }
            }
            get
            {
                return timeElapsed;
            }
        }
        /// <summary>
        /// Changes to the play back slider properties cause the time label property to change, and changes to label causes the slider to change. 
        /// This might seem like an infinite loop, except that the PropertyChanged event isn’t fired unless the property has actually changed. 
        /// This puts an end to the otherwise uncontrollable feedback loop.
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string GetFormattedTime(int value)
        {
            var span = TimeSpan.FromMilliseconds(value);
            if (span.Hours > 0)
            {
                return string.Format("{0}:{1:00}:{2:00}", (int)span.TotalHours, span.Minutes, span.Seconds);
            }
            else
            {
                return string.Format("{0}:{1:00}", (int)span.Minutes, span.Seconds);
            }
        }
    }
}
