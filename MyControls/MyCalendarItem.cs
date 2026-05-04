using System;
using System.Drawing;

namespace MyControls
{
    public class MyCalendarItem
    {
        #region declaration
        private Rectangle bound;
        private DateTime startDate;
        private DateTime endDate;
        private int day;
        private int month;
        private int year;
        public TimeSpan timeBeginn;
        public TimeSpan timeEnd;
        private string subject;
        private string description;
        private Color color;
        private bool isSelected;
        private bool isVisible;
        private string id;
        private MyCalendarEventType eventType;
        private MyCalendarItemState state;

        #endregion
        #region public methode
        public MyCalendarItem()
        {

        }
        public MyCalendarItem(MyCalendarItem _item)
        {
            Bound = _item.Bound;
            Color = _item.Color;
            StartDate = _item.StartDate;
            EndDate = _item.endDate;
            Day = _item.startDate.Day;
            Month = _item.startDate.Month;
            Year = _item.startDate.Year;
            TimeBeginn = _item.TimeBeginn;
            TimeEnd = _item.TimeEnd;
            Subject = _item.Subject;
            Description = _item.Description;
            Id = _item.Id;
            EventType = _item.EventType;
            State = _item.State;

        }
        public MyCalendarItem(string _id, MyCalendarEventType _eventType, Rectangle _bound, DateTime _startDate, DateTime _endDate, TimeSpan _timeBeginn, TimeSpan _timeEnd,
            string _subject, string _description, Color _color, MyCalendarItemState _state)
        {
            Id = _id;
            EventType = _eventType;
            Bound = _bound;
            StartDate = _startDate;
            EndDate = _endDate;
            Day = _startDate.Day;
            Month = _startDate.Month;
            Year = _startDate.Year;
            TimeBeginn = _timeBeginn;
            TimeEnd = _timeEnd;
            subject = _subject;
            description = _description;
            Color = _color;
            State = _state;

        }
        public bool IntersectsWith(TimeSpan _timeBeginn, TimeSpan _timeEnd)
        {
            Rectangle r1 = Rectangle.FromLTRB(0, Convert.ToInt32(TimeBeginn.TotalMinutes), 5, Convert.ToInt32(TimeEnd.TotalMinutes));
            Rectangle r2 = Rectangle.FromLTRB(0, Convert.ToInt32(_timeBeginn.TotalMinutes), 5, Convert.ToInt32(_timeEnd.TotalMinutes - 1));
            return r1.IntersectsWith(r2);
        }
        #endregion
        #region getter/setter
        public Rectangle Bound
        {
            get => bound;
            set => bound = value;
        }
        public TimeSpan TimeBeginn
        {
            get => timeBeginn;
            set => timeBeginn = value;
        }
        public TimeSpan TimeEnd
        {
            get => timeEnd;
            set => timeEnd = value;
        }
        public string Subject
        {
            get => subject;
            set => subject = value;
        }
        public string Description
        {
            get => description;
            set => description = value;
        }
        public DateTime StartDate
        {
            get => startDate;
            set => startDate = value;
        }
        public bool IsSelected
        {
            get => isSelected;
            set => isSelected = value;
        }
        public bool IsVisible
        {
            get => isVisible;
            set => isVisible = value;
        }
        public Color Color
        {
            get => color;
            set => color = value;

        }
        public int Day
        {
            get => day;
            set => day = value;
        }
        public int Month
        {
            get => month;
            set => month = value;
        }
        public int Year
        {
            get => year;
            set => year = value;
        }
        public string Id { get => id; set => id = value; }
        public MyCalendarEventType EventType { get => eventType; set => eventType = value; }
        public MyCalendarItemState State { get => state; set => state = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        #endregion
    }
}
