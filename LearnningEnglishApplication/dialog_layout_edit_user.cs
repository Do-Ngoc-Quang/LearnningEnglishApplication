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
using Android.Widget;

namespace LearnningEnglishApplication
{
    [Activity(Label = "dialog_layout_edit_user")]
    public class dialog_layout_edit_user : Activity
    {
        EditText txt_name_user;

        CheckBox cbox_male, cbox_female;

        int gioitinh;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.dialog_layout_edit_user);

            txt_name_user = FindViewById<EditText>(Resource.Id.txt_name_user);

            //cbox_male = FindViewById<CheckBox>(Resource.Id.cbox_male);
            //cbox_female = FindViewById<CheckBox>(Resource.Id.cbox_female);

            //cbox_male.Click += Cbox_male_Click;
            //cbox_female.Click += Cbox_female_Click;
        }

        //private void Cbox_male_Click(object sender, EventArgs e)
        //{
        //    bool isChecked = cbox_male.Checked;
        //    if (isChecked)
        //    {
        //        gioitinh = 0; // 0 là nam
        //        cbox_female.Checked = false;
        //    }
        //}

        //private void Cbox_female_Click(object sender, EventArgs e)
        //{
        //    bool isChecked = cbox_female.Checked;
        //    if (isChecked)
        //    {
        //        gioitinh = 1; // 1 là nữ
        //        cbox_male.Checked = false;
        //    }
        //}

    }
}