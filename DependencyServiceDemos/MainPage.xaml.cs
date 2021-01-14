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

        void OnGetDeviceOrientationButtonClicked(object sender, EventArgs e)
        {
            DeviceOrientation orientation = DependencyService.Get<IDeviceOrientationService>().GetOrientation();
            orientationLabel.Text = orientation.ToString();
        }

        async void OnPickPhotoButtonClicked(object sender, EventArgs e)
        {
            var bcreader = new BarcodeReader();

            (sender as Button).IsEnabled = false;

            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                img.Source = ImageSource.FromStream(() => stream);
            }

            var getclass = new decodeBarcode();

            var result = getclass.Barcodedecoder(stream);

            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Scanned Barcode", $"Data is {result}", "OK");
            });

            (sender as Button).IsEnabled = true;

        }


        async void OnSpeakButtonClicked(object sender, EventArgs e)
        {
            ITextToSpeechService service = DependencyService.Get<ITextToSpeechService>(DependencyFetchTarget.NewInstance);
            using (service as IDisposable)
            {
                await service.SpeakAsync("Hello world");
            }
        }

    }
}
