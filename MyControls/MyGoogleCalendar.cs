using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Google.Apis.Calendar.v3.Data.Event;

namespace MyControls
{
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Calendar.v3;
    using Google.Apis.Calendar.v3.Data;
    using Google.Apis.Services;
    using Google.Apis.Util.Store;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public class MyGoogleCalendar
    {
        static string[] Scopes = { CalendarService.Scope.Calendar, CalendarService.Scope.CalendarEvents, CalendarService.Scope.CalendarEventsOwned, CalendarService.Scope.CalendarAppCreated };
        static string ApplicationName = "MY_APP";
        private UserCredential userCredential;
        private bool loggedOn = false;

        public string UserName;
        public string Email;
        public string ClientSecretPath;
        public bool ShowDeletedAppointments;
        public bool ShowIslamicHolidays;
        public bool ShowGermanHolidays;

        public MyGoogleCalendar()
        {

        }
        public bool Login()
        {
            if (checkDataFailed())
            {
                loggedOn = false;
                return false;
            }
            using (var stream = new FileStream(ClientSecretPath, FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(System.Environment
                  .SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials");

                userCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                   GoogleClientSecrets.FromStream(stream).Secrets,
                  Scopes,
                  UserName,
                  CancellationToken.None,
                  new FileDataStore(credPath, true)).Result;
            }
            loggedOn = true;
            return true;
        }
        public List<MyCalendarItem> GetEvents(DateTime startDate, DateTime endDate)
        {
            if (!loggedOn)
            {
                return new List<MyCalendarItem>();
            }
            return GetData(startDate, endDate);
        }
        public void CreateEvent(MyCalendarItem item)
        {
            if (!loggedOn)
            {
                return;
            }
            CreateData(item);
        }
        public void UpdateEvent(MyCalendarItem item)
        {
            if (!loggedOn)
            {
                return;
            }
            UpdateData(item);
        }
        public void DeleteEvent(string eventID)
        {
            if (!loggedOn)
            {
                return;
            }
            DeleteData(eventID);
        }
        #region private methods
        
        private List<MyCalendarItem> GetData(DateTime startDate, DateTime endDate)
        {
            List<MyCalendarItem> items = new List<MyCalendarItem>();
            //*** Termine
            Events events = excute(Email, startDate, endDate);
            if (events != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTimeDateTimeOffset.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        DateTime start = DateTime.Parse(eventItem.Start.Date);
                        DateTime end = DateTime.Parse(eventItem.End.Date);

                        int result = DateTime.Compare(start, end);
                        if (result == 0)
                        {
                            items.Add(new MyCalendarItem(eventItem.Id, MyCalendarEventType.DEFAULT, Rectangle.Empty, start, end,
                           startDate.TimeOfDay, startDate.TimeOfDay, eventItem.Summary,
                           eventItem.Description, getColorFromID(eventItem.ColorId), getState(eventItem.Status)));
                        }
                        else
                        {
                            while (DateTime.Compare(start, end) < 0)
                            {
                                items.Add(new MyCalendarItem(eventItem.Id, MyCalendarEventType.DEFAULT, Rectangle.Empty, start, start,
                            startDate.TimeOfDay, startDate.TimeOfDay, eventItem.Summary,
                            eventItem.Description, getColorFromID(eventItem.ColorId), getState(eventItem.Status)));

                                start = start.AddDays(1);

                            }
                        }


                    }
                    else
                    {
                        DateTime start = eventItem.Start.DateTimeDateTimeOffset.Value.Date;
                        DateTime end = eventItem.End.DateTimeDateTimeOffset.Value.Date;

                        int result = DateTime.Compare(start, end);
                        if (result == 0)
                        {
                            items.Add(new MyCalendarItem(eventItem.Id, MyCalendarEventType.DEFAULT, Rectangle.Empty, start,
                            end, eventItem.Start.DateTimeDateTimeOffset.Value.TimeOfDay, eventItem.End.DateTimeDateTimeOffset.Value.TimeOfDay,
                            eventItem.Summary, eventItem.Description, getColorFromID(eventItem.ColorId), getState(eventItem.Status)));
                        }
                        else
                        {
                            while (DateTime.Compare(start, end.AddDays(1)) < 0)
                            {
                                items.Add(new MyCalendarItem(eventItem.Id, MyCalendarEventType.DEFAULT, Rectangle.Empty, start,
                            start, eventItem.Start.DateTimeDateTimeOffset.Value.TimeOfDay, eventItem.End.DateTimeDateTimeOffset.Value.TimeOfDay,
                            eventItem.Summary, eventItem.Description, getColorFromID(eventItem.ColorId), getState(eventItem.Status)));

                                start = start.AddDays(1);

                            }

                        }
                    }
                }
            }
            // Feiertage
            if (ShowGermanHolidays)
            {
                Events holidays = excute("de.german#holiday@group.v.calendar.google.com", startDate, endDate);
                if (holidays.Items.Count > 0)
                {
                    foreach (var eventItem in holidays.Items)
                    {
                        DateTime date = DateTime.Parse(eventItem.Start.Date);

                        items.Add(new MyCalendarItem(eventItem.Id, MyCalendarEventType.HOLIDAY, Rectangle.Empty, date, date,
                            date.TimeOfDay, date.TimeOfDay, eventItem.Summary,
                            eventItem.Description, getColorFromID("10"), getState(eventItem.Status)));
                    }
                }
            }


            //**** islamische-Feiertage
            if (ShowIslamicHolidays)
            {
                Events islamic_holidays = excute("de.islamic#holiday@group.v.calendar.google.com", startDate, endDate);
                if (islamic_holidays.Items.Count > 0)
                {
                    foreach (var eventItem in islamic_holidays.Items)
                    {
                        DateTime date = DateTime.Parse(eventItem.Start.Date);

                        items.Add(new MyCalendarItem(eventItem.Id, MyCalendarEventType.HOLIDAY, Rectangle.Empty, date,
                            date, date.TimeOfDay, date.TimeOfDay, eventItem.Summary,
                            eventItem.Description, getColorFromID("5"), getState(eventItem.Status)));
                    }
                }
            }

            // das Ergebnis zurückgeben       
            return items;
        }
        private void CreateData(MyCalendarItem item)
        {
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = userCredential,
                ApplicationName = ApplicationName,
            });
            Event _event = buildEvent(item);

            // Define parameters of request.
            EventsResource.InsertRequest request = service.Events.Insert(_event, Email);
            //request.SendUpdates = EventsResource.InsertRequest.SendUpdatesEnum.All;
            Stream stream = request.ExecuteAsStream();
            using (var reader = new StreamReader(stream))
            {
                Console.WriteLine(reader.ReadToEnd());
            }
            //Event events = request.Execute();
        }
        private void UpdateData(MyCalendarItem item)
        {
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = userCredential,
                ApplicationName = ApplicationName,
            });
            Event _event = buildEvent(item);
            // Define parameters of request.
            EventsResource.UpdateRequest request = service.Events.Update(_event, Email, item.Id);
            request.SendUpdates = EventsResource.UpdateRequest.SendUpdatesEnum.All;

            Event events = request.Execute();
        }
        private void DeleteData(string eventId)
        {
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = userCredential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            EventsResource.DeleteRequest request = service.Events.Delete(Email, eventId);
            request.SendUpdates = EventsResource.DeleteRequest.SendUpdatesEnum.All;

            string events = request.Execute();
        }
        
        private Events excute(string calenderID, DateTime startDate, DateTime endDate)
        {
            // Create Calendar Service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = userCredential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List(calenderID);
            request.ShowDeleted = ShowDeletedAppointments;
            request.SingleEvents = true;
            request.TimeMin = startDate.Date;
            request.TimeMax = endDate.Date;

            //request.MaxResults = 500;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            //**Events
            try
            {
                Events events = request.Execute();
                return events;
            }
            catch (Google.GoogleApiException gex)
            {
                MessageBox.Show($"Fehler beim Abrufen der Events: {gex.Message}");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unerwarteter Fehler: {ex.Message}");
            }
            return null;
        }
        private static Event buildEvent(MyCalendarItem _item)
        {
            Event _event = new Event();
            _event.Kind = "calendar#event";
            _event.EventType = "default";
            _event.ColorId = "11";
            _event.Summary = _item.Subject;
            _event.Description = _item.Description;
            List<EventReminder> reminders = new List<EventReminder>();
            reminders.Add(new EventReminder { ETag = null, Method = "popup", Minutes = 420 });
            _event.Reminders = new RemindersData();
            _event.Reminders.Overrides = reminders;
            _event.Reminders.UseDefault = false;

            EventDateTime start = new EventDateTime();
            EventDateTime end = new EventDateTime();

            if (_item.TimeBeginn == _item.timeEnd)
            {
                // Korrektur des Datums bei ganztag-Termine
                if (DateTime.Compare(_item.StartDate.Date, _item.EndDate.Date) < 0)
                {
                    _item.EndDate = _item.EndDate.AddDays(1);
                }
                //**** Ganzen-Tag-Event
                start.Date = _item.StartDate.ToString("yyyy-MM-dd");
                end.Date = _item.EndDate.ToString("yyyy-MM-dd");
            }
            else
            {
                start.DateTimeDateTimeOffset = _item.StartDate.Add(_item.TimeBeginn);
                end.DateTimeDateTimeOffset = _item.EndDate.Add(_item.TimeEnd);
            }


            _event.Start = start;
            _event.End = end;
            return _event;
        }
        private bool checkDataFailed()
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                MessageBox.Show("Benuzername für den Zugriff auf dem Google-Kalender ist nicht vorhanden." +
                    "\n\nEinstellungen unter Konfiguration->Kalender prüfen");
                return true;
            }
            if (!CheckValidate.ValidateEmail(Email))
            {
                MessageBox.Show("E-Mail-Adresse für den Zugriff auf dem Google-Kalender ist nicht vorhanden." +
                    "\n\nEinstellungen unter Konfiguration->Kalender prüfen");
                return true;
            }
            if (string.IsNullOrWhiteSpace(ClientSecretPath))
            {
                MessageBox.Show("client-Secret für den Zugriff auf dem Google-Kalender ist nicht vorhanden." +
                    "\n\nEinstellungen unter Konfiguration->Kalender prüfen");
                return true;
            }
            if (!File.Exists(ClientSecretPath))
            {
                MessageBox.Show("client-Secret für den Zugriff auf dem Google-Kalender existiert nicht." +
                    "\n\nPfad: " + ClientSecretPath + " " +
                    "\nEinstellungen unter Konfiguration->Kalender prüfen");
                return true;
            }

            return false;
        }
        private Color getColorFromID(string id)
        {
            {
                switch (id)
                {
                    case "1":
                        return Color.Lavender;
                    case "2":
                        return MyCalendarElementsLayout.FromHex("#33B679");
                    case "3":
                        return MyCalendarElementsLayout.FromHex("#8E24AA");
                    case "4":
                        return MyCalendarElementsLayout.FromHex("#E67C73");
                    case "5":
                        return MyCalendarElementsLayout.FromHex("#F6BF26");
                    case "6":
                        return MyCalendarElementsLayout.FromHex("#F4511E");
                    case "7":
                        return MyCalendarElementsLayout.FromHex("#039BE5");
                    case "8":
                        return MyCalendarElementsLayout.FromHex("#616161");
                    case "9":
                        return MyCalendarElementsLayout.FromHex("#3F51B5");
                    case "10":
                        return MyCalendarElementsLayout.FromHex("#0B8043");
                    case "11":
                        return MyCalendarElementsLayout.FromHex("#D50000");
                    default:
                        return Color.WhiteSmoke;
                }
            }
        }
        private static MyCalendarItemState getState(string status)
        {
            switch (status)
            {
                case "cancelled":
                    return MyCalendarItemState.DELETED;
                default:
                    return MyCalendarItemState.CONFIRMED;
            }
        }
        #endregion
    }
}