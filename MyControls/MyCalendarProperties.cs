using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static MyControls.Globals;
using static MyControls.MyCalendarParameters;

namespace MyControls
{
    public class MyCalendarProperties
    {
        #region declaration
        public MyCalendarElementsLayout layout;
        private MyCalendar myCalendar;
        public PaintEventArgs paintEvent;
        private ScaleUnit scaleUnit;
        private MyCalendarUnitScaleTime myCalendarUnitsScaleTime;
        private Week week;
        private int timeFactor;
        private DateTime scaleTimeStar;
        private int headerHeight;
        private int freeAppointmentHeight;
        private Rectangle timeScaleBound;
        public MyCalendarDay day;
        private int scaleTimeWidth;
        private int scaleTimeHeight;
        private int marge;
        private int dayWidth;
        private int leftPoint;
        private List<MyCalendarDayHighLight> Highlight = new List<MyCalendarDayHighLight>();

        #endregion

        #region public methode
        public MyCalendarProperties(MyCalendar _calendar, PaintEventArgs _calendarPaintEvent)
        {
            myCalendar = _calendar;
            paintEvent = _calendarPaintEvent;
            layout = new MyCalendarElementsLayout();
        }
        public MyCalendarProperties(MyCalendar _calendar)
        {
            myCalendar = _calendar;
            layout = new MyCalendarElementsLayout();
        }

        public ScaleUnit ScaleTimeUnite
        {
            set
            {
                scaleUnit = value;
                UnitTimeFactor();
            }
            get => scaleUnit;
        }
        public void StandardSettingsApply()
        {
            layout.StandardLayout();
            CalendarLayout(myCalendar);
            OnCreateHighlight();
            InitializeScaleTime(myCalendar);
        }
        public DateTime FirstWeekDay(DateTime _date)
        {
            int days;
            switch ((int)_date.DayOfWeek)
            {
                case 0:
                    days = -6;
                    break;
                default:
                    days = 1 - (int)_date.DayOfWeek;
                    break;
            }
            return _date.AddDays(days);
        }
        #endregion

        #region private methode
        private void UnitTimeFactor()
        {
            int factor = 1;
            switch (ScaleTimeUnite)
            {
                case ScaleUnit.OneHour:
                    factor = 1;
                    break;
                case ScaleUnit.thirtyMinute:
                    factor = 2;
                    break;
                case ScaleUnit.FifteenMinute:
                    factor = 4;
                    break;
                case ScaleUnit.TenMinute:
                    factor = 6;
                    break;
                case ScaleUnit.FiveMinute:
                    factor = 12;
                    break;
                case ScaleUnit.OneMinute:
                    factor = 60;
                    break;
            }

            ScaleTimeFactor = 24 * factor;
            InitializeScaleUnit();

        }
        private void InitializeScaleUnit()
        {
            UnitsScaleTime = new MyCalendarUnitScaleTime(myCalendar, paintEvent);
            myCalendarUnitsScaleTime._Header.Bound = new Rectangle(myCalendar.ClientRectangle.Left, myCalendar.ClientRectangle.Top, scaleTimeWidth, headerHeight);
            myCalendarUnitsScaleTime.DayFreeAppoint.Bound = new Rectangle(myCalendarUnitsScaleTime._Header.Bound.Left, myCalendarUnitsScaleTime._Header.Bound.Height, scaleTimeWidth, FreeAppointmentHeight);

            myCalendar.DataSource = new List<TimeSpan>();
            TimeSpan scaleTimeNext = new TimeSpan(0, scaleTimeStar.Hour, scaleTimeStar.Minute, scaleTimeStar.Second);

            int top = myCalendarUnitsScaleTime.DayFreeAppoint.Bound.Height + myCalendarUnitsScaleTime._Header.Bound.Height;
            double Bmin = 0;
            double Emin = 0;
            for (int i = 0; i < ScaleTimeFactor; i++)
            {
                Rectangle bound = new Rectangle(0, top, ScaleTimeWidth, ScaleTimeHeight);
                TimeSpan timeBeginn = TimeSpan.FromMinutes(Bmin);
                Emin = Bmin + 60 / (ScaleTimeFactor / 24);
                TimeSpan timeEnd = TimeSpan.FromMinutes(Emin);
                MyCalendarScaleTime scaleTime = new MyCalendarScaleTime(myCalendar, paintEvent);
                scaleTime.insert(scaleTimeNext.Add(timeBeginn), scaleTimeNext.Add(timeEnd), bound, "");
                myCalendarUnitsScaleTime.scaleTime.Insert(i, scaleTime);
                myCalendar.DataSource.Insert(i, timeBeginn);
                Bmin += 60 / (ScaleTimeFactor / 24);
                top += ScaleTimeHeight;
            }

            myCalendar.drawElements.Performance();
            OnCreateDay();
        }
        private void OnCreateDay()
        {
            myCalendar.days = new MyCalendarDays();
            leftPoint = timeScaleBound.Right;
            DayWidth = (myCalendar.ClientSize.Width - (Marge + ScaleTimeWidth)) / (int)_Week;

            for (int i = 0; i < (int)_Week; i++)
            {
                day = new MyCalendarDay(i, myCalendar.FirstDateShow.AddDays(i));


                OnCreateDayBorder();

                if (myCalendar.DaySelected != null && myCalendar.DaySelected.Bound != Rectangle.Empty
                    && day.Bound.Equals(myCalendar.DaySelected.Bound) && myCalendar.DaySelected.IsSelected)
                {
                    day.IsSelected = true;
                }
                else
                {
                    day.IsSelected = false;
                }
                OnCreateDayHeader();
                OnCreateDayFreeAppointment();
                OnCreateDayScaleTime();

                myCalendar.days.AddDay(i, day, DateTime.Now);
                leftPoint += dayWidth;
            }
        }

        private void OnCreateDayBorder()
        {
            Rectangle _bound = new Rectangle(leftPoint, myCalendar.Bounds.Top, DayWidth, myCalendar.Bounds.Height);
            day.OnCreateBorder(_bound);
        }
        private void OnCreateDayHeader()
        {
            Rectangle _bound = new Rectangle(day.Bound.Left, day.Bound.Top, day.Bound.Width, HeaderHeight);
            day.OnCreateHeader(_bound);
            day._Header.IsSelected = IsSelected(HEADER, null);
        }
        private void OnCreateDayFreeAppointment()
        {
            Rectangle _bound = new Rectangle(day.Bound.Left, day._Header.Bound.Bottom, day.Bound.Width, freeAppointmentHeight);
            day.OnCreateFreeAppointment(_bound);
            day.DayFreeAppoint.IsSelected = IsSelected(FREEAPPOINTEMENT, null);
        }
        private void OnCreateDayScaleTime()
        {
            List<MyCalendarScaleTime> _scaleTimeDay = new List<MyCalendarScaleTime>();
            MyCalendarDayHighLight highlight = Highlight.Find(delegate (MyCalendarDayHighLight dy) { return dy.DayofWeek == (int)day.Date.DayOfWeek; });


            int top = day._Header.Bound.Height + day.DayFreeAppoint.Bound.Height;
            for (int j = 0; j < UnitsScaleTime.scaleTime.Count; j++)
            {
                if (!UnitsScaleTime.scaleTime[j].Visible) { continue; }

                MyCalendarScaleTime scale = new MyCalendarScaleTime();
                scale = UnitsScaleTime.scaleTime[j];
                scale.Bound = new Rectangle(leftPoint, top, dayWidth, scaleTimeHeight);
                scale.IsSelected = IsSelected(dayScaleTime, scale);
                scale.Visible = true;
                scale.IsHighlight = false;

                if (highlight != null)
                {
                    if (scale.TimeBeginn >= highlight.Beginn && scale.TimeEnd <= highlight.End)
                    {
                        scale.IsHighlight = true;
                    }
                }
                _scaleTimeDay.Add(scale);
                top += ScaleTimeHeight;

            }
            day.OnCreateScaleTime(_scaleTimeDay);
        }


        private void CalendarLayout(MyCalendar _calendar)
        {
            _calendar.BackColor = layout.CalendarColor;
        }
        private void OnCreateHighlight()
        {
            Highlight = myCalendar.parameters.Highlight;

        }
        private void InitializeScaleTime(MyCalendar _calendar)
        {
            int scaleHourStar = 00;
            int scaleMinuteStar = 00;
            int scaleSecondStar = 00;

            _Week = (Week)myCalendar.parameters.Woche;
            Marge = myCalendar.parameters.Marge;
            HeaderHeight = myCalendar.parameters.HeaderHeight;
            FreeAppointmentHeight = myCalendar.parameters.FreeAppointmentHeight;
            ScaleTimeWidth = myCalendar.parameters.ScaleTimeWidth;
            ScaleTimeHeight = myCalendar.parameters.ScaleTimeHeight;
            TimeScaleBound = new Rectangle(0, 0, ScaleTimeWidth, _calendar.ClientSize.Height);

            DateTime dateWeekBeginn = FirstWeekDay(DateTime.Today);
            ScaleTimeStar = new DateTime(dateWeekBeginn.Year, dateWeekBeginn.Month, dateWeekBeginn.Day, scaleHourStar, scaleMinuteStar, scaleSecondStar);

            ScaleTimeUnite = (ScaleUnit)myCalendar.parameters.Zeiteinheit;
        }
        private bool IsSelected(int _selected, MyCalendarScaleTime _scale)
        {
            bool _return = false;
            if (day.IsSelected)
            {
                switch (_selected)
                {
                    case header:
                        _return = myCalendar.DaySelected._Header.IsSelected;
                        break;
                    case freeAppointement:
                        _return = myCalendar.DaySelected.DayFreeAppoint.IsSelected;
                        break;
                    case dayScaleTime:
                        int _index = myCalendar.DaySelected.ScaleTimes.FindIndex(
                            delegate (MyCalendarScaleTime scaleTime)
                            {
                                return scaleTime.TimeBeginn == _scale.TimeBeginn
                                 && scaleTime.TimeEnd == _scale.TimeEnd;
                            });
                        if (_index < 0 || _index >= myCalendar.DaySelected.ScaleTimes.Count)
                        { return false; }
                        _return = myCalendar.DaySelected.ScaleTimes[_index].IsSelected;
                        break;
                }

            }
            return _return;
        }
        #endregion
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

        public int ScaleTimeFactor
        {
            get => timeFactor;
            set => timeFactor = value;
        }
        public DateTime ScaleTimeStar
        {
            get => scaleTimeStar;
            set => scaleTimeStar = value;
        }
        public Rectangle TimeScaleBound
        {
            get => timeScaleBound;
            set => timeScaleBound = value;
        }
        public MyCalendarUnitScaleTime UnitsScaleTime
        {
            get => myCalendarUnitsScaleTime;
            set => myCalendarUnitsScaleTime = value;
        }
        public int Marge
        {
            get => marge;
            set => marge = value;
        }
        public int DayWidth
        {
            get => dayWidth;
            set => dayWidth = value;

        }
        public Week _Week
        {
            get => week;
            set => week = value;
        }

        #endregion
    }
}
