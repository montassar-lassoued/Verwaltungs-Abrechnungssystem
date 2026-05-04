using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;

namespace MyControls
{
    [ToolboxItem(true)]
    public class MyIconArchiv : PictureBox
    {
        public delegate void OnDragEnterHandler(Object sender, EventArgs e);
        public event OnDragEnterHandler DragStart;
        public DocumentArchiv dokumentArchiv { get; set; }
        private ContextMenuStrip archivContextMenu = new ContextMenuStrip();

        private Image normalImage = Properties.Resources.file_archive_icon;
        private Image grayImage = Properties.Resources.file_archive_grau;

        public void DateiArchivieren(string filePath)
        {
            // PDF in die Datenbank speichern (SqlFileStream oder normal)
            string fName = Path.GetFileNameWithoutExtension(filePath);
            string fExtension = Path.GetExtension(filePath);
            string newName = fName + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + "_" + fExtension;
            // event feuern
            DragStart?.Invoke(this, EventArgs.Empty);
            SavePdfFileStream(filePath, newName);
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            BackColor = System.Drawing.Color.Transparent;
            //Image = normalImage;
            Location = new System.Drawing.Point(754, 63);
            Size = new System.Drawing.Size(32, 32);
            SizeMode = PictureBoxSizeMode.Zoom;
            TabStop = false;
            WaitOnLoad = true;
            ToolTip tip = new ToolTip();
            tip.SetToolTip(this, "Hier werden archivierte Dokumente abgelegt");
            DragDrop += ArchivDragAndDrop;
            DragEnter += ArchivDragEnter;
            AllowDrop = true;
            MouseUp += PictureBoxFiles_MouseUp;

            var b = FindForm();
            if (b is Intf_WinFormsBase handler)
            {
                DragStart += handler.OnArchivDragEnter;
            }
        }
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            this.Image = Enabled ? normalImage : grayImage;
        }
        private void ArchivDragEnter(object sender, DragEventArgs e)
        {
            // event feuern
            DragStart?.Invoke(this, EventArgs.Empty);
            // Prüfen, ob Datei gezogen wird
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                e.Effect = DragDropEffects.Copy;
                // Optional: nur PDFs akzeptieren
                /*if (Path.GetExtension(files[0]).ToLower() == ".pdf")
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;*/
            }
        }
        private void ArchivDragAndDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string filePath = files[0];

            if (Path.GetExtension(filePath).ToLower() != ".pdf")
            {
                //MessageBox.Show("Nur PDF-Dateien erlaubt!");
                // return;
            }
            // PDF in die Datenbank speichern (SqlFileStream oder normal)
            string fName = Path.GetFileNameWithoutExtension(filePath);
            string fExtension = Path.GetExtension(filePath);
            string newName = fName + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + "_" + fExtension;
            SavePdfFileStream(filePath, newName);
        }
        protected virtual void SavePdfFileStream(string path, string name)
        {
            DocumentArchiv e = dokumentArchiv;
            if (string.IsNullOrEmpty(e.TableName) || e.IdColumn < 1)
            {
                MessageBox.Show("Daten sind unvollständig!", "Stop!!");
                return;
            }

            Guid id = Guid.NewGuid();

            // 🔹 TransactionScope starten VOR INSERT + FILESTREAM-Zugriff
            using (TransactionScope ts = new TransactionScope())
            {
                // Verbindung öffnen, falls nicht offen
                if (Database.Connection.State != ConnectionState.Open)
                    Database.Connection.Open();

                using (SqlTransaction tx = Database.Connection.BeginTransaction())
                {
                    // 1️⃣ Datensatz anlegen
                    string sqlInsert = @"
        INSERT INTO DOKUMENTE (Id, PRIMARY_ID, _TABLE, _NAME, _FILE)
        VALUES (@ID, @PRIMARY_ID, @_TABLE, @_NAME,CAST('' AS VARBINARY(MAX)))";
                    using (SqlCommand cmd = new SqlCommand(sqlInsert, Database.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@PRIMARY_ID", e.IdColumn);
                        cmd.Parameters.AddWithValue("@_TABLE", e.TableName);
                        cmd.Parameters.AddWithValue("@_NAME", name);
                        cmd.ExecuteNonQuery();
                    }

                    // 2️⃣ Pfad und Transaktionskontext holen
                    string filePathName;
                    byte[] txContext;

                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT _FILE.PathName(), GET_FILESTREAM_TRANSACTION_CONTEXT() FROM DOKUMENTE WHERE Id=@id",
                        Database.Connection, tx))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                                throw new Exception("FILESTREAM-Pfad konnte nicht abgerufen werden.");

                            // Prüfen auf NULL
                            if (reader.IsDBNull(0))
                                throw new Exception("_FILE.PathName() ist NULL – wahrscheinlich FILESTREAM-Spalte noch leer.");

                            filePathName = reader.GetString(0);
                            txContext = (byte[])reader[1];
                        }
                    }

                    // 3️⃣ PDF in FILESTREAM schreiben
                    using (SqlFileStream sfs = new SqlFileStream(filePathName, txContext, FileAccess.Write))
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        fs.CopyTo(sfs);
                    }

                    tx.Commit();
                }

                ts.Complete();
            }
        }
        protected virtual void LoadPdfFileStream(Guid id, string savePath)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                if (Database.Connection.State != ConnectionState.Open)
                    Database.Connection.Open();

                // 1️⃣ FILESTREAM Pfad + TransactionContext abrufen
                string sqlPath = @"
            SELECT _FILE.PathName(), GET_FILESTREAM_TRANSACTION_CONTEXT()
            FROM DOKUMENTE
            WHERE Id=@ID";

                string filePathName = null;
                byte[] txContext = null;

                using (SqlCommand cmd = new SqlCommand(sqlPath, Database.Connection))
                {
                    cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = id;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            filePathName = reader.GetString(0);
                            object ctxObj = reader.GetValue(1);

                            if (ctxObj == DBNull.Value)
                            {
                                MessageBox.Show("TransactionContext ist NULL – FILESTREAM Zugriff nicht möglich!");
                                return;
                            }

                            txContext = (byte[])ctxObj;
                        }
                        else
                        {
                            MessageBox.Show("Datensatz nicht gefunden!");
                            return;
                        }
                    }
                }

                // 2️⃣ Datei aus FILESTREAM lesen und lokal speichern
                using (SqlFileStream sfs = new SqlFileStream(filePathName, txContext, FileAccess.Read))
                using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                {
                    sfs.CopyTo(fs);
                }

                ts.Complete();
            }

            MessageBox.Show($"PDF erfolgreich gespeichert: {savePath}");
        }
        private void PictureBoxFiles_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // event feuern
                DragStart?.Invoke(this, EventArgs.Empty);

                DocumentArchiv dok = dokumentArchiv;
                if (dok == null || string.IsNullOrEmpty(dok.TableName) || dok.IdColumn < 1)
                {
                    return;
                }

                var files = GetAttachedFiles(dok.IdColumn, dok.TableName);

                archivContextMenu.Items.Clear();

                if (files.Count == 0)
                {
                    archivContextMenu.Items.Add("Keine Dateien vorhanden");
                }
                else
                {
                    bool setting = false;
                    if (setting)
                    {
                        foreach (var file in files)
                        {
                            var fileMenu = CreateFileMenu(file);
                            archivContextMenu.Items.Add(fileMenu);
                            archivContextMenu.Items.Add(new ToolStripSeparator());
                        }
                    }
                    else
                    {
                        // Dateien nach Extension gruppieren
                        var pdfs = files.Where(f => f.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase)).ToList();
                        var words = files.Where(f => f.Name.EndsWith(".doc") || f.Name.EndsWith(".docx")).ToList();
                        var excels = files.Where(f => f.Name.EndsWith(".xls") || f.Name.EndsWith(".xlsx")).ToList();
                        var others = files.Except(pdfs).Except(words).Except(excels).ToList();

                        // PDFs einfügen
                        foreach (var file in pdfs)
                            archivContextMenu.Items.Add(CreateFileMenu(file));

                        if (pdfs.Any() && (words.Any() || excels.Any() || others.Any()))
                            archivContextMenu.Items.Add(new ToolStripSeparator());

                        // Word einfügen
                        foreach (var file in words)
                            archivContextMenu.Items.Add(CreateFileMenu(file));

                        if (words.Any() && (excels.Any() || others.Any()))
                            archivContextMenu.Items.Add(new ToolStripSeparator());

                        // Excel einfügen
                        foreach (var file in excels)
                            archivContextMenu.Items.Add(CreateFileMenu(file));

                        if (excels.Any() && others.Any())
                            archivContextMenu.Items.Add(new ToolStripSeparator());

                        // Rest einfügen
                        foreach (var file in others)
                            archivContextMenu.Items.Add(CreateFileMenu(file));
                    }

                }

                archivContextMenu.Show(this, e.Location);
            }
        }
        private ToolStripMenuItem CreateFileMenu((Guid Id, string Name) file)
        {
            var fileMenu = new ToolStripMenuItem(file.Name) { Tag = file.Id };

            var openItem = new ToolStripMenuItem("Öffnen") { Tag = file.Id };
            openItem.Click += PdfMenuItem_Click;

            var deleteItem = new ToolStripMenuItem("Löschen") { Tag = file.Id };
            deleteItem.Click += PdfDeleteItem_Click;

            fileMenu.DropDownItems.Add(openItem);
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add(deleteItem);

            return fileMenu;
        }
        private void PdfMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item && item.Tag is Guid fileId)
            {
                string tempPath = Path.Combine(Path.GetTempPath(), item.OwnerItem.Text);
                DownloadPdf(fileId, tempPath);

                System.Diagnostics.Process.Start(
                    new System.Diagnostics.ProcessStartInfo(tempPath) { UseShellExecute = true });
            }
        }
        private void PdfDeleteItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item && item.Tag is Guid fileId)
            {
                var confirm = MessageBox.Show(
                    $"Soll die Datei \"{item.OwnerItem.Text}\" wirklich gelöscht werden?",
                    "Löschen bestätigen",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    DeletePdf(fileId);
                }
            }
        }
        protected void DeletePdf(Guid fileId)
        {
            string sql = "DELETE FROM DOKUMENTE WHERE Id = @Id";

            using (SqlCommand cmd = new SqlCommand(sql, Database.Connection))
            {
                cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = fileId;
                cmd.ExecuteNonQuery();
            }
        }
        protected void DownloadPdf(Guid fileId, string savePath)
        {

            Transaction transaction = new Transaction();
            string sql = "SELECT _FILE.PathName(), GET_FILESTREAM_TRANSACTION_CONTEXT() FROM DOKUMENTE WHERE Id='" + fileId + "'";
            string filePathName = null;
            byte[] txContext = null;

            using (SqlDataReader reader = transaction.QuerySqlDataReader(sql))
            {
                if (reader.Read())
                {
                    filePathName = reader.GetString(0);
                    txContext = (byte[])reader.GetValue(1);
                }
                else
                    throw new Exception("Datei nicht gefunden!");
            }
            using (SqlFileStream sfs = new SqlFileStream(filePathName, txContext, FileAccess.Read))
            using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
            {
                sfs.CopyTo(fs);
            }

            transaction.Commit();
        }
        protected List<(Guid Id, string Name)> GetAttachedFiles(long primaryId, string tableName)
        {
            List<(Guid, string)> files = new List<(Guid, string)>();
            string sql = "SELECT Id, _NAME FROM DOKUMENTE WHERE PRIMARY_ID=@PRIMARY_ID AND _TABLE=@TABLE";

            using (SqlCommand cmd = new SqlCommand(sql, Database.Connection))
            {
                cmd.Parameters.AddWithValue("@PRIMARY_ID", primaryId);
                cmd.Parameters.AddWithValue("@TABLE", tableName);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        files.Add((reader.GetGuid(0), reader.GetString(1)));
                    }
                }
            }
            return files;
        }
    }
}
