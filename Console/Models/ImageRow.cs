using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Models
{
    public class ImageRow
    {
        public Bitmap Image { get; set; }
        public ICollection<ImageColumn> Columns { get; set; }
    }
}
