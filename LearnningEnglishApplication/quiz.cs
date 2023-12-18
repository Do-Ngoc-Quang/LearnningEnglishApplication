using Android.App;
using Android.Content;
using Android.Content.PM;
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
        List<string> vocab_en = new List<string>();
        List<string> mean_vn = new List<string>();
        List<string> mean_vn_total = new List<string>();

        Random random = new Random();

        int tongSocau = 0;
        int cauDung = 0;

        // Biến lính canh 
        int btn_index_true = 0; //vị trí button của đáp án đúng
        int soluotchoi = 3; //Số mạng là 3 - làm sai tối đa 3 lần

        CheckBox ckb_soluotchoi_1, ckb_soluotchoi_2, ckb_soluotchoi_3;
        TextView txt_EN, txt_socau;
        Button btn_dapan1, btn_dapan2, btn_dapan3, btn_endquiz;

        private Handler handler = new Handler();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "---" layout resource
            SetContentView(Resource.Layout.quiz);

            ckb_soluotchoi_1 = FindViewById<CheckBox>(Resource.Id.ckb_soluotchoi_1);
            ckb_soluotchoi_2 = FindViewById<CheckBox>(Resource.Id.ckb_soluotchoi_2);
            ckb_soluotchoi_3 = FindViewById<CheckBox>(Resource.Id.ckb_soluotchoi_3);

            txt_EN = FindViewById<TextView>(Resource.Id.txt_EN);
            txt_socau = FindViewById<TextView>(Resource.Id.txt_socau);

            btn_dapan1 = FindViewById<Button>(Resource.Id.btn_dapan1);
            btn_dapan2 = FindViewById<Button>(Resource.Id.btn_dapan2);
            btn_dapan3 = FindViewById<Button>(Resource.Id.btn_dapan3);
            btn_endquiz = FindViewById<Button>(Resource.Id.btn_endquiz);

            btn_dapan1.Click += Btn_dapan1_Click;
            btn_dapan2.Click += Btn_dapan2_Click;
            btn_dapan3.Click += Btn_dapan3_Click;
            btn_endquiz.Click += Btn_endquiz_Click;

            LoadXML();

            ShowRandomItem();
        }

        private void LoadXML()
        {
            //reader = XmlReader.Create(Assets.Open("vocabulary.xml"));
            //while (reader.Read())
            //{
            //    if (reader.Name.ToString() == "en")
            //        vocab_en.Add(reader.ReadString());
            //    if (reader.Name.ToString() == "vn")
            //        mean_vn.Add(reader.ReadString());
            //}

            
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
                        else if (reader.Name == "vn")
                        {
                            // Thêm nghĩa vào danh sách chung mean_vn_total
                            mean_vn_total.Add(reader.ReadString());
                        }
                        else if (reader.Name == "word" && currentCategory == "stage_2")
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

        private void Btn_dapan1_Click(object sender, EventArgs e)
        {
            if (btn_index_true == 1)
            {
                //True answer!
                btn_dapan1.SetBackgroundColor(Android.Graphics.Color.LightGreen);

                //Đếm số câu đúng
                cauDung++;
            }
            else
            {
                //Xử lý nếu trả lời sai
                soluotchoi--;

                switch (btn_index_true)
                {
                    case 2:
                        //This is wrong answer!
                        btn_dapan1.SetBackgroundColor(Android.Graphics.Color.PaleVioletRed);


                        //True answer!
                        btn_dapan2.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                        
                        break;

                    case 3:
                        //This is wrong answer!
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
            if (btn_index_true == 2)
            {
                //True answer!
                btn_dapan2.SetBackgroundColor(Android.Graphics.Color.LightGreen);

                //Đếm số câu đúng
                cauDung++;
            }
            else
            {
                //Xử lý nếu trả lời sai
                soluotchoi--;

                switch (btn_index_true)
                {
                    case 1:
                        //This is wrong answer!
                        btn_dapan2.SetBackgroundColor(Android.Graphics.Color.PaleVioletRed);

                        //True answer!
                        btn_dapan1.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                        break;

                    case 3:
                        //This is wrong answer!
                        btn_dapan2.SetBackgroundColor(Android.Graphics.Color.PaleVioletRed);

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

        private void Btn_dapan3_Click(object sender, EventArgs e)
        {
            if (btn_index_true == 3)
            {
                //True answer!
                btn_dapan3.SetBackgroundColor(Android.Graphics.Color.LightGreen);

                //Đếm số câu đúng
                cauDung++;
            }
            else
            {
                //Xử lý nếu trả lời sai
                soluotchoi--;

                switch (btn_index_true)
                {
                    case 1:
                        //This is wrong answer!
                        btn_dapan3.SetBackgroundColor(Android.Graphics.Color.PaleVioletRed);

                        //True answer!
                        btn_dapan1.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                        break;

                    case 2:
                        //This is wrong answer!
                        btn_dapan3.SetBackgroundColor(Android.Graphics.Color.PaleVioletRed);

                        //True answer!
                        btn_dapan2.SetBackgroundColor(Android.Graphics.Color.LightGreen);
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

        private void Btn_endquiz_Click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(home));

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


        private void ShowRandomItem()
        {
            //Hiển thị số lượt chơi
            switch (soluotchoi)
            {
                case 1:
                    ckb_soluotchoi_1.Checked = true;
                    ckb_soluotchoi_2.Checked = false;
                    ckb_soluotchoi_3.Checked = false;
                    break;
                case 2:
                    ckb_soluotchoi_1.Checked = true;
                    ckb_soluotchoi_2.Checked = true;
                    ckb_soluotchoi_3.Checked = false;
                    break;
                case 3:
                    ckb_soluotchoi_1.Checked = true;
                    ckb_soluotchoi_2.Checked = true;
                    ckb_soluotchoi_3.Checked = true;
                    break;
                default:
                    break;
            }

            //Kiểm tra điều kiện
            if (tongSocau == 10 || soluotchoi == 0)
            {
                if (cauDung >= 5)
                {
                    Intent it = new Intent(this, typeof(quiz_completed));

                    it.PutExtra("tongSocau", tongSocau.ToString());
                    it.PutExtra("cauDung", cauDung.ToString());

                    StartActivity(it);
                }
                else
                {
                    Intent it = new Intent(this, typeof(quiz_lost));
                    StartActivity(it);
                }
                
            }
            else
            {
                btn_dapan1.SetBackgroundColor(Android.Graphics.Color.WhiteSmoke);
                btn_dapan2.SetBackgroundColor(Android.Graphics.Color.WhiteSmoke);
                btn_dapan3.SetBackgroundColor(Android.Graphics.Color.WhiteSmoke);

                tongSocau++;

                txt_socau.Text = tongSocau.ToString();

                // Chọn ngẫu nhiên một phần tử từ danh sách vocab_en ------ Tiếng anh
                int randomIndex_en = random.Next(0, vocab_en.Count);
                string randomItem_en = vocab_en[randomIndex_en];

                // Gán giá trị của phần tử ngẫu nhiên vào EditText
                txt_EN.Text = randomItem_en;

                // Chọn ngẫu nhiên một phần tử từ danh sách vocab_en ------ Tiếng việt
                string rightAnswer = mean_vn[randomIndex_en];

                // Gán giá trị của phần tử ngẫu nhiên
                int randomIndex_vn = random.Next(1, 4);
                switch (randomIndex_vn)
                {
                    case 1:
                        //Câu trả lời đúng
                        btn_dapan1.Text = rightAnswer;
                        btn_index_true = randomIndex_vn;

                        //Các lựa chọn còn lại sẽ sai
                        btn_dapan2.Text = (mean_vn_total[random.Next(0, mean_vn_total.Count)]).ToString();
                        btn_dapan3.Text = (mean_vn_total[random.Next(0, mean_vn_total.Count)]).ToString();
                        break;
                    case 2:
                        //Câu trả lời đúng
                        btn_dapan2.Text = rightAnswer;
                        btn_index_true = randomIndex_vn;

                        //Các lựa chọn còn lại sẽ sai
                        btn_dapan1.Text = (mean_vn_total[random.Next(0, mean_vn_total.Count)]).ToString();
                        btn_dapan3.Text = (mean_vn_total[random.Next(0, mean_vn_total.Count)]).ToString();
                        break;
                    case 3:
                        //Câu trả lời đúng
                        btn_dapan3.Text = rightAnswer;
                        btn_index_true = randomIndex_vn;

                        //Các lựa chọn còn lại sẽ sai
                        btn_dapan1.Text = (mean_vn_total[random.Next(0, mean_vn_total.Count)]).ToString();
                        btn_dapan2.Text = (mean_vn_total[random.Next(0, mean_vn_total.Count)]).ToString();
                        break;

                    default:
                        break;
                }
            }
        }
    }
}