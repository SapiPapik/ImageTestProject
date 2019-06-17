using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console.Models;
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

            var test = new ImageModifierService(repository);

            var r1 = new Row();
            var c1 = new Column();
            var r2 = new Row();
            var c2 = new Column();
            var r3 = new Row();

            r1.Add(repository.LoadFile(path + "/" + "1.jpg"))
                .Add(c1)
                .Add(repository.LoadFile(path + "/" + "2.jpg"));

            c1.Add(r2)
                .Add(repository.LoadFile(path + "/" + "3.jpg"));

            r2.Add(repository.LoadFile(path + "/" + "4.jpg"))
                .Add(c2);

            c2.Add(r3)
                .Add(repository.LoadFile(path + "/" + "5.jpg"));

            r3.Add(repository.LoadFile(path + "/" + "6.jpg")).Add(repository.LoadFile(path + "/" + "7.jpg"));

            test.Test<Row, Column>(r1, 1000);

            //c1.Add(repository.LoadFile(path + "/" + "1.jpg")).Add(repository.LoadFile(path + "/" + "2.jpg"))
            //    .Add(repository.LoadFile(path + "/" + "3.jpg")).Add(repository.LoadFile(path + "/" + "4.jpg"));
            //test.Test<Column, Row>(c1, 10);
        }
    }
}
