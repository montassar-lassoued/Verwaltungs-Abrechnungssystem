using MyControls;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class Maske_Kosten : MyForm
    {
        private MyGroupBox myGroupBox2;
        private MyDuseCheckBox cb_AnsichtJahr;
        private MyGroupBox myGroupBox4;
        private MyGroupBox myGroupBox3;
        private MySearchFieldData feld_datum_von;
        private MyDuseCheckBox cb_AnsichtMonat;
        private MySearchCheckBox cb_FixKosten;
        private MySearchCheckBox cb_Jaehrlich;
        private MySearchCheckBox cb_Monatlich;
        private MySearchCheckBox cb_VariableKosten;
        private MySearchCheckBox cb_einmalig;
        private MySearchRadioButton rb_3JahreZeitram;
        private MySearchRadioButton rb_2JahreZeitram;
        private MySearchRadioButton rb_1JahrZeitram;
        private MySearchRadioButton rb_6MonateZeitram;
        private MySearchRadioButton rb_3MonateZeitram;
        private MySearchRadioButton rb_1MonatZeitram;
        private MyPrintButton myPrintButton1;
        private RowMergeView dataGridView;
        private MyPushButton pb_NeuenEintrag;
        private MyFieldText feld_total;
        private MyGroupBox myGroupBox1;

        protected override string _name()
        {
            return "Kostenübersicht";
        }
        protected override bool _EnableArchiv()
        {
            return true;
        }
        protected override DocumentArchiv _DocumentArchivData(DocumentArchiv dokument)
        {
            dokument.IdColumn = User.ID;
            dokument.TableName = "KOSTEN";

            return dokument;
        }
        protected override void _OnLoad(EventArgs e)
        {
            cb_AnsichtMonat.Checked = true;
            rb_3MonateZeitram.Checked = true;
            // Kostenart
            cb_einmalig.Checked = true;
            cb_Monatlich.Checked = true;
            cb_Jaehrlich.Checked = true;
            // Kosten
            cb_FixKosten.Checked = true;
            cb_VariableKosten.Checked = true;
            feld_datum_von.Text = DateTime.Now.Date.ToString();
        }
        protected override bool _Populate()
        {
            if (string.IsNullOrEmpty(feld_datum_von.Texts))
            {
                MessageBox.Show("Start Datum fehlt");
            }
            DateTime bis = DateTime.Parse(feld_datum_von.Texts);
            if (rb_1MonatZeitram.Checked)
            {
                bis = bis.AddMonths(1);
            }
            else if (rb_3MonateZeitram.Checked)
            {
                bis = bis.AddMonths(3);
            }
            else if (rb_6MonateZeitram.Checked)
            {
                bis = bis.AddMonths(6);
            }
            else if (rb_1JahrZeitram.Checked)
            {
                bis = bis.AddYears(1);
            }
            else if (rb_2JahreZeitram.Checked)
            {
                bis = bis.AddYears(2);
            }
            else if (rb_3JahreZeitram.Checked)
            {
                bis = bis.AddYears(3);
            }
            else
            {
                return false;
            }
            dataGridView.DataSource = null;
            string ansicht;
            if (cb_AnsichtMonat.Checked)
            {

                ansicht = Globals.KOSTEN_INTERVALL_MONATLICH;
            }
            else
            {

                ansicht = Globals.KOSTEN_INTERVALL_JAEHRLICH;
            }
            // GridView Darstellung
            if (cb_AnsichtMonat.Checked)
            {
                if (rb_1MonatZeitram.Checked || rb_3MonateZeitram.Checked || rb_6MonateZeitram.Checked)
                {
                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
            }
            else if (cb_AnsichtJahr.Checked)
            {
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            DataTable dataTable = DataAccessLayer.queryKosten(ansicht, feld_datum_von.Texts, bis.Date.AddDays(-1).ToString());

            DataTable newDt = CreateOwnerView(dataTable);

            dataGridView.DataSource = newDt;
            checkDataGridViewCellNullValues(dataGridView);
            addSumaryRow(newDt);
            DataGridViewStyle();
            dataGridView.ClearSelection();

            return true;
        }
        protected override bool _Save()
        {
            return false;
        }
        private void Pb_NeuerEintrag_Click(object sender, EventArgs e)
        {
            Maske_KostenEintrag maske_KostenEintrag = new Maske_KostenEintrag();
            maske_KostenEintrag.ShowDialog();
        }
        private void Ansicht_Wechseln(object sender, EventArgs e)
        {
            MyDuseCheckBox cb = sender as MyDuseCheckBox;
            if (cb != null)
            {
                if (cb.Checked)
                {
                    if (cb.Name.Equals(cb_AnsichtJahr.Name))
                    {
                        cb_AnsichtMonat.Checked = false;
                    }
                    else if (cb.Name.Equals(cb_AnsichtMonat.Name))
                    {
                        cb_AnsichtJahr.Checked = false;
                    }
                }
                else
                {
                    if (cb.Name.Equals(cb_AnsichtJahr.Name))
                    {
                        cb_AnsichtMonat.Checked = true;
                    }
                    else if (cb.Name.Equals(cb_AnsichtMonat.Name))
                    {
                        cb_AnsichtJahr.Checked = true;
                    }
                }

            }
        }
        protected override void _InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Maske_Kosten));
            myGroupBox1 = new MyGroupBox();
            cb_AnsichtMonat = new MyDuseCheckBox();
            cb_AnsichtJahr = new MyDuseCheckBox();
            myGroupBox2 = new MyGroupBox();
            feld_datum_von = new MySearchFieldData();
            myGroupBox3 = new MyGroupBox();
            rb_3JahreZeitram = new MySearchRadioButton();
            rb_2JahreZeitram = new MySearchRadioButton();
            rb_1JahrZeitram = new MySearchRadioButton();
            rb_6MonateZeitram = new MySearchRadioButton();
            rb_3MonateZeitram = new MySearchRadioButton();
            rb_1MonatZeitram = new MySearchRadioButton();
            myGroupBox4 = new MyGroupBox();
            cb_FixKosten = new MySearchCheckBox();
            cb_Jaehrlich = new MySearchCheckBox();
            cb_Monatlich = new MySearchCheckBox();
            cb_VariableKosten = new MySearchCheckBox();
            cb_einmalig = new MySearchCheckBox();
            dataGridView = new RowMergeView();
            myPrintButton1 = new MyPrintButton();
            pb_NeuenEintrag = new MyPushButton();
            feld_total = new MyFieldText();
            BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).BeginInit();
            myGroupBox1.SuspendLayout();
            myGroupBox2.SuspendLayout();
            myGroupBox3.SuspendLayout();
            myGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGridView)).BeginInit();
            SuspendLayout();
            // 
            // BorderBody
            // 
            BorderBody.Controls.Add(feld_total);
            BorderBody.Controls.Add(pb_NeuenEintrag);
            BorderBody.Controls.Add(myPrintButton1);
            BorderBody.Controls.Add(dataGridView);
            BorderBody.Controls.Add(myGroupBox4);
            BorderBody.Controls.Add(myGroupBox3);
            BorderBody.Controls.Add(myGroupBox2);
            BorderBody.Controls.Add(myGroupBox1);
            // 
            // myGroupBox1
            // 
            myGroupBox1.BorderColor = Color.Red;
            myGroupBox1.BorderThickness = 1;
            myGroupBox1.Controls.Add(cb_AnsichtMonat);
            myGroupBox1.Controls.Add(cb_AnsichtJahr);
            myGroupBox1.Location = new Point(16, 29);
            myGroupBox1.Name = "myGroupBox1";
            myGroupBox1.Size = new Size(228, 57);
            myGroupBox1.TabIndex = 0;
            myGroupBox1.TabStop = false;
            myGroupBox1.Text = "Ansicht";
            // 
            // cb_AnsichtMonat
            // 
            cb_AnsichtMonat.Location = new Point(120, 24);
            cb_AnsichtMonat.Name = "cb_AnsichtMonat";
            cb_AnsichtMonat.Size = new Size(94, 23);
            cb_AnsichtMonat.TabIndex = 3;
            cb_AnsichtMonat.Text = "Monatlich";
            cb_AnsichtMonat.CheckedChanged += Ansicht_Wechseln;
            // 
            // cb_AnsichtJahr
            // 
            cb_AnsichtJahr.Location = new Point(11, 25);
            cb_AnsichtJahr.Name = "cb_AnsichtJahr";
            cb_AnsichtJahr.Size = new Size(77, 23);
            cb_AnsichtJahr.TabIndex = 2;
            cb_AnsichtJahr.Text = "Jährlich";
            cb_AnsichtJahr.CheckedChanged += Ansicht_Wechseln;
            // 
            // myGroupBox2
            // 
            myGroupBox2.BorderColor = Color.DimGray;
            myGroupBox2.BorderThickness = 1;
            myGroupBox2.Controls.Add(feld_datum_von);
            myGroupBox2.Location = new Point(16, 90);
            myGroupBox2.Name = "myGroupBox2";
            myGroupBox2.Size = new Size(228, 59);
            myGroupBox2.TabIndex = 1;
            myGroupBox2.TabStop = false;
            myGroupBox2.Text = "Suche ab dem";
            // 
            // feld_datum_von
            // 
            feld_datum_von.Location = new Point(11, 20);
            feld_datum_von.Name = "feld_datum_von";
            feld_datum_von.PlaceholderColor = Color.DarkGray;
            feld_datum_von.PlaceholderText = "";
            feld_datum_von.Size = new Size(203, 27);
            feld_datum_von.TabIndex = 4;
            feld_datum_von.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            feld_datum_von.Texts = "  .  .";
            feld_datum_von.ValidatingType = typeof(System.DateTime);
            feld_datum_von.ToolTip = " Startdatum";
            // 
            // myGroupBox3
            // 
            myGroupBox3.BorderColor = Color.DimGray;
            myGroupBox3.BorderThickness = 1;
            myGroupBox3.Controls.Add(rb_3JahreZeitram);
            myGroupBox3.Controls.Add(rb_2JahreZeitram);
            myGroupBox3.Controls.Add(rb_1JahrZeitram);
            myGroupBox3.Controls.Add(rb_6MonateZeitram);
            myGroupBox3.Controls.Add(rb_3MonateZeitram);
            myGroupBox3.Controls.Add(rb_1MonatZeitram);
            myGroupBox3.Location = new Point(251, 29);
            myGroupBox3.Name = "myGroupBox3";
            myGroupBox3.Size = new Size(250, 120);
            myGroupBox3.TabIndex = 2;
            myGroupBox3.TabStop = false;
            myGroupBox3.Text = "Zeitfenster";
            // 
            // rb_3JahreZeitram
            // 
            rb_3JahreZeitram.Location = new Point(130, 82);
            rb_3JahreZeitram.Name = "rb_3JahreZeitram";
            rb_3JahreZeitram.Size = new Size(73, 23);
            rb_3JahreZeitram.TabIndex = 5;
            rb_3JahreZeitram.TabStop = true;
            rb_3JahreZeitram.Text = "3 Jahre";
            // 
            // rb_2JahreZeitram
            // 
            rb_2JahreZeitram.Location = new Point(130, 53);
            rb_2JahreZeitram.Name = "rb_2JahreZeitram";
            rb_2JahreZeitram.Size = new Size(73, 23);
            rb_2JahreZeitram.TabIndex = 4;
            rb_2JahreZeitram.TabStop = true;
            rb_2JahreZeitram.Text = "2 Jahre";
            // 
            // rb_1JahrZeitram
            // 
            rb_1JahrZeitram.Location = new Point(130, 24);
            rb_1JahrZeitram.Name = "rb_1JahrZeitram";
            rb_1JahrZeitram.Size = new Size(65, 23);
            rb_1JahrZeitram.TabIndex = 3;
            rb_1JahrZeitram.TabStop = true;
            rb_1JahrZeitram.Text = "1 Jahr";
            // 
            // rb_6MonateZeitram
            // 
            rb_6MonateZeitram.Location = new Point(15, 82);
            rb_6MonateZeitram.Name = "rb_6MonateZeitram";
            rb_6MonateZeitram.Size = new Size(90, 23);
            rb_6MonateZeitram.TabIndex = 2;
            rb_6MonateZeitram.TabStop = true;
            rb_6MonateZeitram.Text = "6 Monate";
            // 
            // rb_3MonateZeitram
            // 
            rb_3MonateZeitram.Location = new Point(15, 53);
            rb_3MonateZeitram.Name = "rb_3MonateZeitram";
            rb_3MonateZeitram.Size = new Size(90, 23);
            rb_3MonateZeitram.TabIndex = 1;
            rb_3MonateZeitram.TabStop = true;
            rb_3MonateZeitram.Text = "3 Monate";
            // 
            // rb_1MonatZeitram
            // 
            rb_1MonatZeitram.Location = new Point(15, 24);
            rb_1MonatZeitram.Name = "rb_1MonatZeitram";
            rb_1MonatZeitram.Size = new Size(82, 23);
            rb_1MonatZeitram.TabIndex = 0;
            rb_1MonatZeitram.TabStop = true;
            rb_1MonatZeitram.Text = "1 Monat";
            // 
            // myGroupBox4
            // 
            myGroupBox4.BorderColor = Color.DimGray;
            myGroupBox4.BorderThickness = 1;
            myGroupBox4.Controls.Add(cb_FixKosten);
            myGroupBox4.Controls.Add(cb_Jaehrlich);
            myGroupBox4.Controls.Add(cb_Monatlich);
            myGroupBox4.Controls.Add(cb_VariableKosten);
            myGroupBox4.Controls.Add(cb_einmalig);
            myGroupBox4.Location = new Point(507, 29);
            myGroupBox4.Name = "myGroupBox4";
            myGroupBox4.Size = new Size(283, 120);
            myGroupBox4.TabIndex = 3;
            myGroupBox4.TabStop = false;
            myGroupBox4.Text = "Kostenart";
            // 
            // cb_FixKosten
            // 
            cb_FixKosten.Location = new Point(126, 54);
            cb_FixKosten.Name = "cb_FixKosten";
            cb_FixKosten.Size = new Size(93, 23);
            cb_FixKosten.TabIndex = 4;
            cb_FixKosten.Text = "Fix Kosten";
            // 
            // cb_Jaehrlich
            // 
            cb_Jaehrlich.Location = new Point(16, 83);
            cb_Jaehrlich.Name = "cb_Jaehrlich";
            cb_Jaehrlich.Size = new Size(72, 23);
            cb_Jaehrlich.TabIndex = 3;
            cb_Jaehrlich.Text = "Jählich";
            // 
            // cb_Monatlich
            // 
            cb_Monatlich.Location = new Point(16, 54);
            cb_Monatlich.Name = "cb_Monatlich";
            cb_Monatlich.Size = new Size(94, 23);
            cb_Monatlich.TabIndex = 2;
            cb_Monatlich.Text = "Monatlich";
            // 
            // cb_VariableKosten
            // 
            cb_VariableKosten.Location = new Point(126, 24);
            cb_VariableKosten.Name = "cb_VariableKosten";
            cb_VariableKosten.Size = new Size(128, 23);
            cb_VariableKosten.TabIndex = 1;
            cb_VariableKosten.Text = "Variable Kosten";
            // 
            // cb_einmalig
            // 
            cb_einmalig.Location = new Point(16, 25);
            cb_einmalig.Name = "cb_einmalig";
            cb_einmalig.Size = new Size(85, 23);
            cb_einmalig.TabIndex = 0;
            cb_einmalig.Text = "Einmalig";
            // 
            // rowMergeView1
            // 
            dataGridView.Location = new Point(16, 197);
            dataGridView.Name = "rowMergeView1";
            dataGridView.Size = new Size(774, 295);
            dataGridView.TabStop = false;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            // 
            // myPrintButton1
            // 
            myPrintButton1.BackColor = Color.Transparent;
            myPrintButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            myPrintButton1.FlatAppearance.BorderSize = 2;
            myPrintButton1.FlatAppearance.MouseDownBackColor = Color.Lime;
            myPrintButton1.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            myPrintButton1.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myPrintButton1.Image = ((Image)(resources.GetObject("myPrintButton1.Image")));
            myPrintButton1.ImageAlign = ContentAlignment.TopLeft;
            myPrintButton1.Location = new Point(670, 156);
            myPrintButton1.Name = "myPrintButton1";
            myPrintButton1.Size = new Size(120, 35);
            myPrintButton1.TabIndex = 5;
            myPrintButton1.Text = "Drucken";
            myPrintButton1.TextAlign = ContentAlignment.MiddleRight;
            myPrintButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            myPrintButton1.UseVisualStyleBackColor = false;
            myPrintButton1.ToPrintedTable = dataGridView;
            // 
            // pb_NeuenEintrag
            // 
            pb_NeuenEintrag.BackColor = Color.Transparent;
            pb_NeuenEintrag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            pb_NeuenEintrag.FlatAppearance.BorderSize = 2;
            pb_NeuenEintrag.FlatAppearance.MouseDownBackColor = Color.Lime;
            pb_NeuenEintrag.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pb_NeuenEintrag.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            pb_NeuenEintrag.ImageAlign = ContentAlignment.TopLeft;
            pb_NeuenEintrag.Location = new Point(251, 156);
            pb_NeuenEintrag.Name = "pb_NeuenEintrag";
            pb_NeuenEintrag.Size = new Size(413, 34);
            pb_NeuenEintrag.TabIndex = 8;
            pb_NeuenEintrag.Text = "Neue Kosten eintragen";
            pb_NeuenEintrag.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            pb_NeuenEintrag.UseVisualStyleBackColor = false;
            pb_NeuenEintrag.Click += Pb_NeuerEintrag_Click;
            // 
            // feld_total
            // 
            feld_total.ForeColor = Color.Red;
            feld_total.Location = new Point(19, 159);
            feld_total.Name = "feld_total";
            feld_total.PlaceholderColor = Color.Red;
            feld_total.PlaceholderText = " Total:";
            feld_total.Size = new Size(224, 27);
            feld_total.TabIndex = 9;
            feld_total.Text = " Total:";
            feld_total.Enabled = false;
            BorderBody.ResumeLayout(false);
            BorderBody.PerformLayout();
            Icon = Properties.Resources.Konto_ico;
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).EndInit();
            myGroupBox1.ResumeLayout(false);
            myGroupBox1.PerformLayout();
            myGroupBox2.ResumeLayout(false);
            myGroupBox2.PerformLayout();
            myGroupBox3.ResumeLayout(false);
            myGroupBox3.PerformLayout();
            myGroupBox4.ResumeLayout(false);
            myGroupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGridView)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
        private DataTable CreateOwnerView(DataTable data)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            DataColumn colBez = new DataColumn();
            colBez.ColumnName = "Bezeichnung";
            colBez.DataType = typeof(string);

            dt.Columns.Add(colBez);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                bool showRow = true;
                int index = 0;

                var bezeichnung = data.Rows[i]["BEZEICHNUNG"].ToString();
                var intervall = data.Rows[i]["INTERVALL"].ToString();
                var kostenart = data.Rows[i]["KOSTENART"].ToString();

                if (bezeichnung.Equals("--"))
                {
                    showRow = false;
                }
                // Filter Intervall
                if (intervall.Equals(Globals.KOSTEN_INTERVALL_EINMALIG))
                {
                    if (!cb_einmalig.Checked)
                    {
                        showRow = false;
                    }
                }
                else if (intervall.Equals(Globals.KOSTEN_INTERVALL_MONATLICH))
                {
                    if (!cb_Monatlich.Checked)
                    {
                        showRow = false;
                    }
                }
                else if (intervall.Equals(Globals.KOSTEN_INTERVALL_JAEHRLICH))
                {
                    if (!cb_Jaehrlich.Checked)
                    {
                        showRow = false;
                    }
                }
                // Filter Kostenart
                if (kostenart.Equals(Globals.FIX_KOSTEN))
                {
                    if (!cb_FixKosten.Checked)
                    {
                        showRow = false;
                    }
                }
                else if (kostenart.Equals(Globals.VARIABLE_KOSTEN))
                {
                    if (!cb_VariableKosten.Checked)
                    {
                        showRow = false;
                    }
                }

                // Zeile erzeugen wenn...
                if (showRow)
                {
                    DataRow[] dataRows = dt.Select("Bezeichnung = '" + bezeichnung + "'");
                    if (dataRows.Length == 0)
                    {
                        DataRow dataRow = dt.NewRow();
                        dataRow["Bezeichnung"] = bezeichnung;
                        dt.Rows.Add(dataRow);
                        index = dt.Rows.IndexOf(dataRow);
                    }
                    else
                    {
                        index = dt.Rows.IndexOf(dataRows[0]);
                    }
                }
                var col = data.Rows[i]["DATUM"].ToString();

                if (!dt.Columns.Contains(col))
                {
                    DataColumn column = new DataColumn();
                    column.ColumnName = col;
                    column.DataType = typeof(string);
                    dt.Columns.Add(column);
                }
                if (showRow)
                {
                    string betr = string.Format("{0:#,##0.00}", Convert.ToDecimal(data.Rows[i]["BETRAG"].ToString().Trim()));
                    dt.Rows[index][col] = betr;
                }

            }
            return dt;
        }
        private void checkDataGridViewCellNullValues(DataGridView dgv)
        {
            foreach (DataGridViewRow rw in dgv.Rows)
            {
                for (int i = 0; i < rw.Cells.Count; i++)
                {
                    if (rw.Cells[i].Value == null || rw.Cells[i].Value == DBNull.Value || string.IsNullOrEmpty(rw.Cells[i].Value.ToString()))
                    {
                        rw.Cells[i].Value = "0,00";
                    }
                }
            }
        }
        private const int SumaryRowIndex = 0;
        public void addSumaryRow(DataTable dataTable)
        {
            var foundRows = dataGridView.Rows.Cast<DataGridViewRow>();

            DataRow newRow = dataTable.NewRow();
            dataTable.Rows.InsertAt(newRow, SumaryRowIndex);
            int total = 0;
            foreach (DataColumn col in dataTable.Columns)
            {
                if (!col.ColumnName.Equals("Bezeichnung"))
                {
                    var brutto_total = foundRows.Where(row => !string.IsNullOrWhiteSpace(row.Cells[col.ColumnName].Value.ToString()));
                    string s = brutto_total.Sum(row => Convert.ToDouble(row.Cells[col.ColumnName].Value)).ToString();
                    newRow[col.ColumnName] = string.Format("{0:#,##0.00}", Convert.ToDecimal(s));
                    total += Convert.ToInt32(s);
                }
            }

            feld_total.Text = "Total:   " + string.Format("{0:#,##0.00}", Convert.ToDecimal(total.ToString()));

            dataTable.AcceptChanges();
            newRow.SetModified();

        }
        private void DataGridViewStyle()
        {
            //rowMergeView1.Rows[SumaryRowIndex].HeaderCell.Style.BackColor = Color.Orange;
            dataGridView.Rows[SumaryRowIndex].HeaderCell.Value = "$";
            foreach (DataGridViewTextBoxColumn col in dataGridView.Columns)
            {
                if (col.Index == 0)
                {
                    continue;
                }
                dataGridView.Rows[SumaryRowIndex].Cells[col.Name].Style.BackColor = Color.Orange;
                dataGridView.Rows[SumaryRowIndex].Cells[col.Name].Style.SelectionBackColor = Color.Orange;
            }
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Index == 0)
                {
                    dataGridView.Rows[row.Index].Cells[0].Style.BackColor = Color.WhiteSmoke;
                    continue;
                }
                dataGridView.Rows[row.Index].Cells[0].Style.BackColor = Color.LightGray;
            }
            dataGridView.Rows[SumaryRowIndex].Frozen = true;
            dataGridView.Columns[0].Frozen = true;
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
    }
}