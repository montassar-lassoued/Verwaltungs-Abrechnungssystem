using System.Collections.Generic;
using System.Windows.Forms;

namespace MyControls
{
    public class MyCalendarUnitScaleTime
    {
        #region declaration
        private MyCalendar myCalendar;
        private MyCalendarProperties myCalendarProperties;
        private PaintEventArgs calendarPaintEvent;
        public List<MyCalendarScaleTime> scaleTime;
        private MyCalendarHeader myCalendarHeader;
        private MyCalendarDayFreeAppointment dayFreeAppointment;
        #endregion

        #region public methode
        public MyCalendarUnitScaleTime(MyCalendar _calendar, PaintEventArgs _calendarPaintEvent)
        {
            myCalendar = _calendar;
            myCalendarProperties = myCalendar.properties;
            calendarPaintEvent = _calendarPaintEvent;
            scaleTime = new List<MyCalendarScaleTime>();
            _Header = new MyCalendarHeader();
            DayFreeAppoint = new MyCalendarDayFreeAppointment();
        }


        #endregion
        #region getter/setter
        public MyCalendarHeader _Header
        {
            get => myCalendarHeader;
            set => myCalendarHeader = value;
        }
        public MyCalendarDayFreeAppointment DayFreeAppoint
        {
            get => dayFreeAppointment;
            set => dayFreeAppointment = value;
        }
        #endregion
    }
}
