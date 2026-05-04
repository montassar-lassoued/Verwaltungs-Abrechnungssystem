using MyControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class Maske_KostenEintrag : MyForm
    {
        private MyGroupBox myGroupBox4;
        private MyGroupBox myGroupBox3;
        private MyPushButton pbNeuerZeitraum;
        private MyFieldFactory feld_Betrag;
        private MyFieldDate feld_DateBis;
        private MyFieldDate feld_DateVon;
        private MyGroupBox myGroupBox2;
        private MyRadioButton rb_Jaehrlich;
        private MyRadioButton rb_Monatlich;
        private MyRadioButton rb_Einmalig;
        private MyGroupBox myGroupBox1;
        private MyRadioButton rb_VariableKosten;
        private MyRadioButton rb_FixKosten;
        private System.Windows.Forms.Panel panel1;
        private TreeGridView dataGridView;
        private MyFieldText feld_Bezeichnung;
        private MyFieldText feld_ID;
        private KostenEintragHelper KostenEintragHelper = new KostenEintragHelper();

        protected override void _InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            feld_Bezeichnung = new MyFieldText();
            myGroupBox1 = new MyGroupBox();
            rb_VariableKosten = new MyRadioButton();
            rb_FixKosten = new MyRadioButton();
            myGroupBox2 = new MyGroupBox();
            rb_Jaehrlich = new MyRadioButton();
            rb_Monatlich = new MyRadioButton();
            rb_Einmalig = new MyRadioButton();
            myGroupBox3 = new MyGroupBox();
            panel1 = new System.Windows.Forms.Panel();
            pbNeuerZeitraum = new MyPushButton();
            feld_DateBis = new MyFieldDate();
            feld_DateVon = new MyFieldDate();
            feld_Betrag = new MyFieldFactory();
            myGroupBox4 = new MyGroupBox();
            dataGridView = new TreeGridView();
            feld_ID = new MyFieldText();
            BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).BeginInit();
            myGroupBox1.SuspendLayout();
            myGroupBox2.SuspendLayout();
            myGroupBox3.SuspendLayout();
            panel1.SuspendLayout();
            myGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGridView)).BeginInit();
            SuspendLayout();
            // 
            // BorderBody
            // 
            BorderBody.Controls.Add(dataGridView);
            BorderBody.Controls.Add(myGroupBox4);
            BorderBody.Controls.Add(myGroupBox3);
            BorderBody.Controls.Add(myGroupBox2);
            BorderBody.Controls.Add(myGroupBox1);
            // 
            // feld_Bezeichnung
            // 
            feld_Bezeichnung.Location = new System.Drawing.Point(6, 23);
            feld_Bezeichnung.Name = "feld_Bezeichnung";
            feld_Bezeichnung.PlaceholderColor = System.Drawing.Color.DarkGray;
            feld_Bezeichnung.Size = new System.Drawing.Size(527, 27);
            feld_Bezeichnung.TabIndex = 0;
            feld_Bezeichnung.Texts = "";
            // 
            // myGroupBox1
            // 
            myGroupBox1.BorderColor = System.Drawing.Color.DimGray;
            myGroupBox1.BorderThickness = 1;
            myGroupBox1.Controls.Add(rb_VariableKosten);
            myGroupBox1.Controls.Add(rb_FixKosten);
            myGroupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            myGroupBox1.Location = new System.Drawing.Point(560, 19);
            myGroupBox1.Name = "myGroupBox1";
            myGroupBox1.Size = new System.Drawing.Size(237, 66);
            myGroupBox1.TabIndex = 2;
            myGroupBox1.TabStop = false;
            myGroupBox1.Text = "Kostenart";
            // 
            // rb_VariableKosten
            // 
            rb_VariableKosten.Location = new System.Drawing.Point(121, 19);
            rb_VariableKosten.Name = "rb_VariableKosten";
            rb_VariableKosten.Size = new System.Drawing.Size(80, 23);
            rb_VariableKosten.TabIndex = 1;
            rb_VariableKosten.TabStop = true;
            rb_VariableKosten.Text = "Variable";
            // 
            // rb_FixKosten
            // 
            rb_FixKosten.Location = new System.Drawing.Point(40, 19);
            rb_FixKosten.Name = "rb_FixKosten";
            rb_FixKosten.Size = new System.Drawing.Size(45, 23);
            rb_FixKosten.TabIndex = 0;
            rb_FixKosten.TabStop = true;
            rb_FixKosten.Text = "Fix";
            // 
            // myGroupBox2
            // 
            myGroupBox2.BorderColor = System.Drawing.Color.DimGray;
            myGroupBox2.BorderThickness = 1;
            myGroupBox2.Controls.Add(rb_Jaehrlich);
            myGroupBox2.Controls.Add(rb_Monatlich);
            myGroupBox2.Controls.Add(rb_Einmalig);
            myGroupBox2.Location = new System.Drawing.Point(651, 91);
            myGroupBox2.Name = "myGroupBox2";
            myGroupBox2.Size = new System.Drawing.Size(146, 143);
            myGroupBox2.TabIndex = 3;
            myGroupBox2.TabStop = false;
            myGroupBox2.Text = "Zeitintervalle";
            // 
            // rb_Jaehrlich
            // 
            rb_Jaehrlich.Location = new System.Drawing.Point(26, 105);
            rb_Jaehrlich.Name = "rb_Jaehrlich";
            rb_Jaehrlich.Size = new System.Drawing.Size(76, 23);
            rb_Jaehrlich.TabIndex = 2;
            rb_Jaehrlich.TabStop = true;
            rb_Jaehrlich.Text = "Jährlich";
            // 
            // rb_Monatlich
            // 
            rb_Monatlich.Location = new System.Drawing.Point(26, 67);
            rb_Monatlich.Name = "rb_Monatlich";
            rb_Monatlich.Size = new System.Drawing.Size(93, 23);
            rb_Monatlich.TabIndex = 1;
            rb_Monatlich.TabStop = true;
            rb_Monatlich.Text = "Monatlich";
            // 
            // rb_Einmalig
            // 
            rb_Einmalig.Location = new System.Drawing.Point(26, 29);
            rb_Einmalig.Name = "rb_Einmalig";
            rb_Einmalig.Size = new System.Drawing.Size(84, 23);
            rb_Einmalig.TabIndex = 0;
            rb_Einmalig.TabStop = true;
            rb_Einmalig.Text = "Einmalig";
            // 
            // myGroupBox3
            // 
            myGroupBox3.BorderColor = System.Drawing.Color.DimGray;
            myGroupBox3.BorderThickness = 1;
            myGroupBox3.Controls.Add(panel1);
            myGroupBox3.Location = new System.Drawing.Point(9, 91);
            myGroupBox3.Name = "myGroupBox3";
            myGroupBox3.Size = new System.Drawing.Size(636, 143);
            myGroupBox3.TabIndex = 4;
            myGroupBox3.TabStop = false;
            myGroupBox3.Text = "Zeitraum";
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(pbNeuerZeitraum);
            panel1.Controls.Add(feld_DateBis);
            panel1.Controls.Add(feld_DateVon);
            panel1.Controls.Add(feld_Betrag);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(3, 16);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(630, 124);
            panel1.TabIndex = 0;
            // 
            // myPushButton1
            // 
            pbNeuerZeitraum.BackColor = System.Drawing.Color.Transparent;
            pbNeuerZeitraum.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            pbNeuerZeitraum.FlatAppearance.BorderSize = 2;
            pbNeuerZeitraum.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            pbNeuerZeitraum.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pbNeuerZeitraum.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pbNeuerZeitraum.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            pbNeuerZeitraum.Location = new System.Drawing.Point(534, 14);
            pbNeuerZeitraum.Name = "myPushButton1";
            pbNeuerZeitraum.Size = new System.Drawing.Size(89, 28);
            pbNeuerZeitraum.TabIndex = 3;
            pbNeuerZeitraum.Text = "+ Zeitraum";
            pbNeuerZeitraum.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            pbNeuerZeitraum.UseVisualStyleBackColor = false;
            pbNeuerZeitraum.Click += ErstelleZeitraum;
            // 
            // feld_DateBis
            // 
            feld_DateBis.BackColor = System.Drawing.SystemColors.ControlLightLight;
            feld_DateBis.Location = new System.Drawing.Point(218, 13);
            feld_DateBis.Name = "feld_DateBis";
            feld_DateBis.PlaceholderColor = System.Drawing.Color.DarkGray;
            feld_DateBis.PlaceholderText = "";
            feld_DateBis.Size = new System.Drawing.Size(203, 27);
            feld_DateBis.TabIndex = 1;
            feld_DateBis.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            feld_DateBis.Texts = "  .  .";
            feld_DateBis.ValidatingType = typeof(System.DateTime);
            // 
            // feld_DateVon
            // 
            feld_DateVon.BackColor = System.Drawing.SystemColors.ControlLightLight;
            feld_DateVon.Location = new System.Drawing.Point(7, 13);
            feld_DateVon.Name = "feld_DateVon";
            feld_DateVon.PlaceholderColor = System.Drawing.Color.DarkGray;
            feld_DateVon.PlaceholderText = "";
            feld_DateVon.Size = new System.Drawing.Size(203, 27);
            feld_DateVon.TabIndex = 0;
            feld_DateVon.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            feld_DateVon.Texts = "  .  .";
            feld_DateVon.ValidatingType = typeof(System.DateTime);
            // 
            // feld_Betrag
            // 
            feld_Betrag.Location = new System.Drawing.Point(433, 13);
            feld_Betrag.Name = "feld_Betrag";
            feld_Betrag.PlaceholderColor = System.Drawing.Color.DarkGray;
            feld_Betrag.PlaceholderText = "";
            feld_Betrag.Size = new System.Drawing.Size(92, 27);
            feld_Betrag.TabIndex = 2;
            feld_Betrag.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // myGroupBox4
            // 
            myGroupBox4.BorderColor = System.Drawing.SystemColors.Control;
            myGroupBox4.BorderThickness = 1;
            myGroupBox4.Controls.Add(feld_Bezeichnung);
            myGroupBox4.Location = new System.Drawing.Point(9, 19);
            myGroupBox4.Name = "myGroupBox4";
            myGroupBox4.Size = new System.Drawing.Size(545, 66);
            myGroupBox4.TabIndex = 3;
            myGroupBox4.TabStop = false;
            myGroupBox4.Text = "Bezeichnung";
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.Location = new System.Drawing.Point(9, 240);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.Size = new System.Drawing.Size(787, 257);
            dataGridView.CellDoubleClick += DataGridView_DoubleClick;
            BorderBody.ResumeLayout(false);
            Icon = Properties.Resources.Eintrag_ico;
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).EndInit();
            myGroupBox1.ResumeLayout(false);
            myGroupBox1.PerformLayout();
            myGroupBox2.ResumeLayout(false);
            myGroupBox2.PerformLayout();
            myGroupBox3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            myGroupBox4.ResumeLayout(false);
            myGroupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGridView)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
        protected override string _name()
        {
            return "Neuer Eintrag";
        }
        private void DataGridView_DoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

        }

        protected override void _OnLoad(EventArgs e)
        {
            rb_FixKosten.Checked = true;
            rb_Monatlich.Checked = true;
            feld_DateVon.Texts = DateTime.Now.Date.ToString();
            //KostenEintragHelper.startFieldCoordination(feld_DateVon, feld_DateBis, feld_Betrag, pbNeuerZeitraum);
            KostenEintragHelper.add(feld_DateVon, feld_DateBis, feld_Betrag, pbNeuerZeitraum);
            _Populate();
        }

        protected override bool _Save()
        {
            Kosten kosten = new Kosten();
            kosten.Bezeichnung = feld_Bezeichnung.Texts;

            string kostenart = "";
            if (rb_FixKosten.Checked)
            {
                kostenart = Globals.FIX_KOSTEN;
            }
            else if (rb_VariableKosten.Checked)
            {
                kostenart = Globals.VARIABLE_KOSTEN;
            }

            string intervall = "";
            if (rb_Einmalig.Checked)
            {
                intervall = Globals.KOSTEN_INTERVALL_EINMALIG;
            }
            else if (rb_Monatlich.Checked)
            {
                intervall = Globals.KOSTEN_INTERVALL_MONATLICH;
            }
            else if (rb_Jaehrlich.Checked)
            {
                intervall = Globals.KOSTEN_INTERVALL_JAEHRLICH;
            }

            kosten.Kostenart = kostenart;
            kosten.Intervall = intervall;
            List<Kostenzeitraum> kostenzeitraum = new List<Kostenzeitraum>();
            for (int i = 0; i < KostenEintragHelper.Count(); i++)
            {
                Kostenzeitraum zeitraum = new Kostenzeitraum();
                zeitraum.Datum_von = KostenEintragHelper.Zeitraeume_von[i].Texts;
                zeitraum.Datum_bis = KostenEintragHelper.Zeitraeume_bis[i].Texts;
                zeitraum.Betrag = float.Parse(KostenEintragHelper.Betraege[i].Texts);

                kostenzeitraum.Add(zeitraum);
            }
            kosten.Kostenzeitraum = kostenzeitraum;

            if (string.IsNullOrEmpty(feld_ID.Texts))
            {
                DataAccessLayer.Insert_Kosten(kosten);
            }
            else
            {
                kosten.Id = int.Parse(feld_ID.Texts);
                DataAccessLayer.Update_Kosten(kosten);
            }

            return true;
        }
        protected override void _AfterSave()
        {
            DeleteAllZeitraums();
            _Populate();
        }
        protected override bool _Populate()
        {
            DataTable dataTable = DataAccessLayer.getKosten();
            dataGridView.BuildTreeRecursive(dataTable, new[] { "KOSTENART", "INTERVALL", "BEZEICHNUNG" }, "KOSTEN");
            dataGridView.Columns["ID"].Visible = false;
            return true;
        }
        private void DeleteAllZeitraums()
        {
            List<Control> controls = KostenEintragHelper.getAllControls();
            foreach (Control ctr in controls)
            {
                panel1.Controls.Remove(ctr);
            }
            KostenEintragHelper.DeleteAllElements();
        }
        private void ZeitintervalleChanged(object sender, EventArgs e)
        {
            MyRadioButton radioButton = sender as MyRadioButton;
            if (radioButton.Name.Equals(rb_Einmalig.Name))
            {
                rb_FixKosten.Checked = false;
                rb_FixKosten.Enabled = false;

                rb_VariableKosten.Checked = false;
                rb_VariableKosten.Enabled = false;

                feld_DateBis.ResetText();
                feld_DateBis.Enabled = false;
            }
            else
            {
                rb_FixKosten.Checked = true;
                rb_FixKosten.Enabled = true;
                rb_VariableKosten.Enabled = true;
                feld_DateBis.Enabled = true;
            }
        }
        private void ErstelleZeitraum(object sender, EventArgs e)
        {
            MyFieldDate dateTextBox_von = ErstelleDateTextBox(null);
            //
            MyFieldDate dateTextBox_bis = ErstelleDateTextBox(null);
            //
            MyFieldFactory textFeld_betrag = ErstelleTextBox(null);
            //
            MyPushButton pbLoeschen = ErstelleButton();
            //
            AddToPanel(dateTextBox_von, dateTextBox_bis, textFeld_betrag, pbLoeschen);
        }

        private void AddToPanel(MyFieldDate dateTextBox_von, MyFieldDate dateTextBox_bis, MyFieldFactory textFeld_betrag, MyPushButton pbLoeschen)
        {
            KostenEintragHelper.add(dateTextBox_von, dateTextBox_bis, textFeld_betrag, pbLoeschen);
            KostenEintragHelper.ReassignmentElementsCoord();
            // Add Controls
            panel1.Controls.Add(dateTextBox_von);
            panel1.Controls.Add(dateTextBox_bis);
            panel1.Controls.Add(textFeld_betrag);
            panel1.Controls.Add(pbLoeschen);
        }

        private MyFieldDate ErstelleDateTextBox(string value)
        {
            int count = panel1.Controls.OfType<MyFieldDate>().ToList().Count;
            MyFieldDate txtBox = new MyFieldDate();
            txtBox.Size = feld_DateVon.Size;
            txtBox.Texts = value;
            txtBox.Name = "TB" + count;

            return txtBox;
        }
        private MyFieldFactory ErstelleTextBox(string value)
        {
            int count = panel1.Controls.OfType<MyFieldFactory>().ToList().Count;
            MyFieldFactory txtBox = new MyFieldFactory
            {
                Size = feld_Betrag.Size
            };
            //txtBox.Leave += BetragFormatieren;
            if (!string.IsNullOrEmpty(value))
            {
                txtBox.Texts = value;
            }
            return txtBox;
        }
        private MyPushButton ErstelleButton()
        {
            MyPushButton pb = new MyPushButton
            {
                Text = "Löschen",
                Size = pbNeuerZeitraum.Size
            };
            pb.Click += PbLoeschen_Click;
            pb.BackColor = Color.MistyRose;
            pb.TabIndex = 5000 + KostenEintragHelper.Count();

            return pb;
        }
        private void PbLoeschen_Click(object sender, EventArgs e)
        {
            MyPushButton button = sender as MyPushButton;
            if (button != null)
            {
                deleteControlByIndex(button.TabIndex);
                KostenEintragHelper.ReassignmentElementsCoord();
            }
        }
        private void deleteControlByIndex(int index)
        {
            Control t1 = KostenEintragHelper.getElementByIndex(index, "ZEITRAUM_VON");
            Control t2 = KostenEintragHelper.getElementByIndex(index, "ZEITRAUM_BIS"); ;
            Control t3 = KostenEintragHelper.getElementByIndex(index, "FELD_BETRAG");
            Control b1 = KostenEintragHelper.getElementByIndex(index, "BUTTON");

            KostenEintragHelper.DeleteByIndex(index);

            panel1.Controls.Remove(t1);
            panel1.Controls.Remove(t2);
            panel1.Controls.Remove(t3);
            panel1.Controls.Remove(b1);
        }
        private void DataGridView_DoubleClick(object sender, TreeGridViewCellEventArgs e)
        {
            if (e.Node.Level == 2)
            {
                DeleteAllZeitraums();
                long _id = long.Parse(dataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                if (_id > 0)
                {
                    //KOSTENART,INTERVALL,BEZEICHNUNG,VON,BIS,BETRAG,KOSTEN.ID
                    DataTable datatable = DataAccessLayer.getKostenByID(_id);
                    if (datatable.Rows.Count > 0)
                    {
                        DataRow row0 = datatable.Rows[0];
                        string kostenart = (string)row0["KOSTENART"];
                        string interval = (string)row0["INTERVALL"];
                        string bez = (string)row0["BEZEICHNUNG"];
                        DateTime von = (DateTime)row0["VON"];
                        DateTime bis = (DateTime)row0["BIS"];
                        float betrag = float.Parse(row0["BETRAG"].ToString());

                        if (kostenart.Equals(Globals.FIX_KOSTEN))
                        {
                            rb_FixKosten.Checked = true;
                        }
                        else if (kostenart.Equals(Globals.VARIABLE_KOSTEN))
                        {
                            rb_VariableKosten.Checked = true;
                        }
                        if (interval.Equals(Globals.KOSTEN_INTERVALL_MONATLICH))
                        {
                            rb_Monatlich.Checked = true;
                        }
                        else if (interval.Equals(Globals.KOSTEN_INTERVALL_JAEHRLICH))
                        {
                            rb_Jaehrlich.Checked = true;
                        }
                        else if (interval.Equals(Globals.KOSTEN_INTERVALL_EINMALIG))
                        {
                            rb_Einmalig.Checked = true;
                        }
                        feld_Bezeichnung.Texts = bez;
                        feld_ID.Texts = _id.ToString();
                        feld_DateVon.Texts = von.Date.ToString();
                        feld_DateBis.Texts = bis.Date.ToString();
                        feld_Betrag.Texts = betrag.ToString();
                    }
                    for (int i = 1; i < datatable.Rows.Count; i++)
                    {
                        DataRow row = datatable.Rows[i];
                        DateTime von = (DateTime)row["VON"];
                        DateTime bis = (DateTime)row["BIS"];
                        float betrag = float.Parse(row["BETRAG"].ToString());

                        MyFieldDate dateTextBox_von = ErstelleDateTextBox(von.Date.ToString());
                        //dateTextBox_von.Texts = von.Date.ToString();
                        //
                        MyFieldDate dateTextBox_bis = ErstelleDateTextBox(bis.Date.ToString());
                        //dateTextBox_bis.Texts = bis.Date.ToString();
                        //
                        MyFieldFactory textFeld_betrag = ErstelleTextBox(betrag.ToString());
                        //
                        MyPushButton pbLoeschen = ErstelleButton();
                        //
                        AddToPanel(dateTextBox_von, dateTextBox_bis, textFeld_betrag, pbLoeschen);
                    }
                }
            }
            else if (e.Node.Level == 3)
            {

            }
        }
    }
}