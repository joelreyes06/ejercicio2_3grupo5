using ejercicio2_3grupo.Servicios;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ejercicio2_3grupo
{
    public partial class App : Application
    {
        static BaseDeDatos db;


        public static BaseDeDatos DBase
        {
            get
            {
                if (db == null)
                {
                    String folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ejercicio2_3grupo.db3");

                    db = new BaseDeDatos(folderPath);
                }

                return db;
            }
        }




        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
