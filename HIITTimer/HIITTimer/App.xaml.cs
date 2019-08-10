using PCLStorage;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HIITTimer
{
    public partial class App : Application
    {
        public App()
        {
            ActiveProgram = 0;
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        static HIITRepository database;

        public static HIITRepository Database
        {
            get
            {
                if (database == null)
                {
                    var sqliteFilename = "HIITRepo.db3";
                    IFolder folder = FileSystem.Current.LocalStorage;
                    string path = PortablePath.Combine(folder.Path.ToString(), sqliteFilename);
                    database = new HIITRepository(path);
                }
                return database;
            }
        }
        public static int ActiveProgram { get; set; }

    }
}
