using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Invert
    {
        internal static Image invert(Image original)
        {
            Bitmap myImage = new Bitmap(original);
            BitmapData imageData = myImage.LockBits(new Rectangle(0, 0, myImage.Width, myImage.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int stride = imageData.Stride;
            IntPtr Scan0 = imageData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - myImage.Width * 4;
                int nWidth = myImage.Width;

                for (int y = 0; y < myImage.Height; y++)
                {
                    for (int x = 0; x < nWidth; x++)
                    {
                        p[0] = (byte)(255 - p[0]);
                        p[1] = (byte)(255 - p[1]);
                        p[2] = (byte)(255 - p[2]);
                        p += 4;
                    }
                    p += nOffset;
                }
            }

            myImage.UnlockBits(imageData);
            return (Image)myImage;
        }
    }
}
