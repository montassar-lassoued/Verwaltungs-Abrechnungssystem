using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    public class MyCalendarItems
    {
        #region declaration
        private List<MyCalendarItem> items = new List<MyCalendarItem>();
        #endregion
        #region public methode
        public MyCalendarItems()
        {
        }
        public void AddItem(MyCalendarItem _item)
        {
            ListItems.Add(new MyCalendarItem(_item));
        }
        public List<MyCalendarItem> GetItemsWithTimeFromDate(DateTime _date)
        {
            List<MyCalendarItem> itms = ListItems.FindAll(
                delegate (MyCalendarItem itm)
                { return ((itm.StartDate == _date || itm.EndDate == _date) || (itm.StartDate < _date && itm.EndDate > _date)) && itm.TimeBeginn < itm.TimeEnd; }
                );
            if (itms.Count != 0)
            {
                return itms;
            }
            return null;
        }
        public List<MyCalendarItem> GetItemsFreeTimeFromDate(DateTime _date)
        {
            List<MyCalendarItem> itms = ListItems.FindAll(
                delegate (MyCalendarItem itm)
                { return (itm.StartDate == _date.Date || itm.EndDate == _date.Date || (itm.StartDate < _date.Date && itm.EndDate > _date.Date)) && itm.TimeBeginn == itm.TimeEnd; }
                );
            if (itms.Count != 0)
            {
                return itms;
            }
            return null;
        }
        public MyCalendarItem OnGetItemSelected(DateTime _date, Point _location)
        {

            MyCalendarItem itm = ListItems.Find(
                delegate (MyCalendarItem item)
                {
                    if (item.IsVisible)
                    { return item.Bound.Contains(_location) && item.StartDate == _date.Date; }
                    else
                    { return false; }
                });

            if (itm != null)
            {
                ListItems[ListItems.IndexOf(itm)].IsSelected = true;
                return itm;
            }

            return null;
        }
        public void ClearSelection()
        {
            foreach (MyCalendarItem itm in ListItems)
            {
                if (itm.IsSelected)
                { itm.IsSelected = false; }
            }
        }
        public int getToDeleteItem()
        {
            for (int i = 0; i < ListItems.Count; i++)
            {

                if (ListItems[i].IsSelected)
                {
                    DialogResult result = MessageBox.Show("Termin: " + ListItems[i].Subject + ".\n\nTermin löschen?", "Warnung!!",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result.Equals(DialogResult.Yes))
                    {
                        if (ListItems[i].State.Equals(MyCalendarItemState.DELETED))
                        {
                            MessageBox.Show("Termin: " + ListItems[i].Subject + " wurde schon gelöscht.\n\n" +
                                "Hinweis!!\nGelöscht Termine werden hier auch angezeigt", "Warnung!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return -1;
                        }
                        else if (ListItems[i].EventType.Equals(MyCalendarEventType.HOLIDAY))
                        {
                            MessageBox.Show("Feiertag: " + ListItems[i].Subject + " kann nicht gelöscht werden.\n\n" +
                                "Hinweis!!\nUnter Konfiguration->Kalender, kann ausgeblendet werden", "Warnung!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return -1;
                        }
                        //.DeleteEvent(ListItems[i].Id);
                        return i;
                        //ListItems.RemoveAt(i);
                    }

                }

            }
            return -1;
        }
        #endregion
        #region getter/setter
        public List<MyCalendarItem> ListItems
        {
            get => items;
            set => items = value;
        }
        #endregion
    }
}
