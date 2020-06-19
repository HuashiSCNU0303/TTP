using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTP.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TTP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataAnalysisPage : ContentPage
    {
        public DataAnalysisPage()
        {
            BindingContext = new DataAnalysisViewModel();
            InitializeComponent();
        }
    }
}
