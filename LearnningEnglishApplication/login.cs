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
            //Xác thực thành công
            Intent it = new Intent(this, typeof(home));
            // Lấy id người dùng
            string id = "1";
            it.PutExtra("id", id);
            StartActivity(it);


            //// Đọc dữ liệu
            //ICursor cur = mysqlite.ReadableDatabase.RawQuery("SELECT * FROM nguoidung WHERE tendangnhap = '" + txt_tendangnhap.Text + "' LIMIT 1", null);

            //// Kiểm tra dữ liệu
            //if (cur != null && cur.Count > 0)
            //{
            //    // Di chuyển con trỏ đến dòng đầu tiên
            //    cur.MoveToFirst();

            //    // Lấy giá trị từ cột "matkhau"
            //    string matkhau = cur.GetString(cur.GetColumnIndex("matkhau"));

            //    // Kiểm tra mật khẩu
            //    if (txt_matkhau.Text == matkhau)
            //    {
            //        // Xác thực thành công
            //        Intent it = new Intent(this, typeof(home));

            //        // Lấy id người dùng
            //        string id = cur.GetString(cur.GetColumnIndex("id"));
            //        it.PutExtra("id", id);

            //        StartActivity(it);
            //    }
            //    else
            //    {
            //        // Thông báo mật khẩu không đúng
            //        Toast.MakeText(this, "Mật khẩu không chính xác!", ToastLength.Short).Show();
            //    }
            //}
            //else
            //{
            //    // Thông báo tên đăng nhập không đúng
            //    Toast.MakeText(this, "Tên đăng nhập không chính xác!", ToastLength.Short).Show();
            //}  
        }

        private void Btn_taotaikhoan_Click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(signup)); 
            StartActivity(it);
        }
    }
}

//// Đọc dữ liệu và hiển thị lên TextView:
//ICursor cur;
//cur = mysqlite.ReadableDatabase.RawQuery("SELECT * FROM SINHVIEN", null);
//while (cur.MoveToNext())
//{
//    textView.Text += cur.GetString(1) + "\n";
//}