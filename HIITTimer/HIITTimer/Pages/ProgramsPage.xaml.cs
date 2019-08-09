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
    public partial class ProgramsPage : ContentPage
    {
        public ProgramsPage()
        {
            BindingContext = new ProgramsViewModel();
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            BindingContext = null;
            BindingContext = new ProgramsViewModel();
            base.OnAppearing();
        }
    }
}