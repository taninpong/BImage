using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DependencyServiceDemos.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DependencyServiceDemos.Droid
{
    //public class MediaService : IMediaService
    //{
    //    Context CurrentContext =>  CrossCurrentActivity.Current.Activity;
    //    public void SaveImageFromByte(byte[] imageByte, string fileName)
    //    {
    //        try
    //        {
    //            Java.IO.File storagePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
    //            string path = System.IO.Path.Combine(storagePath.ToString(), fileName);
    //            System.IO.File.WriteAllBytes(path, imageByte);
    //            var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
    //            mediaScanIntent.SetData(Android.Net.Uri.FromFile(new Java.IO.File(path)));
    //            CurrentContext.SendBroadcast(mediaScanIntent);
    //        }
    //        catch (Exception ex)
    //        {

    //        }
    //    }
    //}
}