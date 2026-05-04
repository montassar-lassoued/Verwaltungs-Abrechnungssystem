using MyControls;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class Maske_Umsatz : MyForm
    {
        private RowMergeView dataGridView;
        private MyGroupBox myGroupBox2;
        private MyGroupBox myGroupBox1;
        private MyPrintButton pbPrint;
        private MySearchRadioButton rbTypAlle;
        private MySearchRadioButton rbStatusBezahlt;
        private MySearchRadioButton rbStatusAlle;
        private MySearchRadioButton rbStatusOffen;
        private MySearchRadioButton rbTypRechnung;
        private MySearchRadioButton rbTypStorno;
        private MyLabel myLabel1;
        private MySearchFieldData dt_bis;
        private MySearchFieldData dt_von;
        private MySearchCheckBox cb_Inkl_Vertraege;
        private MyGroupBox groupBox5;

        protected override void _InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Maske_Umsatz));
            groupBox5 = new MyGroupBox();
            dataGridView = new RowMergeView();
            myGroupBox1 = new MyGroupBox();
            myGroupBox2 = new MyGroupBox();
            pbPrint = new MyPrintButton();
            rbTypAlle = new MySearchRadioButton();
            rbTypRechnung = new MySearchRadioButton();
            rbTypStorno = new MySearchRadioButton();
            rbStatusBezahlt = new MySearchRadioButton();
            rbStatusOffen = new MySearchRadioButton();
            rbStatusAlle = new MySearchRadioButton();
            dt_von = new MySearchFieldData();
            dt_bis = new MySearchFieldData();
            myLabel1 = new MyLabel();
            cb_Inkl_Vertraege = new MySearchCheckBox();
            BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).BeginInit();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGridView)).BeginInit();
            myGroupBox1.SuspendLayout();
            myGroupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // BorderBody
            // 
            BorderBody.Controls.Add(pbPrint);
            BorderBody.Controls.Add(myGroupBox2);
            BorderBody.Controls.Add(myGroupBox1);
            BorderBody.Controls.Add(dataGridView);
            BorderBody.Controls.Add(groupBox5);
            // 
            // groupBox5
            // 
            groupBox5.BorderColor = Color.DarkGray;
            groupBox5.BorderThickness = 1;
            groupBox5.Controls.Add(cb_Inkl_Vertraege);
            groupBox5.Controls.Add(rbTypRechnung);
            groupBox5.Controls.Add(rbTypStorno);
            groupBox5.Controls.Add(rbTypAlle);
            groupBox5.Location = new Point(14, 19);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(349, 109);
            groupBox5.TabIndex = 0;
            groupBox5.TabStop = false;
            groupBox5.Text = "Typ";
            // 
            // dataGridView
            dataGridView.Location = new Point(14, 174);
            dataGridView.Size = new Size(778, 321);
            dataGridView.TabStop = false;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.CellFormatting += DataGridView_CellFormatting;
            // 
            // myGroupBox1
            // 
            myGroupBox1.BorderColor = Color.DarkGray;
            myGroupBox1.BorderThickness = 1;
            myGroupBox1.Controls.Add(rbStatusBezahlt);
            myGroupBox1.Controls.Add(rbStatusAlle);
            myGroupBox1.Controls.Add(rbStatusOffen);
            myGroupBox1.Location = new Point(369, 19);
            myGroupBox1.Name = "myGroupBox1";
            myGroupBox1.Size = new Size(176, 109);
            myGroupBox1.TabIndex = 1;
            myGroupBox1.TabStop = false;
            myGroupBox1.Text = "Status";
            // 
            // myGroupBox2
            // 
            myGroupBox2.BorderColor = Color.DarkGray;
            myGroupBox2.BorderThickness = 1;
            myGroupBox2.Controls.Add(myLabel1);
            myGroupBox2.Controls.Add(dt_bis);
            myGroupBox2.Controls.Add(dt_von);
            myGroupBox2.Location = new Point(551, 19);
            myGroupBox2.Name = "myGroupBox2";
            myGroupBox2.Size = new Size(241, 109);
            myGroupBox2.TabIndex = 2;
            myGroupBox2.TabStop = false;
            myGroupBox2.Text = "Zeitraum";
            // 
            // pbPrint
            // 
            pbPrint.BackColor = Color.Transparent;
            pbPrint.BackgroundImageLayout = ImageLayout.None;
            pbPrint.FlatAppearance.BorderSize = 2;
            pbPrint.FlatAppearance.MouseDownBackColor = Color.Lime;
            pbPrint.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pbPrint.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            pbPrint.Image = ((Image)(resources.GetObject("pbPrint.Image")));
            pbPrint.ImageAlign = ContentAlignment.TopLeft;
            pbPrint.Location = new Point(14, 136);
            pbPrint.Name = "pbPrint";
            pbPrint.Size = new Size(182, 32);
            pbPrint.TabIndex = 4;
            pbPrint.Text = "Drucken";
            pbPrint.TextAlign = ContentAlignment.MiddleRight;
            pbPrint.TextImageRelation = TextImageRelation.ImageBeforeText;
            pbPrint.UseVisualStyleBackColor = false;
            pbPrint.ToPrintedTable = dataGridView;
            // 
            // rbTypAlle
            // 
            rbTypAlle.Location = new Point(20, 20);
            rbTypAlle.Name = "rbTypAlle";
            rbTypAlle.Size = new Size(52, 23);
            rbTypAlle.TabIndex = 0;
            rbTypAlle.TabStop = true;
            rbTypAlle.Text = "Alle";
            rbTypAlle.CheckedChanged += RbTyp_Changed;
            // 
            // rbTypRechnung
            // 
            rbTypRechnung.Location = new Point(20, 78);
            rbTypRechnung.Name = "rbTypRechnung";
            rbTypRechnung.Size = new Size(91, 23);
            rbTypRechnung.TabIndex = 1;
            rbTypRechnung.TabStop = true;
            rbTypRechnung.Text = "Rechnung";
            rbTypRechnung.CheckedChanged += RbTyp_Changed;
            // 
            // rbTypStorno
            // 
            rbTypStorno.Location = new Point(20, 49);
            rbTypStorno.Name = "rbTypStorno";
            rbTypStorno.Size = new Size(68, 23);
            rbTypStorno.TabIndex = 2;
            rbTypStorno.TabStop = true;
            rbTypStorno.Text = "Storno";
            rbTypStorno.CheckedChanged += RbTyp_Changed;
            // 
            // rbStatusBezahlt
            // 
            rbStatusBezahlt.Location = new Point(21, 78);
            rbStatusBezahlt.Name = "rbStatusBezahlt";
            rbStatusBezahlt.Size = new Size(75, 23);
            rbStatusBezahlt.TabIndex = 4;
            rbStatusBezahlt.TabStop = true;
            rbStatusBezahlt.Text = "Bezahlt";
            // 
            // rbStatusOffen
            // 
            rbStatusOffen.Location = new Point(21, 49);
            rbStatusOffen.Name = "rbStatusOffen";
            rbStatusOffen.Size = new Size(62, 23);
            rbStatusOffen.TabIndex = 5;
            rbStatusOffen.TabStop = true;
            rbStatusOffen.Text = "Offen";
            // 
            // rbStatusAlle
            // 
            rbStatusAlle.Location = new Point(21, 20);
            rbStatusAlle.Name = "rbStatusAlle";
            rbStatusAlle.Size = new Size(52, 23);
            rbStatusAlle.TabIndex = 3;
            rbStatusAlle.TabStop = true;
            rbStatusAlle.Text = "Alle";
            // 
            // dt_von
            // 
            dt_von.Location = new Point(19, 40);
            dt_von.Name = "dt_von";
            dt_von.PlaceholderColor = Color.DarkGray;
            dt_von.PlaceholderText = "";
            dt_von.Size = new Size(203, 27);
            dt_von.TabIndex = 0;
            dt_von.TextAlign = HorizontalAlignment.Center;
            dt_von.Texts = "  .  .";
            dt_von.ValidatingType = typeof(System.DateTime);
            dt_von.ToolTip = "Suche in Zeitraum von";
            // 
            // dt_bis
            // 
            dt_bis.Location = new Point(19, 72);
            dt_bis.Name = "dt_bis";
            dt_bis.PlaceholderColor = Color.DarkGray;
            dt_bis.PlaceholderText = "";
            dt_bis.Size = new Size(203, 27);
            dt_bis.TabIndex = 1;
            dt_bis.TextAlign = HorizontalAlignment.Center;
            dt_bis.Texts = "  .  .";
            dt_bis.ToolTip = "Suche in Zeitraum bis";
            dt_bis.ValidatingType = typeof(System.DateTime);
            // 
            // myLabel1
            // 
            myLabel1.AutoSize = true;
            myLabel1.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel1.ForeColor = Color.Black;
            myLabel1.Location = new Point(20, 17);
            myLabel1.Name = "myLabel1";
            myLabel1.Size = new Size(63, 19);
            myLabel1.TabIndex = 2;
            myLabel1.Text = "von - bis";
            myLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cb_Inkl_Vertraege
            // 
            cb_Inkl_Vertraege.Location = new Point(137, 20);
            cb_Inkl_Vertraege.Name = "cb_Inkl_Vertraege";
            cb_Inkl_Vertraege.Size = new Size(191, 23);
            cb_Inkl_Vertraege.TabIndex = 3;
            cb_Inkl_Vertraege.Text = "inkl. monatliche Verträge";
            BorderBody.ResumeLayout(false);
            Icon = Properties.Resources.Rechnung_ico;
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).EndInit();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGridView)).EndInit();
            myGroupBox1.ResumeLayout(false);
            myGroupBox1.PerformLayout();
            myGroupBox2.ResumeLayout(false);
            myGroupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        protected override string _name()
        {
            return "Umsatzübersicht";
        }
        protected override bool _EnableArchiv()
        {
            return true;
        }
        protected override DocumentArchiv _DocumentArchivData(DocumentArchiv dokument)
        {
            dokument.IdColumn = User.ID;
            dokument.TableName = "UMSATZ";

            return dokument;
        }
        protected override void _OnLoad(EventArgs e)
        {
            // Standard Werte setzen
            rbTypAlle.Checked = true;
            rbStatusAlle.Checked = true;
            cb_Inkl_Vertraege.Checked = true;
            dt_von.Texts = (new DateTime(DateTime.Now.Year, 1, 1)).ToString();
            dt_bis.Texts = (DateTime.Now.Date).ToString();
        }
        protected override bool _Populate()
        {
            Umsatz umsatz = new Umsatz();
            umsatz.TypAlle = rbTypAlle.Checked;
            umsatz.TypRechnung = rbTypRechnung.Checked;
            umsatz.TypStorno = rbTypStorno.Checked;
            //
            umsatz.StatusAlle = rbStatusAlle.Checked;
            umsatz.StatusBezahlt = rbStatusBezahlt.Checked;
            umsatz.StatusOffen = rbStatusOffen.Checked;
            //
            umsatz.DatumVon = dt_von.Texts;
            umsatz.DatumBis = dt_bis.Texts;
            //
            //umsatz.InklKurse = cb_inkl_Kurse.getChecked();
            umsatz.InklVertraege = cb_Inkl_Vertraege.Checked;

            // suche
            DataTable dataTable = DataAccessLayer.queryUmsatz(umsatz);

            dataGridView.addSummaryRow("Zwischensumme", "Steuer", "Brutto");
            dataGridView.SetDataSource(dataTable);

            return true;
        }
        protected override bool _Save()
        {
            return false;
        }
        private void RbTyp_Changed(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButton.Name.Equals(rbTypAlle.Name))
            {
                rbStatusAlle.Checked = false;
                rbStatusBezahlt.Checked = false;
                rbStatusOffen.Checked = false;
                cb_Inkl_Vertraege.Checked = true;
                //
                rbStatusAlle.Enabled = false;
                rbStatusBezahlt.Enabled = false;
                rbStatusOffen.Enabled = false;
                cb_Inkl_Vertraege.Enabled = true;
            }
            else if (radioButton.Name.Equals(rbTypRechnung.Name))
            {
                rbStatusAlle.Checked = true;
                cb_Inkl_Vertraege.Checked = false;
                //
                rbStatusAlle.Enabled = true;
                rbStatusBezahlt.Enabled = true;
                rbStatusOffen.Enabled = true;
                cb_Inkl_Vertraege.Enabled = false;
            }
            else
            {
                rbStatusAlle.Checked = false;
                rbStatusBezahlt.Checked = false;
                rbStatusOffen.Checked = false;
                cb_Inkl_Vertraege.Checked = false;
                //
                rbStatusAlle.Enabled = false;
                rbStatusBezahlt.Enabled = false;
                rbStatusOffen.Enabled = false;
                cb_Inkl_Vertraege.Enabled = false;
            }

        }
        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string col = dataGridView.Columns[e.ColumnIndex].Name;
            if (col.Equals("TYP"))
            {
                if (e.Value.ToString().Equals("STORNO"))
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
            else if (col.Equals("BRUTTO") && e.RowIndex != 0)
            {
                float x;
                bool ok = float.TryParse(e.Value.ToString(), out x);
                if (ok)
                {
                    if (x < 0)
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.DarkGreen;
                    }
                }
            }

        }
    }
}