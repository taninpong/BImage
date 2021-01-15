using Plugin.ImageEdit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZXing;

namespace DependencyServiceDemos
{
    public class decodeBarcode
    {
        public async Task<string> Barcodedecoder(Stream stream)
        {
            var image = await CrossImageEdit.Current.CreateImageAsync(stream);
            var bytes = image.ToArgbPixels()
                .Select(it =>
                {
                    var color = System.Drawing.Color.FromArgb(it);
                    return new[] { color.R, color.G, color.B };
                }).SelectMany(it => it).ToArray();
            if (image.Width < 1281 && image.Height < 961)
            {
                image = image.Resize(1280, 960);
            }
            var bcreader = new BarcodeReader();
            var result = bcreader.Decode(bytes, image.Width, image.Height, RGBLuminanceSource.BitmapFormat.RGB24);
            if (result == null)
            {
                return "Nosupport";
            }
            else
            {
                return result.Text;
            }
        }

        //public string Getcode(string result)
        //{
        //    return result;
        //}

        //private static byte[] GetRgbBytesX(int[] pixel)
        //{
        //    var rgbBytes = new List<byte>();
        //    foreach (var item in pixel)
        //    {
        //        var r = item & 0x00FF0000 >> 16; //Get R
        //        var g = item & 0x0000FF00 >> 8;  //Get G
        //        var b = item & 0x000000FF;       //Get B

        //        byte bytesR = Convert.ToByte(r.ToString());
        //        byte bytesG = Convert.ToByte(g.ToString());
        //        byte bytesB = Convert.ToByte(b.ToString());

        //        rgbBytes.AddRange(new[] { bytesR, bytesG, bytesB });
        //    }
        //    return rgbBytes.ToArray();
        //}

        //public async Task<string> xDecode(Stream stream)
        //{
        //    var image = await CrossImageEdit.Current.CreateImageAsync(stream);
        //    var pixels = image.ToArgbPixels();
        //    var imgArray = GetRgbBytesX(pixels);
        //    var bcreader = new BarcodeReader();
        //    var result = bcreader.Decode(imgArray, image.Width, image.Height, RGBLuminanceSource.BitmapFormat.Unknown);
        //    if (result == null)
        //    {
        //        return "Nosupport";
        //    }
        //    else
        //    {
        //        return result.Text;
        //    }

        //}
    }
}
