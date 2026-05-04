using MyControls;
using System;
using System.Data;
using System.Drawing;

namespace FCC_Verwaltungssystem
{
    public class Maske_RechnungsFormular : MyForm
    {
        private MyDuseRichTextBox feld_Zahlungszieltext;
        private MyDuseRichTextBox feld_fusstext;
        private MyDuseRichTextBox feld_Kopftext;
        private MyDuseRichTextBox feld_EmailBody;

        protected override string _name()
        {
            return "Formular->Rechnung";
        }
        protected override void _OnLoad(EventArgs e)
        {
            _Populate();
        }
        protected override bool _Populate()
        {
            Formular formular = new Formular(Globals.FORMULAR_RECHNUNG);

            DataTable datatable_texte = DataAccessLayer.queryFormular(formular);
            if (datatable_texte != null && datatable_texte.Rows.Count > 0)
            {
                DataRow rowf = datatable_texte.Rows[0];
                string kText = rowf.Field<string>("KOPFTEXT");
                string fText = rowf.Field<string>("FUSSTEXT");
                string zahltext = rowf.Field<string>("ZAHLUNGSTEXT");
                string zusatztext = rowf.Field<string>("ZUSATZTEXT");
                feld_Kopftext.Texts = kText;
                feld_fusstext.Texts = fText;
                feld_Zahlungszieltext.Texts = zahltext;
                feld_EmailBody.Texts = zusatztext;
            }
            return true;
        }
        protected override bool _Save()
        {
            Formular formular = new Formular(Globals.FORMULAR_RECHNUNG)
            {
                Kopfttext = feld_Kopftext.Texts,
                Fusstext = feld_fusstext.Texts,
                Zahlungszieltext = feld_Zahlungszieltext.Texts,
                Zusatztext = feld_EmailBody.Texts
            };

            DataAccessLayer.Update_Formular(formular);

            return true;
        }
        protected override void _InitializeComponent()
        {
            feld_EmailBody = new MyDuseRichTextBox();
            feld_Kopftext = new MyDuseRichTextBox();
            feld_fusstext = new MyDuseRichTextBox();
            feld_Zahlungszieltext = new MyDuseRichTextBox();
            BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).BeginInit();
            SuspendLayout();
            // 
            // BorderBody
            // 
            BorderBody.Controls.Add(feld_Zahlungszieltext);
            BorderBody.Controls.Add(feld_fusstext);
            BorderBody.Controls.Add(feld_Kopftext);
            BorderBody.Controls.Add(feld_EmailBody);
            // 
            // feld_EmailBody
            // 
            feld_EmailBody.Location = new Point(18, 24);
            feld_EmailBody.Name = "feld_EmailBody";
            feld_EmailBody.PlaceholderColor = Color.DarkGray;
            feld_EmailBody.PlaceholderText = " EMail-Text";
            feld_EmailBody.Size = new Size(764, 145);
            feld_EmailBody.TabIndex = 1;
            // 
            // feld_Kopftext
            // 
            feld_Kopftext.Location = new Point(18, 185);
            feld_Kopftext.Name = "feld_Kopftext";
            feld_Kopftext.PlaceholderColor = Color.DarkGray;
            feld_Kopftext.PlaceholderText = " Kopftext auf dem Rechnungsformular";
            feld_Kopftext.Size = new Size(764, 133);
            feld_Kopftext.TabIndex = 2;
            // 
            // feld_fusstext
            // 
            feld_fusstext.Location = new Point(18, 337);
            feld_fusstext.Name = "feld_fusstext";
            feld_fusstext.PlaceholderColor = Color.DarkGray;
            feld_fusstext.PlaceholderText = " Fußtext auf dem Rechnungsformular";
            feld_fusstext.Size = new Size(354, 152);
            feld_fusstext.TabIndex = 3;
            // 
            // feld_Zahlungszieltext
            // 
            feld_Zahlungszieltext.BackColor = SystemColors.ControlLightLight;
            feld_Zahlungszieltext.ForeColor = Color.DarkGray;
            feld_Zahlungszieltext.Location = new Point(396, 337);
            feld_Zahlungszieltext.Name = "feld_Zahlungszieltext";
            feld_Zahlungszieltext.PlaceholderColor = Color.DarkGray;
            feld_Zahlungszieltext.PlaceholderText = " Zahlungziel auf dem Rechnungsformular";
            feld_Zahlungszieltext.Size = new Size(386, 152);
            feld_Zahlungszieltext.TabIndex = 4;
            BorderBody.ResumeLayout(false);
            Icon = Properties.Resources.document_ico;
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}