using Android.App;
using Android.Content;
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
    [Activity(Label = "home")]
    public class home : Activity
    {
        Button btn_home, btn_category, btn_leaderboard, btn_profile;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.home);

            btn_home = FindViewById<Button>(Resource.Id.btn_home);
            btn_category = FindViewById<Button>(Resource.Id.btn_category);
            btn_leaderboard = FindViewById<Button>(Resource.Id.btn_leaderboard);
            btn_profile = FindViewById<Button>(Resource.Id.btn_profile);

            btn_home.Click += Btn_home_Click;
            btn_category.Click += Btn_category_Click;
            btn_leaderboard.Click += Btn_leaderboard_Click;
            btn_profile.Click += Btn_profile_Click;
        }

        private void Btn_profile_Click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(profile));
            StartActivity(it);
        }

        private void Btn_leaderboard_Click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(leaderboard));
            StartActivity(it);
        }

        private void Btn_category_Click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(category));
            StartActivity(it);
        }

        private void Btn_home_Click(object sender, EventArgs e)
        {
            
        }
    }
}