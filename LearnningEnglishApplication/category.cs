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
using System.Xml;

namespace LearnningEnglishApplication
{
    [Activity(Label = "category")]
    public class category : Activity
    {
        string id_user;

        ArrayAdapter adapter;
        List<string> planNames = new List<string>();
        ListView listPlan;

        ImageButton img_btn_home, img_btn_category, img_btn_leaderboard, img_btn_profile;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.category);

            id_user = Intent.GetStringExtra("id_user");

            listPlan = FindViewById<ListView>(Resource.Id.listPlan);

            img_btn_home = FindViewById<ImageButton>(Resource.Id.img_btn_home);
            img_btn_category = FindViewById<ImageButton>(Resource.Id.img_btn_category);
            img_btn_leaderboard = FindViewById<ImageButton>(Resource.Id.img_btn_leaderboard);
            img_btn_profile = FindViewById<ImageButton>(Resource.Id.img_btn_profile);

            listPlan.ItemClick += ListPlan_ItemClick;

            LoadXML();
            ShowPlan();

            img_btn_home.Click += Img_btn_home_Click;
            img_btn_category.Click += Img_btn_category_Click;
            img_btn_leaderboard.Click += Img_btn_leaderboard_Click;
            img_btn_profile.Click += Img_btn_profile_Click;
        }


        private void LoadXML()
        {
            XmlReader reader = XmlReader.Create(Assets.Open("vocabulary.xml"));
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "plan")
                {
                    // Nếu đang đọc một plan, lấy tên plan và thêm vào danh sách planNames
                    string planName = reader.GetAttribute("name");
                    planNames.Add(planName);
                }
            }
        }

        private void ShowPlan()
        {
            // Hiển thị danh sách planNames lên ListView
            adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, planNames);
            listPlan.Adapter = adapter;
        }

        private void ListPlan_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            // Đóng Activity hiện tại
            Finish();

            Intent it = new Intent(this, typeof(vocabulary));
            it.PutExtra("id_user", id_user);
            it.PutExtra("planName", planNames[e.Position]);
            StartActivity(it);
        }

        private void Img_btn_home_Click(object sender, EventArgs e)
        {
            // Đóng Activity hiện tại
            Finish();

            Intent it = new Intent(this, typeof(home));

            // Check if the activity is already in the task stack
            ComponentName cn = it.ResolveActivity(PackageManager);
            String currentActivity = PackageManager.GetActivityInfo(cn, PackageInfoFlags.Activities).Name;

            if (!currentActivity.Equals(GetType().FullName))
            {
                // If the activity is not the current one, reorder it to the front
                it.AddFlags(ActivityFlags.ReorderToFront);
            }

            it.PutExtra("id_user", id_user);

            StartActivity(it);
        }

        private void Img_btn_category_Click(object sender, EventArgs e)
        {
            // Thông báo
            Toast.MakeText(this, "You are here", ToastLength.Short).Show();
        }

        private void Img_btn_leaderboard_Click(object sender, EventArgs e)
        {
            // Đóng Activity hiện tại
            Finish();

            Intent it = new Intent(this, typeof(leaderboard));

            it.PutExtra("id_user", id_user);

            StartActivity(it);
        }

        private void Img_btn_profile_Click(object sender, EventArgs e)
        {
            // Đóng Activity hiện tại
            Finish();

            Intent it = new Intent(this, typeof(profile));

            // Check if the activity is already in the task stack
            ComponentName cn = it.ResolveActivity(PackageManager);
            String currentActivity = PackageManager.GetActivityInfo(cn, PackageInfoFlags.Activities).Name;

            if (!currentActivity.Equals(GetType().FullName))
            {
                // If the activity is not the current one, reorder it to the front
                it.AddFlags(ActivityFlags.ReorderToFront);
            }

            it.PutExtra("id_user", id_user);

            StartActivity(it);
        }
    }
}