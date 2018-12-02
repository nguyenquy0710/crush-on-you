using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CoYqlp.WepApp.Helpers
{
    public class ImageHelpers
    {
        public static void SaveJpeg(string path, Bitmap img)
        {
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, (long)1000);

            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, jpegCodec, encoderParams);
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        public static void CropImage(string sourceImg, string desPath, int width, int height)
        {
            using (Bitmap img = (Bitmap)Image.FromFile(sourceImg))
            {
                Rectangle cropArea = new Rectangle(0, 0, 0, 0);
                cropArea.Width = Math.Min(Math.Min(img.Width, img.Height), width);
                cropArea.Height = Math.Min(Math.Min(img.Width, img.Height), height);

                Bitmap bmpCrop = img.Clone(cropArea, img.PixelFormat);

                SaveJpeg(desPath, bmpCrop);
            };
        }

        public static void SaveResizedImage(string filePath, string filename, string resizedFilename, int percent)
        {
            Image origImg = Image.FromFile(Path.Combine(filePath, filename));
            Image resizedImg = ScaleByPercent(origImg, percent);

            resizedImg.Save(Path.Combine(filePath, resizedFilename), ImageFormat.Jpeg);
            resizedImg.Dispose();
            origImg.Dispose();
        }

        public static void SaveResizedImage(string filePath, string filename, string resizedFilename, int width, int height, bool flexibleOrientation)
        {
            int quality = 100;
            Image origImg = Image.FromFile(Path.Combine(filePath, filename));
            Image resizedImg = FixedSize(origImg, width, height, flexibleOrientation);

            // check the quality passed in

            //if ((quality < 0) || (quality > 100))
            //{
            //    string error = string.Format("quality must be 0, 100", quality);
            //    throw new ArgumentOutOfRangeException(error);

            //}

            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            string lookupKey = "image/jpeg";
            var jpegCodec = ImageCodecInfo.GetImageEncoders().Where(i => i.MimeType.Equals(lookupKey)).FirstOrDefault();

            //create a collection of EncoderParameters and set the quality parameter

            var encoderParams = new EncoderParameters(1);

            encoderParams.Param[0] = qualityParam;

            //save the image using the codec and the encoder parameter
            resizedImg.Save(Path.Combine(filePath, resizedFilename), jpegCodec, encoderParams);
            resizedImg.Dispose();
            origImg.Dispose();
        }

        public static Image ScaleByPercent(Image imgPhoto, int percent)
        {
            float nPercent = ((float)percent / 100);
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            grPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }

        public static Image FixedSize(Image imgPhoto, int Width, int Height, bool flexibleOrientation)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            if ((sourceWidth < sourceHeight) && flexibleOrientation)
            {
                int tmp = Height;
                Height = Width;
                Width = tmp;
            }
            if (sourceWidth < Width)
            {
                Height = sourceWidth * Height / Width;
                Width = sourceWidth;
            }
            if (sourceHeight < Height)
            {
                Width = sourceHeight * Width / Height;
                Height = sourceHeight;
            }

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentW > 1)
            {
                Width = sourceWidth;
                nPercentW = 1;
            }
            if (nPercentH > 1)
            {
                Height = sourceHeight;
                nPercentH = 1;
            }

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentW;
                sourceY = System.Convert.ToInt16((sourceHeight - (Height / nPercent)) / 2);
                sourceHeight = System.Convert.ToInt16(Height / nPercent);
            }
            else
            {
                nPercent = nPercentH;
                //destY = System.Convert.ToInt16((Height - (sourceHeight * nPercent)) / 2);
                sourceX = System.Convert.ToInt16((sourceWidth - (Width / nPercent)) / 2);
                sourceWidth = System.Convert.ToInt16(Width / nPercent);
            }

            //int destWidth = (int)(sourceWidth * nPercent);
            //int destHeight = (int)(sourceHeight * nPercent);
            int destWidth = Width;
            int destHeight = Height;

            Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format48bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Transparent);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);
            grPhoto.Dispose();

            return bmPhoto;
        }

        /// <summary>
        /// Chuyển từ dạng Base64 sang dạng hình ảnh
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static Image Base64ToImage(string base64String)
        {
            var base64Data = Regex.Match(base64String, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            var binData = Convert.FromBase64String(base64Data);

            using (var stream = new MemoryStream(binData))
            {
                Image image = Image.FromStream(new MemoryStream(binData));
                return image;
            }
        }
    }
}