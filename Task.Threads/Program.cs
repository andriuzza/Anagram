using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task.Threads
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadLocal<Apartment> CURRENT = new ThreadLocal<Apartment>(() =>
            {
                return new Apartment();
            }, false);

            CURRENT.Value.ChangeArea(15);

            new Thread(() =>
            {
                CURRENT.Value.ChangeArea(12);
            }).Start();

            new Thread(() =>
            {
                CURRENT.Value.ChangeArea(18);
            }).Start();


            new Thread(() =>
            {
                CURRENT.Value.ChangeArea(19);
            }).Start();

            CURRENT.Value.Get();
        }   
    }
    public class Apartment
    {

        private int Area;

        public void ChangeArea(int number)
        {
             Area = number;
           
        }

        public void Get()
        {
            Console.WriteLine(Area);
        }

    }
}
