using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Contracts;
using TestProject.Models;
using TestProject.Repositories;

namespace TestProject.Services
{
    public class ImageModifierService : IImageModiferService
    {
        private readonly FileRepository _fileRepository;

        public ImageModifierService(FileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public void DrawStoryboard(ImageRow row, int width)
        {
            
        }

        public void DrawStoryboard(ImageColumn column, int width)
        {
            var averageHeight = Round((double)column.Rows.Sum(c => c.Image.Height) / column.Rows.Count);

            foreach (var columnRow in column.Rows)
            {
                if (columnRow?.Image == null) throw new ArgumentException(nameof(columnRow));

                var averageWidth = Round((double)columnRow.Image.Width * averageHeight / columnRow.Image.Height);
                columnRow.Image = ResizeImage(columnRow.Image, averageWidth, averageHeight);
            }

            var currentWidth = column.Rows.Sum(r => r.Image.Width);
            var widthDifference = currentWidth - width;
            if (widthDifference != 0)
            {
                if (widthDifference > 0)
                {
                    var smoothingFactor = 1 - Math.Abs((double)widthDifference) / currentWidth;
                    foreach (var columnRow in column.Rows)
                    {
                        columnRow.Image = ResizeImage(columnRow.Image, 
                            Round(columnRow.Image.Width * smoothingFactor),
                            Round(columnRow.Image.Height * smoothingFactor));
                    }
                }
                else
                {
                    var smoothingFactor = Math.Abs((double)widthDifference) / currentWidth;
                    foreach (var columnRow in column.Rows)
                    {
                        columnRow.Image = ResizeImage(columnRow.Image,
                            Round(columnRow.Image.Width + columnRow.Image.Width * smoothingFactor),
                            Round(columnRow.Image.Height + columnRow.Image.Height * smoothingFactor));
                    }
                }
            }

            var result = MergedBitmaps(column.Rows.Select(r => r.Image).ToList());

            _fileRepository.SaveFile("C:\\Users\\Sapik\\Desktop\\result.png", result);
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

        private Bitmap MergedBitmaps(ICollection<Bitmap> bitmaps)
        {
            Bitmap result = new Bitmap(bitmaps.Sum(b => b.Width),
                bitmaps.Max(b => b.Height));
            using (Graphics graphics = Graphics.FromImage(result))
            {
                Point temp = new Point(0,0);
                foreach (var bitmap in bitmaps)
                {
                    graphics.DrawImage(bitmap, temp);
                    temp.X += bitmap.Width;
                }
            }
            return result;
        }

        private int Round(double value)
        {
            return Convert.ToInt32(Math.Round(value, MidpointRounding.AwayFromZero));
        }
    }
}
