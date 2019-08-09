using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;
using Xamarin.Forms;

namespace HIITTimer.ViewModels
{
    class ProgramsViewModel : BaseViewModel
    {
        public ProgramsViewModel()
        {
            ProgramList = new ObservableRangeCollection<ProgramTable>();
            ExecuteLoadProgramsCommand();            
        }
        public ObservableRangeCollection<ProgramTable> ProgramList { get; set; }
        ProgramTable selectedProgram;
        public ProgramTable SelectedProgram
        {
            get { return selectedProgram; }
            set
            {
                SetProperty(ref selectedProgram, value);
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
                ProgramList.ReplaceRange(data.OrderBy(a => a.DisplayOrder));
                SelectedProgram = ProgramList.FirstOrDefault();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
