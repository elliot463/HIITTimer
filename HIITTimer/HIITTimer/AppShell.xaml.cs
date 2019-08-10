using HIITTimer.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HIITTimer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    { 
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("runprogram", typeof(ProgramRunningPage));
            Routing.RegisterRoute("editinterval", typeof(EditIntervalPage));
        }
    }
}