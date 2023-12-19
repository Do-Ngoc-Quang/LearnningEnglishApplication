using Android.App;
using Android.Content;
using Android.Content.PM;
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
        TextView txt_planName, txt_vocab_en, txt_type, txt_pronounce, txt_describe, txt_mean_vn;

        ImageButton img_btn_goback, img_btn_audio, img_btn_back1node, img_btn_go1node;

        // Danh sách để lưu trữ dữ liệu từ file XML
        List<string> vocab_en = new List<string>();
        List<string> type = new List<string>();
        List<string> audio = new List<string>();
        List<string> pronounce = new List<string>();
        List<string> describe = new List<string>();
        List<string> mean_vn = new List<string>();

        int i = 0;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.vocabulary);

            img_btn_goback = FindViewById<ImageButton>(Resource.Id.img_btn_goback);
            img_btn_audio = FindViewById<ImageButton>(Resource.Id.img_btn_audio);
            img_btn_back1node = FindViewById<ImageButton>(Resource.Id.img_btn_back1node);
            img_btn_go1node = FindViewById<ImageButton>(Resource.Id.img_btn_go1node);

            txt_planName = FindViewById<TextView>(Resource.Id.txt_planName);
            txt_vocab_en = FindViewById<TextView>(Resource.Id.txt_vocab_en);
            txt_type = FindViewById<TextView>(Resource.Id.txt_type);
            txt_pronounce = FindViewById<TextView>(Resource.Id.txt_pronounce);
            txt_describe = FindViewById<TextView>(Resource.Id.txt_describe);
            txt_mean_vn = FindViewById<TextView>(Resource.Id.txt_mean_vn);

            txt_planName.Text = Intent.GetStringExtra("planName");

            string planName = Intent.GetStringExtra("planName");
            LoadVocabularyfromXML(planName);

            //Fill phần từ đầu tiên của một plan
            FillData(i);

            img_btn_goback.Click += Img_btn_goback_Click;
            img_btn_audio.Click += Img_btn_audio_Click;
            img_btn_back1node.Click += Img_btn_back1node_Click;
            img_btn_go1node.Click += Img_btn_go1node_Click;
        }

        private void Img_btn_goback_Click(object sender, EventArgs e)
        {
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

        private void Img_btn_audio_Click(object sender, EventArgs e)
        {
            PlayAudio();
        }

        private void PlayAudio()
        {
            try
            {
                string audioUrl = "https://www.oxfordlearnersdictionaries.com/media/english/uk_pron/e/exi/exist/exist__gb_3.mp3";

                // Tạo một đối tượng SoundPlayer
                using (SoundPlayer player = new SoundPlayer(audioUrl))
                {
                    // Chờ cho việc tải âm thanh hoàn tất
                    player.LoadCompleted += (sender, args) =>
                    {
                        // Phát âm thanh
                        player.Play();
                    };

                    // Bắt đầu quá trình tải âm thanh
                    player.LoadAsync();
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (nếu cần)
            }
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
                                    else if (reader.Name == "audio")
                                    {
                                        audio.Add(reader.ReadString());
                                    }
                                    else if (reader.Name == "pronounce")
                                    {
                                        pronounce.Add(reader.ReadString());
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
            
            txt_pronounce.Text = pronounce[i];
            txt_describe.Text = describe[i];
            txt_mean_vn.Text = mean_vn[i];

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