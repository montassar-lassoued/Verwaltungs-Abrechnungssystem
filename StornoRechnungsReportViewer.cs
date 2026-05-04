
//using System.Net;
//using System.Net.Mail;
using Microsoft.Reporting.WinForms;
using MyControls;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace FCC_Verwaltungssystem
{
    public class StornoRechnungsReportViewer : MyForm
    {
        DataTable rechnungsdaten;
        DataTable leistungsdaten;
        DataTable setting;
        string kundenMail;
        string anrede;
        string zusatztext;
        public string FilePath { get; private set; }

        protected override string _name()
        {
            return "Report";
        }
        protected override void _InitializeComponent()
        {
            report = new ReportViewer();
            SuspendLayout();
            // 
            // reportViewer1
            // 
            BorderBody.Controls.Add(this.report);
            report.LocalReport.ReportEmbeddedResource = "FCC_Verwaltungssystem.StornoRechnung_Report.rdlc";
            this.report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.report.Location = new System.Drawing.Point(3, 16);
            this.report.Name = "reportViewer1";
            this.report.ServerReport.BearerToken = null;
            this.report.Size = new System.Drawing.Size(800, 490);

            StartPosition = FormStartPosition.CenterParent;
        }
        private ReportViewer report;

        public void mailReport(string stornoRechnungsnummer)
        {
            getData(stornoRechnungsnummer);
            sendMessage();
            Close();
        }
        public void PrintReport(string stornoRechnungsnummer)
        {
            WindowState = System.Windows.Forms.FormWindowState.Minimized;
            getData(stornoRechnungsnummer);
            try
            {
                Export(report.LocalReport, false);
                Print();
                Dispose_Stream();

                string fileName = Globals.FORMULAR_STORNO_RECHNUNG + "_" + stornoRechnungsnummer + ".pdf";
                FilePath = saveAsPDF(fileName);

                Close();
            }
            catch (Exception exp)
            {
                Log.Error(exp.Source + "\n" + exp.TargetSite.DeclaringType + "\n" + exp.Message);
            }
        }
        public void PreviewReport(string stornoRechnungsnummer)
        {
            getData(stornoRechnungsnummer);
            //string fileName = Globals.FORMULAR_STORNO_RECHNUNG + "_" + stornoRechnungsnummer + ".pdf";
            MaximizeBox = true;
            report.RefreshReport();
            ShowDialog();
        }
        public void getData(string stornoRechnungsnummer)
        {
            leistungsdaten = DataAccessLayer.getStornoLeistungenByRechnungsnummer(stornoRechnungsnummer);
            rechnungsdaten = DataAccessLayer.getStornoRechnungByNummer(stornoRechnungsnummer);
            Formular formular = new Formular(Globals.FORMULAR_STORNO_RECHNUNG);
            setting = DataAccessLayer.queryFormular(formular);

            ReportViewer_Load();
        }
        private void ReportViewer_Load()
        {
            try
            {
                DataRow row = rechnungsdaten.Rows[0];
                anrede = (string)row["ANREDE"];
                var kundenname = (string)row["NAME"];
                var strasse = (string)row["STRASSE"];
                var plz = (string)row["PLZ"];
                var ort = (string)row["ORT"];
                var land = (string)row["LAND"];
                var stornoNummer = (string)row["STORNO_RECHNUNGNR"];
                var bezugRechnungsnummer = (string)row["BEZUG_RECHNUNG"];
                var stornoDatum = (DateTime)row["STORNO_DATUM"];
                var waehrung = (string)row["WAEHRUNG"];
                kundenMail = (string)row["EMAIL"];

                var mwst = false;
                if (setting != null && setting.Rows.Count > 0)
                {
                    DataRow rowf = setting.Rows[0];
                    // Mehrwertsteuer auf der Rechnung
                    object bMwst = rowf["MWST"];

                    if (bMwst == DBNull.Value)
                    {

                    }
                    else
                    {
                        mwst = (bool)bMwst;
                    }

                }

                ReportParameter[] p = new ReportParameter[] {
            new ReportParameter("Kundenname",kundenname),
            new ReportParameter("Strasse",strasse),
            new ReportParameter("Plz",plz +", "+ort),
            new ReportParameter("Ort",ort),
            new ReportParameter("Land",land),
            new ReportParameter("Rechnungsnummer",stornoNummer),
            new ReportParameter("Rechnungsdatum",stornoDatum.Date.ToString("dd-MM-yyyy")),
             new ReportParameter("WAEHRUNG",waehrung),
            new ReportParameter("Bezug_Rechnung","Die vorliegende Stornierung bezieht sich auf die Rechnung Nr.: "+bezugRechnungsnummer),
            new ReportParameter("UST_ID",Firma.UmsatzsteuerID),
            new ReportParameter("Tel",Firma.Tel),
            new ReportParameter("Mail",Firma.Mail),
            new ReportParameter("Webseite",Firma.Webseite),
            new ReportParameter("G_fuehrer",Firma.G_Fuehrer),
            new ReportParameter("Bankbezeichnung",Firma.Bankbezeichnung),
            new ReportParameter("Kontoinhaber",Firma.Kontoinhaber),
            new ReportParameter("IBAN",Firma.Iban),
            new ReportParameter("BIC",Firma.Bic),
                new ReportParameter("MwSt",mwst.ToString())};

                ReportDataSource reportDataSource = new ReportDataSource("Leistungen", leistungsdaten);
                report.LocalReport.DataSources.Clear();
                report.LocalReport.DataSources.Add(reportDataSource);
                report.LocalReport.SetParameters(p);
            }
            catch (Exception exp)
            {
                Log.Error(exp.Source + "\n" + exp.TargetSite.DeclaringType + "\n" + exp.Message);
            }
        }
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        private Stream CreateStream(string name,
          string fileNameExtension, Encoding encoding,
          string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
        private void Export(LocalReport report, bool isLandscape)
        {
            string deviceInfo = string.Empty;
            if (isLandscape)
            {
                deviceInfo =
                   @"<DeviceInfo>
            <OutputFormat>EMF</OutputFormat>
            <PageWidth>11in</PageWidth>
            <PageHeight>8.5in</PageHeight>
            <MarginTop>0.25in</MarginTop>
            <MarginLeft>0.25in</MarginLeft>
            <MarginRight>0.25in</MarginRight>
            <MarginBottom>0.25in</MarginBottom>
        </DeviceInfo>";
            }
            else
            {
                deviceInfo =
                @"<DeviceInfo>
            <OutputFormat>EMF</OutputFormat>
            <PageWidth>8.5in</PageWidth>
            <PageHeight>11in</PageHeight>
            <MarginTop>0.25in</MarginTop>
            <MarginLeft>0.25in</MarginLeft>
            <MarginRight>0.25in</MarginRight>
            <MarginBottom>0.25in</MarginBottom>
        </DeviceInfo>";
            }
            Warning[] warnings;
            m_streams = new List<Stream>();

            // Create Report DataSource
            ReportDataSource rds = new ReportDataSource();

            rds.Value = leistungsdaten;

            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }
        private void Print()
        {
            PrinterSettings settings = new PrinterSettings(); //set printer settings
            string printerName = settings.PrinterName; //use default printer name

            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();

            if (!printDoc.PrinterSettings.IsValid)
            {
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }

        }
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);

        }
        private void Dispose_Stream()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }
        private void sendMessage()
        {
            DataRow r_row = rechnungsdaten.Rows[0];
            var rechnungsnummer = (string)r_row["STORNO_RECHNUNGNR"];

            string fileName = Globals.FORMULAR_STORNO_RECHNUNG + "_" + rechnungsnummer + ".pdf";
            FilePath = saveAsPDF(fileName);

            DataTable dataTable = DataAccessLayer.query_MailEinstellungen();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                string email_smtp = (string)row["EMAIL_SMTP"];
                string psw_smtp = (string)row["PASSWORT_SMTP"];
                int port_smtp = (int)row["PORT"];
                string host_smtp = (string)row["HOST"];
                bool ssl_smtp = (bool)row["SSL"];
                bool sInfo_smtp = (bool)row["STANDARD_INFO"];
                string email_outl = (string)row["EMAIL_OUT"];
                bool display_outl = (bool)row["DISPLAY_OUT"];
                string aktive_Einstellung = (string)row["AKTIV_EINST"];

                if (aktive_Einstellung.Equals("OUTLK"))
                {
                    Outlook.Application outlookApp = new Outlook.Application();
                    Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

                    mailItem.To = kundenMail;
                    mailItem.Subject = "Storno-Rechnung - Fight Club Chemnitz";
                    mailItem.Body = zusatztext.Replace("<ANREDE>", anrede);
                    mailItem.BodyFormat = Outlook.OlBodyFormat.olFormatHTML;
                    mailItem.Attachments.Add(FilePath);
                    if (display_outl)
                    {
                        mailItem.Display(false);

                    }
                    else
                    {
                        mailItem.Send();
                    }
                }
                else if (aktive_Einstellung.Equals("SMTP"))
                {
                    EMailData eMailData = new EMailData();
                    eMailData.Host = host_smtp;
                    eMailData.Port = port_smtp;
                    eMailData.Von = email_smtp;
                    eMailData.Passwort = psw_smtp;
                    eMailData.Ssl = ssl_smtp;
                    eMailData.Standard_info_verwenden = sInfo_smtp;
                    eMailData.An = kundenMail;
                    eMailData.Betreff = "Storno/Rechnungskorrektur - Fight Club Chemnitz";
                    eMailData.Body = "";
                    eMailData.Anhang1 = FilePath;
                    eMailData.Anhang1_Name = fileName;

                    Maske_Mailing maske_Mailing = new Maske_Mailing();
                    maske_Mailing.setParameters(eMailData, rechnungsnummer);
                    maske_Mailing.ShowDialog();
                }

            }

        }
        private string saveAsPDF(string fileName)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;

            byte[] bytes = report.LocalReport.Render(
                "PDF", null, out mimeType, out encoding, out filenameExtension,
                out streamids, out warnings);

            string standardpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (setting != null && setting.Rows.Count > 0)
            {
                DataRow rowf = setting.Rows[0];
                standardpath = rowf.Field<string>("ABLAGEORT");

            }

            var path = Path.Combine(standardpath, fileName);
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush();
                fs.Close();

                return path;
            }

        }
    }
}
