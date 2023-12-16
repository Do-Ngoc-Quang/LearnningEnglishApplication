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
using System.Xml;

namespace LearnningEnglishApplication
{
    [Activity(Label = "quiz")]
    public class quiz : Activity
    {
        XmlReader reader;

        // Danh sách để lưu trữ dữ liệu từ file XML
        List<string> mydataEn = new List<string>();
        List<string> mydataVn = new List<string>();

        Random random = new Random();

        int tongSocau = 0;
        int cauDung = 0;

        // Biến lính canh có vị trí button của đáp án đúng
        int btn_index_true = 0;

        TextView txt_EN, txt_socau;
        Button btn_dapan1, btn_dapan2, btn_dapan3;

        private Handler handler = new Handler();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.quiz);

            txt_EN = FindViewById<TextView>(Resource.Id.txt_EN);
            txt_socau = FindViewById<TextView>(Resource.Id.txt_socau);

            btn_dapan1 = FindViewById<Button>(Resource.Id.btn_dapan1);
            btn_dapan2 = FindViewById<Button>(Resource.Id.btn_dapan2);
            btn_dapan3 = FindViewById<Button>(Resource.Id.btn_dapan3);

            btn_dapan1.Click += Btn_dapan1_Click;
            btn_dapan2.Click += Btn_dapan2_Click;
            btn_dapan3.Click += Btn_dapan3_Click;

            LoadXML();

            ShowRandomItem();
        }

        private void Btn_dapan1_Click(object sender, EventArgs e)
        {
            if (btn_index_true == 1)
            {
                btn_dapan1.Text = "Sure anwser!";
                btn_dapan1.SetBackgroundColor(Android.Graphics.Color.LightGreen);

                cauDung++;
            }
            else
            {
                switch (btn_index_true)
                {
                    case 2:
                        //Wrong answer!
                        btn_dapan1.SetBackgroundColor(Android.Graphics.Color.PaleVioletRed);

                        //True answer!
                        btn_dapan2.SetBackgroundColor(Android.Graphics.Color.LightGreen);

                        break;

                    case 3:
                        //Wrong answer!
                        btn_dapan1.SetBackgroundColor(Android.Graphics.Color.PaleVioletRed);

                        //True answer!
                        btn_dapan3.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                        break;
                    default:
                        break;
                }
            }

            // Sử dụng Handler để tạo độ trễ
            handler.PostDelayed(() => {
                ShowRandomItem();
            }, 2000);

        }

        private void Btn_dapan2_Click(object sender, EventArgs e)
        {
            
        }

        private void Btn_dapan3_Click(object sender, EventArgs e)
        {
            
        }

        private void LoadXML()
        {
            reader = XmlReader.Create(Assets.Open("vocabulary.xml"));
            while (reader.Read())
            {
                if (reader.Name.ToString() == "en")
                    mydataEn.Add(reader.ReadString());
                if (reader.Name.ToString() == "vn")
                    mydataVn.Add(reader.ReadString());
            }
        }

        private void ShowRandomItem()
        {
            btn_dapan1.SetBackgroundColor(Android.Graphics.Color.WhiteSmoke);
            btn_dapan2.SetBackgroundColor(Android.Graphics.Color.WhiteSmoke);
            btn_dapan3.SetBackgroundColor(Android.Graphics.Color.WhiteSmoke);

            tongSocau++;

            txt_socau.Text = tongSocau.ToString();

            // Chọn ngẫu nhiên một phần tử từ danh sách mydataEn ------ Tiếng anh
            int randomIndex_en = random.Next(0, mydataEn.Count);
            string randomItem_en = mydataEn[randomIndex_en];

            // Gán giá trị của phần tử ngẫu nhiên vào EditText
            txt_EN.Text = randomItem_en;

            // Chọn ngẫu nhiên một phần tử từ danh sách mydataEn ------ Tiếng việt
            string rightAnswer = mydataVn[randomIndex_en];

            // Gán giá trị của phần tử ngẫu nhiên
            int randomIndex_vn = random.Next(1, 4);
            switch (randomIndex_vn)
            {
                case 1:
                    //Câu trả lời đúng
                    btn_dapan1.Text = rightAnswer;
                    btn_index_true = randomIndex_vn;

                    //Các lựa chọn còn lại sẽ sai
                    btn_dapan2.Text = (mydataVn[random.Next(0, mydataVn.Count)]).ToString();
                    btn_dapan3.Text = (mydataVn[random.Next(0, mydataVn.Count)]).ToString();
                    break;
                case 2:
                    //Câu trả lời đúng
                    btn_dapan2.Text = rightAnswer;
                    btn_index_true = randomIndex_vn;

                    //Các lựa chọn còn lại sẽ sai
                    btn_dapan1.Text = (mydataVn[random.Next(0, mydataVn.Count)]).ToString();
                    btn_dapan3.Text = (mydataVn[random.Next(0, mydataVn.Count)]).ToString();
                    break;
                case 3:
                    //Câu trả lời đúng
                    btn_dapan3.Text = rightAnswer;
                    btn_index_true = randomIndex_vn;

                    //Các lựa chọn còn lại sẽ sai
                    btn_dapan1.Text = (mydataVn[random.Next(0, mydataVn.Count)]).ToString();
                    btn_dapan2.Text = (mydataVn[random.Next(0, mydataVn.Count)]).ToString();
                    break;
               
                default:
                    break;
            }
        }
    }
}