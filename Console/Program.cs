using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Contracts;
using TestProject.Models;
using TestProject.Repositories;
using TestProject.Services;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new FileRepository();
            var path = "C:/Users/Sapik/Desktop/New folder";
            var names = new List<string>
            {
                "1.jpg",
                "2.jpg",
                "3.jpg",
                "4.jpg",
                "5.jpg",
            };

            var column = repository.GetColumn(path, names);
            var service = new ImageModifierService(repository);
            service.DrawStoryboard(column, 500);
        }
    }
}
