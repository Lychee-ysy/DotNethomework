using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace dotnethomework2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                alarmclock newclock = new alarmclock();
                newclock.alarmtime = new clock(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                newclock.tickevent += showtime;
                newclock.alarmevent += alarming;
                new Thread(newclock.run).Start();
                Console.ReadKey();
            }
            catch
            {
                Console.WriteLine("something worng!");
            }
        }
        private static void showtime(alarmclock sender)
        {
            clock time = sender.currenttime;
            Console.WriteLine("Now Time is:{0},{1},{2}", time.hours, time.minutes, time.seconds);
        }
        public static void alarming(alarmclock sender)
        {
            clock time = sender.currenttime;
            Console.WriteLine("Now Time is:{0},{1},{2}", time.hours, time.minutes, time.seconds);
            Console.WriteLine("alarming!get up now!");
        }
    }
}
