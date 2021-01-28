using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DependencyServiceDemos.Services;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DependencyServiceDemos.Droid.Services
{
    public class AssetHelper : IAssetHelper
    {
        public SKTypeface GetSkiaTypefaceFromAssetFont()
        {
            throw new NotImplementedException();
        }
    }
}