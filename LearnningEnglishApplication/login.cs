using Android.App;
using Android.Content;
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
        Button btn_taotaikhoan, btn_dangnhap;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.login);

            btn_taotaikhoan = FindViewById<Button>(Resource.Id.btn_taotaikhoan);
            btn_dangnhap = FindViewById<Button>(Resource.Id.btn_dangnhap);

            btn_taotaikhoan.Click += Btn_taotaikhoan_Click;
            btn_dangnhap.Click += Btn_dangnhap_Click;
        }

        private void Btn_dangnhap_Click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(home));
            StartActivity(it);
        }

        private void Btn_taotaikhoan_Click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(signup)); 
            StartActivity(it);
        }
    }
}