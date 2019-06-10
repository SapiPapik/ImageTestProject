using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Repositories
{
    public class FileRepository
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
    }
}
