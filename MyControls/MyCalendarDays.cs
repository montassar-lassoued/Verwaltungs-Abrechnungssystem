using System;
using System.Collections.Generic;
using System.Drawing;
using static MyControls.Globals;

namespace MyControls
{
    public class MyCalendarDays
    {
        #region declaration
        private List<MyCalendarDay> day = new List<MyCalendarDay>();
        public int count;
        #endregion

        #region public methode
        public MyCalendarDays()
        {

        }

        public void AddDay(int _index, MyCalendarDay _day, DateTime _date)
        {
            _Day.Insert(_index, _day);
        }
        public MyCalendarDay GetSelectedDay(Point _location)
        {
            foreach (MyCalendarDay _day in day)
            {
                if (_day.Bound.Contains(_location))
                {
                    _day.IsSelected = true;
                    return _day;
                }
            }
            return null;
        }
        public void ClearSelectedDay()
        {
            foreach (MyCalendarDay _day in day)
            {
                if (_day.IsSelected)
                {
                    _day.IsSelected = false;
                    _day._Header.IsSelected = false;
                    _day.DayFreeAppoint.IsSelected = false;
                    foreach (MyCalendarScaleTime Time in _day.ScaleTimes)
                    {
                        //if (!Time.Visible) { continue; }
                        if (Time.IsSelected)
                        {
                            Time.IsSelected = false;
                        }
                    }
                }
            }
        }
        public int OnGetIndexOf(MyCalendarDay _day, Point _location)
        {

            if (_day._Header.Bound.Contains(_location))
            {
                return HEADER;
            }
            if (_day.DayFreeAppoint.Bound.Contains(_location))
            {
                return FREEAPPOINTEMENT;
            }
            foreach (MyCalendarScaleTime Time in _day.ScaleTimes)
            {
                if (!Time.Visible) { continue; }
                if (Time.Bound.Contains(_location))
                {
                    return ITEM_OR_SCALE;
                }
            }
            return NONE;
        }
        /*public int OnGetIndexOf(Day _day, Point _location)
        {
            if (_day._Header.Bound.Contains(_location))
            {
                return Header_Idx;
            }
            if (_day.DayFreeAppoint.Bound.Contains(_location))
            {
                return freeAppointement_Idx;
            }
            foreach (ScaleTime Time in _day.ScaleTimes)
            {
                if (!Time.Visible) { continue; }
                if (Time.Bound.Contains(_location))
                {
                    return _day.ScaleTimes.IndexOf(Time);
                }
            }
            return None_Idx;
        }*/
        public MyCalendarScaleTime OnGetSelectedScaleTime(MyCalendarDay _day, int _index, bool _state)
        {
            return _Day[_Day.IndexOf(_day)].ScaleTimes[_index];
        }
        public MyCalendarDay GetDayFromDate(DateTime date)
        {
            foreach (MyCalendarDay dy in _Day)
            {
                if (dy.Date.Date == date.Date)
                {
                    return dy;
                }
            }
            return null;
        }
        #endregion

        #region getter/setter
        public List<MyCalendarDay> _Day
        {
            get => day;
            set { List<MyCalendarDay> day = value; }
        }
        #endregion
    }
}
