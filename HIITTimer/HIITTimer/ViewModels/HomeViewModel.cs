using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HIITTimer.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            ResetDatabaseCommand = new Command(async () => ExecuteResetDatabaseCommand());
        }   
        public Command ResetDatabaseCommand { get; private set; }
        private async Task ExecuteResetDatabaseCommand()
        {
            App.Database.Reset();
            await Application.Current.MainPage.DisplayAlert("DATABASE RESET", "", "OK");
        }
    }
}
