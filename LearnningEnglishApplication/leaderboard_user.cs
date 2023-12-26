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
    [Activity(Label = "leaderboard_user")]
    public class leaderboard_user : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //// Thiết lập giao diện cho activity từ layout resource
            //SetContentView(Resource.Layout.leaderboard_user);

            //// Tham chiếu đến ImageView bằng ID
            //ImageView gender_user_avatar = FindViewById<ImageView>(Resource.Id.gender_user_avatar);

            //// Nhận giá trị gioitinh_user từ Intent
            //int gioitinh_user = int.Parse(Intent.GetStringExtra("gioitinh_user"));

            //if (gioitinh_user == 0)
            //{
            //    // Thay đổi hình ảnh bằng mã nguồn (resource ID)
            //    gender_user_avatar.SetImageResource(Resource.Drawable.icon_avatar_male_32);
            //}
            //else
            //{
            //    //gender_user_avatar.SetImageResource(Resource.Drawable.icon_avatar_female_32);
            //}

        }
    }
}