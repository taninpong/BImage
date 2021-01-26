using Plugin.ImageEdit;
using Plugin.Screenshot;
using System;
using System.IO;
using Xamarin.Essentials;
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

        private void scanView_OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Scanned result", "The barcode's text is " + result.Text + ". The barcode's format is " + result.BarcodeFormat, "OK");
            });
        }

        private async void OpenCamara(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OpenCamera());
        }

        private async void OpenFlash(object sender, EventArgs e)
        {
            try
            {
                // Turn On
                await Flashlight.TurnOnAsync();

                // Turn Off
                await Flashlight.TurnOffAsync();
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to turn on/off flashlight
            }
        }

        private async void OpenScreenShot(object sender, EventArgs e)
        {
            label.Text = DateTime.UtcNow.ToString();
            var stream = new MemoryStream(await CrossScreenshot.Current.CaptureAsync());
            byte[] dataImg = stream.ToArray();
            var guid = Guid.NewGuid().ToString();
            DependencyService.Get<IPicture>().SavePictureToDisk(guid, dataImg);
            await DisplayAlert("Image Save",
              "The image has been saved",
              "OK");
        }
        private async void ScreenShotAndSave(object sender, EventArgs e)
        {
            try
            {
                string path = await CrossScreenshot.Current.CaptureAndSaveAsync();

                //ste0
                label.Text = "Location: " + path + DateTime.UtcNow.ToString();

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "ok");
            }
        }
    }
}
