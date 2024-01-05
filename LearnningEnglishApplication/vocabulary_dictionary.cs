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
using System.Xml;

namespace LearnningEnglishApplication
{
    [Activity(Label = "vocabulary_dictionary")]
    public class vocabulary_dictionary : Activity
    {
        string id_user;

        int position;

        ImageButton img_btn_goback;

        TextView txt_vocab_en, txt_type, txt_pronounce_eng, txt_pronounce_ame, txt_describe, txt_mean_vn;

        ImageButton img_btn_audio_eng, img_btn_audio_ame;

        MediaPlayer audio_player;

        // Danh sách để lưu trữ dữ liệu từ file XML
        List<string> vocab_en = new List<string>();
        List<string> type = new List<string>();
        List<string> audio_eng = new List<string>();
        List<string> audio_ame = new List<string>();
        List<string> pronounce_eng = new List<string>();
        List<string> pronounce_ame = new List<string>();
        List<string> describe = new List<string>();
        List<string> mean_vn = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.vocabulary_dictionary);

            // id của người dùng
            id_user = Intent.GetStringExtra("id_user");

            // position
            position = int.Parse(Intent.GetStringExtra("position"));

            LoadVocabularyfromXML();

            img_btn_goback = FindViewById<ImageButton>(Resource.Id.img_btn_goback);

            txt_vocab_en = FindViewById<TextView>(Resource.Id.txt_vocab_en);
            txt_type = FindViewById<TextView>(Resource.Id.txt_type);

            img_btn_audio_eng = FindViewById<ImageButton>(Resource.Id.img_btn_audio_eng);
            img_btn_audio_ame = FindViewById<ImageButton>(Resource.Id.img_btn_audio_ame);

            txt_pronounce_eng = FindViewById<TextView>(Resource.Id.txt_pronounce_eng);
            txt_pronounce_ame = FindViewById<TextView>(Resource.Id.txt_pronounce_ame);
            txt_describe = FindViewById<TextView>(Resource.Id.txt_describe);
            txt_mean_vn = FindViewById<TextView>(Resource.Id.txt_mean_vn);

            img_btn_goback.Click += Img_btn_goback_Click;

            load_data(position);
        }

        private void LoadVocabularyfromXML()
        {
            XmlReader reader = XmlReader.Create(Assets.Open("vocabulary.xml"));
            while (reader.Read())
            {
                if (reader.Name == "en")
                {
                    vocab_en.Add(reader.ReadString());
                }
                else if (reader.Name == "type")
                {
                    type.Add(reader.ReadString());
                }
                else if (reader.Name == "audio_eng")
                {
                    audio_eng.Add(reader.ReadString());
                }
                else if (reader.Name == "audio_ame")
                {
                    audio_ame.Add(reader.ReadString());
                }
                else if (reader.Name == "pronounce_eng")
                {
                    pronounce_eng.Add(reader.ReadString());
                }
                else if (reader.Name == "pronounce_ame")
                {
                    pronounce_ame.Add(reader.ReadString());
                }
                else if (reader.Name == "describe")
                {
                    describe.Add(reader.ReadString());
                }
                else if (reader.Name == "vn")
                {
                    mean_vn.Add(reader.ReadString());
                }
            }
        }

        private void load_data(int pos)
        {
            // ---
            txt_vocab_en.Text = vocab_en[pos];
            txt_type.Text = type[pos];


            txt_pronounce_eng.Text = pronounce_eng[pos];
            txt_pronounce_ame.Text = pronounce_ame[pos];
            txt_describe.Text = "- " + describe[pos];
            txt_mean_vn.Text = mean_vn[pos];

            img_btn_audio_eng.Click += (s, args) =>
            {
                // Lấy tên tài nguyên từ biến audio_eng[i]
                string resourceName = audio_eng[pos];

                // Xác định ID của tài nguyên
                int resourceId = Resources.GetIdentifier(resourceName, "raw", PackageName);

                if (resourceId != 0)
                {
                    // Khởi tạo MediaPlayer
                    audio_player = MediaPlayer.Create(this, resourceId);

                    // Phát âm thanh
                    audio_player.Start();
                }
                else
                {
                    // Thông báo 
                    Toast.MakeText(this, "No sound found", ToastLength.Short).Show();
                }
            };

            img_btn_audio_ame.Click += (s, args) =>
            {
                // Lấy tên tài nguyên từ biến audio_eng[i]
                string resourceName = audio_ame[pos];

                // Xác định ID của tài nguyên
                int resourceId = Resources.GetIdentifier(resourceName, "raw", PackageName);

                if (resourceId != 0)
                {
                    // Khởi tạo MediaPlayer
                    audio_player = MediaPlayer.Create(this, resourceId);

                    // Phát âm thanh
                    audio_player.Start();
                }
                else
                {
                    // Thông báo 
                    Toast.MakeText(this, "No sound found", ToastLength.Short).Show();
                }
            };
        }

        private void Img_btn_goback_Click(object sender, EventArgs e)
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
    }
}