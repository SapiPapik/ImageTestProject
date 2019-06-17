using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console.Models;
using TestProject.Repositories;

namespace TestProject.Services
{
    public class ImageModifierService
    {
        private readonly FileRepository _fileRepository;

        public ImageModifierService(FileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public void Test<T, U>(T row, int width) where T : BaseModelContent<U> where U : BaseModelContent<T>
        {
            var bitmap = Test<T, U>(row);
            var averageHeight = Round((double)bitmap.Height * width / bitmap.Width);
            bitmap = ResizeImage(bitmap, width, averageHeight);
            _fileRepository.SaveFile("C:\\Users\\Sapik\\Desktop\\result.png", bitmap);
        }

        private Bitmap Test<T, U>(T row) where T : BaseModelContent<U> where U : BaseModelContent<T>
        {
            if (row.Child != null)
            {
                var bitmap = Test<U, T>(row.Child);
                row.Content.Insert(row.ChildPosition, bitmap);
                return DrawStoryboard<T, U>(row.Content);
            }

            return DrawStoryboard<T, U>(row.Content);
        }

        public Bitmap DrawStoryboard<T, U>(ICollection<Bitmap> bitmaps) where T : BaseModelContent<U> where U : BaseModelContent<T>
        {
            int average;
            var result = new List<Bitmap>();
            if (typeof(T) == typeof(Row))
            {
                average = Round((double)bitmaps.Sum(c => c.Height) / bitmaps.Count);
                foreach (var bitmap in bitmaps)
                {
                    var averageWidth = Round((double)bitmap.Width * average / bitmap.Height);
                    result.Add(ResizeImage(bitmap, averageWidth, average));
                }

                return MergedBitmaps(result, typeof(T) == typeof(Row) ? TypeOfContent.Row : TypeOfContent.Column);
            }
            else
            {
                average = Round((double)bitmaps.Sum(c => c.Width) / bitmaps.Count);
                foreach (var bitmap in bitmaps)
                {
                    var averageHeight = Round((double)bitmap.Height * average / bitmap.Width);
                    result.Add(ResizeImage(bitmap, average, averageHeight));
                }

                return MergedBitmaps(result, typeof(T) == typeof(Row) ? TypeOfContent.Row : TypeOfContent.Column);
            }
        }


        private Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            //maintains DPI regardless of physical size
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                //determines whether pixels from a source image overwrite or are combined with background pixels. SourceCopy specifies that when a color is rendered, it overwrites the background color.
                graphics.CompositingMode = CompositingMode.SourceCopy;
                //determines the rendering quality level of layered images.
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                //determines how intermediate values between two endpoints are calculated
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //specifies whether lines, curves, and the edges of filled areas use smoothing (also called antialiasing) -- probably only works on vectors
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                //affects rendering quality when drawing the new image
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    //prevents ghosting around the image border
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private Bitmap MergedBitmaps(ICollection<Bitmap> bitmaps, TypeOfContent typeOfContent)
        {
            Bitmap result;
            if (typeOfContent == TypeOfContent.Row)
            {
                result = new Bitmap(bitmaps.Sum(b => b.Width),
                    bitmaps.Max(b => b.Height));
            }
            else
            {
                result = new Bitmap(bitmaps.Max(b => b.Width),
                    bitmaps.Sum(b => b.Height));
            }
            using (Graphics graphics = Graphics.FromImage(result))
            {
                Point temp = new Point(0, 0);
                foreach (var bitmap in bitmaps)
                {
                    graphics.DrawImage(bitmap, temp);
                    if (typeOfContent == TypeOfContent.Column)
                    {
                        temp.Y += bitmap.Height;
                    }
                    else
                    {
                        temp.X += bitmap.Width;
                    }
                }
            }
            return result;
        }

        private int Round(double value)
        {
            return Convert.ToInt32(Math.Round(value, MidpointRounding.AwayFromZero));
        }
    }

    public enum TypeOfContent
    {
        Column,
        Row
    }
}
