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
            this.metroTextBox1 = new MetroFramework.Controls.MetroTextBox();
            this.processListButton = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.injectButton = new MetroFramework.Controls.MetroButton();
            this.autoInjectCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.addDLLButton = new MetroFramework.Controls.MetroButton();
            this.removeDLLButton = new MetroFramework.Controls.MetroButton();
            this.metroToggle1 = new MetroFramework.Controls.MetroToggle();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroTextBox1
            // 
            // 
            // 
            // 
            this.metroTextBox1.CustomButton.Image = null;
            this.metroTextBox1.CustomButton.Location = new System.Drawing.Point(116, 1);
            this.metroTextBox1.CustomButton.Name = "";
            this.metroTextBox1.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.metroTextBox1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox1.CustomButton.TabIndex = 1;
            this.metroTextBox1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox1.CustomButton.UseSelectable = true;
            this.metroTextBox1.CustomButton.Visible = false;
            this.metroTextBox1.Lines = new string[] {
        "metroTextBox1"};
            this.metroTextBox1.Location = new System.Drawing.Point(23, 90);
            this.metroTextBox1.MaxLength = 32767;
            this.metroTextBox1.Name = "metroTextBox1";
            this.metroTextBox1.PasswordChar = '\0';
            this.metroTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox1.SelectedText = "";
            this.metroTextBox1.SelectionLength = 0;
            this.metroTextBox1.SelectionStart = 0;
            this.metroTextBox1.ShortcutsEnabled = true;
            this.metroTextBox1.Size = new System.Drawing.Size(138, 23);
            this.metroTextBox1.TabIndex = 0;
            this.metroTextBox1.Text = "metroTextBox1";
            this.metroTextBox1.UseSelectable = true;
            this.metroTextBox1.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox1.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // processListButton
            // 
            this.processListButton.Location = new System.Drawing.Point(179, 90);
            this.processListButton.Name = "processListButton";
            this.processListButton.Size = new System.Drawing.Size(75, 23);
            this.processListButton.TabIndex = 2;
            this.processListButton.Text = "Choose";
            this.processListButton.UseSelectable = true;
            this.processListButton.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(23, 68);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(53, 19);
            this.metroLabel1.TabIndex = 3;
            this.metroLabel1.Text = "Process";
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            // 
            // injectButton
            // 
            this.injectButton.Location = new System.Drawing.Point(179, 200);
            this.injectButton.Name = "injectButton";
            this.injectButton.Size = new System.Drawing.Size(75, 23);
            this.injectButton.TabIndex = 4;
            this.injectButton.Text = "Inject";
            this.injectButton.UseSelectable = true;
            this.injectButton.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // autoInjectCheckBox
            // 
            this.autoInjectCheckBox.AutoSize = true;
            this.autoInjectCheckBox.Location = new System.Drawing.Point(23, 205);
            this.autoInjectCheckBox.Name = "autoInjectCheckBox";
            this.autoInjectCheckBox.Size = new System.Drawing.Size(83, 15);
            this.autoInjectCheckBox.TabIndex = 5;
            this.autoInjectCheckBox.Text = "Auto-inject";
            this.autoInjectCheckBox.UseSelectable = true;
            this.autoInjectCheckBox.CheckedChanged += new System.EventHandler(this.autoInjectCheckBox_CheckedChanged);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(3, 0);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(35, 19);
            this.metroLabel2.TabIndex = 7;
            this.metroLabel2.Text = "DLLs";
            this.metroLabel2.Click += new System.EventHandler(this.metroLabel2_Click);
            // 
            // metroPanel1
            // 
            this.metroPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel1.Controls.Add(this.removeDLLButton);
            this.metroPanel1.Controls.Add(this.addDLLButton);
            this.metroPanel1.Controls.Add(this.metroLabel2);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(23, 119);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(229, 75);
            this.metroPanel1.TabIndex = 8;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // addDLLButton
            // 
            this.addDLLButton.Location = new System.Drawing.Point(3, 22);
            this.addDLLButton.Name = "addDLLButton";
            this.addDLLButton.Size = new System.Drawing.Size(75, 23);
            this.addDLLButton.TabIndex = 8;
            this.addDLLButton.Text = "Add";
            this.addDLLButton.UseSelectable = true;
            // 
            // removeDLLButton
            // 
            this.removeDLLButton.Location = new System.Drawing.Point(3, 51);
            this.removeDLLButton.Name = "removeDLLButton";
            this.removeDLLButton.Size = new System.Drawing.Size(75, 23);
            this.removeDLLButton.TabIndex = 9;
            this.removeDLLButton.Text = "Remove";
            this.removeDLLButton.UseSelectable = true;
            // 
            // metroToggle1
            // 
            this.metroToggle1.AutoSize = true;
            this.metroToggle1.Location = new System.Drawing.Point(26, 226);
            this.metroToggle1.Name = "metroToggle1";
            this.metroToggle1.Size = new System.Drawing.Size(80, 17);
            this.metroToggle1.TabIndex = 9;
            this.metroToggle1.Text = "Aus";
            this.metroToggle1.UseSelectable = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 254);
            this.Controls.Add(this.metroToggle1);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.autoInjectCheckBox);
            this.Controls.Add(this.injectButton);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.processListButton);
            this.Controls.Add(this.metroTextBox1);
            this.Name = "Form1";
            this.Text = "SharpInjector";
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox metroTextBox1;
        private MetroFramework.Controls.MetroButton processListButton;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroCheckBox autoInjectCheckBox;
        private MetroFramework.Controls.MetroButton injectButton;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroButton removeDLLButton;
        private MetroFramework.Controls.MetroButton addDLLButton;
        private MetroFramework.Controls.MetroToggle metroToggle1;
    }
}

