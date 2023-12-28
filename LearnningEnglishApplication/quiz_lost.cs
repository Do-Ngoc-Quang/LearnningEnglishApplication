using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Media;
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
    [Activity(Label = "quiz_lost")]
    public class quiz_lost : Activity
    {
        string id_user = "";

        string planName = "";

        ImageButton img_btn_endquiz;

        Button btn_replay_quiz;

        MediaPlayer audio_player;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.quiz_lost);

            // Ring tone
            play_audio_lose();

            id_user = Intent.GetStringExtra("id_user");

            planName = Intent.GetStringExtra("planName");

            img_btn_endquiz = FindViewById<ImageButton>(Resource.Id.img_btn_endquiz);
            btn_replay_quiz = FindViewById<Button>(Resource.Id.btn_replay_quiz);

            img_btn_endquiz.Click += Img_btn_endquiz_Click;
            btn_replay_quiz.Click += Btn_replay_quiz_Click;
        }



        private void play_audio_lose()
        {
            // Khởi tạo MediaPlayer
            audio_player = MediaPlayer.Create(this, Resource.Raw.lose);
            // Phát âm thanh
            audio_player.Start();
        }

        private void Img_btn_endquiz_Click(object sender, EventArgs e)
        {
            // Đóng Activity hiện tại
            Finish();

            Intent it = new Intent(this, typeof(vocabulary));

            // Check if the activity is already in the task stack
            ComponentName cn = it.ResolveActivity(PackageManager);
            String currentActivity = PackageManager.GetActivityInfo(cn, PackageInfoFlags.Activities).Name;

            if (!currentActivity.Equals(GetType().FullName))
            {
                // If the activity is not the current one, reorder it to the front
                it.AddFlags(ActivityFlags.ReorderToFront);
            }

            it.PutExtra("id_user", id_user);
            it.PutExtra("planName", planName);

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