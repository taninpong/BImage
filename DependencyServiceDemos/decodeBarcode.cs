using Plugin.ImageEdit;
using Plugin.ImageEdit.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing;
using ZXing.Common;

namespace DependencyServiceDemos
{
    public class decodeBarcode
    {
        public async Task<(string, bool)> Barcodedecoder(Stream stream)
        {
            var bcreader = new BarcodeReader()
            {
                Options = new DecodingOptions()
                {
                    TryHarder = true,
                    PossibleFormats = new List<BarcodeFormat>() { BarcodeFormat.QR_CODE }
                },
                AutoRotate = true,
                TryInverted = true,

            };

            var image = await CrossImageEdit.Current.CreateImageAsync(stream);

            var maxlen = 1000;

            if (image.Width > maxlen || image.Height > maxlen)
            {
                image = image.Resize(500);
            }

            var pixels = image.ToArgbPixels();
            var orgLen = pixels.Length;
            var bytes = new byte[orgLen + (orgLen << 1)];
            for (int i = 0, j = 0; i < bytes.Length; ++j)
            {
                var pixel = pixels[j];
                bytes[i++] = (byte)(pixel & 0x00ff0000 >> 16);
                bytes[i++] = (byte)(pixel & 0x0000ff00 >> 8);
                bytes[i++] = (byte)(pixel & 0x000000ff);
            }

            var result = bcreader.Decode(bytes, image.Width, image.Height, RGBLuminanceSource.BitmapFormat.RGB24);
            if (result == null)
            {
                return ("image not support",false) ;
            }
            else
            {
                return (result.Text, true);
            }
        }

    }
}
