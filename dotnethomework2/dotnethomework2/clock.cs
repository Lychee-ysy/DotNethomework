using System;
using System.Collections.Generic;
using System.Text;

namespace dotnethomework2
{
    class clock
    {
        private int hour, minute, second;
        public clock(int hour=0, int minute=0, int second=0)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }
        public int hours
        {
            set
            {
                if (value < 0 || value > 24) Console.WriteLine("ERROR!");
                hours = value;
            }
            get
            {
                return hours;
            }
        }
        public int minutes
        {
            set
            {
                if (value < 0 || value > 60) Console.WriteLine("ERROR!");
                minutes = value;
            }
            get
            {
                return minutes;
            }
            
        }
        public int seconds
        {
            set
            {
                if (value < 0 || value > 60) Console.WriteLine("ERROR!");
                seconds = value;
            }
            get
            {
                return seconds;
            }
        }
        public override bool Equals(object obj)
        {
            var time = obj as clock;
            return (time != null) && (time.seconds == seconds) && (time.minutes == minutes) && (time.hours == hours);
        }
        public override int GetHashCode()
        {
            var hashCode = 1505761165;
            hashCode = hashCode * -1521134295 + hours.GetHashCode();
            hashCode = hashCode * -1521134295 + minutes.GetHashCode();
            hashCode = hashCode * -1521134295 + seconds.GetHashCode();
            return hashCode;
        }

    }

}
