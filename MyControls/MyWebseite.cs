using System.Windows.Forms;

namespace MyControls
{
    public class MyWebseite : LinkLabel
    {
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            AutoSize = true;
            Cursor = Cursors.Hand;
            Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            LinkBehavior = LinkBehavior.HoverUnderline;
            //Size = new System.Drawing.Size(142, 13);
            TabStop = true;
            Text = !string.IsNullOrEmpty(Firma.Webseite) ? Firma.Webseite : "Webseite nicht vorhanden";
            LinkClicked += new LinkLabelLinkClickedEventHandler(LinkWebseite);
        }

        private void LinkWebseite(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var webseite = Firma.Webseite;
            if (!string.IsNullOrEmpty(webseite))
            {
                System.Diagnostics.Process.Start(webseite);
            }

        }
    }
}
