using DependencyServiceDemos.Services;
using Foundation;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;

namespace DependencyServiceDemos.iOS.Services
{
    public class AssetHelper : IAssetHelper
    {
        public SKTypeface GetSkiaTypefaceFromAssetFont()
        {
            string fontFile = Path.Combine(NSBundle.MainBundle.BundlePath, "Kanit-Regular.ttf");
            SkiaSharp.SKTypeface typeFace;

            using (var asset = File.OpenRead(fontFile))
            {
                var fontStream = new MemoryStream();
                asset.CopyTo(fontStream);
                fontStream.Flush();
                fontStream.Position = 0;
                typeFace = SKTypeface.FromStream(fontStream);
            }

            return typeFace;
        }
    }
}