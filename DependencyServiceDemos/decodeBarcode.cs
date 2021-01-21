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
    public class DecodeBarcode
    {
        public async Task<(string, bool)> Decode(Stream stream)
        {
            var reader = new BarcodeReader()
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

            const int maxlength = 1000;
            var isImageReachMaximum = image.Width > maxlength || image.Height > maxlength;
            if (isImageReachMaximum)
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

            var result = reader.Decode(bytes, image.Width, image.Height, RGBLuminanceSource.BitmapFormat.RGB24)?.Text;
            return (result, !string.IsNullOrEmpty(result));
        }

    }
}
