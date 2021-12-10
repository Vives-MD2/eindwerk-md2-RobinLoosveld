using System;

namespace Thunderstruck.Mock
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //time test
            DateTime d = DateTime.Now; 
            Console.WriteLine(d.AddMilliseconds(152511).ToString("hh:mm:ss"));
        }
    }
}
