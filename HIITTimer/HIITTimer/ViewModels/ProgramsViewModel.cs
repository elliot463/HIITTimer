using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HIITTimer.ViewModels
{   
    class ProgramsViewModel : BaseViewModel
    {
        public ProgramsViewModel()
        {
            ProgramList = new ObservableRangeCollection<ProgramTable>();
            ProgramIntervals = new ObservableRangeCollection<IntervalTable>();
            DeleteProgramCommand = new Command(async () => await ExecuteDeleteProgramCommand());
            StartProgramCommand = new Command(async () => await ExecuteStartProgramCommand());
            EditIntervalCommand = new Command<int>(async (int e) => await ExecuteEditIntervalCommand(e));
            NewIntervalCommand = new Command(async () => await ExecuteNewIntervalCommand());
            LoadProgramsCommand = new Command(async () => await ExecuteLoadProgramsCommand());
            ExecuteLoadProgramsCommand();
        }
        public ObservableRangeCollection<ProgramTable> ProgramList { get; set; }
        public ObservableRangeCollection<IntervalTable> ProgramIntervals { get; set; }
        ProgramTable selectedProgram;
        public Command DeleteProgramCommand { get; private set; }
        public Command StartProgramCommand { get; private set; }
        public Command EditIntervalCommand { get; private set; }
        public Command NewIntervalCommand { get; private set; }
        public Command LoadProgramsCommand { get; private set; }
        public ProgramTable SelectedProgram
        {
            get { return selectedProgram; }
            set
            {
                SetProperty(ref selectedProgram, value);
                LoadIntervals();
            }
        }
        private async Task LoadIntervals()
        {
            
            if (SelectedProgram != null)
            {
                List<IntervalTable> data = await App.Database.GetProgramIntervals(SelectedProgram.ID);
                ProgramIntervals.ReplaceRange(data.OrderBy(a => a.IntervalOrder));
            }
            else
            {
                List<IntervalTable> data = ProgramIntervals.ToList();
                ProgramIntervals.RemoveRange(data);                
            }
        }
        private async Task ExecuteLoadProgramsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                List<ProgramTable> data = await App.Database.GetProgramsAsync();
                ProgramList.ReplaceRange(data.OrderByDescending(a => a.DisplayOrder));
                if (App.ActiveProgram == 0)
                {
                    SelectedProgram = ProgramList.FirstOrDefault();
                }
                else
                {
                    SelectedProgram = ProgramList[App.ActiveProgram];
                }
                    
            }
            catch (Exception ex)
            {
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task ExecuteDeleteProgramCommand()
        {
            if (SelectedProgram != null)
            {
                List<IntervalTable> data = ProgramIntervals.ToList();
                await App.Database.DeleteProgramIntervals(data);
                await App.Database.DeleteProgramAsync(SelectedProgram);
                await ExecuteLoadProgramsCommand();
                SelectedProgram = ProgramList.FirstOrDefault();
                App.ActiveProgram = 0;
            }
        }
        private async Task ExecuteStartProgramCommand()
        {
            await Shell.Current.GoToAsync($"runprogram?programID={SelectedProgram.ID}");
        }
        private async Task ExecuteEditIntervalCommand(int ID)
        {
            App.ActiveProgram = ProgramList.IndexOf(SelectedProgram);
            await Shell.Current.GoToAsync($"editinterval?intervalID={ID}");
        }
        private async Task ExecuteNewIntervalCommand()
        {
            App.ActiveProgram = ProgramList.IndexOf(SelectedProgram);
            App.ActiveProgramID = SelectedProgram.ID;
            await Shell.Current.GoToAsync("editinterval?intervalID=-1");
        }
    }
}
