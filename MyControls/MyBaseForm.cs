using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MyControls
{
    public abstract partial class MyBaseForm : MyBaseWinForm
    {
        #region Variablen
        protected MainMenu mainMenu;
        MenuItem menu_Option;
        protected MenuItem item_SearchMode;
        private Label line;
        private Label Firmenname;
        private Label FirmenAddresse;
        private MyWebseite Webseite;
        private PictureBox FirmenLogo;
        protected myPbSave pbOk;
        private myPbClose pbAbbrechen;
        private Label BorderKopf;
        protected MyIconArchiv FileArchiv;
        private ContextMenuStrip archivContextMenu = new ContextMenuStrip();
        ContextMenuStrip contextMenuArchiv;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblText;
        private ToolStripStatusLabel lblSpacer;
        private ToolStripStatusLabel lblTime;
        private Timer timer;
        protected MyGroupBox BorderBody;
        protected ErrorProvider errorProvider;
        private List<ICustomControl> controls = new List<ICustomControl>();
        private bool searchModeEnabled = false;
        private bool DataNotSaved = false;
        #endregion
        #region
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        private bool saveAfterSearchMode = false;
        private bool isSearchWindow = false;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
        #region konstructor
        public MyBaseForm()
        {
            InitializeCustomComponent();
            _InitializeComponent();
        }
        private void InitializeCustomComponent()
        {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MyBaseForm));
            mainMenu = new MainMenu();
            menu_Option = new MenuItem();
            FirmenLogo = new PictureBox();
            pbOk = new myPbSave();
            pbAbbrechen = new myPbClose();
            Firmenname = new Label();
            FirmenAddresse = new Label();
            Webseite = new MyWebseite();
            BorderKopf = new Label();
            FileArchiv = new MyIconArchiv();
            contextMenuArchiv = new ContextMenuStrip();
            line = new Label();
            errorProvider = new ErrorProvider();
            statusStrip = new StatusStrip();
            BorderBody = new MyGroupBox();
            ((ISupportInitialize)(FirmenLogo)).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            //
            // Set the caption of the menu items.
            menu_Option.Text = "&Option";
            menu_Option.Shortcut = Shortcut.CtrlO;
            menu_Option.ShowShortcut = true;
            menu_Option.BarBreak = false;
            menu_Option.PerformClick();
            menu_Option.Visible = Enable_MenuOption(menu_Option);

            mainMenu.MenuItems.Add(menu_Option);
            Menu = mainMenu;
            // 
            // pMyLogo
            // 
            FirmenLogo.Image = Properties.Resources.toro;
            FirmenLogo.Location = new Point(8, 9);
            FirmenLogo.Name = "pMyLogo";
            FirmenLogo.Size = new Size(106, 94);
            FirmenLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            FirmenLogo.TabStop = false;
            // 
            // pbSpeichern
            // 
            pbOk.Location = new Point(433, 12);
            pbOk.Enabled = _EnablePbSave();
            // 
            // pbAbbrechen
            // 
            pbAbbrechen.Location = new Point(620, 12);
            // 
            // tFirmenname
            // 
            Firmenname.AutoSize = true;
            Firmenname.BackColor = Color.Transparent;
            Firmenname.BorderStyle = BorderStyle.Fixed3D;
            Firmenname.Font = new Font("Calibri", 20F, FontStyle.Bold);
            Firmenname.ForeColor = Color.Red;
            Firmenname.Location = new Point(120, 9);
            Firmenname.Name = "tFirmenname";
            Firmenname.Size = new Size(238, 35);
            Firmenname.Text = Firma.Name;
            // 
            // tAddresse
            // 
            FirmenAddresse.BackColor = Color.Transparent;
            FirmenAddresse.BorderStyle = BorderStyle.Fixed3D;
            FirmenAddresse.Font = new Font("Calibri", 12F, FontStyle.Bold);
            FirmenAddresse.ForeColor = Color.DimGray;
            FirmenAddresse.Location = new Point(120, 48);
            FirmenAddresse.Name = "tAddresse";
            FirmenAddresse.Size = new Size(238, 47);
            FirmenAddresse.Text = Firma.Adresse;
            // 
            // Webseite
            // 
            Webseite.Location = new Point(119, 97);
            // label1
            // 
            BorderKopf.BorderStyle = BorderStyle.Fixed3D;
            BorderKopf.FlatStyle = FlatStyle.System;
            BorderKopf.Location = new Point(4, 3);
            BorderKopf.Name = "label1";
            BorderKopf.Size = new Size(806, 106);
            // Archiv
            FileArchiv.Enabled = false;
            FileArchiv.Enabled = true;
            FileArchiv.Location = new Point(754, 63);
            FileArchiv.Enabled = _EnableArchiv();
            
            // 
            //Line
            //
            line.BackColor = Color.DarkOrange;
            line.Location = new Point(224, 114);
            line.Name = "label1";
            line.Size = new Size(371, 1);
            line.Height = 1;
            line.Text = "";
            // 
            // statusStrip1
            // 

            //statusStrip.Location = new Point(0, 640);
            statusStrip.Name = "statusStrip";
            //statusStrip.Size = new Size(814, 22);
            statusStrip.Text = "statusStrip";
            statusStrip.Dock = DockStyle.Bottom;

            lblText = new ToolStripStatusLabel("  eingeloggt: " + Anmeldung.Vorname + " " + Anmeldung.Nachname);
            lblSpacer = new ToolStripStatusLabel { Spring = true };
            lblTime = new ToolStripStatusLabel();

            lblText.ForeColor = Color.Gray;
            lblTime.ForeColor = Color.Gray;

            statusStrip.Items.Add(lblText);
            statusStrip.Items.Add(lblSpacer);
            statusStrip.Items.Add(lblTime);

            // Timer für Uhrzeit
            timer = new Timer();
            timer.Interval = 1000; // 1 Sekunde
            timer.Tick += Timer_Tick;
            timer.Start();
            Timer_Tick(null, null); // initiale Anzeige
            //
            // BodyRahmen
            //
            BorderBody.BackColor = Color.Transparent;
            BorderBody.BorderColor = Color.LightBlue;
            BorderBody.BorderThickness = 1;
            BorderBody.Location = new Point(4, 128);
            BorderBody.Name = "BorderBody";
            BorderBody.Size = new Size(806, 509);
            BorderBody.TabStop = false;
            // 
            // MyBaseForm
            // 
            AcceptButton = pbOk;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            CancelButton = pbAbbrechen;
            ClientSize = new Size(814, 662);

            Controls.Add(FirmenLogo);
            Controls.Add(pbOk);
            Controls.Add(pbAbbrechen);
            Controls.Add(Firmenname);
            Controls.Add(FirmenAddresse);
            Controls.Add(line);
            Controls.Add(Webseite);
            Controls.Add(FileArchiv);
            Controls.Add(BorderKopf);
            Controls.Add(statusStrip);
            Controls.Add(BorderBody);

            FormBorderStyle = FormBorderStyle.FixedSingle;
            ImeMode = ImeMode.Off;
            MaximizeBox = false;
            Name = _name();
            Text = _name();
            StartPosition = FormStartPosition.CenterParent;
            TransparencyKey = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            //Load += new System.EventHandler(base_Load);
            ((ISupportInitialize)(FirmenLogo)).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }


        #endregion

        #region virtual
        protected virtual bool _EnableArchiv()
        {
            return false;
        }
        protected virtual bool _Enable_MenuOption(MenuItem menu_Option)
        {
            return true;
        }
        protected virtual bool _EnableSearchModeMenu()
        {
            return false;
        }
        protected virtual bool _SearchModeEnabling()
        {
            return true;
        }
        protected virtual void _SearchModeEnabled()
        {
        }
        protected virtual void _SearchModeDisabled()
        {
        }
        protected abstract void _InitializeComponent();
        protected abstract string _name();
        protected abstract void _OnLoad(EventArgs e);
        protected abstract bool _Save();
        protected abstract bool _Populate();
        protected virtual bool _PbSave_AllwayEnabled()
        {
            return false;
        }
        protected virtual bool _PlausibleBevorSave()
        {
            return true;
        }
        protected virtual void _AfterSave()
        {
        }
        protected virtual void _Close()
        {

        }
        protected virtual bool _EnablePbSave()
        {
            return true;
        }
        protected virtual DocumentArchiv _DocumentArchivData(DocumentArchiv document)
        {
            return document;
        }
        #endregion

        #region private
        private DocumentArchiv DocumentArchivData()
        {
            DocumentArchiv document = new DocumentArchiv();
            return _DocumentArchivData(document);
        }
        private bool Enable_MenuOption(MenuItem menu_Option)
        {
            if (_EnableSearchModeMenu())
            {
                // Backup
                item_SearchMode = new MenuItem();
                item_SearchMode.Text = "Suchfilter aktivieren";
                item_SearchMode.Shortcut = Shortcut.CtrlS;
                item_SearchMode.ShowShortcut = true;
                item_SearchMode.BarBreak = false;
                item_SearchMode.Click += new System.EventHandler(WindowModeChanged);
                menu_Option.MenuItems.Add(item_SearchMode);
            }
            return _Enable_MenuOption(menu_Option);
        }
        private void WindowModeChanged(object sender, EventArgs e)
        {
            // Status umschalten
            searchModeEnabled = !searchModeEnabled;
            saveAfterSearchMode = false;

            if (searchModeEnabled)
            {
                if (!_SearchModeEnabling())
                {
                    searchModeEnabled = !searchModeEnabled;
                    return;
                }
                item_SearchMode.Text = "Suchfilter deaktivieren";
                // Suchmodus aktivieren
                foreach (var ctrl in GetAllControls(this))
                {
                    if (ctrl is ICustomControl handler)
                    {
                        handler.ActivateSearchMode();
                    }
                }
                EnablePbSave();
                _SearchModeEnabled();
            }
            else
            {
                item_SearchMode.Text = "Suchfilter aktivieren";
                // TODO: Suchmodus deaktivieren
                foreach (var ctrl in GetAllControls(this))
                {
                    if (ctrl is ICustomControl handler)
                    {                      
                       handler.DeactivateSearchMode();
                    }
                }
                DisablePbSave();
                _SearchModeDisabled();
            }
            //Maske leeren
            ClearMask();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("dd.MMMM.yyyy") +"   "+DateTime.Now.ToString("HH:mm:ss");
        }
        private void UpdateSaveButtonState()
        {
            bool hasSearch = controls.Any(c => c.Role.Equals(ControlRole.Search));
            
            if (hasSearch)
            {
                EnablePbSave(); // bleibt immer aktiv
                isSearchWindow = true;
            }
        }
        #endregion

        #region override
        protected sealed override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateSaveButtonState();
            if ((_EnablePbSave() && searchModeEnabled) || isSearchWindow)
            {
                EnablePbSave();
            }
            else
            {
                DisablePbSave();
            }

            _OnLoad(e);
            DataNotSaved = false;
        }
        public sealed override void OnSaveData(object sender, EventArgs e)
        {
            if ((!searchModeEnabled || saveAfterSearchMode) && !isSearchWindow)
            {
                if (!_PlausibleBevorSave())
                    return;
                if (_Save())
                {
                    // Tooltip
                    int VisibleTime = 2000;  //in milliseconds
                    ToolTip tt = new ToolTip();
                    tt.BackColor = Color.LightYellow;
                    tt.IsBalloon = true;
                    tt.ForeColor = Color.Black;
                    tt.ToolTipIcon = ToolTipIcon.Info;
                    tt.Show("Daten sind gespeichert", pbOk, 60, 60, VisibleTime);

                    //Maske leeren -> nur wenn kein Suchmodus bzw. Suchfenster ist
                    ClearMask();
                    if (saveAfterSearchMode)
                    {
                        WindowModeChanged(this, EventArgs.Empty);
                    }
                    
                    _AfterSave();
                    DataNotSaved = false;
                    return;
                }
            }

            // für das Speichern nach Bearbeiten, dürfen die Daten nicht nochmal populiert werden
            else
            {
                // Daten anzeigen
                if (_Populate())
                {
                    if (!isSearchWindow && searchModeEnabled)
                        saveAfterSearchMode = true;
                }
            }
            
        }
        public sealed override void OnEditMask(object sender, EventArgs e)
        {
            // den Error erstmal ausblenden
            var control = sender as Control;
            if(control is Control ctrl)
            {
                if (!string.IsNullOrEmpty(errorProvider.GetError(ctrl)))
                {
                    errorProvider.SetError(ctrl, "");
                }
            }
            // OK aktivieren
            EnablePbSave();
            // Variable später für abbrechen setzen
            // 
            if (!isSearchWindow && (!searchModeEnabled || (searchModeEnabled && saveAfterSearchMode)))
            {
                DataNotSaved = true;
            }
            
        }
        protected sealed override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(!_EnableSearchModeMenu())
                return base.ProcessCmdKey(ref msg, keyData);

            if (keyData == (Keys.Control | Keys.S))
            {
                if(_EnableSearchModeMenu()) // Suchfilter im Fenster ist nicht aktiviert
                    return base.ProcessCmdKey(ref msg, keyData);

                WindowModeChanged(this, EventArgs.Empty);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public override void OnArchivDragEnter(object sender, EventArgs e)
        {
            FileArchiv.dokumentArchiv = DocumentArchivData();
        }

        #endregion
        #region private
        private void ClearMask()
        {
            foreach (var ctrl in GetAllControls(this))
            {
                if (ctrl is ICustomControl handler)
                {
                    handler.ClearField();
                }
            }

        }
        private static IEnumerable<Control> GetAllControls(Control root)
        {
            var stack = new Stack<Control>();
            stack.Push(root);

            while (stack.Any())
            {
                var next = stack.Pop();
                foreach (Control child in next.Controls)
                    stack.Push(child);

                yield return next;
            }
        }
        private void EnablePbSave()
        {
            if (!_EnablePbSave())
            {
                // wenn am Fenster die _EnablePbSave False zurückgibt dann nicht aktivieren
                return;
            }
            foreach (var ctrl in GetAllControls(this))
            {
                if (ctrl is myPbSave handler && !ctrl.Enabled && ctrl.Visible)
                {
                    handler.OnEnableControl();
                }
            }
        }
        private void DisablePbSave()
        {
            if (isSearchWindow || _PbSave_AllwayEnabled())
            {
                return;
            }
            foreach (var ctrl in GetAllControls(this))
            {
                if (ctrl is myPbSave handler && ctrl.Enabled && ctrl.Visible)
                {
                    handler.OnDisableControl();
                }
            }
        }
        // Klasse ModusCheckBox aboniert
        public sealed override void OnSearchModeCheckBoxStateChanged(object sender, MyEventArgs e)
        {
            WindowModeChanged(this, EventArgs.Empty);
        }
        // Close Windows - Klase pbAbbrechen aboniert
        public sealed override void OnCloseWindow(object sender, EventArgs e)
        {
            _Close();
            Close();
        }
        protected sealed override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (DataNotSaved)
            {
                DialogResult result = MessageBox.Show("Wollen Sie die Änderung speichern?", "Info!!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (result)
                {
                    case DialogResult.Yes:
                        InvokeOnClick(pbOk, e);
                        e.Cancel = true;
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case DialogResult.No:
                        e.Cancel = false;
                        break;
                }
            }
            if (!e.Cancel)
            {
                _Close();
            }
        }
        protected sealed override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }
        public sealed override void RegisterControl(ICustomControl control)
        {
            controls.Add(control);
        }
        // Darf nicht überschrieben werden
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new Color BackColor
        {
            get => base.BackColor;
            private set => base.BackColor = value; // nur intern/privat
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new IButtonControl AcceptButton
        {
            get => base.AcceptButton;
            private set => base.AcceptButton = value; // nur intern/privat
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new IButtonControl CancelButton
        {
            get => base.CancelButton;
            private set => base.CancelButton = value; // nur intern/privat
        }
        /*[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new bool MaximizeBox
        {
            get => base.MaximizeBox;
            private set => base.MaximizeBox = value; // nur intern/privat
        }*/
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new string Name
        {
            get => base.Name;
            private set => base.Name = value; // nur intern/privat
        }
        #endregion

    }
}
