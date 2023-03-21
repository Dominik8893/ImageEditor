using System.Drawing;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;

class ImageEditor
{

    static Bitmap CreateBitmap(string ImagePath)
    {
        Bitmap CreatedBitmap = new Bitmap(System.Drawing.Image.FromFile(ImagePath));

        return CreatedBitmap;
    }

    static void SaveImage(string SavePath, Bitmap BitMapToSave)
    {
        BitMapToSave.Save(SavePath, ImageFormat.Png);
    }

    static Bitmap ChangeBrightness(Bitmap BitmapToEdit, double BrightnessAmount)//Brightness should be larger then 0 , to large values will wield a white image
    {

        for (int i = 0; i < BitmapToEdit.Width; i++)
        {
            for (int j = 0; j < BitmapToEdit.Height; j++)
            {
                Color PixelColor = BitmapToEdit.GetPixel(i, j);

                int ModifiedRed = Math.Clamp((int)Math.Floor((double)PixelColor.R * BrightnessAmount), 0, 255);
                int ModifiedGreen = Math.Clamp((int)Math.Floor((double)PixelColor.G * BrightnessAmount), 0, 255);
                int ModifiedBlue = Math.Clamp((int)Math.Floor((double)PixelColor.B * BrightnessAmount), 0, 255);

                PixelColor = Color.FromArgb(PixelColor.A, ModifiedRed, ModifiedGreen, ModifiedBlue);

                BitmapToEdit.SetPixel(i, j, (PixelColor));
            }
        }

        return BitmapToEdit;
    }


    //Changes Image to Grayscale, SaturationAmount at default will just convert to grayscale values higher then 1 will make image brighter and smaller will make it darker
    static Bitmap ChangeGrayScale(Bitmap BitmapToEdit, double SaturationAmount = 1)
    {

        for (int i = 0; i < BitmapToEdit.Width; i++)
        {
            for (int j = 0; j < BitmapToEdit.Height; j++)
            {
                Color PixelColor = BitmapToEdit.GetPixel(i, j);

                int NewGrayTone = Math.Clamp(((int)Math.Floor((((double)PixelColor.R + (double)PixelColor.G + (double)PixelColor.B)) / (double)3 * SaturationAmount)), 0, 255);


                PixelColor = Color.FromArgb(PixelColor.A, NewGrayTone, NewGrayTone, NewGrayTone);

                BitmapToEdit.SetPixel(i, j, (PixelColor));
            }
        }

        return BitmapToEdit;
    }


    static void Main()
    {
        Bitmap MyImage = CreateBitmap("D:\\VisualStudio\\ImageEditor\\bin\\Debug\\net6.0\\Zrzut ekranu (4).png");

        MyImage = ChangeBrightness(MyImage,.5);

        Graphics.DrawImage(MyImage, 180F, 18F, MyImage.Width, MyImage.Height);


        SaveImage("EditedImage.png", MyImage);

    }
}