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
    public class Picture : IPicture
    {
        public void SavePictureToDisk(string filename, byte[] imageData)
        {
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


            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonPictures);
            string finalPath = Path.Combine(documentsPath, "Mana");
            if (!System.IO.Directory.Exists(finalPath))
            {
                Directory.CreateDirectory(finalPath);
            }
            File.WriteAllBytes(finalPath, imageData);
        }
    }
}