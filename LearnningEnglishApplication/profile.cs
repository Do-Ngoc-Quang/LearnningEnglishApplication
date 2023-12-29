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

        // --
        string name_user;
        int gioitinh_user;

        ImageButton img_btn_home, img_btn_category, img_btn_leaderboard, img_btn_profile, img_btn_logout;

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


            img_btn_logout = FindViewById<ImageButton>(Resource.Id.img_btn_logout);

            img_btn_home = FindViewById<ImageButton>(Resource.Id.img_btn_home);
            img_btn_category = FindViewById<ImageButton>(Resource.Id.img_btn_category);
            img_btn_leaderboard = FindViewById<ImageButton>(Resource.Id.img_btn_leaderboard);
            img_btn_profile = FindViewById<ImageButton>(Resource.Id.img_btn_profile);

            load_info_user();

            img_btn_edit_user.Click += Img_btn_edit_user_Click;

            img_btn_logout.Click += Img_btn_logout_Click;

            img_btn_home.Click += Img_btn_home_Click;
            img_btn_category.Click += Img_btn_category_Click;
            img_btn_leaderboard.Click += Img_btn_leaderboard_Click;
            img_btn_profile.Click += Img_btn_profile_Click;
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
                name_user = cur.GetString(cur.GetColumnIndex("hoten"));
                gioitinh_user = int.Parse(cur.GetString(cur.GetColumnIndex("gioitinh")));
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
            //CheckBox cbox_male = viewInflated.FindViewById<CheckBox>(Resource.Id.cbox_male);
            //CheckBox cbox_female = viewInflated.FindViewById<CheckBox>(Resource.Id.cbox_female);

            // ---
            txt_name_user.Text = name_user;

            //if (gioitinh_user == 0)
            //{
            //    cbox_male.Checked = true;
            //}
            //else
            //{
            //    cbox_female.Checked = true;
            //}

            //if (cbox_male.Checked)
            //{
            //    cbox_female.Checked = false;
            //}

            //if (cbox_female.Checked)
            //{
            //    cbox_male.Checked = false;
            //}

            // Thiết lập giao diện của dialog
            builder.SetView(viewInflated)
                   .SetPositiveButton("OK", (sender, args) =>
                   {
                       int gioitinh = 0; // 0 is male, 1 is female

                       // Xử lý khi nhấn nút OK
                       //bool isMaleChecked = cbox_male.Checked;
                       //bool isFemaleChecked = cbox_female.Checked;

                       //if (isMaleChecked)
                       //{
                       //    gioitinh = 0;
                       //    cbox_female.Checked = false;
                       //}

                       //if (isFemaleChecked)
                       //{
                       //    gioitinh = 1;
                       //    cbox_male.Checked = false;
                       //}

                       // ---
                       //mysqlite.ReadableDatabase.ExecSQL("UPDATE nguoidung SET hoten = '" + txt_name_user.Text + "', gioitinh = '" + gioitinh.ToString() + "'" +
                       //    " WHERE id = '" + id_user + "';");

                       mysqlite.ReadableDatabase.ExecSQL("UPDATE nguoidung SET hoten = '" + txt_name_user.Text + "'" +
                           " WHERE id = '" + id_user + "';");

                       // ---
                       load_info_user();
                   })
                   .SetNegativeButton("Cancel", (sender, args) =>
                   {
                       // Xử lý khi nhấn nút Cancel
                   });

            // Tạo và hiển thị AlertDialog
            AlertDialog alertDialog = builder.Create();
            alertDialog.Show();
        }

        private void Img_btn_logout_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            // Thiết lập nội dung của dialog
            builder.SetMessage("Do you want to log out?");

            // Thiết lập nút "Logout"
            builder.SetPositiveButton("Logout", (sender, args) =>
            {
                // Đóng Activity hiện tại
                Finish();

                Intent it = new Intent(this, typeof(login));

                // Check if the activity is already in the task stack
                ComponentName cn = it.ResolveActivity(PackageManager);
                String currentActivity = PackageManager.GetActivityInfo(cn, PackageInfoFlags.Activities).Name;

                if (!currentActivity.Equals(GetType().FullName))
                {
                    // If the activity is not the current one, reorder it to the front
                    it.AddFlags(ActivityFlags.ReorderToFront);
                }

                out_session_active();

                StartActivity(it);
            });

            // Thiết lập nút "Cancel"
            builder.SetNegativeButton("Cancel", (sender, args) =>
            {
                // Xử lý khi nhấn nút Cancel
            });

            // Tạo và hiển thị AlertDialog
            AlertDialog alertDialog = builder.Create();
            alertDialog.Show();

        }

        private void out_session_active()
        {
            try
            {
                ISharedPreferences sp = Application.Context.GetSharedPreferences("login_session", FileCreationMode.Private);
                ISharedPreferencesEditor spE = sp.Edit();
                spE.PutString("id_user_session", "");
                spE.Commit();

                //Toast.MakeText(this.ApplicationContext, "Logout successfully", ToastLength.Short).Show();
            }
            catch (Exception e)
            {
                Toast.MakeText(this.ApplicationContext, "Error! Cannot logout", ToastLength.Short).Show();

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
            // Đóng Activity hiện tại
            Finish();

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

        private void Img_btn_profile_Click(object sender, EventArgs e)
        {
            // Thông báo 
            Toast.MakeText(this, "You are here", ToastLength.Short).Show();
        }
    }
}