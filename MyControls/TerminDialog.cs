using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MyControls
{
    internal class TerminDialog : MyForm
    {
        private MyFieldText Subj;
        private System.Windows.Forms.ComboBox End_box;
        private System.Windows.Forms.ComboBox Beginn_box;
        private MyFieldDate EndDay;
        private MyGroupBox myGroupBox2;
        private MyLabel myLabel2;
        private MyLabel myLabel1;
        private MyGroupBox myGroupBox1;
        private MyLabel myLabel4;
        private MyLabel myLabel3;
        private MyDuseRichTextBox Descrip;
        private MyFieldDate StartDay;

        private TimeSpan Tbeginn;
        private TimeSpan Tend;
        private string subject;
        private string description;
        public MyCalendarScaleTime scaleTime = new MyCalendarScaleTime();
        private DateTime sDate;
        private DateTime eDate;
        private MyCalendarDay day = new MyCalendarDay();
        private bool bCancel;
        private List<TimeSpan> DataSource = new List<TimeSpan>();
        private Color itemColor;
        private bool IsFreeTime;
        private Rectangle rect;

        protected override void _InitializeComponent()
        {
            this.StartDay = new MyControls.MyFieldDate();
            this.EndDay = new MyControls.MyFieldDate();
            this.Beginn_box = new System.Windows.Forms.ComboBox();
            this.End_box = new System.Windows.Forms.ComboBox();
            this.Subj = new MyControls.MyFieldText();
            this.Descrip = new MyControls.MyDuseRichTextBox();
            this.myGroupBox1 = new MyControls.MyGroupBox();
            this.myGroupBox2 = new MyControls.MyGroupBox();
            this.myLabel1 = new MyControls.MyLabel();
            this.myLabel2 = new MyControls.MyLabel();
            this.myLabel3 = new MyControls.MyLabel();
            this.myLabel4 = new MyControls.MyLabel();
            this.BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.myGroupBox1.SuspendLayout();
            this.myGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BorderBody
            // 
            this.BorderBody.Controls.Add(this.myGroupBox2);
            this.BorderBody.Controls.Add(this.myGroupBox1);
            this.BorderBody.Controls.Add(this.Descrip);
            this.BorderBody.Controls.Add(this.Subj);
            // 
            // StartDay
            // 
            this.StartDay.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.StartDay.Location = new System.Drawing.Point(23, 48);
            this.StartDay.Name = "StartDay";
            this.StartDay.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.StartDay.PlaceholderText = "";
            this.StartDay.Size = new System.Drawing.Size(203, 27);
            this.StartDay.TabIndex = 0;
            this.StartDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.StartDay.Texts = "  .  .";
            this.StartDay.ValidatingType = typeof(System.DateTime);
            // 
            // EndDay
            // 
            this.EndDay.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.EndDay.Location = new System.Drawing.Point(29, 48);
            this.EndDay.Name = "EndDay";
            this.EndDay.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.EndDay.PlaceholderText = "";
            this.EndDay.Size = new System.Drawing.Size(203, 27);
            this.EndDay.TabIndex = 1;
            this.EndDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.EndDay.Texts = "  .  .";
            this.EndDay.ValidatingType = typeof(System.DateTime);
            // 
            // Beginn_box
            // 
            this.Beginn_box.FormattingEnabled = true;
            this.Beginn_box.Location = new System.Drawing.Point(23, 110);
            this.Beginn_box.Name = "Beginn_box";
            this.Beginn_box.Size = new System.Drawing.Size(203, 21);
            this.Beginn_box.TabIndex = 2;
            // 
            // End_box
            // 
            this.End_box.FormattingEnabled = true;
            this.End_box.Location = new System.Drawing.Point(29, 110);
            this.End_box.Name = "End_box";
            this.End_box.Size = new System.Drawing.Size(203, 21);
            this.End_box.TabIndex = 3;
            // 
            // Subj
            // 
            this.Subj.ForeColor = System.Drawing.Color.DarkGray;
            this.Subj.Location = new System.Drawing.Point(31, 192);
            this.Subj.Name = "Subj";
            this.Subj.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.Subj.PlaceholderText = "  Bezeichnung";
            this.Subj.Size = new System.Drawing.Size(513, 27);
            this.Subj.TabIndex = 4;
            this.Subj.Text = "  Bezeichnung des Termins";
            this.Subj.Texts = "";
            // 
            // Descrip
            // 
            this.Descrip.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Descrip.ForeColor = System.Drawing.Color.DarkGray;
            this.Descrip.Location = new System.Drawing.Point(31, 240);
            this.Descrip.Name = "Descrip";
            this.Descrip.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.Descrip.PlaceholderText = "  Beschreibing";
            this.Descrip.Size = new System.Drawing.Size(513, 241);
            this.Descrip.TabIndex = 5;
            this.Descrip.Text = "  Beschreibing des Termins";
            this.Descrip.Texts = "";
            // 
            // myGroupBox1
            // 
            this.myGroupBox1.BorderColor = System.Drawing.Color.DimGray;
            this.myGroupBox1.BorderThickness = 1;
            this.myGroupBox1.Controls.Add(this.myLabel4);
            this.myGroupBox1.Controls.Add(this.myLabel3);
            this.myGroupBox1.Controls.Add(this.EndDay);
            this.myGroupBox1.Controls.Add(this.End_box);
            this.myGroupBox1.Location = new System.Drawing.Point(290, 26);
            this.myGroupBox1.Name = "myGroupBox1";
            this.myGroupBox1.Size = new System.Drawing.Size(254, 151);
            this.myGroupBox1.TabIndex = 6;
            this.myGroupBox1.TabStop = false;
            this.myGroupBox1.Text = "Ende des Termins";
            // 
            // myGroupBox2
            // 
            this.myGroupBox2.BorderColor = System.Drawing.Color.DimGray;
            this.myGroupBox2.BorderThickness = 1;
            this.myGroupBox2.Controls.Add(this.myLabel2);
            this.myGroupBox2.Controls.Add(this.myLabel1);
            this.myGroupBox2.Controls.Add(this.StartDay);
            this.myGroupBox2.Controls.Add(this.Beginn_box);
            this.myGroupBox2.Location = new System.Drawing.Point(31, 26);
            this.myGroupBox2.Name = "myGroupBox2";
            this.myGroupBox2.Size = new System.Drawing.Size(254, 151);
            this.myGroupBox2.TabIndex = 7;
            this.myGroupBox2.TabStop = false;
            this.myGroupBox2.Text = "Beginn desw Termins";
            // 
            // myLabel1
            // 
            this.myLabel1.AutoSize = true;
            this.myLabel1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myLabel1.ForeColor = System.Drawing.Color.Black;
            this.myLabel1.Location = new System.Drawing.Point(19, 26);
            this.myLabel1.Name = "myLabel1";
            this.myLabel1.Size = new System.Drawing.Size(32, 19);
            this.myLabel1.TabIndex = 3;
            this.myLabel1.Text = "Tag";
            this.myLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // myLabel2
            // 
            this.myLabel2.AutoSize = true;
            this.myLabel2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myLabel2.ForeColor = System.Drawing.Color.Black;
            this.myLabel2.Location = new System.Drawing.Point(19, 88);
            this.myLabel2.Name = "myLabel2";
            this.myLabel2.Size = new System.Drawing.Size(55, 19);
            this.myLabel2.TabIndex = 4;
            this.myLabel2.Text = "Uhrzeit";
            this.myLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // myLabel3
            // 
            this.myLabel3.AutoSize = true;
            this.myLabel3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myLabel3.ForeColor = System.Drawing.Color.Black;
            this.myLabel3.Location = new System.Drawing.Point(25, 26);
            this.myLabel3.Name = "myLabel3";
            this.myLabel3.Size = new System.Drawing.Size(32, 19);
            this.myLabel3.TabIndex = 5;
            this.myLabel3.Text = "Tag";
            this.myLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // myLabel4
            // 
            this.myLabel4.AutoSize = true;
            this.myLabel4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myLabel4.ForeColor = System.Drawing.Color.Black;
            this.myLabel4.Location = new System.Drawing.Point(25, 88);
            this.myLabel4.Name = "myLabel4";
            this.myLabel4.Size = new System.Drawing.Size(55, 19);
            this.myLabel4.TabIndex = 5;
            this.myLabel4.Text = "Uhrzeit";
            this.myLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BorderBody.ResumeLayout(false);
            this.BorderBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.myGroupBox1.ResumeLayout(false);
            this.myGroupBox1.PerformLayout();
            this.myGroupBox2.ResumeLayout(false);
            this.myGroupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        protected override string _name()
        {
            return "Termin";
        }
        protected override void _OnLoad(EventArgs e)
        {
            Beginn_box.DataSource = DataSource.ToList();
            Beginn_box.Text = Tbeginn.ToString();

            End_box.DataSource = DataSource.ToList();
            End_box.Text = Tend.ToString();
            StartDay.Texts = sDate.Date.ToString();
            EndDay.Texts = eDate.Date.ToString();
            Subj.Text = subject;
            Descrip.Text = description;

            if (IsFreeTime)
            {
                Beginn_box.Enabled = false;
                End_box.Enabled = false;
            }
        }
        protected override bool _Save()
        {
            TimeSpan ts = TimeSpan.Parse(Beginn_box.Text.ToString());
            TimeSpan ts2 = TimeSpan.Parse(End_box.Text.ToString());
            if (ts >= ts2 && !IsFreeTime)
            {
                MessageBox.Show("Zeit passt nicht, es soll Terminbeginn < als Terminende", "Hinweis!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            scaleTime.TimeBeginn = ts;
            scaleTime.TimeEnd = ts2;
            scaleTime.Subject = Subj.Text;
            scaleTime.Text = Descrip.Text;
            scaleTime.Color = itemColor;

            day.Date = DateTime.Parse(StartDay.Texts);
            eDate = DateTime.Parse(EndDay.Texts).Date;
            day.SelectedscaleTime = scaleTime;
            Cancel = false;
            Close();
            return true;
        }
        
        protected override void _Close()
        {
            Cancel = true;
        }
        public void SetParameters(MyCalendarDay _selectedDay, List<TimeSpan> _dataSource, DateTime startDate, DateTime endDate)
        {
            DataSource = _dataSource;
            IsFreeTime = false;

            if (_selectedDay.CreateNewItem)
            {
                if (_selectedDay.DayFreeAppoint.IsSelected)
                {
                    IsFreeTime = true;
                    Tbeginn = _selectedDay.Item.TimeBeginn;
                    Tend = _selectedDay.Item.TimeEnd;
                    subject = _selectedDay.Item.Subject;
                    description = _selectedDay.Item.Description;
                    sDate = _selectedDay.Date;
                    eDate = _selectedDay.Date;
                    //itemColor = Color.FromArgb(224, 230, 236);

                }
                else
                {
                    MyCalendarScaleTime _scaleTime = _selectedDay.ScaleTimes.Find(
              delegate (MyCalendarScaleTime scaleTime) { return scaleTime.IsSelected == true; });
                    if (_scaleTime != null)
                    {
                        Tbeginn = _scaleTime.TimeBeginn;
                        Tend = _scaleTime.TimeEnd;
                        subject = _scaleTime.Subject;
                        description = _scaleTime.Text;
                        sDate = _selectedDay.Date;
                        eDate = _selectedDay.Date;
                        //itemColor = Color.FromArgb(224, 230, 236);
                    }
                }

            }

            else if (_selectedDay.Item != null)
            {
                Tbeginn = _selectedDay.Item.TimeBeginn;
                Tend = _selectedDay.Item.TimeEnd;
                if (Tbeginn == Tend) { IsFreeTime = true; }
                subject = _selectedDay.Item.Subject;
                description = _selectedDay.Item.Description;
                sDate = startDate;
                eDate = endDate;
                itemColor = _selectedDay.Item.Color;
            }
        }
        public bool Cancel
        {
            get => bCancel;
            set => bCancel = value;
        }
        public MyCalendarDay getDay()
        {
            return day;
        }
        public DateTime getEndDate()
        {
            return eDate;
        }
    }
}