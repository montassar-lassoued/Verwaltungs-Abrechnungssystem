using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyControls
{
    public class MyFormCalendar : MyFormCalendar_Base
    {
        private Label anzahlAbgesagt;
        private SplitContainer splitContainer1;
        private MonthCalendar monthCalendar1;
        private MyCalendar myCalendar1;
        private Label anzahlTermine;
        private string username;
        private string mail;
        private string secretPath;
        private bool geloeschte_termine;
        private bool feiertage;
        private bool i_feiertage;
        private DateTime StartDate;
        private DateTime EndDate;

        public string Username { get => username; set => username = value; }
        public string Mail { get => mail; set => mail = value; }
        public string SecretPath { get => secretPath; set => secretPath = value; }
        public bool Geloeschte_termine { get => geloeschte_termine; set => geloeschte_termine = value; }
        public bool Feiertage { get => feiertage; set => feiertage = value; }
        public bool I_feiertage { get => i_feiertage; set => i_feiertage = value; }
        public bool USER_RECHT_TERMIN_BEARBEITEN { get; set; }
        public bool USER_RECHT_TERMIN_ERSTELLEN { get; set; }
        public bool USER_RECHT_TERMIN_LOESCHEN { get; set; }

        protected override void _InitializeComponent()
        {
            MyControls.MyCalendarDay myCalendarDay1 = new MyControls.MyCalendarDay();
            MyControls.MyCalendarItem myCalendarItem1 = new MyControls.MyCalendarItem();
            this.anzahlAbgesagt = new System.Windows.Forms.Label();
            this.anzahlTermine = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.myCalendar1 = new MyControls.MyCalendar();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BorderBody
            // 
            this.BorderBody.Controls.Add(this.splitContainer1);
            this.BorderBody.Size = new System.Drawing.Size(1222, 728);
            //BorderBody.Anchor = AnchorStyles.Top| AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            // 
            // anzahlAbgesagt
            // 
            this.anzahlAbgesagt.AutoSize = true;
            this.anzahlAbgesagt.Font = new System.Drawing.Font("Lucida Calligraphy", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.anzahlAbgesagt.ForeColor = System.Drawing.Color.Crimson;
            this.anzahlAbgesagt.Location = new System.Drawing.Point(861, 68);
            this.anzahlAbgesagt.Name = "anzahlAbgesagt";
            this.anzahlAbgesagt.Size = new System.Drawing.Size(259, 21);
            this.anzahlAbgesagt.TabIndex = 12;
            this.anzahlAbgesagt.Text = "abgesagte/gelöschte Termine:";
            this.anzahlAbgesagt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // anzahlTermine
            // 
            this.anzahlTermine.AutoSize = true;
            this.anzahlTermine.Font = new System.Drawing.Font("Lucida Calligraphy", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.anzahlTermine.ForeColor = System.Drawing.Color.Blue;
            this.anzahlTermine.Location = new System.Drawing.Point(861, 35);
            this.anzahlTermine.Name = "anzahlTermine";
            this.anzahlTermine.Size = new System.Drawing.Size(285, 21);
            this.anzahlTermine.TabIndex = 13;
            this.anzahlTermine.Text = "Diese Woche sind keine Termine";
            this.anzahlTermine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            //splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.myCalendar1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.monthCalendar1);
            this.splitContainer1.Size = new System.Drawing.Size(1216, 709);
            this.splitContainer1.SplitterDistance = 1032;
            this.splitContainer1.TabIndex = 0;
            // 
            // myCalendar1
            // 
            this.myCalendar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            myCalendarDay1._Header = null;
            myCalendarDay1.Bound = new System.Drawing.Rectangle(0, 0, 0, 0);
            myCalendarDay1.CreateNewItem = false;
            myCalendarDay1.Date = new System.DateTime(((long)(0)));
            myCalendarDay1.DayFreeAppoint = null;
            myCalendarDay1.ID = 0;
            myCalendarDay1.IsSelected = false;
            myCalendarItem1.Bound = new System.Drawing.Rectangle(0, 0, 0, 0);
            myCalendarItem1.Color = System.Drawing.Color.Empty;
            myCalendarItem1.Day = 0;
            myCalendarItem1.Description = null;
            myCalendarItem1.EndDate = new System.DateTime(((long)(0)));
            myCalendarItem1.EventType = MyControls.MyCalendarEventType.DEFAULT;
            myCalendarItem1.Id = null;
            myCalendarItem1.IsSelected = false;
            myCalendarItem1.IsVisible = false;
            myCalendarItem1.Month = 0;
            myCalendarItem1.StartDate = new System.DateTime(((long)(0)));
            myCalendarItem1.State = MyControls.MyCalendarItemState.CANCELLED;
            myCalendarItem1.Subject = null;
            myCalendarItem1.TimeBeginn = System.TimeSpan.Parse("00:00:00");
            myCalendarItem1.TimeEnd = System.TimeSpan.Parse("00:00:00");
            myCalendarItem1.Year = 0;
            myCalendarDay1.Item = myCalendarItem1;
            myCalendarDay1.ScaleTimes = null ;
            this.myCalendar1.DaySelected = myCalendarDay1;
            this.myCalendar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myCalendar1.FirstDateShow = new System.DateTime(2025, 9, 8, 0, 0, 0, 0);
            this.myCalendar1.LastDateShow = new System.DateTime(2025, 9, 7, 0, 0, 0, 0);
            this.myCalendar1.Location = new System.Drawing.Point(0, 0);
            this.myCalendar1.Name = "myCalendar1";
            this.myCalendar1.Size = new System.Drawing.Size(1032, 709);
            this.myCalendar1.TabIndex = 0;
            this.myCalendar1.Text = "myCalendar1";
            //this.myCalendar1.TimeUnitsOffset = 0;
            this.myCalendar1.USER_RECHT_TERMIN_BEARBEITEN = false;
            this.myCalendar1.USER_RECHT_TERMIN_ERSTELLEN = false;
            this.myCalendar1.USER_RECHT_TERMIN_LOESCHEN = false;
            //myCalendar1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.CalendarDimensions = new System.Drawing.Size(1, 4);
            this.monthCalendar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monthCalendar1.FirstDayOfWeek = System.Windows.Forms.Day.Monday;
            this.monthCalendar1.Location = new System.Drawing.Point(0, 0);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 0;
            this.monthCalendar1.TabStop = false;
            this.monthCalendar1.TitleBackColor = System.Drawing.Color.DodgerBlue;
            this.monthCalendar1.TitleForeColor = System.Drawing.Color.AliceBlue;
            this.monthCalendar1.TrailingForeColor = System.Drawing.Color.LightGray;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            this.monthCalendar1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnMonthCalendarMouseUp);
            this.monthCalendar1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMonthCalendarMouseDown);
            this.monthCalendar1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.OnMonthCalendarMouseDown);
            //monthCalendar1.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            //
            // 
            // MyFormCalendar
            // 
            this.Controls.Add(this.anzahlTermine);
            this.Controls.Add(this.anzahlAbgesagt);
            this.Controls.SetChildIndex(this.BorderBody, 0);
            this.Controls.SetChildIndex(this.anzahlAbgesagt, 0);
            this.Controls.SetChildIndex(this.anzahlTermine, 0);
            this.BorderBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        protected sealed override bool _EnablePbSave()
        {
            return false;
        }
        protected sealed override bool _Save()
        {
            return false;
        }
        protected sealed override bool _Populate()
        {
            return true;
        }
        protected void PopulateCalendar()
        {
            StartDate = FirstWeekDay(DateTime.Today);
            EndDate = LastWeekDay(StartDate);
            LogIn(StartDate, EndDate);
        }
        private  void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            StartDate = FirstWeekDay(e.Start);
            EndDate = LastWeekDay(StartDate);
            LogIn(StartDate, EndDate);
        }
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            StartDate = FirstWeekDay(e.Start);
            EndDate = LastWeekDay(StartDate);
            LogIn (StartDate, EndDate);
        }
        private void OnMonthCalendarMouseDown(object sender, MouseEventArgs e)
        {
            StartDate = FirstWeekDay(monthCalendar1.SelectionStart);
            EndDate = LastWeekDay(StartDate);
            LogIn(StartDate, EndDate);
        }
        private void OnMonthCalendarMouseUp(object sender, KeyEventArgs e)
        {
            StartDate = FirstWeekDay(StartDate);
            EndDate = LastWeekDay(StartDate);
            LogIn(StartDate, EndDate);
        }

        private void LogIn(DateTime StartDate, DateTime EndDate)
        {
            myCalendar1.googleCalendar.UserName = Username;
            myCalendar1.googleCalendar.Email = Mail;
            myCalendar1.googleCalendar.ClientSecretPath = SecretPath;
            myCalendar1.googleCalendar.ShowDeletedAppointments = Geloeschte_termine;
            myCalendar1.googleCalendar.ShowGermanHolidays = Feiertage;
            myCalendar1.googleCalendar.ShowIslamicHolidays = I_feiertage;
            //Rechte
            myCalendar1.USER_RECHT_TERMIN_BEARBEITEN = USER_RECHT_TERMIN_BEARBEITEN;
            myCalendar1.USER_RECHT_TERMIN_ERSTELLEN = USER_RECHT_TERMIN_ERSTELLEN;
            myCalendar1.USER_RECHT_TERMIN_LOESCHEN = USER_RECHT_TERMIN_LOESCHEN;

            monthCalendar1.SetSelectionRange(StartDate, EndDate);

            if (!myCalendar1.googleCalendar.Login())
            {
                MessageBox.Show("Anmeldung ist fehlgeschlagen!", "Stop!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return ;
            }
            myCalendar1.OnSetRange(StartDate, EndDate);
            setAnzahlTermine(myCalendar1.numberOfAppointments, myCalendar1.numberOfAppointmentsCancelled);
        }
        private void setAnzahlTermine(int termine, int abgesagt)
        {
            if (termine == 0)
            {
                anzahlTermine.Text = "Diese Woche stehen keine Termine an";
            }
            else
            {
                anzahlTermine.Text = "Diese Woche stehen (" + termine + ") Termin(e) an";
            }
            anzahlAbgesagt.Text = "abgesagte/gelöschte Termine: " + abgesagt + "";
        }
        private DateTime FirstWeekDay(DateTime _date)
        {
            int days = 0;
            switch ((int)myCalendar1.parameters.WochenBeginn)
            {
                #region mit Montag
                case 1:
                    switch ((int)_date.DayOfWeek)
                    {
                        case 0:
                            days = -6;
                            break;
                        default:
                            days = 1 - (int)_date.DayOfWeek;
                            break;
                    }
                    break;
                #endregion

                #region mit Sonntag
                case 0:
                    int wkB = 0;
                    if (myCalendar1.parameters.Woche == MyCalendarParameters.Week.Work)
                    {
                        wkB = 1;
                    }
                    else
                    {
                        wkB = 0;
                    }
                    switch ((int)_date.DayOfWeek)
                    {
                        case 0:
                            days = wkB;
                            break;
                        default:
                            days = wkB - (int)_date.DayOfWeek;
                            break;
                    }
                    break;
                    #endregion
            }

            return _date.AddDays(days);
        }
        private DateTime LastWeekDay(DateTime _date)
        {
            int days = 0;
            switch ((int)myCalendar1.parameters.WochenBeginn)
            {
                #region mit Montag
                case 1:
                    if (myCalendar1.parameters.Woche == MyCalendarParameters.Week.Full)
                    {
                        days = 6;
                    }
                    else
                    {
                        days = 4;
                    }
                    break;
                #endregion

                #region mit Sonntag
                case 0:
                    if (myCalendar1.parameters.Woche == MyCalendarParameters.Week.Full)
                    {
                        days = 6;
                    }
                    else
                    {
                        days = 4;
                    }
                    break;
                    #endregion
            }

            return _date.AddDays(days);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyFormCalendar));
            ((System.ComponentModel.ISupportInitialize)(this.FileArchiv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // pbOk
            // 
            this.pbOk.BackColor = System.Drawing.Color.Transparent;
            this.pbOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbOk.FlatAppearance.BorderSize = 2;
            this.pbOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.pbOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pbOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbOk.Image = ((System.Drawing.Image)(resources.GetObject("pbOk.Image")));
            this.pbOk.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pbOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.pbOk.UseVisualStyleBackColor = false;
            // 
            // FileArchiv
            // 
            this.FileArchiv.BackColor = System.Drawing.Color.Transparent;
            this.FileArchiv.Image = ((System.Drawing.Image)(resources.GetObject("FileArchiv.Image")));
            this.FileArchiv.Size = new System.Drawing.Size(32, 32);
            this.FileArchiv.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.FileArchiv.WaitOnLoad = true;
            // 
            // MyFormCalendar
            // 
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            ((System.ComponentModel.ISupportInitialize)(this.FileArchiv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
