//using Plugin.Media;
//using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
    public partial class AddGoodsPage : ContentPage
    {
        private GoodsModel gm;
        public AddGoodsPage()
        {
            InitializeComponent();
            this.BindingContext = new AddGoodsViewModel();
            gm = new GoodsModel();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            AddGoodsViewModel agvm = BindingContext as AddGoodsViewModel;
            agvm.GoodsModel.Type = comboBox.SelectedItem.ToString();
            agvm.GoodsModel.Date= DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            agvm.GoodsModel.Uri = gm.Uri;
            agvm.GoodsModel.UserId = App.StaticUser.UserId;
            Random ran = new Random();
            long RandKey = ran.Next(0, 9999);
            agvm.GoodsModel.Id = RandKey;
            await App.GoodsManager.AddGoodsTaskAsync(agvm.GoodsModel);
            GoodsViewModel.refresh();
            DependencyService.Get<IToastService>().LongAlert("增加商品成功！");
            await Navigation.PopAsync();
        }

        private async void SfButton_Clicked(object sender, EventArgs e)
        {
            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream == null) return;
            
            HttpClient client = new HttpClient();

            MultipartFormDataContent form = new MultipartFormDataContent();
            StreamContent fileContent = new StreamContent(stream);
            form.Add(fileContent, "file", "upload.jpg");
            HttpResponseMessage res = await client.PostAsync(Constants.PicUrl, form);
            var responseContent = "";
            responseContent = await res.Content.ReadAsStringAsync();
            gm.Uri = responseContent;
            
            image.Source = ImageSource.FromUri(new Uri(responseContent));
        }
    }
}