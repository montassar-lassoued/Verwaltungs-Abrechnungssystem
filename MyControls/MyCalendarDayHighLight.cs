using System;

namespace MyControls
{
    public class MyCalendarDayHighLight
    {
        #region derclaration
        private int dayofWeek;
        private TimeSpan beginn;
        private TimeSpan end;
        #endregion

        #region konstruktor
        public MyCalendarDayHighLight(int _dayofWeek, TimeSpan _beginn, TimeSpan _end)
        {
            DayofWeek = _dayofWeek;
            Beginn = _beginn;
            End = _end;


        }
        #endregion

        #region public Methode
        #endregion

        #region getter/setter
        public int DayofWeek
        {
            get => dayofWeek;
            set => dayofWeek = value;
        }
        public string DayName(int _dayOfWeek)
        {
            return Enum.GetName(typeof(MyCalendarParameters.DayOfWeek), _dayOfWeek);
        }

        public TimeSpan Beginn
        {
            get => beginn;
            set => beginn = value;
        }
        public TimeSpan End
        {
            get => end;
            set => end = value;
        }
        #endregion
    }
}
