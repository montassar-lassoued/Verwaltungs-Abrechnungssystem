using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MyControls.Globals;

namespace MyControls
{
    public class MyCalendar : ScrollableControl
    {
        public MyGoogleCalendar googleCalendar = new MyGoogleCalendar();
        public MyCalendarDrawElements drawElements;
        public MyCalendarProperties properties;
        public MyCalendarParameters parameters;
        public MyCalendarDays days;
        public List<TimeSpan> dataSource;
        public MyCalendarItems items = new MyCalendarItems();
        private int timeUnitsOffset;
        public bool bUpdate;
        private bool bScroll;
        private MyCalendarDay selectedDay = new MyCalendarDay();
        private DateTime firstDateShow;
        private DateTime lastDateShow;
        public List<TimeSpan> DataSource;
        public List<Rectangle> ScrollUp;
        public List<Rectangle> ScrollDown;
        public int numberOfAppointments;
        public int numberOfAppointmentsCancelled;

        public event EventHandler<System.Windows.Forms.Day> _FirstDayOfWeek;
        public MyCalendar()
        {
            SetStyle(ControlStyles.ResizeRedraw, false);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            //googleCalendar = new MyGoogleCalendar();
            drawElements = new MyCalendarDrawElements(this);
            properties = new MyCalendarProperties(this);
            parameters = new MyCalendarParameters();
            parameters.intializeCalendarParameters();

            FirstDateShow = properties.FirstWeekDay(DateTime.Today);
            LastDateShow = FirstDateShow.AddDays((int)properties._Week - 1);
        }
        public void OnSetRange(DateTime _firstDate, DateTime _lastDate)
        {
            FirstDateShow = _firstDate;
            LastDateShow = _lastDate;
            DaySelected = null;
            items = new MyCalendarItems();
            var  myCalendarItems =  googleCalendar.GetEvents(firstDateShow, lastDateShow);
            foreach (MyCalendarItem item in myCalendarItems)
            {
                items.AddItem(item);
            }
            numberOfAppointments = items.ListItems.Count;
            numberOfAppointmentsCancelled = items.ListItems.FindAll(itm => itm.State.Equals(MyCalendarItemState.DELETED)).Count;
            Invalidate();
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }
        protected override void OnPaint(PaintEventArgs paintEvent)
        {
            base.OnPaint(paintEvent);

            MyCalendarSetting();
            paintEvent.Graphics.SmoothingMode =
        System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            drawElements.PaintEvent(paintEvent);
            drawElements.ScaleTime();
            drawElements.Days();
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            char key = e.KeyChar;
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Delete)
            {
                OnDeleteSelectedItem();
            }
            Invalidate();
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            Select();
        }
        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            if (DaySelected == null || DaySelected._Header.IsSelected || bScroll) { bScroll = false; return; }
            TerminDialog CreateItem = new TerminDialog();
            DateTime sDate = DaySelected.Date;
            DateTime eDate = DaySelected.Date;
            if (!DaySelected.CreateNewItem && DaySelected.Item != null)
            {
                if (!USER_RECHT_TERMIN_BEARBEITEN)
                {
                    MessageBox.Show("Keine Berechtigung", "Stop!!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

                List<MyCalendarItem> itms = items.ListItems.FindAll(
                    delegate (MyCalendarItem itm)
                    { return itm.Id.Equals(DaySelected.Item.Id); }
                    );
                itms.Sort((x, y) => x.StartDate.CompareTo(y.StartDate));

                sDate = itms[0].StartDate;
                eDate = itms[itms.Count - 1].StartDate;
            }
            else
            {
                if (!USER_RECHT_TERMIN_ERSTELLEN)
                {
                    MessageBox.Show("Keine Berechtigung", "Stop!!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
            }

            CreateItem.SetParameters(DaySelected, DataSource, sDate, eDate);
            CreateItem.ShowDialog(this);

            if (!CreateItem.Cancel)
            {
                MessageBox.Show("Item wird erstellt");
                return;
                MyCalendarDay day = CreateItem.getDay();
                DateTime endDate = CreateItem.getEndDate();

                if (day != null && !day.Equals(DaySelected))
                {
                    MyCalendarItem item = new MyCalendarItem("", MyCalendarEventType.DEFAULT, Rectangle.Empty, day.Date, endDate.Date, day.SelectedscaleTime.TimeBeginn, day.SelectedscaleTime.TimeEnd,
                        day.SelectedscaleTime.Subject, day.SelectedscaleTime.Text, day.SelectedscaleTime.Color, MyCalendarItemState.CONFIRMED);

                    if (DaySelected.CreateNewItem)
                    {
                        onCreateItem(item);
                    }
                    else
                    {
                        if (DaySelected.Item.EventType.Equals(MyCalendarEventType.HOLIDAY))
                        {
                            MessageBox.Show("Ein Feiertag kann nicht bearbeitet werden!", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        OnUpdateItem(DaySelected.Item, item);
                    }
                    DaySelected.IsSelected = false;
                    OnSetRange(FirstDateShow, LastDateShow);

                }
            }
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            ScrollTimeUnits(e.Delta);


        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            OnGetSelection(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

        }
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (DaySelected != null && DaySelected.Item != null && DaySelected.Item.IsVisible)
            {
                if (e.Y == DaySelected.Item.Bound.Y || e.Y == DaySelected.Item.Bound.Bottom)
                {
                    Cursor = Cursors.SizeNS;
                }
                else
                {
                    Cursor = Cursors.Default;
                }
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }
        private void onCreateItem(MyCalendarItem item)
        {
            //***Create-GoogleClendar-Event
            googleCalendar.CreateEvent(item);
            //***Create-Local-Event
            items.AddItem(item);
        }
        private void OnUpdateItem(MyCalendarItem _oldItem, MyCalendarItem _newItem)
        {
            //**wir behalten uns die ID (Event-ID)
            _newItem.Id = _oldItem.Id;
            //***Create-GoogleClendar-Event
            googleCalendar.UpdateEvent(_newItem);
            //***Create-Local-Event
            items.ListItems[items.ListItems.IndexOf(_oldItem)] = _newItem;
        }
        private void OnDeleteSelectedItem()
        {
            if (!USER_RECHT_TERMIN_LOESCHEN)
            {
                MessageBox.Show("Keine Berechtigung", "Stop!!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            int index = items.getToDeleteItem();
            if (index < 0)
            {
                return;
            }
            googleCalendar.DeleteEvent(items.ListItems[index].Id);
            //items.ListItems.RemoveAt(index);
            OnSetRange(FirstDateShow, LastDateShow);
        }

        private void OnGetSelection(MouseEventArgs e)
        {
            days.ClearSelectedDay();
            items.ClearSelection();
            DaySelected = days.GetSelectedDay(e.Location);
            bScroll = false;

            if (DaySelected == null) { return; }

            int _location = days.OnGetIndexOf(DaySelected, e.Location);
            switch (_location)
            {
                case HEADER:
                    DaySelected._Header.IsSelected = true;
                    DaySelected.CreateNewItem = false;
                    break;
                case FREEAPPOINTEMENT:
                    if (!ClickFreeAppointementScroll(DaySelected, e.Location))
                    {
                        MyCalendarItem _freeItem = items.OnGetItemSelected(DaySelected.Date, e.Location);
                        if (_freeItem != null)
                        {
                            DaySelected.CreateNewItem = false;
                            DaySelected.Item = _freeItem;
                            DaySelected.Date = _freeItem.StartDate;
                        }
                        else
                        {
                            DaySelected.CreateNewItem = true;
                            DaySelected.DayFreeAppoint.IsSelected = true;
                            DaySelected.Item.StartDate = DaySelected.Date;
                        }
                    }
                    break;
                case ITEM_OR_SCALE:
                    MyCalendarItem _item = items.OnGetItemSelected(DaySelected.Date, e.Location);
                    if (_item != null)
                    {
                        DaySelected.CreateNewItem = false;
                        DaySelected.Item = _item;
                        DaySelected.Date = _item.StartDate;
                    }
                    else
                    {
                        for (int i = 0; i < DaySelected.ScaleTimes.Count; i++)
                        {
                            if (!DaySelected.ScaleTimes[i].Visible) { continue; }
                            if (DaySelected.ScaleTimes[i].Bound.Contains(e.Location))
                            {
                                DaySelected.ScaleTimes[i].IsSelected = true;
                                DaySelected.CreateNewItem = true;
                            }
                        }
                    }
                    break;

                case NONE:
                    break;
            }

            Invalidate();
        }
        private bool ClickFreeAppointementScroll(MyCalendarDay _day, Point _location)
        {
            bScroll = true;
            foreach (Rectangle rec in ScrollUp)
            {
                if (rec.Contains(_location))
                {
                    drawElements.UpdateItemsFreeAppointement(_day, Scroll_Up);
                    return true;
                }
            }
            foreach (Rectangle rec in ScrollDown)
            {
                if (rec.Contains(_location))
                {
                    drawElements.UpdateItemsFreeAppointement(_day, Scroll_Down);
                    return true;
                }
            }
            bScroll = false;
            return false;
        }
        private void ScrollTimeUnits(int delta)
        {
            int possible = TimeUnitsOffset;
            int visible = Convert.ToInt32(Math.Floor(
                    Convert.ToSingle(properties.TimeScaleBound.Height - properties.FreeAppointmentHeight - properties.HeaderHeight) /
                    Convert.ToSingle(properties.ScaleTimeHeight)
                    )); ;

            if (delta < 0)
            {
                possible--;
            }
            else
            {
                possible++;
            }

            if (possible > 0)
            {
                possible = 0;
            }
            else if (
                properties.UnitsScaleTime != null
                && properties.UnitsScaleTime.scaleTime.Count > 0
                && possible * -1 >= properties.UnitsScaleTime.scaleTime.Count)
            {
                possible = properties.UnitsScaleTime.scaleTime.Count - 1;
                possible *= -1;
            }
            else if (properties.UnitsScaleTime != null
               && properties.UnitsScaleTime.scaleTime.Count > 0)
            {
                int max = properties.UnitsScaleTime.scaleTime.Count - visible;
                max *= -1;
                if (possible < max) possible = max;
            }

            if (possible != TimeUnitsOffset)
            {
                TimeUnitsOffset = possible;
            }
        }
        private void MyCalendarSetting()
        {
            properties.StandardSettingsApply();
        }
        public int TimeUnitsOffset
        {
            get => timeUnitsOffset;
            set
            {
                timeUnitsOffset = value;
                bUpdate = true;
                drawElements.Performance();
                Invalidate();
            }
        }
        public MyCalendarDay DaySelected
        {
            get => selectedDay;
            set => selectedDay = value;
        }
        public DateTime FirstDateShow
        {
            get => firstDateShow;
            set => firstDateShow = value;
        }
        public DateTime LastDateShow
        {
            get => lastDateShow;
            set => lastDateShow = value;
        }
        public bool USER_RECHT_TERMIN_BEARBEITEN { get; set; }
        public bool USER_RECHT_TERMIN_ERSTELLEN { get; set; }
        public bool USER_RECHT_TERMIN_LOESCHEN { get; set; }
    }
}
