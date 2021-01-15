using Plugin.ImageEdit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ZXing;

namespace DependencyServiceDemos
{
    public class decodeBarcode
    {
        public async Task<string> Barcodedecoder(Stream stream)
        {
            //var input = BitmapFactory.DecodeStream(stream);

            var input2 = await xDecode(stream);
            //var imgArray = GetRgbBytes(input);

            //var bcreader = new BarcodeReader();
            //var result = bcreader.Decode(imgArray,input.Width,input.Height,RGBLuminanceSource.BitmapFormat.Unknown);
            return input2;
        }

        public string Getcode(string result)
        {
            return result;
        }

        //private static byte[] GetRgbBytes(Bitmap image)
        //{
        //    var rgbBytes = new List<byte>();
        //    for (int y = 0; y < image.Height; y++)
        //    {
        //        for (int x = 0; x < image.Width; x++)
        //        {
        //            var c = new Android.Graphics.Color(image.GetPixel(x, y));
        //            rgbBytes.AddRange(new[] { c.R, c.G, c.B });
        //        }
        //    }
        //    return rgbBytes.ToArray();
        //}

        private static byte[] GetRgbBytesX(int[] pixel)
        {
            var rgbBytes = new List<byte>();
            foreach (var item in pixel)
            {
                var r = item & 0x00FF0000 >> 16; //Get R
                var g = item & 0x0000FF00 >> 8;  //Get G
                var b = item & 0x000000FF;       //Get B

                byte bytesR = Convert.ToByte(r.ToString());
                byte bytesG = Convert.ToByte(g.ToString());
                byte bytesB = Convert.ToByte(b.ToString());

                rgbBytes.AddRange(new[] { bytesR, bytesG, bytesB });
            }

            //for (int y = 0; y < image.Height; y++)
            //{
            //    for (int x = 0; x < image.Width; x++)
            //    {
            //        var c = new Android.Graphics.Color(image.GetPixel(x, y));
            //        rgbBytes.AddRange(new[] { c.R, c.G, c.B });
            //    }
            //}
            return rgbBytes.ToArray();
        }

        public async Task<string> xDecode(Stream stream)
        {
            var image = await CrossImageEdit.Current.CreateImageAsync(stream);
            var pixels = image.ToArgbPixels();
            var imgArray = GetRgbBytesX(pixels);
            var bcreader = new BarcodeReader();
            var result = bcreader.Decode(imgArray, image.Width, image.Height, RGBLuminanceSource.BitmapFormat.Unknown);
            return result.Text;
        }
    }
}
