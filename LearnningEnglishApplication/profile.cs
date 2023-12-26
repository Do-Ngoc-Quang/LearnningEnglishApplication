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
    [Activity(Label = "profile")]
    public class profile : Activity
    {
        string id_user;

        mySQLite mysqlite;

        ImageButton img_btn_edit_user;

        TextView txt_name_user, txt_point;

        Button btn_home, btn_category, btn_leaderboard, btn_profile;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.profile);

            // id của người dùng
            id_user = Intent.GetStringExtra("id_user");

            mysqlite = new mySQLite(this.ApplicationContext);

            img_btn_edit_user = FindViewById<ImageButton>(Resource.Id.img_btn_edit_user);

            txt_name_user = FindViewById<TextView>(Resource.Id.txt_name_user);
            txt_point = FindViewById<TextView>(Resource.Id.txt_point);

            btn_home = FindViewById<Button>(Resource.Id.btn_home);
            btn_category = FindViewById<Button>(Resource.Id.btn_category);
            btn_leaderboard = FindViewById<Button>(Resource.Id.btn_leaderboard);
            btn_profile = FindViewById<Button>(Resource.Id.btn_profile);

            load_info_user();

            img_btn_edit_user.Click += Img_btn_edit_user_Click;

            btn_home.Click += Btn_home_Click;
            btn_category.Click += Btn_category_Click;
            btn_leaderboard.Click += Btn_leaderboard_Click;
            btn_profile.Click += Btn_profile_Click;
        }

        private void load_info_user()
        {
            // Đọc dữ liệu
            ICursor cur = mysqlite.ReadableDatabase.RawQuery("SELECT * FROM nguoidung WHERE id = '" + id_user + "' LIMIT 1", null);

            // Kiểm tra dữ liệu
            if (cur != null && cur.Count > 0)
            {
                //Di chuyển con trỏ đến dòng đầu tiên
                cur.MoveToFirst();

                string id = cur.GetString(cur.GetColumnIndex("id"));

                // Lấy giá trị
                string name_user = cur.GetString(cur.GetColumnIndex("hoten"));
                int gioitinh_user = int.Parse(cur.GetString(cur.GetColumnIndex("gioitinh")));
                string point_user = cur.GetString(cur.GetColumnIndex("diemso"));

                // ---
                txt_name_user.Text = name_user;
                txt_point.Text = point_user + " points";

            }
            else
            {
                // Thông báo lỗi
                Toast.MakeText(this, "Cannot connect to database!", ToastLength.Short).Show();
            }
        }

        private void Img_btn_edit_user_Click(object sender, EventArgs e)
        {
            ShowModal();
        }

        private void ShowModal()
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            // Inflate giao diện tùy chỉnh từ layout
            View viewInflated = LayoutInflater.Inflate(Resource.Layout.dialog_layout_edit_user, null);

            // Tìm các controls trong layout tùy chỉnh
            EditText txt_name_user = viewInflated.FindViewById<EditText>(Resource.Id.txt_name_user);
            CheckBox cbox_male = viewInflated.FindViewById<CheckBox>(Resource.Id.cbox_male);
            CheckBox cbox_female = viewInflated.FindViewById<CheckBox>(Resource.Id.cbox_female);

            // Thiết lập giao diện của dialog
            builder.SetView(viewInflated)
                   .SetPositiveButton("OK", (sender, args) =>
                   {
                       // Xử lý khi nhấn nút OK
                       string inputText = txt_name_user.Text;
                       bool isMaleChecked = cbox_male.Checked;
                       bool isFemaleChecked = cbox_female.Checked;

                       // Thực hiện xử lý với inputText, isMaleChecked và isFemaleChecked
                   })
                   .SetNegativeButton("Cancel", (sender, args) =>
                   {
                       // Xử lý khi nhấn nút Cancel
                   });

            // Tạo và hiển thị AlertDialog
            AlertDialog alertDialog = builder.Create();
            alertDialog.Show();
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

            it.PutExtra("id_user", id_user);

            StartActivity(it);
        }

        private void Btn_profile_Click(object sender, EventArgs e)
        {
            // Thông báo 
            Toast.MakeText(this, "You are here", ToastLength.Short).Show();
        }
    }
}