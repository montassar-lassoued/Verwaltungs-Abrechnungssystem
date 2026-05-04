using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using static MyControls.Globals;

namespace MyControls
{
    public class MyCalendarDrawElements
    {
        #region delclaration
        private MyCalendar myCalendar;
        private PaintEventArgs paintEvent;
        private Rectangle timeScaleBound;
        private MyCalendarDay day;
        private List<MyCalendarScaleTime> scaleTimeCalendar = new List<MyCalendarScaleTime>();
        private int marge;

        #endregion

        public MyCalendarDrawElements(MyCalendar _myCalendar)
        {
            myCalendar = _myCalendar;
        }
        public void PaintEvent(PaintEventArgs _paintEvent)
        {
            paintEvent = _paintEvent;
        }
        public void Performance()
        {
            for (int j = 0; j < myCalendar.properties.UnitsScaleTime.scaleTime.Count; j++)
            {
                if (myCalendar.TimeUnitsOffset * -1 >= (j + 1))
                {
                    myCalendar.properties.UnitsScaleTime.scaleTime[j].Visible = false;
                }
                else
                {
                    myCalendar.properties.UnitsScaleTime.scaleTime[j].Visible = true;
                }
            }
        }
        public void ScaleTime()
        {
            using (Pen _pen = new Pen(Color.Empty))
            {
                _pen.Color = myCalendar.properties.layout.ScaleTimeColor;
                _pen.Width = 0.9F;
                MyCalendarUnitScaleTime unit = myCalendar.properties.UnitsScaleTime;

                timeScaleBound = myCalendar.properties.TimeScaleBound;
                int timeScaleHeight = myCalendar.properties.ScaleTimeHeight;
                int top = unit.DayFreeAppoint.Bound.Height + unit._Header.Bound.Height;
                scaleTimeCalendar = myCalendar.properties.UnitsScaleTime.scaleTime;

                foreach (MyCalendarScaleTime time in scaleTimeCalendar)
                {
                    if (!time.Visible) continue;

                    int x1;
                    int x2;

                    if (time.TimeBeginn.Minutes == 00)
                    {
                        x1 = timeScaleBound.Left + marge;

                    }
                    else if (time.TimeBeginn.Minutes == 30)
                    {
                        x1 = (timeScaleBound.Right / 2) - (timeScaleBound.Right / 4) + marge;
                    }
                    else
                    {
                        x1 = timeScaleBound.Right / 2 + marge;
                    }
                    x2 = timeScaleBound.Right - marge; ;
                    paintEvent.Graphics.DrawLine(_pen, x1, top, x2, top);

                    top += timeScaleHeight;
                }
            }
            ScaleTimeText();
        }
        public void ScaleTimeText()
        {
            using (SolidBrush _brush = new SolidBrush(myCalendar.properties.layout.ScaleTimeColor))
            {
                FontStyle fontStyle = FontStyle.Regular;
                Font font = new Font("Arial", 10, fontStyle);
                StringFormat format = StringFormat.GenericDefault;

                MyCalendarUnitScaleTime unit = myCalendar.properties.UnitsScaleTime;
                scaleTimeCalendar = myCalendar.properties.UnitsScaleTime.scaleTime;
                timeScaleBound = myCalendar.properties.TimeScaleBound;
                int timeScaleHeight = myCalendar.properties.ScaleTimeHeight;
                int top = unit.DayFreeAppoint.Bound.Height + unit._Header.Bound.Height;

                foreach (MyCalendarScaleTime time in scaleTimeCalendar)
                {
                    if (!time.Visible) continue;

                    int x1;
                    string text;
                    font = new Font("Arial", 8, fontStyle);
                    if (time.TimeBeginn.Minutes == 00)
                    {
                        x1 = timeScaleBound.Left + marge;
                        text = time.TimeBeginn.Hours.ToString(@"00");
                        font = new Font("Arial", 10, fontStyle);
                    }
                    else if (time.TimeBeginn.Minutes == 30)
                    {
                        x1 = (timeScaleBound.Right / 2) - (timeScaleBound.Right / 4) + marge;
                        text = time.TimeBeginn.Minutes.ToString(@"00");
                    }
                    else
                    {
                        x1 = timeScaleBound.Right / 2 + marge;
                        text = "";//time.TimeBeginn.Minutes.ToString(@"00");
                    }

                    paintEvent.Graphics.DrawString(text, font, _brush, x1, top + 1, format);

                    if (time.TimeBeginn.Minutes == 00)
                    {
                        x1 = timeScaleBound.Right / 2 + marge;
                        text = "";//time.TimeBeginn.Minutes.ToString(@"00");
                        font = new Font("Arial", 7, fontStyle);
                        paintEvent.Graphics.DrawString(text, font, _brush, x1, top + 1, format);
                    }
                    top += timeScaleHeight;

                }
            }
        }

        public void Days()
        {
            MyCalendarDays days = myCalendar.days;

            for (int i = 0; i < days._Day.Count; i++)
            {
                day = days._Day[i];

                OnDrawDayHeader();
                OnDrawDayFreeAppointment();
                OnDrawDayScaleTime();

                if (i == 0 || i == days._Day.Count) { continue; }
                OnDrawDayBorder(day);
            }
            using (Pen _pen = new Pen(myCalendar.properties.layout.DayBorderColor))
            {
                _pen.Width = 0.9F;
                int x = myCalendar.properties.TimeScaleBound.Right;
                int y = myCalendar.Bounds.Top;
                int width = myCalendar.Bounds.Width - myCalendar.properties.TimeScaleBound.Right - 5;
                int height = myCalendar.Size.Height;
                paintEvent.Graphics.DrawRectangle(_pen, x, y, width, height);
            }
            OnCreateItems()
;
        }
        public void OnDrawDayFreeAppointment()
        {
            using (Pen _pen = new Pen(myCalendar.properties.layout.DayBorderColor))
            {
                _pen.Width = 0.9F;
                paintEvent.Graphics.DrawRectangle(_pen, day.DayFreeAppoint.Bound);
                Color _Fillcolor;
                if (day.DayFreeAppoint.IsSelected)
                {
                    _Fillcolor = myCalendar.properties.layout.BackgroudSelectedColor;
                }
                else
                {
                    _Fillcolor = myCalendar.properties.layout.DayFreeAppointmentColor;
                }
                SetColorBound(_Fillcolor, day.DayFreeAppoint.Bound);
            }
        }
        public void OnDrawDayHeader()
        {
            Color borderColor = myCalendar.properties.layout.DayBorderColor;
            if (day.Date.Equals(DateTime.Today.Date) || day.Date.Equals(DateTime.Today.Date.AddDays(1)))
            {
                borderColor = myCalendar.properties.layout.TodayBorderColor;
            }
            using (Pen _pen = new Pen(borderColor))
            {
                _pen.Width = 0.9F;
                paintEvent.Graphics.DrawRectangle(_pen, day._Header.Bound);

                Color headColA = myCalendar.properties.layout.HeaderA;
                Color headColB = myCalendar.properties.layout.HeaderB;
                Color headColC = myCalendar.properties.layout.HeaderC;
                Color headColD = myCalendar.properties.layout.HeaderD;
                GlossyRect(paintEvent.Graphics, day._Header.Bound, headColA, headColB, headColC, headColD);
                OnDrawHeaderText();
            }
        }
        public void OnDrawHeaderText()
        {
            SolidBrush _brush = new SolidBrush(Color.Black);
            FontStyle fontStyle = FontStyle.Bold;
            Font font = new Font("Arial", 9, fontStyle);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            paintEvent.Graphics.DrawString(day._Header.DayofWeek, font, _brush, day._Header.Bound, format);
            format.Alignment = StringAlignment.Near;
            paintEvent.Graphics.DrawString(day._Header.DateOfDay, font, _brush, day._Header.Bound, format);


        }
        public void OnDrawDayBorder(MyCalendarDay day)
        {
            Color borderColor = myCalendar.properties.layout.DayBorderColor;
            if (day.Date.Equals(DateTime.Today.Date) || day.Date.Equals(DateTime.Today.Date.AddDays(1)))
            {
                borderColor = myCalendar.properties.layout.TodayBorderColor;
            }
            using (Pen _pen = new Pen(borderColor))
            {
                _pen.Width = 0.9F;
                paintEvent.Graphics.DrawLine(_pen, day.Bound.Left, day._Header.Bound.Height, day.Bound.Left, day.Bound.Bottom);
            }
        }
        public void OnDrawDayScaleTime()
        {
            using (Pen _pen = new Pen(Color.Empty))
            {
                foreach (MyCalendarScaleTime Time in day.ScaleTimes)
                {
                    if (!Time.Visible) { continue; }

                    if (Time.TimeBeginn.Minutes == 00)
                    { _pen.Color = myCalendar.properties.layout.ScaleTimeLineDarkColor; }
                    else if (Time.TimeBeginn.Minutes == 30)
                    { _pen.Color = myCalendar.properties.layout.ScaleTimeLineLightColor; }
                    else if (Time.IsHighlight)
                    {
                        _pen.Color = myCalendar.properties.layout.BackgroudHighlightColor;
                    }
                    else
                    { _pen.Color = myCalendar.properties.layout.MuniteSandardColor; }

                    _pen.Width = 0.8F;
                    if (Time.Bound.Bottom == myCalendar.Bounds.Bottom) { continue; }

                    Color _fillColor;
                    if (Time.IsSelected)
                    {
                        _fillColor = myCalendar.properties.layout.BackgroudSelectedColor;
                    }
                    else if (Time.IsHighlight)
                    {
                        _fillColor = myCalendar.properties.layout.BackgroudHighlightColor;
                    }
                    else
                    {
                        _fillColor = myCalendar.properties.layout.DayStandardColor;
                    }
                    SetColorBound(_fillColor, Time.Bound);
                    paintEvent.Graphics.DrawLine(_pen, Time.Bound.Location, new Point(Time.Bound.Right, Time.Bound.Top));
                }
            }
        }

        public void OnCreateItems()
        {
            MyCalendarDays days = myCalendar.days;

            myCalendar.ScrollUp = new List<Rectangle>();
            myCalendar.ScrollDown = new List<Rectangle>();

            for (int i = 0; i < days._Day.Count; i++)
            {
                MyCalendarDay _day = days._Day[i];
                MyCalendarItems _items = new MyCalendarItems();
                _items.ListItems = new List<MyCalendarItem>();
                MyCalendarItems _itemsFree = new MyCalendarItems();

                #region Items with TimeScale
                _items.ListItems = myCalendar.items.GetItemsWithTimeFromDate(_day.Date);

                if (_items.ListItems != null)
                {
                    foreach (MyCalendarItem itm in _items.ListItems)
                    {
                        _items.ListItems[_items.ListItems.IndexOf(itm)].IsVisible = false;
                        bool topIsfounded = false;
                        int top = 0;
                        int height = 0;

                        foreach (MyCalendarScaleTime scale in day.ScaleTimes)
                        {
                            if (scale.TimeBeginn >= itm.timeEnd) { continue; }
                            if (!topIsfounded)
                            {
                                if (scale.TimeBeginn >= itm.TimeBeginn)
                                {
                                    if (!scale.Visible) { continue; }
                                    top = scale.Bound.Top;
                                    height = scale.Bound.Height;
                                    topIsfounded = true;
                                }
                            }

                            if (!topIsfounded || scale.TimeEnd < itm.TimeEnd && scale.Visible)
                            {
                                if (topIsfounded)
                                {
                                    height += scale.Bound.Height;
                                }
                                continue;
                            }
                            //Rectangle rec = new Rectangle(_day.Bound.Left, top + 1, _day.Bound.Width - 10, height);
                            _items.ListItems[_items.ListItems.IndexOf(itm)].Bound = new Rectangle(_day.Bound.Left, top + 1, _day.Bound.Width - 10, height - 3);
                            _items.ListItems[_items.ListItems.IndexOf(itm)].IsVisible = true;
                        }

                    }
                    UpdateItemBound(_day, _items.ListItems);
                }
                #endregion

                #region Items Free
                _itemsFree.ListItems.Clear();
                _itemsFree.ListItems = myCalendar.items.GetItemsFreeTimeFromDate(_day.Date);

                if (_itemsFree.ListItems == null) { continue; }

                List<MyCalendarItem> Item_visible = _itemsFree.ListItems.FindAll(delegate (MyCalendarItem itm) { return itm.IsVisible; });
                if (Item_visible.Count < 3 || Item_visible.Count > 3)
                {
                    UpdateItemsFreeAppointement(_day, Scroll_Initialize);
                    _itemsFree.ListItems.Clear();
                    _itemsFree.ListItems = myCalendar.items.GetItemsFreeTimeFromDate(_day.Date);
                }

                if (_itemsFree.ListItems.Count > 3)
                {
                    if (!_itemsFree.ListItems[0].IsVisible)
                    {
                        Rectangle up = new Rectangle();
                        up.Location = new Point(_day.DayFreeAppoint.Bound.Right - 18, _day.DayFreeAppoint.Bound.Top);
                        up.Size = new Size(18, 18);
                        paintEvent.Graphics.DrawImage(Properties.Resources.S_Up_png, up);

                        myCalendar.ScrollUp.Add(up);
                    }
                    if (!_itemsFree.ListItems[_itemsFree.ListItems.Count - 1].IsVisible)
                    {
                        Rectangle down = new Rectangle();
                        down.Location = new Point(_day.DayFreeAppoint.Bound.Right - 18, _day.DayFreeAppoint.Bound.Bottom - 18);
                        down.Size = new Size(18, 18);
                        paintEvent.Graphics.DrawImage(Properties.Resources.S_Down_png, down);

                        myCalendar.ScrollDown.Add(down);
                    }
                }

                if (_itemsFree.ListItems != null)
                {
                    int _top = _day._Header.Bound.Height - 6 + _day.DayFreeAppoint.Bound.Height / 3;
                    foreach (MyCalendarItem item in _itemsFree.ListItems)
                    {
                        if (!item.IsVisible) { continue; }
                        Rectangle rec = new Rectangle(_day.Bound.Left + 5, _top, _day.Bound.Width - 8, 25);
                        _itemsFree.ListItems[_itemsFree.ListItems.IndexOf(item)].Bound = rec;
                        DrawItem(item, rec, 5);
                        _top += 27;
                    }
                }
                #endregion
            }
        }
        public void UpdateItemBound(MyCalendarDay _day, List<MyCalendarItem> _items)
        {
            List<MyCalendarItem> _itemsCopy = _items.ToList();

            List<MyCalendarScaleTime> concurence = new List<MyCalendarScaleTime>();
            concurence = myCalendar.properties.UnitsScaleTime.scaleTime;
            //******************** muss concurence gecleart werden***********************
            concurence.ForEach(delegate (MyCalendarScaleTime scale) { if (scale.Concurence != null) { scale.Concurence.Clear(); } });
            List<List<MyCalendarItem>> groups = new List<List<MyCalendarItem>>();


            while (_itemsCopy.Count > 0)
            {
                List<MyCalendarItem> group = new List<MyCalendarItem>();
                OnFindIntersect(_itemsCopy[0], _itemsCopy, group);
                groups.Add(group);

                foreach (MyCalendarItem item in group)
                    _itemsCopy.Remove(item);
            }

            foreach (List<MyCalendarItem> group in groups)
            {

                double unitDurationMinutes = Convert.ToDouble((int)myCalendar.properties.ScaleTimeUnite);
                foreach (MyCalendarItem _itm in group)
                {

                    int indexStart = Convert.ToInt32(Math.Floor(_itm.timeBeginn.TotalMinutes / unitDurationMinutes));
                    int indexEnd = Convert.ToInt32(Math.Ceiling(_itm.timeEnd.TotalMinutes / unitDurationMinutes));

                    for (int i = 0; i < myCalendar.properties.UnitsScaleTime.scaleTime.Count; i++)
                    {
                        if (i >= indexStart && i < indexEnd)
                        {

                            concurence[i].AddItem(_itm);

                        }
                    }
                }

                int startIndex = 0, endIndex = 0, max_concurence = 0;
                OnGetStartEndIndex(group, out startIndex, out endIndex);
                endIndex = endIndex - 1;

                for (int i = 0; i <= endIndex; i++)
                {
                    if (i >= concurence.Count) { break; }
                    max_concurence = Math.Max(max_concurence, concurence[i].Concurence.Count);
                }

                int[,] matrix = new int[max_concurence, endIndex - startIndex + 1];

                foreach (MyCalendarItem item in group)
                {
                    int x = 0;
                    int unitStart = Convert.ToInt32(Math.Floor(item.timeBeginn.TotalMinutes / unitDurationMinutes)) - startIndex;
                    int unitEnd = Convert.ToInt32(Math.Floor(item.timeEnd.TotalMinutes / unitDurationMinutes)) - startIndex - 1;
                    bool xFound = false;
                    int idx = Convert.ToInt32(Math.Floor(item.timeBeginn.TotalMinutes / unitDurationMinutes));

                    while (!xFound)
                    {
                        xFound = true;

                        for (int i = unitStart; i <= unitEnd; i++)
                        {

                            if (matrix[x, i] != 0)
                            {
                                xFound = false;
                                break;
                            }
                        }
                        if (!xFound) x++;
                    }



                    for (int i = unitStart; i <= unitEnd; i++)
                    {
                        if (matrix[x, i] == 0)
                        {
                            matrix[x, i] = group.IndexOf(item) + 1;
                        }

                    }


                }

                int itemWidth = Convert.ToInt32(Math.Floor(Convert.ToSingle(myCalendar.properties.DayWidth - 0) / Convert.ToSingle(matrix.GetLength(0))));

                foreach (MyCalendarItem item in group)
                {
                    int index = group.IndexOf(item);
                    int top, left;
                    int width = 1;
                    FindInMatrix(matrix, index + 1, out left, out top);

                    if (left >= 0 && top >= 0)
                    {
                        for (int i = left + 1; i < matrix.GetLength(0); i++)
                        {
                            bool found = false;
                            if (matrix[i, top] == index + 1)
                            {
                                width++;
                            }
                            else if (matrix[i, top] == 0)
                            {
                                for (int j = top; j < matrix.GetLength(1); j++)
                                {
                                    if (matrix[i, j] != 0)
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                                if (!found) { width++; }
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    int rleft = item.Bound.Left + left * itemWidth + 1;
                    int right = itemWidth * width - 2;
                    _items[_items.IndexOf(item)].Bound = new Rectangle(rleft, item.Bound.Top, right, item.Bound.Height);

                }
            }

            foreach (MyCalendarItem itm in _items)
            {
                if (itm.IsVisible)
                {
                    DrawItem(itm, itm.Bound, 10);
                }

            }
        }
        public void FindInMatrix(int[,] m, int number, out int left, out int top)
        {
            for (int i = 0; i < m.GetLength(1); i++)
            {
                for (int j = 0; j < m.GetLength(0); j++)
                {
                    if (m[j, i] == number)
                    {
                        left = j;
                        top = i;
                        return;
                    }
                }
            }

            left = top = -1;
        }
        public void OnGetStartEndIndex(List<MyCalendarItem> group, out int startIndex, out int endIndex)
        {
            startIndex = int.MaxValue;
            endIndex = int.MinValue;
            double unitDurationMinutes = Convert.ToDouble((int)myCalendar.properties.ScaleTimeUnite);
            foreach (MyCalendarItem itm in group)
            {
                startIndex = Math.Min(startIndex, Convert.ToInt32(Math.Floor(itm.timeBeginn.TotalMinutes / unitDurationMinutes)));
                endIndex = Math.Max(endIndex, Convert.ToInt32(Math.Ceiling(itm.timeEnd.TotalMinutes / unitDurationMinutes)));
            }
        }
        public void OnFindIntersect(MyCalendarItem item, List<MyCalendarItem> items, List<MyCalendarItem> grouped)
        {
            if (!grouped.Contains(item))
                grouped.Add(item);

            foreach (MyCalendarItem itm in items)
            {
                if (!grouped.Contains(itm) &&
                    item.IntersectsWith(itm.timeBeginn, itm.timeEnd))
                {
                    grouped.Add(itm);

                    OnFindIntersect(itm, items, grouped);
                }
            }
        }
        public void UpdateItemsFreeAppointement(MyCalendarDay _day, int _scroll)
        {

            List<MyCalendarItem> _items = new List<MyCalendarItem>();
            _items = myCalendar.items.GetItemsFreeTimeFromDate(_day.Date);

            if (_items == null) { return; }
            if (_scroll == Scroll_Initialize)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (i == _items.Count) { break; }
                    myCalendar.items.ListItems[myCalendar.items.ListItems.IndexOf(_items[i])].IsVisible = true;
                }
                for (int i = 3; i < _items.Count; i++)
                {
                    if (i == _items.Count) { break; }
                    myCalendar.items.ListItems[myCalendar.items.ListItems.IndexOf(_items[i])].IsVisible = false;
                }
                return;
            }
            MyCalendarItem firstItem = _items.Find(delegate (MyCalendarItem itm) { return itm.IsVisible; });
            MyCalendarItem lastItem = _items.FindLast(delegate (MyCalendarItem itm) { return itm.IsVisible; });


            switch (_scroll)
            {
                case Scroll_Up:
                    firstItem = _items[_items.IndexOf(firstItem) - 1];
                    myCalendar.items.ListItems[myCalendar.items.ListItems.IndexOf(firstItem)].IsVisible = true;
                    myCalendar.items.ListItems[myCalendar.items.ListItems.IndexOf(lastItem)].IsVisible = false;
                    break;
                case Scroll_Down:
                    lastItem = _items[_items.IndexOf(lastItem) + 1];
                    myCalendar.items.ListItems[myCalendar.items.ListItems.IndexOf(firstItem)].IsVisible = false;
                    myCalendar.items.ListItems[myCalendar.items.ListItems.IndexOf(lastItem)].IsVisible = true;
                    break;
            }
        }
        public void DrawItem(MyCalendarItem itm, Rectangle rec, int radius)
        {
            Pen p = new Pen(Color.Black); //new Pen(myCalendar.properties.layout.DayBorderColor);
            p.Width = 1.0f;

            //wenn Item seleted
            if (itm.IsSelected) { p.Width = 4.0f; p.DashCap = DashCap.Round; p.DashStyle = DashStyle.Custom; p.Color = Color.Black; };

            using (GraphicsPath path = RoundedRect(rec, radius))
            {
                paintEvent.Graphics.DrawPath(p, path);

                Rectangle r = rec;
                r.Offset(4, 4);
                GraphicsPath path1 = RoundedRect(r, radius);
                SolidBrush br = new SolidBrush(Color.FromArgb(80, Color.Black));
                paintEvent.Graphics.FillPath(br, path1);

                Color headColD = myCalendar.properties.layout.HeaderD;
                using (LinearGradientBrush b = new LinearGradientBrush(rec, headColD, itm.Color, 95))
                {
                    paintEvent.Graphics.FillPath(b, path);
                }
                if (itm.State.Equals(MyCalendarItemState.DELETED))
                {
                    var pattern = System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal;
                    using (Brush b = new HatchBrush(pattern, Color.Red, Color.Transparent))
                    {
                        paintEvent.Graphics.FillPath(b, path);
                    }
                }
                SolidBrush _brush = new SolidBrush(Color.Black);
                FontStyle fontStyle = FontStyle.Bold;
                Font font = new Font("Arial", 8, fontStyle);
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                paintEvent.Graphics.DrawString(itm.Subject, font, _brush, rec, format);
            }

        }

        #region private methode
        private void SetColorBound(Color _color, Rectangle _bound)
        {
            using (SolidBrush _brush = new SolidBrush(_color))
            {
                paintEvent.Graphics.FillRectangle(_brush, _bound);
            }
        }
        private void GradientRect(Graphics g, Rectangle bounds, Color a, Color b)
        {
            using (LinearGradientBrush _lgBrush = new LinearGradientBrush(bounds, b, a, -90))
            {
                g.FillRectangle(_lgBrush, bounds);

            }
        }

        private void GlossyRect(Graphics g, Rectangle bounds, Color a, Color b, Color c, Color d)
        {
            Rectangle top = new Rectangle(bounds.Left, bounds.Top, bounds.Width, bounds.Height / 2);
            Rectangle bot = Rectangle.FromLTRB(bounds.Left, top.Bottom, bounds.Right, bounds.Bottom);

            GradientRect(g, top, a, b);
            GradientRect(g, bot, c, d);
        }

        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
        #endregion

        #region getter/setter
        public void Invalidate(Rectangle _bound)
        {
            myCalendar.Invalidate(_bound);
        }
        #endregion
    }
}
