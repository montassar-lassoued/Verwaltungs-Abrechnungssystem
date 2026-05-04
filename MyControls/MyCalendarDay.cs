using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace MyControls
{
    public class MyCalendarDay
    {
        #region declaration
        private MyCalendar calendar;
        private DateTime date;
        private List<MyCalendarScaleTime> scaleTime = new List<MyCalendarScaleTime>();
        private MyCalendarScaleTime selectedscaleTime = new MyCalendarScaleTime();
        private Rectangle bound;
        private MyCalendarHeader header;
        private MyCalendarDayFreeAppointment dayFreeAppointment;
        private int id;
        private bool isSelected;
        private MyCalendarItem item = new MyCalendarItem();
        private bool createNewItem;
        #endregion

        #region public methode
        public MyCalendarDay()
        {

        }

        public MyCalendarDay(DateTime _date, List<MyCalendarScaleTime> _scaleTimes, Rectangle _bound)
        {
        }
        public MyCalendarDay(int _id, DateTime _dateDay)
        {
            ID = _id;
            Date = _dateDay;
        }
        public void OnCreateHeader(Rectangle _headerBound)
        {
            _Header = new MyCalendarHeader(_headerBound, date);

        }
        public void OnCreateFreeAppointment(Rectangle _FreeAppointBound)
        {
            DayFreeAppoint = new MyCalendarDayFreeAppointment(_FreeAppointBound);
        }
        public void OnCreateScaleTime(List<MyCalendarScaleTime> _scaleTime)
        {
            foreach (MyCalendarScaleTime _scale in _scaleTime)
            {
                ScaleTimes.Add(new MyCalendarScaleTime(_scale.TimeBeginn, _scale.TimeEnd, _scale.Bound, "", _scale.Visible, _scale.IsSelected, _scale.IsHighlight));
            }
        }
        public void OnCreateBorder(Rectangle _boundDay)
        {
            Bound = _boundDay;
        }
        public bool Equals(MyCalendarDay _day)
        {
            if (SelectedscaleTime.TimeBeginn == _day.SelectedscaleTime.TimeBeginn &&
                SelectedscaleTime.TimeEnd == _day.SelectedscaleTime.TimeEnd &&
                SelectedscaleTime.Subject == _day.SelectedscaleTime.Subject &&
                SelectedscaleTime.Text == _day.SelectedscaleTime.Text
                && Date == _day.Date)
            { return true; }
            else { return false; }

        }
        public void clear()
        {
            ScaleTimes.Clear();
            Bound = Rectangle.Empty;

        }
        #endregion

        #region private methode
        #endregion

        #region getter/setter
        public DateTime Date
        {
            get => date;
            set => date = value;
        }
        public List<MyCalendarScaleTime> ScaleTimes
        {
            get => scaleTime;
            set => scaleTime = value;
        }
        public Rectangle Bound
        {
            get => bound;
            set => bound = value;
        }
        public MyCalendarHeader _Header
        {
            get => header;
            set => header = value;
        }
        public MyCalendarDayFreeAppointment DayFreeAppoint
        {
            get => dayFreeAppointment;
            set => dayFreeAppointment = value;
        }
        public int ID
        {
            get => id;
            set => id = value;
        }
        public bool IsSelected
        {
            get => isSelected;
            set => isSelected = value;
        }
        [BrowsableAttribute(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MyCalendarScaleTime SelectedscaleTime
        {
            get => selectedscaleTime;
            set => selectedscaleTime = value;
        }
        public MyCalendarItem Item
        {
            get => item;
            set => item = value;
        }
        public bool CreateNewItem
        {
            get => createNewItem;
            set => createNewItem = value;
        }
        #endregion
    }
}
