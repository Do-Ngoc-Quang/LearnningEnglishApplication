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
        ArrayAdapter adapter;
        List<string> planNames = new List<string>();
        ListView listPlan;

        Button btn_home, btn_category, btn_leaderboard, btn_profile;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.category);

            listPlan = FindViewById<ListView>(Resource.Id.listPlan);

            btn_home = FindViewById<Button>(Resource.Id.btn_home);
            btn_category = FindViewById<Button>(Resource.Id.btn_category);
            btn_leaderboard = FindViewById<Button>(Resource.Id.btn_leaderboard);
            btn_profile = FindViewById<Button>(Resource.Id.btn_profile);

            listPlan.ItemClick += ListPlan_ItemClick;

            btn_home.Click += Btn_home_Click;
            btn_category.Click += Btn_category_Click;
            btn_leaderboard.Click += Btn_leaderboard_Click;
            btn_profile.Click += Btn_profile_Click;

            LoadXML();
            ShowPlan();
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
            Intent it = new Intent(this, typeof(vocabulary));
            it.PutExtra("planName", planNames[e.Position]);
            StartActivity(it);
        }

        private void Btn_home_Click(object sender, EventArgs e)
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

        private void Btn_category_Click(object sender, EventArgs e)
        {
            // Thông báo tên đăng nhập không đúng
            Toast.MakeText(this, "Bạn đang ở trang này", ToastLength.Short).Show();
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
    }
}