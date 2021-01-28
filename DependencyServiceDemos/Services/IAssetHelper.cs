using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyServiceDemos.Services
{
    public interface IAssetHelper
    {
        SKTypeface GetSkiaTypefaceFromAssetFont();
    }
}
