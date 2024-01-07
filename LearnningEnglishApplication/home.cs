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

        private Dictionary<int, int> positionMapping = new Dictionary<int, int>();

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

        bool dictionary_default = true;

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
            load_chaomung();

            LoadVocabularyfromXML();

            // --
            txt_enter_EN.Text = null;

            adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, vocab_en_root);
            lView_EN.Adapter = adapter;

            txt_enter_EN.TextChanged += Txt_enter_EN_TextChanged;

            if (dictionary_default)
            {
                lView_EN.ItemClick += LView_EN_ItemClick;
            }

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
                    vocab_en_root.Add(reader.ReadString());
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
                List<string> filteredList = vocab_en_root.Where(x => x.Contains(e.Text.ToString())).ToList();

                // Cập nhật ánh xạ vị trí
                UpdatePositionMapping(filteredList);

                // Hiển thị kết quả lọc
                DisplayFilteredList(filteredList);
            }
            else
            {
                // Nếu EditText trống, hiển thị toàn bộ danh sách
                DisplayFilteredList(vocab_en_root);
            }
        }

        private void UpdatePositionMapping(List<string> filteredList)
        {
            positionMapping.Clear();

            // key: index in Mapping - value is position in new vocab(filter)

            for (int i = 0; i < filteredList.Count; i++)
            {
                int originalPosition = vocab_en_root.IndexOf(filteredList[i]);
                positionMapping[i] = originalPosition;
            }
        }

        private void DisplayFilteredList(List<string> filteredList)
        {
            // Hiển thị danh sách lọc trong ListView
            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, filteredList);
            lView_EN.Adapter = adapter;

            // Xử lý sự kiện khi một mục trong ListView được chọn
            lView_EN.ItemClick += (s, e) =>
            {
                // Lấy vị trí của mục trong danh sách lọc
                int positionInFilteredList = e.Position;

                // Lấy vị trí tương ứng trong danh sách gốc
                if (positionMapping.TryGetValue(positionInFilteredList, out int positionInRoot))
                {
                    Intent it = new Intent(this, typeof(vocabulary_dictionary));
                    //---
                    it.PutExtra("id_user", id_user);

                    //---
                    it.PutExtra("position", positionInRoot.ToString());

                    StartActivity(it);
                }

                // Tắt sự kiện click ban đầu khi đã có sự kiện click này
                dictionary_default = false;
                    
            };
        }

        private void LView_EN_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent it = new Intent(this, typeof(vocabulary_dictionary));
            //---
            it.PutExtra("id_user", id_user);

            //---
            it.PutExtra("position", e.Position.ToString());

            StartActivity(it);
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
                txt_chaomung.Text = "User information could not be found, please log in again!";
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