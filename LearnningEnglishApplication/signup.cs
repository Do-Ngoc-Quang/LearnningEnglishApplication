using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
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
    [Activity(Label = "signup")]
    public class signup : Activity
    {
        mySQLite mysqlite;

        EditText txt_hoten, txt_tendangnhap, txt_matkhau;
        CheckBox cbox_male, cbox_female;
        Button btn_taotaikhoan;

        int gioitinh = 0; // 0 là nam, 1 là nữ

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.signup);

            //Create db SQLite
            mysqlite = new mySQLite(this.ApplicationContext);
            // Tạo table người dùng:
            string sql = "CREATE TABLE IF NOT EXISTS nguoidung " +
                "(id INTEGER PRIMARY KEY AUTOINCREMENT, hoten Text, gioitinh bit, tendangnhap text, matkhau text, diemso integer)";
            //string sql = "DROP TABLE nguoidung;";
            mysqlite.ReadableDatabase.ExecSQL(sql);


            txt_hoten = FindViewById<EditText>(Resource.Id.txt_hoten);
            txt_tendangnhap = FindViewById<EditText>(Resource.Id.txt_tendangnhap);
            txt_matkhau = FindViewById<EditText>(Resource.Id.txt_matkhau);

            cbox_male = FindViewById<CheckBox>(Resource.Id.cbox_male);
            cbox_female = FindViewById<CheckBox>(Resource.Id.cbox_female);

            btn_taotaikhoan = FindViewById<Button>(Resource.Id.btn_taotaikhoan);

            cbox_male.Click += Cbox_male_Click;
            cbox_female.Click += Cbox_female_Click;

            btn_taotaikhoan.Click += Btn_taotaikhoan_Click;
        }

        private void Cbox_male_Click(object sender, EventArgs e)
        {
            bool isChecked = cbox_male.Checked;
            if (isChecked)
            {
                gioitinh = 0; // 0 là nam
                cbox_female.Checked = false;
            }
        }

        private void Cbox_female_Click(object sender, EventArgs e)
        {
            bool isChecked = cbox_female.Checked;
            if (isChecked)
            {
                gioitinh = 1; // 1 là nữ
                cbox_male.Checked = false;
            }
        }

        private void Btn_taotaikhoan_Click(object sender, EventArgs e)
        {
            ICursor cur = mysqlite.ReadableDatabase.RawQuery("SELECT id FROM nguoidung WHERE tendangnhap = '" + txt_tendangnhap.Text + "' LIMIT 1", null);

            if (cur != null && cur.MoveToFirst())
            {
                // Thông báo tên đăng nhập đã tồn tại
                Toast.MakeText(this, "The username already exists, please try again!", ToastLength.Short).Show();
            }
            else
            {
                //Chưa tồn tại - tạo mới tài khoản
                try
                {
                    // Thực thi câu lệnh SQL INSERT
                    mysqlite.ReadableDatabase.ExecSQL("INSERT INTO nguoidung(hoten, gioitinh, tendangnhap, matkhau, diemso) " +
                        "VALUES('" + txt_hoten.Text + "', '" + gioitinh.ToString() + "', '" + txt_tendangnhap.Text + "', '" + txt_matkhau.Text + "', '0')");

                    // Nếu không có ngoại lệ, tức là thực thi thành công, thông báo tạo tài khoản thành công
                    Toast.MakeText(this, "Account successfully created!", ToastLength.Short).Show();
                }
                catch (SQLiteException ex)
                {
                    // Nếu có ngoại lệ, thông báo tạo tài khoản không thành công - hiển thị lỗi
                    Toast.MakeText(this, "Error creating account!" + ex.Message, ToastLength.Short).Show();
                }

            }
        }
    }
}