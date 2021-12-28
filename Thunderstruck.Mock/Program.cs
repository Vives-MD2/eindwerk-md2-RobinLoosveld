using System;
using Microsoft.EntityFrameworkCore.Query;
using Thunderstruck.DAL.DBs;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.Mock
{
    class Program
    {
        //private readonly User
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //time test
            DateTime d = DateTime.Now; 
            Console.WriteLine(d.AddMilliseconds(152511).ToString("hh:mm:ss"));

            var test = new AchievementRain
            {
                Name= "test",
                Description = "more test",
                Reward = 10,
                Id=11,
                IsRaining = true,
            };
            Console.WriteLine(test.Description, test.Id);
        }
    }
}
