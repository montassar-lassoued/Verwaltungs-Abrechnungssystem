using MyControls;
using Serilog;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class Maske_Kurse : MyForm
    {
        private MyGroupBox myGroupBox2;
        private RowMergeView dataGrid;
        private MyFieldFactory feld_Betrag;
        private MyFieldText feld_Kursart;
        private MyGroupBox myGroupBox1;
        private MyFieldText feld_ID;

        protected override void _InitializeComponent()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Maske_Kurse));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.myGroupBox1 = new MyControls.MyGroupBox();
            this.myGroupBox2 = new MyControls.MyGroupBox();
            this.dataGrid = new MyControls.RowMergeView();
            this.feld_Kursart = new MyControls.MyFieldText();
            this.feld_Betrag = new MyControls.MyFieldFactory();
            feld_ID = new MyFieldText();
            this.BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.myGroupBox1.SuspendLayout();
            this.myGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
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
            // BorderBody
            // 
            this.BorderBody.Controls.Add(this.myGroupBox2);
            this.BorderBody.Controls.Add(this.myGroupBox1);
            // 
            // myGroupBox1
            // 
            this.myGroupBox1.BorderColor = System.Drawing.Color.DimGray;
            this.myGroupBox1.BorderThickness = 1;
            this.myGroupBox1.Controls.Add(this.feld_Betrag);
            this.myGroupBox1.Controls.Add(this.feld_Kursart);
            this.myGroupBox1.Location = new System.Drawing.Point(8, 29);
            this.myGroupBox1.Name = "myGroupBox1";
            this.myGroupBox1.Size = new System.Drawing.Size(788, 160);
            this.myGroupBox1.TabIndex = 0;
            this.myGroupBox1.TabStop = false;
            this.myGroupBox1.Text = "Anlegen";
            // 
            // myGroupBox2
            // 
            this.myGroupBox2.BorderColor = System.Drawing.Color.DimGray;
            this.myGroupBox2.BorderThickness = 1;
            this.myGroupBox2.Controls.Add(this.dataGrid);
            this.myGroupBox2.Location = new System.Drawing.Point(8, 195);
            this.myGroupBox2.Name = "myGroupBox2";
            this.myGroupBox2.Size = new System.Drawing.Size(788, 282);
            this.myGroupBox2.TabIndex = 1;
            this.myGroupBox2.TabStop = false;
            this.myGroupBox2.Text = "Kursarten";
            // 
            // dataGrid
            // 
            dataGrid.AllowUserToAddRows = false;
            dataGrid.ReadOnly = true;
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid.Location = new System.Drawing.Point(3, 16);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(782, 263);
            dataGrid.UserDeletingRow += Delete_Kurs;
            dataGrid.CellClick += DataGrid_CellClick;
            dataGrid.CellDoubleClick += dataGrid_CellDoubleClick;
            // 
            // feld_Kursart
            // 
            this.feld_Kursart.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Kursart.Location = new System.Drawing.Point(12, 28);
            this.feld_Kursart.Name = "feld_Kursart";
            this.feld_Kursart.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Kursart.PlaceholderText = " Kursart";
            this.feld_Kursart.Size = new System.Drawing.Size(762, 27);
            this.feld_Kursart.TabIndex = 0;
            this.feld_Kursart.Text = " Kursart";
            this.feld_Kursart.Texts = "";
            // 
            // feld_Betrag
            // 
            this.feld_Betrag.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Betrag.Location = new System.Drawing.Point(12, 71);
            this.feld_Betrag.Name = "feld_Betrag";
            this.feld_Betrag.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Betrag.PlaceholderText = "Betrag";
            this.feld_Betrag.Size = new System.Drawing.Size(249, 27);
            this.feld_Betrag.TabIndex = 1;
            this.feld_Betrag.Text = "Betrag";
            this.feld_Betrag.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.feld_Betrag.Texts = "";
            this.BorderBody.ResumeLayout(false);
            Icon = Properties.Resources.Kurse_ico;
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.myGroupBox1.ResumeLayout(false);
            this.myGroupBox1.PerformLayout();
            this.myGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        protected override string _name()
        {
            return "Kursübersicht";
        }

        protected override void _OnLoad(EventArgs e)
        {
            _Populate();

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            btn.HeaderText = "Löschen";
            btn.Name = "?";
            btn.Text = "?";
            btn.UseColumnTextForButtonValue = true;
            btn.ToolTipText = "Datensatz löschen";
            dataGrid.Columns.Add(btn);
        }

        protected override bool _Populate()
        {
            DataTable dt = DataAccessLayer.Query_Kurs();
            dataGrid.DataSource = dt;

            dataGrid.Columns["ID"].Visible = false;
            dataGrid.Columns["BETRAG"].HeaderText = "Betrag [€]";
            dataGrid.TabStop = false;
            dataGrid.ClearSelection();
            return true;
        }
        protected override bool _PlausibleBevorSave()
        {
            if (string.IsNullOrWhiteSpace(feld_Kursart.Texts))
            {
                errorProvider.SetError(feld_Kursart, "Bitte Kursart eingeben!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Betrag.Texts))
            {
                errorProvider.SetError(feld_Betrag, "Bitte Betrag eingeben!");
                return false;
            }
            if (string.IsNullOrEmpty(feld_ID.Texts))
            {
                var foundRows = dataGrid.Rows.Cast<DataGridViewRow>();
                var result = foundRows.Where(row => row.Cells["NAME"].Value.ToString().Trim().Equals(feld_Kursart.Texts.Trim()));

                if (result.Count() > 0)
                {
                    errorProvider.SetError(feld_Kursart, "Kursart ist schon vorhanden!");
                    return false;
                }
            }
            return true;
        }
        protected override bool _Save()
        {
            string kursart = feld_Kursart.Texts;
            string betrag = feld_Betrag.Texts;

            Kurs kurs = new Kurs();
            kurs.Name = kursart;
            kurs.Betrag = float.Parse(betrag);
            if (!string.IsNullOrWhiteSpace(feld_ID.Texts))
            {

                DataAccessLayer.update_KursBetrag(kurs);
                //log
                Log.Information("Kursart'{0}' mit dem Betrag {1}€ wurde aktualisiert", kurs.Name, kurs.Betrag);
                //grid refresh
                feld_Kursart.Enabled = true;
                feld_Kursart.Focus();
            }
            else
            {
                DataAccessLayer.Insert_Kursart(kurs);
                //log
                Log.Information("Kursart'{0}' mit dem Betrag {1}€ wurde hinzugefügt", kurs.Name, kurs.Betrag);
            }
            return _Populate();
        }
        private void DataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            string name = dataGrid.Columns[e.ColumnIndex].Name;

            if (name.Equals("?"))
            {
                string id = dataGrid.Rows[e.RowIndex].Cells["ID"].Value.ToString().Trim();

                DialogResult result = MessageBox.Show("Kursart löschen?", "Warning!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result.Equals(DialogResult.No))
                {
                    return;
                }
                if (!string.IsNullOrWhiteSpace(id))
                {
                    if (DataAccessLayer.GetExists_VertragByKursID(int.Parse(id)))
                    {
                        MessageBox.Show("Kursart kann nicht gelöscht werden. Es sind noch aktive Verträge mit diesem Kurs vorhanden");
                        dataGrid.ClearSelection();

                        return;
                    }
                }
                DataAccessLayer.Delete_KursByID(int.Parse(id));
                string kursart = dataGrid.Rows[e.RowIndex].Cells["NAME"].Value.ToString().Trim();
                //log
                Log.Information("Kursart'{0}' wurde gelöscht", kursart);
                _Populate();
                dataGrid.ClearSelection();
            }
        }
        private void Delete_Kurs(object sender, DataGridViewRowCancelEventArgs e)
        {
            string id = dataGrid.Rows[e.Row.Index].Cells[0].Value.ToString().Trim();

            if (!string.IsNullOrWhiteSpace(id))
            {
                DialogResult result = MessageBox.Show("Kursart löschen?", "Warning!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result.Equals(DialogResult.No))
                {
                    e.Cancel = true;
                    return;
                }
                if (DataAccessLayer.GetExists_VertragByKursID(int.Parse(id)))
                {
                    MessageBox.Show("Kursart kann nicht gelöscht werden. Es sind noch Verträge mit diesem Kurs aktiv vorhanden");
                    e.Cancel = true;

                    dataGrid.TabStop = false;
                    dataGrid.ClearSelection();

                    return;
                }
                DataAccessLayer.Delete_KursByID(int.Parse(id));
                string kursart = dataGrid.Rows[e.Row.Index].Cells["NAME"].Value.ToString().Trim();
                //log
                Log.Information("Kursart'{0}' wurde gelöscht", kursart);

                dataGrid.ClearSelection();
            }
        }
        private void dataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            string id = dataGrid.Rows[e.RowIndex].Cells["ID"].Value.ToString().Trim();
            string kursart = dataGrid.Rows[e.RowIndex].Cells["NAME"].Value.ToString().Trim();
            string betrag = dataGrid.Rows[e.RowIndex].Cells["BETRAG"].Value.ToString().Trim();

            feld_Kursart.Texts = kursart;
            feld_Kursart.Enabled = false;
            feld_Betrag.Texts = betrag;
            feld_ID.Texts = id;
        }
    }
}