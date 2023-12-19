using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Database;
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
        string id;

        mySQLite mysqlite;

        TextView txt_chaomung;
        Button btn_playquiz, btn_home, btn_category, btn_leaderboard, btn_profile;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.home);

            // id của người dùng
            id = Intent.GetStringExtra("id");

            mysqlite = new mySQLite(this.ApplicationContext);

            txt_chaomung = FindViewById<TextView>(Resource.Id.txt_chaomung);

            btn_playquiz = FindViewById<Button>(Resource.Id.btn_playquiz);

            btn_home = FindViewById<Button>(Resource.Id.btn_home);
            btn_category = FindViewById<Button>(Resource.Id.btn_category);
            btn_leaderboard = FindViewById<Button>(Resource.Id.btn_leaderboard);
            btn_profile = FindViewById<Button>(Resource.Id.btn_profile);

            //Load 
            //load_chaomung();

            btn_playquiz.Click += Btn_playquiz_Click;

            btn_home.Click += Btn_home_Click;
            btn_category.Click += Btn_category_Click;
            btn_leaderboard.Click += Btn_leaderboard_Click;
            btn_profile.Click += Btn_profile_Click;
        }

        private void Btn_playquiz_Click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(quiz));
            StartActivity(it);
        }

        private void load_chaomung()
        {
            // Đọc dữ liệu
            ICursor cur = mysqlite.ReadableDatabase.RawQuery("SELECT * FROM nguoidung WHERE id = '" + id.ToString() + "' LIMIT 1", null);
            if (cur != null && cur.Count > 0)
            {
                // Di chuyển con trỏ đến dòng đầu tiên
                cur.MoveToFirst();

                // Lấy giá trị 
                string hoten = cur.GetString(cur.GetColumnIndex("hoten"));

                txt_chaomung.Text = "Welcome back, " + hoten;
            }
            else
            {
                txt_chaomung.Text = "Không tìm thấy thông tin người dùng, hãy đăng nhập lại!";
            }
        }

        private void Btn_profile_Click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(profile));

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

        private void Btn_leaderboard_Click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(leaderboard));

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

        private void Btn_category_Click(object sender, EventArgs e)
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

        //Kiểm thử
        private void Btn_home_Click(object sender, EventArgs e)
        {
            
        }
    }
}