using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console.Models;
using Console.Services;
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
            //var names = new List<string>
            //{
            //    "1.jpg",
            //    "2.jpg",
            //    "3.jpg",
            //    "4.jpg",
            //    "5.jpg",
            //};

            //var column = repository.GetColumn(path, names);
            //var service = new ImageModifierService(repository);
            //service.DrawStoryboard(column, 500);

            var test = new Blabla(repository);

            var r1 = new Row();
            var c1 = new Column();
            var r2 = new Row();
            var c2 = new Column();
            var r3 = new Row();

            //r1.Add(repository.LoadFile(path + "/" + "1.jpg"))
            //    .Add(c1)
            //    .Add(repository.LoadFile(path + "/" + "2.jpg"));

            //c1.Add(r2)
            //    .Add(repository.LoadFile(path + "/" + "3.jpg"));

            //r2.Add(repository.LoadFile(path + "/" + "4.jpg"))
            //    .Add(c2);

            //c2.Add(r3)
            //    .Add(repository.LoadFile(path + "/" + "5.jpg"));

            //r3.Add(repository.LoadFile(path + "/" + "6.jpg")).Add(repository.LoadFile(path + "/" + "7.jpg"));
            c1.Add(repository.LoadFile(path + "/" + "1.jpg")).Add(repository.LoadFile(path + "/" + "2.jpg"))
                .Add(repository.LoadFile(path + "/" + "3.jpg")).Add(repository.LoadFile(path + "/" + "4.jpg"));

            //test.Test<Row, Column>(r1, 10);
            test.Test<Column, Row>(c1, 10);
            
        }
    }
}
