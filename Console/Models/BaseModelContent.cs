using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console.Services;

namespace Console.Models
{
    public abstract class BaseModelContent<T> where T: class
    {
        public T Child { get; set; }
        public int ChildPosition { get; set; }
        public List<Bitmap> Content { get; set; }

        protected BaseModelContent()
        {
            Content = new List<Bitmap>();
        }

        public BaseModelContent<T> Add(object obj)
        {
            switch (obj)
            {
                case T child:
                    if (Child != null) throw new ArgumentException("Child already installed");
                    Child = child;
                    ChildPosition = Content.Count;
                    break;
                case Bitmap bitmap:
                    Content.Add(bitmap);
                    break;
                default:
                    throw new ArgumentException("Object can be only Bitmap or Column type");
            }

            return this;
        }
    }
}
