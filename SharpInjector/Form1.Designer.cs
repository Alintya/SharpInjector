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
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.InjectButton = new MetroFramework.Controls.MetroButton();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.DLLList = new MetroFramework.Controls.MetroListView();
            this.ClearDLLListButton = new MetroFramework.Controls.MetroButton();
            this.RemoveDLLButton = new MetroFramework.Controls.MetroButton();
            this.AddDLLButton = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProcessNameTextbox
            // 
            // 
            // 
            // 
            this.ProcessNameTextbox.CustomButton.Image = null;
            this.ProcessNameTextbox.CustomButton.Location = new System.Drawing.Point(137, 1);
            this.ProcessNameTextbox.CustomButton.Name = "";
            this.ProcessNameTextbox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.ProcessNameTextbox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.ProcessNameTextbox.CustomButton.TabIndex = 1;
            this.ProcessNameTextbox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.ProcessNameTextbox.CustomButton.UseSelectable = true;
            this.ProcessNameTextbox.CustomButton.Visible = false;
            this.ProcessNameTextbox.Lines = new string[0];
            this.ProcessNameTextbox.Location = new System.Drawing.Point(23, 73);
            this.ProcessNameTextbox.MaxLength = 32767;
            this.ProcessNameTextbox.Name = "ProcessNameTextbox";
            this.ProcessNameTextbox.PasswordChar = '\0';
            this.ProcessNameTextbox.PromptText = "Process name";
            this.ProcessNameTextbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ProcessNameTextbox.SelectedText = "";
            this.ProcessNameTextbox.SelectionLength = 0;
            this.ProcessNameTextbox.SelectionStart = 0;
            this.ProcessNameTextbox.ShortcutsEnabled = true;
            this.ProcessNameTextbox.Size = new System.Drawing.Size(147, 23);
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
            this.ChooseProcessButton.Location = new System.Drawing.Point(176, 73);
            this.ChooseProcessButton.Name = "ChooseProcessButton";
            this.ChooseProcessButton.Size = new System.Drawing.Size(167, 23);
            this.ChooseProcessButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.ChooseProcessButton.TabIndex = 2;
            this.ChooseProcessButton.Text = "Choose";
            this.ChooseProcessButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ChooseProcessButton.UseSelectable = true;
            this.ChooseProcessButton.Click += new System.EventHandler(this.ChooseProcessButton_Click);
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            // 
            // InjectButton
            // 
            this.InjectButton.Highlight = true;
            this.InjectButton.Location = new System.Drawing.Point(23, 189);
            this.InjectButton.Name = "InjectButton";
            this.InjectButton.Size = new System.Drawing.Size(83, 39);
            this.InjectButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.InjectButton.TabIndex = 4;
            this.InjectButton.Text = "Inject";
            this.InjectButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.InjectButton.UseSelectable = true;
            this.InjectButton.Click += new System.EventHandler(this.InjectButton_Click);
            // 
            // metroPanel1
            // 
            this.metroPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel1.Controls.Add(this.DLLList);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(112, 102);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(231, 126);
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
            this.DLLList.Size = new System.Drawing.Size(223, 118);
            this.DLLList.TabIndex = 10;
            this.DLLList.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.DLLList.UseCompatibleStateImageBehavior = false;
            this.DLLList.UseCustomBackColor = true;
            this.DLLList.UseSelectable = true;
            this.DLLList.View = System.Windows.Forms.View.List;
            // 
            // ClearDLLListButton
            // 
            this.ClearDLLListButton.Location = new System.Drawing.Point(23, 160);
            this.ClearDLLListButton.Name = "ClearDLLListButton";
            this.ClearDLLListButton.Size = new System.Drawing.Size(83, 23);
            this.ClearDLLListButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.ClearDLLListButton.TabIndex = 11;
            this.ClearDLLListButton.Text = "Clear";
            this.ClearDLLListButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ClearDLLListButton.UseSelectable = true;
            // 
            // RemoveDLLButton
            // 
            this.RemoveDLLButton.Location = new System.Drawing.Point(23, 131);
            this.RemoveDLLButton.Name = "RemoveDLLButton";
            this.RemoveDLLButton.Size = new System.Drawing.Size(83, 23);
            this.RemoveDLLButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.RemoveDLLButton.TabIndex = 9;
            this.RemoveDLLButton.Text = "Remove";
            this.RemoveDLLButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.RemoveDLLButton.UseSelectable = true;
            // 
            // AddDLLButton
            // 
            this.AddDLLButton.Location = new System.Drawing.Point(23, 102);
            this.AddDLLButton.Name = "AddDLLButton";
            this.AddDLLButton.Size = new System.Drawing.Size(83, 23);
            this.AddDLLButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.AddDLLButton.TabIndex = 8;
            this.AddDLLButton.Text = "Add";
            this.AddDLLButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.AddDLLButton.UseSelectable = true;
            this.AddDLLButton.Click += new System.EventHandler(this.AddDLLButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 251);
            this.Controls.Add(this.InjectButton);
            this.Controls.Add(this.ClearDLLListButton);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.RemoveDLLButton);
            this.Controls.Add(this.AddDLLButton);
            this.Controls.Add(this.ChooseProcessButton);
            this.Controls.Add(this.ProcessNameTextbox);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "Sharp Injector";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.metroPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox ProcessNameTextbox;
        private MetroFramework.Controls.MetroButton ChooseProcessButton;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroButton InjectButton;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroButton RemoveDLLButton;
        private MetroFramework.Controls.MetroButton AddDLLButton;
        private MetroFramework.Controls.MetroListView DLLList;
        private MetroFramework.Controls.MetroButton ClearDLLListButton;
    }
}

