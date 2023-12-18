using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearnningEnglishApplication
{
    [Activity(Label = "vocabulary")]
    public class vocabulary : Activity
    {
        TextView txt_planName;

        Button btn_goback;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.vocabulary);

            btn_goback = FindViewById<Button>(Resource.Id.btn_goback);

            txt_planName = FindViewById<TextView>(Resource.Id.txt_planName);

            txt_planName.Text = Intent.GetStringExtra("planName");

            btn_goback.Click += Btn_goback_Click;
        }

        private void Btn_goback_Click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(category));

            // Check if the activity is already in the task stack
            ComponentName cn = it.ResolveActivity(PackageManager);
            String currentActivity = PackageManager.GetActivityInfo(cn, PackageInfoFlags.Activities).Name;

            if (!currentActivity.Equals(GetType().FullName))
            {
                // If the activity is not the current one, reorder it to the front
                it.AddFlags(ActivityFlags.ReorderToFront);
            }

            StartActivity(it);
        }
    }
}