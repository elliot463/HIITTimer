using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HIITTimer.ViewModels
{
    [QueryProperty("IntervalID","intervalID")]
    class EditIntervalViewModel : BaseViewModel
    {
        public EditIntervalViewModel()
        {
            SaveIntervalCommand = new Command(async () => await ExecuteSaveIntervalCommand());
        }

        IntervalTable interval = new IntervalTable();
        public Command SaveIntervalCommand { get; private set; }
        public IntervalTable Interval
        {
            get { return interval; }
            set
            {
                SetProperty(ref interval, value);
            }
        }
        public string IntervalID
        {
            set
            {
                Interval.ID = Convert.ToInt32(value);
                LoadInterval();
            }
        }
        private async Task LoadInterval()
        {
            IntervalTable data = await App.Database.GetIntervalByID(Interval.ID);
            Interval = data;
        }
        private async Task ExecuteSaveIntervalCommand()
        {
            await App.Database.SaveIntervalAsync(Interval);
            await Application.Current.MainPage.DisplayAlert("Interval Updated", "" , "OK");
            await Shell.Current.Navigation.PopToRootAsync();
        }


    }
}
