using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Xml;


namespace LearnningEnglishApplication
{
    [Activity(Label = "vocabulary")]
    public class vocabulary : Activity
    {
        string id_user;

        TextView txt_planName, txt_vocab_en, txt_type, txt_pronounce_eng, txt_pronounce_ame, txt_describe, txt_mean_vn;
        Button btn_playquiz;
        ImageButton img_btn_goback, img_btn_audio_eng, img_btn_audio_ame, img_btn_back1node, img_btn_go1node;

        string planName = "";

        // Danh sách để lưu trữ dữ liệu từ file XML
        List<string> vocab_en = new List<string>();
        List<string> type = new List<string>();
        List<string> audio_eng = new List<string>();
        List<string> audio_ame = new List<string>();
        List<string> pronounce_eng = new List<string>();
        List<string> pronounce_ame = new List<string>();
        List<string> describe = new List<string>();
        List<string> mean_vn = new List<string>();

        int i = 0;

        MediaPlayer audio_player;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.vocabulary);

            img_btn_goback = FindViewById<ImageButton>(Resource.Id.img_btn_goback);
            img_btn_audio_eng = FindViewById<ImageButton>(Resource.Id.img_btn_audio_eng);
            img_btn_audio_ame = FindViewById<ImageButton>(Resource.Id.img_btn_audio_ame);
            img_btn_back1node = FindViewById<ImageButton>(Resource.Id.img_btn_back1node);
            img_btn_go1node = FindViewById<ImageButton>(Resource.Id.img_btn_go1node);

            btn_playquiz = FindViewById<Button>(Resource.Id.btn_playquiz);

            txt_planName = FindViewById<TextView>(Resource.Id.txt_planName);
            txt_vocab_en = FindViewById<TextView>(Resource.Id.txt_vocab_en);
            txt_type = FindViewById<TextView>(Resource.Id.txt_type);
            txt_pronounce_eng = FindViewById<TextView>(Resource.Id.txt_pronounce_eng);
            txt_pronounce_ame = FindViewById<TextView>(Resource.Id.txt_pronounce_ame);
            txt_describe = FindViewById<TextView>(Resource.Id.txt_describe);
            txt_mean_vn = FindViewById<TextView>(Resource.Id.txt_mean_vn);

            id_user = Intent.GetStringExtra("id_user");
            planName = Intent.GetStringExtra("planName");
            txt_planName.Text = planName;
            LoadVocabularyfromXML(planName);

            //Fill phần từ đầu tiên của một plan
            FillData(i);

            btn_playquiz.Click += Btn_playquiz_Click;

            img_btn_goback.Click += Img_btn_goback_Click;

            img_btn_audio_eng.Click += Img_btn_audio_eng_Click;
            img_btn_audio_ame.Click += Img_btn_audio_ame_Click;

            img_btn_back1node.Click += Img_btn_back1node_Click;
            img_btn_go1node.Click += Img_btn_go1node_Click;
        }



        private void Img_btn_goback_Click(object sender, EventArgs e)
        {
            // Đóng Activity hiện tại
            Finish();

            Intent it = new Intent(this, typeof(category));

            // Check if the activity is already in the task stack
            ComponentName cn = it.ResolveActivity(PackageManager);
            String currentActivity = PackageManager.GetActivityInfo(cn, PackageInfoFlags.Activities).Name;

            if (!currentActivity.Equals(GetType().FullName))
            {
                // If the activity is not the current one, reorder it to the front
                it.AddFlags(ActivityFlags.ReorderToFront);
            }

            StartActivity(it);
        }

        private void Btn_playquiz_Click(object sender, EventArgs e)
        {
            // Đóng Activity hiện tại
            Finish();

            Intent it = new Intent(this, typeof(quiz));
            it.PutExtra("id_user", id_user);
            it.PutExtra("planName", planName.ToString());
            StartActivity(it);
        }

        private void LoadVocabularyfromXML(string planName)
        {
            using (XmlReader reader = XmlReader.Create(Assets.Open("vocabulary.xml")))
            {
                string currentCategory = "";

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "plan")
                        {
                            // Nếu đang đọc một danh mục, lấy tên danh mục
                            currentCategory = reader.GetAttribute("name");
                        }
                        else if (reader.Name == "word" && currentCategory == planName)
                        {
                            // Nếu đang ở trong danh mục "General", đọc từ vựng
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Element)
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
                                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "word")
                                {
                                    // Kết thúc đọc một từ, thoát khỏi vòng lặp con
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void FillData(int i)
        {

            txt_vocab_en.Text = vocab_en[i];
            txt_type.Text = type[i];

            txt_pronounce_eng.Text = pronounce_eng[i];
            txt_pronounce_ame.Text = pronounce_ame[i];

            txt_describe.Text = "- " + describe[i];
            txt_mean_vn.Text = mean_vn[i];

        }


        private void Img_btn_audio_eng_Click(object sender, EventArgs e)
        {
            // Lấy tên tài nguyên từ biến audio_eng[i]
            string resourceName = audio_eng[i];

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
        }

        private void Img_btn_audio_ame_Click(object sender, EventArgs e)
        {
            // Lấy tên tài nguyên từ biến audio_eng[i]
            string resourceName = audio_ame[i];

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
        }


        private void Img_btn_back1node_Click(object sender, EventArgs e)
        {
            if (i <= 0)
            {
                i = 0;
            }
            else
            {
                i--;
            }

            FillData(i);
        }
        private void Img_btn_go1node_Click(object sender, EventArgs e)
        {

            if (i >= vocab_en.Count - 1)
            {
                i = vocab_en.Count - 1;
            }
            else
            {
                i++;
            }

            FillData(i);
        }

    }
}