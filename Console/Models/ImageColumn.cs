using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TestProject.Models
{
    public class ImageColumn
    {
        public Bitmap Image { get; set; }
        public ICollection<ImageRow> Rows { get; set; }

        public ImageColumn()
        {
            Rows = new List<ImageRow>();
        }
    }
}
