﻿using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using Android.Content;
using Android.Database;

namespace LearnningEnglishApplication
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button btn_start;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //Create db SQLite
            mySQLite mysqlite = new mySQLite(this.ApplicationContext);

            check_sesion_active();

            btn_start = FindViewById<Button>(Resource.Id.btn_start);

            btn_start.Click += Btn_start_Click;
        }

        private void check_sesion_active()
        {
            string id_user_session;

            ISharedPreferences sp = Application.Context.GetSharedPreferences("login_session", FileCreationMode.Private);

            id_user_session = sp.GetString("id_user_session", "");

            if (id_user_session != "")
            {
                //Xác thực thành công
                Intent it = new Intent(this, typeof(home));

                // Truy cập vào application nếu chưa logout
                it.PutExtra("id_user", id_user_session);
                StartActivity(it);
            }
        }

        private void Btn_start_Click(object sender, System.EventArgs e)
        {
            Intent it = new Intent(this, typeof(login)); // Khởi tạo Intent
            StartActivity(it); // Mở Activity mới
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}