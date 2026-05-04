using System.Drawing;

namespace MyControls
{
    public class MyCalendarDayFreeAppointment
    {
        #region declaration
        private Rectangle bound;
        private bool isSelected;
        private MyCalendarItem items = new MyCalendarItem();
        #endregion
        #region public methode
        public MyCalendarDayFreeAppointment()
        {

        }
        public MyCalendarDayFreeAppointment(Rectangle _bound)
        {
            Bound = _bound;
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
        public bool IsSelected
        {
            get => isSelected;
            set => isSelected = value;
        }
        public MyCalendarItem Items
        {
            get => items;
            set => items = value;
        }
        #endregion
    }
}
