using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            DeleteIntervalCommand = new Command(async () => await ExecuteDeleteIntervalCommand());
        }

        IntervalTable interval = new IntervalTable();
        ProgramsViewModel vm = new ProgramsViewModel();
        public Command SaveIntervalCommand { get; private set; }
        public Command DeleteIntervalCommand { get; private set; }
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
            if (Interval.ID == -1)
            {
                Interval.ID = 0;
                Interval.ProgramID = App.ActiveProgramID;
                Debug.Write($"ProgID={Interval.ProgramID}");
                List<IntervalTable> intervaldata = await App.Database.GetProgramIntervals(Interval.ProgramID);
                int neworder = intervaldata.Count() + 1;
                Debug.Write($"NewOrder={neworder}");
                if (neworder != null)
                {
                    Interval.IntervalOrder = neworder;
                }
                else
                {
                    Interval.IntervalOrder = 1;
                }
                
                return;
            }
            IntervalTable data = await App.Database.GetIntervalByID(Interval.ID);
            Interval = data;
        }
        private async Task ExecuteSaveIntervalCommand()
        {
            if (Interval.IntervalName == null || Interval.IntervalName == "")
            {
                await Application.Current.MainPage.DisplayAlert("Invalid Name", "You Must enter a name to be displayed during this interval", "OK");
                return;
            }
            if (Interval.IntervalLength <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Invalid Interval Length", "Interval Length must be a Positive Integer", "OK");
                return;
            }
            await App.Database.SaveIntervalAsync(Interval);
            await Application.Current.MainPage.DisplayAlert("Interval Updated", "" , "OK");
            await Shell.Current.Navigation.PopToRootAsync();
        }
        private async Task ExecuteDeleteIntervalCommand()
        {
            List<IntervalTable> data = vm.ProgramIntervals.ToList();
            await App.Database.DeleteIntervalAsync(Interval);
            foreach (IntervalTable i in data)
            {
                if (i.IntervalOrder > Interval.IntervalOrder)
                {
                    i.IntervalOrder--;
                    await App.Database.SaveIntervalAsync(i);
                }
            }                
            await App.Database.DeleteIntervalAsync(Interval);
            await Shell.Current.Navigation.PopToRootAsync();
        }

    }
}
