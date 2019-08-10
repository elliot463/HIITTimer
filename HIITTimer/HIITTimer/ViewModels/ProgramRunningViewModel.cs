using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HIITTimer.ViewModels
{
    [QueryProperty("ProgramID", "programID")]
    class ProgramRunningViewModel : BaseViewModel
    {
        public ProgramRunningViewModel()
        {
            Intervals = new ObservableRangeCollection<IntervalTable>();
            StartProgram();            
        }
        string display1;
        string display2;
        ProgramTable program = new ProgramTable();
        ObservableRangeCollection<IntervalTable> Intervals { get; set; }
        public string Display1
        {
            get { return display1; }
            set
            {
                SetProperty(ref display1, value);
            }
        }
        public string Display2
        {
            get { return display2; }
            set
            {
                SetProperty(ref display2, value);
            }
        }
        public ProgramTable Program
        {
            get { return program; }
            set
            {
                SetProperty(ref program, value);
            }
        }
        public string ProgramID
        {
            set
            {
                Program.ID = Convert.ToInt32(value);
                LoadIntervals();
            }
        }
        private async Task LoadIntervals()
        {           
                List<IntervalTable> data = await App.Database.GetProgramIntervals(Program.ID);
                Intervals.ReplaceRange(data.OrderBy(a => a.IntervalOrder));                    
        }
        public async void StartProgram()
        {
            DeviceDisplay.KeepScreenOn = true;
            for (int i = 10; i > 0 ; i--)
            {
                Display1 = "GET READY";
                Display2 =  i.ToString();
                await Task.Delay(1000);
            }
            Display1 = "LET'S GO!";
            Display2 = null;
            await Task.Delay(1000);
            foreach (IntervalTable i in Intervals)
            {
                Display1 = i.IntervalName;
                for (int j = i.IntervalLength; j > 0; j--)
                {
                    Display2 = j.ToString();
                    await Task.Delay(1000);
                }
            }
            DeviceDisplay.KeepScreenOn = false;
            Display1 = "PROGRAM COMPLETE!";
            Display2 = null;
        }
    }
}
