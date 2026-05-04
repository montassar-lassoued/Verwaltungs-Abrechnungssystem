using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    [Serializable]
    public class MyCalendarScaleTime
    {
        #region declaration
        private MyCalendar myCalendar;
        private MyCalendarProperties properties;
        private Rectangle bound;
        private TimeSpan timeBeginn;
        private TimeSpan timeEnd;
        // private int id;
        private string text;
        private string subject;
        private bool visible;
        private bool isSelected;
        private bool isHighlight;
        private Color color;
        private List<MyCalendarItem> concurence = new List<MyCalendarItem>();
        #endregion

        #region public methode
        public MyCalendarScaleTime()
        {

        }
        public MyCalendarScaleTime(TimeSpan _timeBeginn, TimeSpan _timeEnd, Rectangle _bound, string _text, bool _visible, bool _isSelected, bool _isHighlight)
        {
            timeBeginn = _timeBeginn;
            timeEnd = _timeEnd;
            bound = _bound;
            text = _text;
            Visible = _visible;
            IsSelected = _isSelected;
            IsHighlight = _isHighlight;
        }
        public MyCalendarScaleTime(MyCalendar _myCalendar, PaintEventArgs _PaintEvent)
        {
            myCalendar = _myCalendar;
            properties = myCalendar.properties;
        }
        public void insert(TimeSpan _timeBeginn, TimeSpan _timeEnd, Rectangle _bound, string _text)
        {
            timeBeginn = _timeBeginn;
            timeEnd = _timeEnd;
            bound = _bound;
            text = _text;
        }
        public void AddItem(MyCalendarItem _item)
        {
            concurence.Add(_item);
        }
        #endregion


        #region private methode
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
        public string Text
        {
            get => text;
            set => text = value;
        }
        public string Subject
        {
            get => subject;
            set => subject = value;
        }
        public bool Visible
        {
            get => visible;
            set => visible = value;
        }
        public bool IsSelected
        {
            get => isSelected;
            set => isSelected = value;
        }
        public bool IsHighlight
        {
            get => isHighlight;
            set => isHighlight = value;
        }
        public List<MyCalendarItem> Concurence => concurence;
        public Color Color
        {
            get => color;
            set => color = value;
        }
        #endregion
    }
}
