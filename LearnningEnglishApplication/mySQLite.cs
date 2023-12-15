using Android.App;
using Android.Content;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LearnningEnglishApplication
{
    class mySQLite : SQLiteOpenHelper
    {
        public mySQLite(Context context) : base(context, "data_LEA", null, 1)
        {
            if (!CheckDatabaseExist())
            {
                // Nếu cơ sở dữ liệu chưa tồn tại, thì tạo mới
                SQLiteDatabase db = this.WritableDatabase;

                // Thực hiện các công việc tạo bảng và khác
                //

                db.Close();
            }
        }

        private bool CheckDatabaseExist()
        {
            string databasePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "data_LEA.db");
            return File.Exists(databasePath);
        }

        public override void OnCreate(SQLiteDatabase db)
        {

        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {

        }
    }
}