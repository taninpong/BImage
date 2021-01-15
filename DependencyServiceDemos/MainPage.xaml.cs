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
            var bcreader = new BarcodeReader();

            (sender as Button).IsEnabled = false;

            string result;
            using (var stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync())
            {
                var getclass = new decodeBarcode();
                result = await getclass.Barcodedecoder(stream);
            }

            Device.BeginInvokeOnMainThread(async () =>
            {

                await DisplayAlert("Scanned Barcode", $"Data is {result}", "OK");
            });

            (sender as Button).IsEnabled = true;

        }


    }
}
