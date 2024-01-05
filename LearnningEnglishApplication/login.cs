using Android.App;
using Android.Content;
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
    [Activity(Label = "login")]
    public class login : Activity
    {
        mySQLite mysqlite;

        EditText txt_tendangnhap, txt_matkhau;

        Button btn_taotaikhoan, btn_dangnhap;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.login);

            mysqlite = new mySQLite(this.ApplicationContext);

            txt_tendangnhap = FindViewById<EditText>(Resource.Id.txt_tendangnhap);
            txt_matkhau = FindViewById<EditText>(Resource.Id.txt_matkhau);

            btn_taotaikhoan = FindViewById<Button>(Resource.Id.btn_taotaikhoan);
            btn_dangnhap = FindViewById<Button>(Resource.Id.btn_dangnhap);

            btn_taotaikhoan.Click += Btn_taotaikhoan_Click;
            btn_dangnhap.Click += Btn_dangnhap_Click;
        }

        private void Btn_dangnhap_Click(object sender, EventArgs e)
        {
            // ---
            //Xác thực thành công
            //Intent it = new Intent(this, typeof(home));
            //// Lấy id người dùng
            //string id_user = "3";
            //it.PutExtra("id_user", id_user);
            //StartActivity(it);
            // ---


            if (txt_tendangnhap.Text == "" || txt_matkhau.Text == "")
            {
                //Thông báo
                Toast.MakeText(this, "Username and password cannot be left blank!", ToastLength.Short).Show();
            }

            // Đọc dữ liệu
            ICursor cur = mysqlite.ReadableDatabase.RawQuery("SELECT * FROM nguoidung WHERE tendangnhap = '" + txt_tendangnhap.Text + "' LIMIT 1", null);

            // Kiểm tra dữ liệu
            if (cur != null && cur.Count > 0)
            {
                // Di chuyển con trỏ đến dòng đầu tiên
                cur.MoveToFirst();

                // Lấy giá trị từ cột "matkhau"
                string matkhau = cur.GetString(cur.GetColumnIndex("matkhau"));

                // Kiểm tra mật khẩu
                if (txt_matkhau.Text == matkhau)
                {
                    // Xác thực thành công
                    Intent it = new Intent(this, typeof(home));

                    // Lấy id người dùng
                    string id_user = cur.GetString(cur.GetColumnIndex("id"));
                    it.PutExtra("id_user", id_user);

                    // -- Lưu phiên đăng nhập cho tài khoản vừa thực hiện đăng nhập
                    save_session_active(id_user);

                    StartActivity(it);
                }
                else
                {
                    // Thông báo mật khẩu không đúng
                    Toast.MakeText(this, "Incorrect password!", ToastLength.Short).Show();
                }
            }
            else
            {
                // Thông báo tên đăng nhập không đúng
                Toast.MakeText(this, "Incorrect username!", ToastLength.Short).Show();
            }
        }

        private void save_session_active(string id_user)
        {
            try
            {
                ISharedPreferences sp = Application.Context.GetSharedPreferences("login_session", FileCreationMode.Private);
                ISharedPreferencesEditor spE = sp.Edit();
                spE.PutString("id_user_session", id_user);
                spE.Commit();

                //Toast.MakeText(this.ApplicationContext, "Login successfully", ToastLength.Short).Show();
            }
            catch (Exception e)
            {
                Toast.MakeText(this.ApplicationContext, "Error! An error occurred", ToastLength.Short).Show();

            }
        }

        private void Btn_taotaikhoan_Click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(signup));
            StartActivity(it);
        }
    }
}