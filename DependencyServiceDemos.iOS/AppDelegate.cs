using Foundation;
using UIKit;
using Xamarin.Forms;

namespace DependencyServiceDemos.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();

            #region For Screen Height & Width  
            App.screenWidth = (int)UIScreen.MainScreen.Bounds.Width;
            App.screenHeight = (int)UIScreen.MainScreen.Bounds.Height;
            #endregion

            LoadApplication(new App());

            DependencyService.Register<ITextToSpeechService, TextToSpeechService>();
            DependencyService.Register<IPhotoPickerService, PhotoPickerService>();
            return base.FinishedLaunching(app, options);
        }
    }
}
