using Android.Graphics;
using System.Collections.Generic;
using System.IO;
using ZXing;

namespace DependencyServiceDemos
{
    public class decodeBarcode
    {
        public string Barcodedecoder(Stream stream)
        {
            var input = BitmapFactory.DecodeStream(stream);
            var imgArray = GetRgbBytes(input);

            var bcreader = new BarcodeReader();
            var result = bcreader.Decode(imgArray,input.Width,input.Height,RGBLuminanceSource.BitmapFormat.Unknown);

            return result.Text;
        }

        private static byte[] GetRgbBytes(Bitmap image)
        {
            var rgbBytes = new List<byte>();
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var c = new Android.Graphics.Color(image.GetPixel(x, y));
                    rgbBytes.AddRange(new[] { c.R, c.G, c.B });
                }
            }
            return rgbBytes.ToArray();
        }
    }
}
