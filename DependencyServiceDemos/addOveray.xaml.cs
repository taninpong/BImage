using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DependencyServiceDemos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class addOveray : ContentPage
    {
        const string TEXT = "ฮัลโหล บิทแมพ";
        SKBitmap helloBitmap;
        byte[] data;

        //SKBitmap imageResult;
        public addOveray()
        {
            InitializeComponent();

            this.ToolbarItems.Add(new ToolbarItem("Click me", "", () =>
            {
                using (MemoryStream memStream = new MemoryStream())
                using (SKManagedWStream wstream = new SKManagedWStream(memStream))
                {
                    helloBitmap.Encode(wstream, SKEncodedImageFormat.Png, 100);
                    data = memStream.ToArray();
                }
                var guid = Guid.NewGuid().ToString();
                DependencyService.Get<IPicture>().SavePictureToDisk(guid, data);

                DisplayAlert("Image Save", "The image has been saved", "OK");
            }));

            Title = TEXT;
            var resizeFactor = 0.5f;

            var setImageWidth = Convert.ToInt32(Application.Current.MainPage.Width);
            var setImageHeight = Convert.ToInt32(Application.Current.MainPage.Height);

            // Create bitmap and draw on it
            using (SKPaint textPaint = new SKPaint { TextSize = 48 })
            {
                //setting image
                SKRect bounds = new SKRect();
                textPaint.MeasureText(TEXT, ref bounds);

                //set image size display
                //helloBitmap = new SKBitmap(setImageWidth, setImageHeight);
                helloBitmap = new SKBitmap(setImageWidth, setImageHeight);

                //begin process
                using (SKCanvas bitmapCanvas = new SKCanvas(helloBitmap))
                {
                    bitmapCanvas.Clear();
                    bitmapCanvas.DrawText(TEXT, 0, 0, textPaint);
                    WebClient wc = new WebClient();

                    //download image and font family from internet  (but font can't use now )
                    using (Stream s = wc.OpenRead("https://www.ikea.com/th/en/images/products/ribba-frame-black__0638334_PE698858_S5.JPG"))
                    using (Stream fontStream = wc.OpenRead(@"https://fonts.gstatic.com/s/kanit/v7/nKKV-Go6G5tXcraQI2GwZoREDFs.woff2"))
                    //using (var fontFamily = SKData.Create(stream))
                    using (Stream qr = wc.OpenRead("https://linuxreviews.org/images/2/2f/Linuxreviews-qr-code.png"))
                    {
                        //decode image to bitmap and resize to same display image
                        SKBitmap bgBit = SKBitmap.Decode(s).Resize(new SKImageInfo(setImageWidth, setImageHeight), SKFilterQuality.High);
                        SKBitmap qrBit = SKBitmap.Decode(qr).Resize(new SKImageInfo(200, 200), SKFilterQuality.High);
                        bitmapCanvas.Clear();
                        bitmapCanvas.DrawBitmap(bgBit, 0, 0);
                        //get font

                        fontStream.Flush();
                        //fontStream.Position = 0;
                        var thaiFont = SKFontManager.Default.MatchCharacter('ก');
                        var engFont = SKFontManager.Default.MatchCharacter('0');
                        //SKFontManager.CreateDefault().CreateTypeface("Kanit-Regular");
                        var fontList = SKFontManager.Default.GetFontFamilies();

                        //begin drawing text
                        var thaiBrush = new SKPaint
                        {
                            Typeface = thaiFont,
                            TextSize = 24.0f,
                            IsAntialias = true,
                            Color = new SKColor(255, 0, 0)
                        };
                        var engBrush = new SKPaint
                        {
                            Typeface = engFont,
                            TextSize = 24.0f,
                            IsAntialias = true,
                            Color = new SKColor(255, 0, 0)
                        };
                        //Write data into image
                        bitmapCanvas.DrawText("โอนเงินสำเร็จ", 50, (bgBit.Height * resizeFactor / 2.0f) + 30, thaiBrush);
                        bitmapCanvas.DrawText("นายสว่าง ไม่ห่างเหิน", 50, (bgBit.Height * resizeFactor / 2.0f) + 60, thaiBrush);
                        bitmapCanvas.DrawText("สว่างอาบอบนวด", 50, (bgBit.Height * resizeFactor / 2.0f) + 90, thaiBrush);
                        bitmapCanvas.DrawText("จำนวนเงิน ", 50, (bgBit.Height * resizeFactor / 2.0f) + 200, thaiBrush);
                        bitmapCanvas.DrawText("9,000 ", 200, (bgBit.Height * resizeFactor / 2.0f) + 200, engBrush);

                        bitmapCanvas.DrawBitmap(qrBit, (bgBit.Width * resizeFactor / 2.0f) + 100, (bgBit.Height * resizeFactor / 2.0f) + 200);
                    }
                }
                // Create SKCanvasView to view result
                SKCanvasView canvasView = new SKCanvasView();
                canvasView.PaintSurface += OnCanvasViewPaintSurface;
                Content = canvasView;
            }

            void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
            {

                SKImageInfo info = args.Info;
                SKSurface surface = args.Surface;
                SKCanvas canvas = surface.Canvas;

                canvas.Clear(SKColors.Bisque);

                canvas.DrawBitmap(helloBitmap, (info.Width - helloBitmap.Width) / 2, (info.Height - helloBitmap.Height) / 2);
            }

        }
    }
}