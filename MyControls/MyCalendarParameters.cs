using System;
using System.Collections.Generic;

namespace MyControls
{
    public class MyCalendarParameters
    {
        #region declaration
        public List<MyCalendarDayHighLight> Highlight;
        public string[] days; //= new string[7] { "Montag", "Dienstag", "Mittwoch", "Donnerstag", "Freitag", "Samstag", "Sonntag" };
        private int scaleTimeWidth;
        private int scaleTimeHeight;
        private int headerHeight;
        private int freeAppointmentHeight;
        private ScaleUnit zeiteinheit;
        private int marge;
        private Week_Beginn wBeginn;
        private Week woche;
        #endregion
        public enum ScaleUnit
        {
            OneMinute = 1,
            FiveMinute = 5,
            TenMinute = 10,
            FifteenMinute = 15,
            thirtyMinute = 30,
            OneHour = 60
        }
        public enum Week
        {
            Work = 5,
            Full = 7
        }
        public enum Week_Beginn
        {

            Montag = 1,
            Sonntag = 0

        }
        public enum DayOfWeek
        {
            Montag = 1,
            Dienstag = 2,
            Mittwoch = 3,
            Donnerstag = 4,
            Freitag = 5,
            Samstag = 6,
            Sonntag = 0
        }
        public void intializeCalendarParameters()
        {
            //days = new string[7] { "Montag", "Dienstag", "Mittwoch", "Donnerstag", "Freitag", "Samstag", "Sonntag" };
            Woche = Week.Full;
            Marge = 5;
            HeaderHeight = 20;
            FreeAppointmentHeight = 100;
            ScaleTimeWidth = 60;
            ScaleTimeHeight = 5;
            Zeiteinheit = ScaleUnit.FiveMinute;
            wBeginn = Week_Beginn.Montag;
            OnCreateHighlight();
        }
        public void OnCreateHighlight()
        {
            Highlight = new List<MyCalendarDayHighLight>
            { new MyCalendarDayHighLight(1, new TimeSpan(08, 00, 00), new TimeSpan(17, 00, 00)),
            new MyCalendarDayHighLight(2, new TimeSpan(08, 00, 00), new TimeSpan(17, 00, 00)),
            new MyCalendarDayHighLight(3, new TimeSpan(08, 00, 00), new TimeSpan(17, 00, 00)),
            new MyCalendarDayHighLight(4, new TimeSpan(08, 00, 00), new TimeSpan(17, 00, 00)),
            new MyCalendarDayHighLight(5, new TimeSpan(08, 00, 00), new TimeSpan(17, 00, 00)) };

        }

        #region getter/setter
        public int ScaleTimeWidth
        {
            get => scaleTimeWidth;
            set => scaleTimeWidth = value;
        }
        public int ScaleTimeHeight
        {
            get => scaleTimeHeight;
            set => scaleTimeHeight = value;
        }
        public int HeaderHeight
        {
            get => headerHeight;
            set => headerHeight = value;
        }
        public int FreeAppointmentHeight
        {
            get => freeAppointmentHeight;
            set => freeAppointmentHeight = value;
        }
        public ScaleUnit Zeiteinheit
        {
            get => zeiteinheit;
            set => zeiteinheit = value;
        }
        public Week_Beginn WochenBeginn
        {
            get => wBeginn;
            set => wBeginn = value;
        }
        public Week Woche
        {
            get => woche;
            set => woche = value;
        }
        public int Marge
        {
            get => marge;
            set => marge = value;
        }
        #endregion
    }
}
