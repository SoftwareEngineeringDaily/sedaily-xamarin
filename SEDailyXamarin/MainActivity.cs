using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android;


namespace SEDailyXamarin
{
    [Activity(Label = "SE Daily", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            List<string> phoneNumbers = new List<string>();
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get out UI Controls from the loaded layout

            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            TextView translatePhoneWord = FindViewById<TextView>(Resource.Id.TranslatePhoneword);
            Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            Button translationHistoryButton = FindViewById<Button>(Resource.Id.TranslationHistoryButton);

            // Add code to translate number
            string translatedNumber = string.Empty;
            translateButton.Click += (sender, e) =>
            {
                // Translate user's alpahnumeric phone number to numeric
                translatedNumber = PhonewordTranslator.ToNumber(phoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(translatedNumber))
                {
                    translatePhoneWord.Text = string.Empty;
                }
                else
                {
                    translatePhoneWord.Text = translatedNumber;
                    phoneNumbers.Add(translatedNumber);
                    translationHistoryButton.Enabled = true;
                }

            };

            translationHistoryButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(TranslationHistoryActivity));
                intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);
                StartActivity(intent);
            };
        }


    }
}

