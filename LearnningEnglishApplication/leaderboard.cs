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
    [Activity(Label = "leaderboard")]
    public class leaderboard : Activity
    {
        string id_user;

        mySQLite mysqlite;

        ImageButton img_btn_home, img_btn_category, img_btn_leaderboard, img_btn_profile;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.leaderboard);

            id_user = Intent.GetStringExtra("id_user");

            mysqlite = new mySQLite(this.ApplicationContext);

            // ---
            load_user_rank();

            img_btn_home = FindViewById<ImageButton>(Resource.Id.img_btn_home);
            img_btn_category = FindViewById<ImageButton>(Resource.Id.img_btn_category);
            img_btn_leaderboard = FindViewById<ImageButton>(Resource.Id.img_btn_leaderboard);
            img_btn_profile = FindViewById<ImageButton>(Resource.Id.img_btn_profile);

            img_btn_home.Click += Img_btn_home_Click;
            img_btn_category.Click += Img_btn_category_Click;
            img_btn_leaderboard.Click += Img_btn_leaderboard_Click;
            img_btn_profile.Click += Img_btn_profile_Click;
        }

        private void load_user_rank()
        {
            // Đọc dữ liệu
            ICursor cur = mysqlite.ReadableDatabase.RawQuery("SELECT * FROM nguoidung ORDER BY diemso DESC LIMIT 10", null); // Top 10

            // Kiểm tra dữ liệu
            if (cur != null && cur.Count > 0)
            {
                int index = 1;

                // Vòng lặp đọc từng trường dữ liệu
                while (cur.MoveToNext())
                {
                    string id = cur.GetString(cur.GetColumnIndex("id"));

                    // Lấy giá trị
                    string name_user = cur.GetString(cur.GetColumnIndex("hoten"));
                    int gioitinh_user = int.Parse(cur.GetString(cur.GetColumnIndex("gioitinh")));
                    string point_user = cur.GetString(cur.GetColumnIndex("diemso"));

                    // ---
                    // Khởi tạo LayoutInflater
                    LayoutInflater inflater = (LayoutInflater)GetSystemService(Context.LayoutInflaterService);

                    // Tạo một instance của LinearLayout từ layout xml
                    LinearLayout newUserLayout = (LinearLayout)inflater.Inflate(Resource.Layout.leaderboard_user, null);

                    // Thêm nó vào LinearLayout chứa tất cả các layout
                    LinearLayout containerLayout = FindViewById<LinearLayout>(Resource.Id.linearLayout_container);

                    // Truyền tham số vào TextView hoặc các thành phần khác trong layout
                    TextView txt_rank = newUserLayout.FindViewById<TextView>(Resource.Id.txt_rank);
                    TextView txt_name_user = newUserLayout.FindViewById<TextView>(Resource.Id.txt_name_user);
                    TextView txt_point = newUserLayout.FindViewById<TextView>(Resource.Id.txt_point);
                    txt_rank.Text = "#" + index.ToString();

                    if (id == id_user)
                    {
                        txt_name_user.Text = name_user.ToString() + " (you)";
                    }
                    else
                    {
                        txt_name_user.Text = name_user.ToString();
                    }

                    txt_point.Text = point_user.ToString() + " points";

                    // --- 
                    containerLayout.AddView(newUserLayout);

                    index++;
                }
            }
            else
            {
                // Thông báo lỗi
                Toast.MakeText(this, "Cannot connect to database!", ToastLength.Short).Show();
            }
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
            // Đóng Activity hiện tại
            Finish();

            Intent it = new Intent(this, typeof(category));

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

        private void Img_btn_leaderboard_Click(object sender, EventArgs e)
        {
            // Thông báo 
            Toast.MakeText(this, "You are here", ToastLength.Short).Show();
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