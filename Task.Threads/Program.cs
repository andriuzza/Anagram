using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Task.Threads
{
    class Program
    {

        static void Main(string[] args)
        {
            Apartment ap = null;

            ap = new Apartment();
      
            Console.WriteLine(ap.GetHashCode());

            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Thread T1 = new Thread(() =>
            {
                Console.WriteLine(ap.GetHashCode());
                ap.CURRENT.Value = true;
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                ap.ChangeArea(15);
            });

            T1.Start();

            Thread.Sleep(1000);
            Console.WriteLine("---");
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            ap.CURRENT.Value = true;
            ap.ChangeArea(20);

            /*  Thread T2 = new Thread(() =>
             {
                 ap.ChangeArea(188);
             });

             T2.Start();*/

            //  Thread.Sleep(1000);


            ap.Get();

        }
        
        public static void MoveManyFiles()
        {

            string fileExtension = ".txt";

            string[] txtFiles = Directory.GetFiles("Source Path", fileExtension);

            foreach (var item in txtFiles)
            {
                File.Move(item, Path.Combine("Destination Directory", Path.GetFileName(item)));
            }
        }

    }
    public class Apartment
    {
        public ThreadLocal<bool> CURRENT = new ThreadLocal<bool>(() =>
        {
            return false;
        });


        private int Area;

        public void ChangeArea(int number)
        {
            if(CURRENT.Value == true)
            {
                Area = number;
            }
            else
            {
                throw new Exception();
            }

        }

        public void Get()
        {
            Console.WriteLine(Area);
        }

    }
}
