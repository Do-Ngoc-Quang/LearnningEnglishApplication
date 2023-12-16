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
    [Activity(Label = "quiz_lost")]
    public class quiz_lost : Activity
    {
        Button btn_endquiz, btn_replay_quiz;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.quiz_lost);

            btn_endquiz = FindViewById<Button>(Resource.Id.btn_endquiz);
            btn_replay_quiz = FindViewById<Button>(Resource.Id.btn_replay_quiz);

            btn_endquiz.Click += Btn_endquiz_Click;
            btn_replay_quiz.Click += Btn_replay_quiz_Click;
        }

        private void Btn_endquiz_Click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(home));

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

        private void Btn_replay_quiz_Click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(quiz));
            StartActivity(it);
        }
    }
}