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
    [Activity(Label = "quiz_completed")]
    public class quiz_completed : Activity
    {
        mySQLite mysqlite;

        string planName = "";
        string id_user = "";

        TextView txt_socau, txt_diemso;
        Button btn_endquiz, btn_replay_quiz;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.quiz_completed);

            mysqlite = new mySQLite(this.ApplicationContext);

            id_user = Intent.GetStringExtra("id_user");
            planName = Intent.GetStringExtra("planName");

            txt_socau = FindViewById<TextView>(Resource.Id.txt_socau);
            txt_diemso = FindViewById<TextView>(Resource.Id.txt_diemso);

            txt_socau.Text = Intent.GetStringExtra("cauDung") + " / " + Intent.GetStringExtra("tongSocau");
            int diemso = int.Parse(Intent.GetStringExtra("cauDung")) * 5;
            txt_diemso.Text = "+" + diemso.ToString() + " points";

            // --- Thực hiện update điểm số cho người dùng
            update_diemso(diemso);

            btn_endquiz = FindViewById<Button>(Resource.Id.btn_endquiz);
            btn_replay_quiz = FindViewById<Button>(Resource.Id.btn_replay_quiz);

            btn_endquiz.Click += Btn_endquiz_Click;
            btn_replay_quiz.Click += Btn_replay_quiz_Click;
        }

        private void update_diemso(int diemso)
        {
            int diemso_temp = 0;

            // Đọc dữ liệu
            ICursor cur = mysqlite.ReadableDatabase.RawQuery("SELECT diemso FROM nguoidung WHERE id = '"+ id_user +"' LIMIT 1", null);

            // Kiểm tra dữ liệu
            if (cur != null && cur.Count > 0)
            {
                // Di chuyển con trỏ đến dòng đầu tiên
                cur.MoveToFirst();

                // Lấy giá trị từ cột "diemso"
                diemso_temp = int.Parse(cur.GetString(cur.GetColumnIndex("diemso")));
            }
            else
            {
                // Thông báo 
                Toast.MakeText(this, "Lỗi không tìm thấy cơ sở dữ liệu!", ToastLength.Short).Show();
            }

            diemso_temp += diemso;

            mysqlite.ReadableDatabase.ExecSQL("UPDATE nguoidung SET diemso = '" + diemso_temp.ToString() + "' WHERE id = '" + id_user + "';");
        }

        private void Btn_endquiz_Click(object sender, EventArgs e)
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

        private void Btn_replay_quiz_Click(object sender, EventArgs e)
        {
            // Đóng Activity hiện tại
            Finish();

            Intent it = new Intent(this, typeof(quiz));
            it.PutExtra("id_user", id_user);
            it.PutExtra("planName", planName.ToString());
            StartActivity(it);
        }
    }
}