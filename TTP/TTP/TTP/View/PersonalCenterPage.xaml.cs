using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TTP.Model;
using TTP.Services;
using TTP.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TTP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalCenterPage : ContentPage
    {
        public ObservableCollection<PersonalCenterSetting> personalCenterSettings { get; set; }
        public PersonalCenterPage()
        {
            InitializeComponent();
            BindingContext = new PersonalCenterViewModel();
            initPersonalCenterSetting();

        }

        public void initPersonalCenterSetting()
        {
            personalCenterSettings = new ObservableCollection<PersonalCenterSetting>();
            personalCenterSettings.Add(new PersonalCenterSetting { Name = "数据统计", HeadImage = "statisticIcon.png", BehindImage = "rightArrow.png" });
            personalCenterSettings.Add(new PersonalCenterSetting { Name = "可运行软件", HeadImage = "tomato.png", BehindImage = "rightArrow.png" });
            personalCenterSettings.Add(new PersonalCenterSetting { Name = "排行", HeadImage = "rankingIcon.png", BehindImage = "rightArrow.png" });
            personalCenterSettings.Add(new PersonalCenterSetting { Name = "帮助", HeadImage = "tomato.png", BehindImage = "rightArrow.png" });
            settingList.ItemsSource = personalCenterSettings;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            settingList.SelectedItem = null;
            //await Navigation.PushModalAsync(new DataAnalysisPage());
            switch (e.SelectedItemIndex)
            {
                case 0:
                    await Navigation.PushModalAsync(new DataAnalysisPage());
                    break;
                case 2:
                    await Navigation.PushModalAsync(new RankingPage());
                    break;
                case 3:
                    await Navigation.PushModalAsync(new HelpPage());
                    break;

            }
        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();



            HttpClient client = new HttpClient();


            MultipartFormDataContent form = new MultipartFormDataContent();
            StreamContent fileContent = new StreamContent(stream);
            form.Add(fileContent, "file", "upload.jpg");
            HttpResponseMessage res = await client.PostAsync(Constants.PicUrl, form);
            var responseContent = "";
            responseContent = await res.Content.ReadAsStringAsync();
            btnAdd.ImageSource = ImageSource.FromUri(new Uri(responseContent));
        }
    }
}