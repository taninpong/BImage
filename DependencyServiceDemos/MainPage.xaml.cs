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
            var bcreader = new BarcodeReader();
            (sender as Button).IsEnabled = false;

            string textResult;
            bool canReadQrCode;

            using (var stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync())
            {
                var getclass = new decodeBarcode();

                var result = await getclass.Barcodedecoder(stream);
                textResult = result.Item1;
                canReadQrCode = result.Item2;
            }
            if (canReadQrCode)
            {

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Scanned Barcode", $"Data is {textResult}", "OK");
                });

            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Scanned Barcode", $"Can't read Qr code", "OK");
                });
            }

            


            (sender as Button).IsEnabled = true;
        }


    }
}
