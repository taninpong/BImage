using Plugin.ImageEdit;
using System;
using System.IO;
using Xamarin.Forms;
using ZXing;

namespace DependencyServiceDemos
{
    public partial class MainPage : TabbedPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        async void OnPickPhotoButtonClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;

            var stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                var decodeBarcode = new DecodeBarcode();
                var result = await decodeBarcode.Decode(stream);

                var message = result.IsSuccess ? $"Data is {result.Value}" : "Can't read Qr code";
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Scanned Barcode", message, "OK");
                });
            }

            (sender as Button).IsEnabled = true;
        }


    }
}
