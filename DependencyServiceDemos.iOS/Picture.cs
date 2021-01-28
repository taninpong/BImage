using DependencyServiceDemos.iOS;
using Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(Picture))]


namespace DependencyServiceDemos.iOS
{
    public class Picture :IPicture
    {
        //https://forums.xamarin.com/discussion/94306/saving-image-to-local-folder
        public void SavePictureToDisk(string filename, byte[] imageData)
        {

            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var directoryname = Path.Combine(documents, "FolderName");

            var chartImage = new UIImage(NSData.FromArray(imageData));

            chartImage.SaveToPhotosAlbum((image, error) =>
            {
                //you can retrieve the saved UI Image as well if needed using
                //var i = image as UIImage;
                if (error != null)
                {
                    Console.WriteLine(error.ToString());
                }
            });
        }
    }
}