using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Mobile;

namespace DependencyServiceDemos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenCamera : ContentPage
    {
        public OpenCamera()
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
                    await Navigation.PopAsync();
                    await DisplayAlert("Scanned Barcode", message, "OK");
                });
            }
            else
            {
                await Navigation.PopAsync();
            }

           (sender as Button).IsEnabled = true;
        }

        private void scanView_OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PopAsync();
                await DisplayAlert("Scanned result", result.Text, "OK");
            });
        }

        private async void ZXingDefaultOverlay_FlashButtonClicked(Button sender, EventArgs e)
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
    }
}