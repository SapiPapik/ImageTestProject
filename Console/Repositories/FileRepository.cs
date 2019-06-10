using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Contracts;
using TestProject.Models;

namespace TestProject.Repositories
{
    public class FileRepository : IBaseFileRepository
    {
        public void SaveFile(string directory, Bitmap file)
        {
            file.Save(directory, ImageFormat.Png);
        }

        public Bitmap LoadFile(string directory)
        {
            try
            {
                return new Bitmap(directory);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                throw;
            }
        }

        public ImageColumn GetColumn(string path, ICollection<string> names)
        {
            var result = new ImageColumn();
            foreach (var name in names)
            {
                var image = LoadFile(path + "/" + name);
                result.Rows.Add(new ImageRow { Image = image });
            }

            return result;
        }
    }
}
