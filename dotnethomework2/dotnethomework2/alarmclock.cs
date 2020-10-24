using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace dotnethomework2
{
    class alarmclock
    {
        public delegate void alarmclockeventhanlder(alarmclock sender);
        public delegate void ticktimeventhander(alarmclock sender);
        public event ticktimeventhander tickevent;
        public event alarmclockeventhanlder alarmevent;
        public clock currenttime { get; set; }
        public alarmclock()
        {
            currenttime = new clock();
        }
        public clock alarmtime { get; set; }
        public void run()
        {
            DateTime time = DateTime.Now;
            currenttime = new clock(time.Hour, time.Minute, time.Second);
            tickevent(this);
            if(alarmtime.Equals(currenttime))
            {
                alarmevent(this);
                Thread.Sleep(500);
            }
        }
    }
    
}
