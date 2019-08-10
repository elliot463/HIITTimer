using HIITTimer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HIITTimer.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewProgramPage : ContentPage
    {
        public NewProgramPage()
        {
            BindingContext = new NewProgramViewModel();
            InitializeComponent();
        }
    }
}