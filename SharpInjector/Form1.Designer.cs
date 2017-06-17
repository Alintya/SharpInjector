namespace SharpInjector
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ProcessNameTextbox = new MetroFramework.Controls.MetroTextBox();
            this.ChooseProcessButton = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.InjectButton = new MetroFramework.Controls.MetroButton();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.DLLList = new MetroFramework.Controls.MetroListView();
            this.ClearDLLListButton = new MetroFramework.Controls.MetroButton();
            this.RemoveDLLButton = new MetroFramework.Controls.MetroButton();
            this.AddDLLButton = new MetroFramework.Controls.MetroButton();
            this.AutoInjectSwitch = new MetroFramework.Controls.MetroToggle();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.metroPanel1.SuspendLayout();
            this.metroPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProcessNameTextbox
            // 
            // 
            // 
            // 
            this.ProcessNameTextbox.CustomButton.Image = null;
            this.ProcessNameTextbox.CustomButton.Location = new System.Drawing.Point(128, 1);
            this.ProcessNameTextbox.CustomButton.Name = "";
            this.ProcessNameTextbox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.ProcessNameTextbox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.ProcessNameTextbox.CustomButton.TabIndex = 1;
            this.ProcessNameTextbox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.ProcessNameTextbox.CustomButton.UseSelectable = true;
            this.ProcessNameTextbox.CustomButton.Visible = false;
            this.ProcessNameTextbox.Lines = new string[0];
            this.ProcessNameTextbox.Location = new System.Drawing.Point(23, 90);
            this.ProcessNameTextbox.MaxLength = 32767;
            this.ProcessNameTextbox.Name = "ProcessNameTextbox";
            this.ProcessNameTextbox.PasswordChar = '\0';
            this.ProcessNameTextbox.PromptText = "Process name";
            this.ProcessNameTextbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ProcessNameTextbox.SelectedText = "";
            this.ProcessNameTextbox.SelectionLength = 0;
            this.ProcessNameTextbox.SelectionStart = 0;
            this.ProcessNameTextbox.ShortcutsEnabled = true;
            this.ProcessNameTextbox.Size = new System.Drawing.Size(150, 23);
            this.ProcessNameTextbox.TabIndex = 0;
            this.ProcessNameTextbox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ProcessNameTextbox.UseSelectable = true;
            this.ProcessNameTextbox.WaterMark = "Process name";
            this.ProcessNameTextbox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.ProcessNameTextbox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.ProcessNameTextbox.TextChanged += new System.EventHandler(this.ProcessNameTextbox_TextChanged);
            // 
            // ChooseProcessButton
            // 
            this.ChooseProcessButton.Location = new System.Drawing.Point(179, 90);
            this.ChooseProcessButton.Name = "ChooseProcessButton";
            this.ChooseProcessButton.Size = new System.Drawing.Size(155, 23);
            this.ChooseProcessButton.TabIndex = 2;
            this.ChooseProcessButton.Text = "Choose";
            this.ChooseProcessButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ChooseProcessButton.UseSelectable = true;
            this.ChooseProcessButton.Click += new System.EventHandler(this.ChooseProcessButton_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(23, 68);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(53, 19);
            this.metroLabel1.TabIndex = 3;
            this.metroLabel1.Text = "Process";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            // 
            // InjectButton
            // 
            this.InjectButton.Location = new System.Drawing.Point(187, 232);
            this.InjectButton.Name = "InjectButton";
            this.InjectButton.Size = new System.Drawing.Size(147, 23);
            this.InjectButton.TabIndex = 4;
            this.InjectButton.Text = "Inject";
            this.InjectButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.InjectButton.UseSelectable = true;
            this.InjectButton.Click += new System.EventHandler(this.InjectButton_Click);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(3, 0);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(76, 19);
            this.metroLabel2.TabIndex = 7;
            this.metroLabel2.Text = "DLL LIST ->";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroPanel1
            // 
            this.metroPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel1.Controls.Add(this.DLLList);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(112, 119);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(222, 109);
            this.metroPanel1.TabIndex = 8;
            this.metroPanel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // DLLList
            // 
            this.DLLList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.DLLList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DLLList.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.DLLList.FullRowSelect = true;
            this.DLLList.Location = new System.Drawing.Point(3, 3);
            this.DLLList.Name = "DLLList";
            this.DLLList.OwnerDraw = true;
            this.DLLList.Size = new System.Drawing.Size(214, 100);
            this.DLLList.TabIndex = 10;
            this.DLLList.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.DLLList.UseCompatibleStateImageBehavior = false;
            this.DLLList.UseCustomBackColor = true;
            this.DLLList.UseSelectable = true;
            this.DLLList.View = System.Windows.Forms.View.List;
            // 
            // ClearDLLListButton
            // 
            this.ClearDLLListButton.Location = new System.Drawing.Point(3, 80);
            this.ClearDLLListButton.Name = "ClearDLLListButton";
            this.ClearDLLListButton.Size = new System.Drawing.Size(75, 23);
            this.ClearDLLListButton.TabIndex = 11;
            this.ClearDLLListButton.Text = "Clear";
            this.ClearDLLListButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ClearDLLListButton.UseSelectable = true;
            // 
            // RemoveDLLButton
            // 
            this.RemoveDLLButton.Location = new System.Drawing.Point(3, 51);
            this.RemoveDLLButton.Name = "RemoveDLLButton";
            this.RemoveDLLButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveDLLButton.TabIndex = 9;
            this.RemoveDLLButton.Text = "Remove";
            this.RemoveDLLButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.RemoveDLLButton.UseSelectable = true;
            // 
            // AddDLLButton
            // 
            this.AddDLLButton.Location = new System.Drawing.Point(3, 22);
            this.AddDLLButton.Name = "AddDLLButton";
            this.AddDLLButton.Size = new System.Drawing.Size(75, 23);
            this.AddDLLButton.TabIndex = 8;
            this.AddDLLButton.Text = "Add";
            this.AddDLLButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.AddDLLButton.UseSelectable = true;
            this.AddDLLButton.Click += new System.EventHandler(this.AddDLLButton_Click);
            // 
            // AutoInjectSwitch
            // 
            this.AutoInjectSwitch.AutoSize = true;
            this.AutoInjectSwitch.Location = new System.Drawing.Point(23, 236);
            this.AutoInjectSwitch.Name = "AutoInjectSwitch";
            this.AutoInjectSwitch.Size = new System.Drawing.Size(80, 17);
            this.AutoInjectSwitch.TabIndex = 9;
            this.AutoInjectSwitch.Text = "Aus";
            this.AutoInjectSwitch.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.AutoInjectSwitch.UseSelectable = true;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(108, 234);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(73, 19);
            this.metroLabel3.TabIndex = 10;
            this.metroLabel3.Text = "Auto-inject";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroPanel2
            // 
            this.metroPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel2.Controls.Add(this.metroLabel2);
            this.metroPanel2.Controls.Add(this.ClearDLLListButton);
            this.metroPanel2.Controls.Add(this.AddDLLButton);
            this.metroPanel2.Controls.Add(this.RemoveDLLButton);
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(23, 119);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(83, 109);
            this.metroPanel2.TabIndex = 11;
            this.metroPanel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 277);
            this.Controls.Add(this.metroPanel2);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.AutoInjectSwitch);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.InjectButton);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.ChooseProcessButton);
            this.Controls.Add(this.ProcessNameTextbox);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "SharpInjector";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox ProcessNameTextbox;
        private MetroFramework.Controls.MetroButton ChooseProcessButton;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroButton InjectButton;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroButton RemoveDLLButton;
        private MetroFramework.Controls.MetroButton AddDLLButton;
        private MetroFramework.Controls.MetroToggle AutoInjectSwitch;
        private MetroFramework.Controls.MetroListView DLLList;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroButton ClearDLLListButton;
        private MetroFramework.Controls.MetroPanel metroPanel2;
    }
}

