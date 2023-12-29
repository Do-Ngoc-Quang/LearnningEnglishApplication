using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Database;
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
    [Activity(Label = "home")]
    public class home : Activity
    {
        string id_user;

        mySQLite mysqlite;

        TextView txt_chaomung;

        EditText txt_enter_EN;
        ListView lView_EN;

        ImageButton img_btn_home, img_btn_category, img_btn_leaderboard, img_btn_profile;

        // Danh sách để lưu trữ dữ liệu từ file XML
        List<string> vocab_en_root = new List<string>(); // root vocab
        List<string> vocab_en = new List<string>();
        List<string> type = new List<string>();
        List<string> audio_eng = new List<string>();
        List<string> audio_ame = new List<string>();
        List<string> pronounce_eng = new List<string>();
        List<string> pronounce_ame = new List<string>();
        List<string> describe = new List<string>();
        List<string> mean_vn = new List<string>();

        ArrayAdapter adapter;

        MediaPlayer audio_player;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.home);

            // id của người dùng
            id_user = Intent.GetStringExtra("id_user");

            mysqlite = new mySQLite(this.ApplicationContext);

            txt_chaomung = FindViewById<TextView>(Resource.Id.txt_chaomung);

            txt_enter_EN = FindViewById<EditText>(Resource.Id.txt_enter_EN);
            lView_EN = FindViewById<ListView>(Resource.Id.lView_EN);

            img_btn_home = FindViewById<ImageButton>(Resource.Id.img_btn_home);
            img_btn_category = FindViewById<ImageButton>(Resource.Id.img_btn_category);
            img_btn_leaderboard = FindViewById<ImageButton>(Resource.Id.img_btn_leaderboard);
            img_btn_profile = FindViewById<ImageButton>(Resource.Id.img_btn_profile);

            //Load 
            //load_chaomung();

            LoadVocabularyfromXML();

            // --
            txt_enter_EN.Text = null;

            adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, vocab_en);
            lView_EN.Adapter = adapter;

            txt_enter_EN.TextChanged += Txt_enter_EN_TextChanged;
            lView_EN.ItemClick += LView_EN_ItemClick;

            img_btn_home.Click += Img_btn_home_Click;
            img_btn_category.Click += Img_btn_category_Click;
            img_btn_leaderboard.Click += Img_btn_leaderboard_Click;
            img_btn_profile.Click += Img_btn_profile_Click;
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

        private void Txt_enter_EN_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            // Kiểm tra xem có dữ liệu trong EditText hay không
            if (!string.IsNullOrEmpty(e.Text.ToString()))
            {
                // Lọc danh sách và gán vào danh sách tạm thời
                List<string> filteredList = vocab_en.Where(x => x.Contains(e.Text.ToString())).ToList();

                // Hiển thị kết quả lọc
                DisplayFilteredList(filteredList);
            }
            else
            {
                // Nếu EditText trống, hiển thị toàn bộ danh sách
                DisplayFilteredList(vocab_en);
            }
        }

        private void DisplayFilteredList(List<string> filteredList)
        {
            // Tạo adapter với danh sách tạm thời
            ArrayAdapter<string> filteredAdapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, filteredList);

            // Gán adapter mới cho ListView
            lView_EN.Adapter = filteredAdapter;
        }

        private void LView_EN_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            // show detail --

            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            // Inflate giao diện tùy chỉnh từ layout
            View viewInflated = LayoutInflater.Inflate(Resource.Layout.vocabulary_dictionary, null);

            // Tìm các controls trong layout tùy chỉnh
            TextView txt_vocab_en = viewInflated.FindViewById<TextView>(Resource.Id.txt_vocab_en);
            TextView txt_type = viewInflated.FindViewById<TextView>(Resource.Id.txt_type);

            ImageButton img_btn_audio_eng = viewInflated.FindViewById<ImageButton>(Resource.Id.img_btn_audio_eng);
            ImageButton img_btn_audio_ame = viewInflated.FindViewById<ImageButton>(Resource.Id.img_btn_audio_ame);

            TextView txt_pronounce_eng = viewInflated.FindViewById<TextView>(Resource.Id.txt_pronounce_eng);
            TextView txt_pronounce_ame = viewInflated.FindViewById<TextView>(Resource.Id.txt_pronounce_ame);
            TextView txt_describe = viewInflated.FindViewById<TextView>(Resource.Id.txt_describe);
            TextView txt_mean_vn = viewInflated.FindViewById<TextView>(Resource.Id.txt_mean_vn);

            // ---
            txt_vocab_en.Text = vocab_en[e.Position];
            txt_type.Text = type[e.Position];


            txt_pronounce_eng.Text = pronounce_eng[e.Position];
            txt_pronounce_ame.Text = pronounce_ame[e.Position];
            txt_describe.Text = "- " + describe[e.Position];
            txt_mean_vn.Text = mean_vn[e.Position];

            img_btn_audio_eng.Click += (s, args) =>
            {
                // Lấy tên tài nguyên từ biến audio_eng[i]
                string resourceName = audio_eng[e.Position];

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
                string resourceName = audio_ame[e.Position];

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


            // Thiết lập giao diện của dialog
            builder.SetView(viewInflated)
                   .SetNegativeButton("Close", (sender, args) =>
                   {
                       // Xử lý khi nhấn nút Close
                   });

            // Tạo và hiển thị AlertDialog
            AlertDialog alertDialog = builder.Create();
            alertDialog.Show();
        }


        private void Btn_playquiz_Click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(quiz));
            StartActivity(it);
        }

        private void load_chaomung()
        {
            // Đọc dữ liệu
            ICursor cur = mysqlite.ReadableDatabase.RawQuery("SELECT * FROM nguoidung WHERE id = '" + id_user.ToString() + "' LIMIT 1", null);
            if (cur != null && cur.Count > 0)
            {
                // Di chuyển con trỏ đến dòng đầu tiên
                cur.MoveToFirst();

                // Lấy giá trị 
                string hoten = cur.GetString(cur.GetColumnIndex("hoten"));

                txt_chaomung.Text = "Welcome back, " + hoten;
            }
            else
            {
                txt_chaomung.Text = "Không tìm thấy thông tin người dùng, hãy đăng nhập lại!";
            }
        }

        private void Img_btn_home_Click(object sender, EventArgs e)
        {
            // Thông báo
            Toast.MakeText(this, "You are here", ToastLength.Short).Show();
        }

        private void Img_btn_category_Click(object sender, EventArgs e)
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

            it.PutExtra("id_user", id_user);

            StartActivity(it);
        }

        private void Img_btn_leaderboard_Click(object sender, EventArgs e)
        {
            // Đóng Activity hiện tại
            Finish();

            Intent it = new Intent(this, typeof(leaderboard));

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

        private void Img_btn_profile_Click(object sender, EventArgs e)
        {
            // Đóng Activity hiện tại
            Finish();

            Intent it = new Intent(this, typeof(profile));

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