using System;
using System.Drawing;

namespace MyControls
{
    public class MyCalendarHeader
    {
        #region declaration
        private Rectangle bound;
        private bool isSelected;
        private string dayOfWeek;
        private string dateOfDay;
        #endregion
        #region public methode
        public MyCalendarHeader()
        {

        }
        public MyCalendarHeader(Rectangle _bound, DateTime _date)
        {
            Bound = _bound;
            OnCreateHeaderText(_date);
        }
        #endregion
        #region private methode
        private void OnCreateHeaderText(DateTime _date)
        {
            switch ((int)_date.DayOfWeek)
            {
                case 0: DayofWeek = "Sonntag"; break;
                case 1: DayofWeek = "Montag"; break;
                case 2: DayofWeek = "Dienstag"; break;
                case 3: DayofWeek = "Mittwoch"; break;
                case 4: DayofWeek = "Donnerstag"; break;
                case 5: DayofWeek = "Freitag"; break;
                case 6: DayofWeek = "Samstag"; break;
            }
            DateOfDay = _date.Day.ToString();
        }
        public void Clear()
        {
            Bound = Rectangle.Empty;
            IsSelected = false;
            DateOfDay = string.Empty;
            DayofWeek = string.Empty;
        }
        #endregion

        #region getter/setter
        public Rectangle Bound
        {
            get => bound;
            set => bound = value;
        }
        public bool IsSelected
        {
            get => isSelected;
            set => isSelected = value;
        }
        public string DayofWeek
        {
            get => dayOfWeek;
            set => dayOfWeek = value;
        }
        public string DateOfDay
        {
            get => dateOfDay;
            set => dateOfDay = value;
        }
        #endregion
    }
}
