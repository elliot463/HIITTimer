using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HIITTimer.ViewModels
{
    class NewProgramViewModel : BaseViewModel
    {
        public NewProgramViewModel()
        {
            SaveProgramCommand = new Command(async () => await SaveProgram());
        }
        string programName;
        int repeats;
        int newIntervals;
        public string ProgramName
        {
            get { return programName; }
            set
            {
                SetProperty(ref programName, value);
            }
        }
        public int Repeats
        {
            get { return repeats; }
            set
            {
                SetProperty(ref repeats, value);
            }
        }
        public int NewIntervals
        {
            get { return newIntervals; }
            set
            {
                SetProperty(ref newIntervals, value);
            }
        }
        
        public ICommand SaveProgramCommand { private set; get; }
        async Task SaveProgram()
        {
            ProgramTable Program = new ProgramTable
            {
                ProgramName = ProgramName,
                Repeats = Repeats
            };
            List<ProgramTable> data = await App.Database.GetProgramsAsync();
            Program.DisplayOrder = data.Count + 1;
            await App.Database.SaveProgramAsync(Program);            
            for (int i = 1; i <= NewIntervals; i++)
            {
                var intervalData = new IntervalTable();
                intervalData.ProgramID = Program.ID;
                intervalData.IntervalName = "Interval " + i.ToString();
                intervalData.IntervalOrder = i;
                intervalData.IntervalLength = 20;

                await App.Database.SaveIntervalAsync(intervalData);
            }
            await Application.Current.MainPage.DisplayAlert("Program Created", "Program has been created!", "OK");
            await Shell.Current.GoToAsync($"///yourprograms?newProgram={Program.ID}");
        }
    }
}
