﻿namespace SharpInjector
{
    partial class MainForm
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
            this.Process_Name_Textbox = new MetroFramework.Controls.MetroTextBox();
            this.Choose_Process_Button = new MetroFramework.Controls.MetroButton();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.Inject_Button = new MetroFramework.Controls.MetroButton();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.UI_DLL_List = new System.Windows.Forms.ListBox();
            this.Clear_DLL_List_Button = new MetroFramework.Controls.MetroButton();
            this.Remove_DLL_Button = new MetroFramework.Controls.MetroButton();
            this.Add_DLL_Button = new MetroFramework.Controls.MetroButton();
            this.Inject_Method_Combobox = new MetroFramework.Controls.MetroComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Process_Name_Textbox
            // 
            // 
            // 
            // 
            this.Process_Name_Textbox.CustomButton.Image = null;
            this.Process_Name_Textbox.CustomButton.Location = new System.Drawing.Point(125, 1);
            this.Process_Name_Textbox.CustomButton.Name = "";
            this.Process_Name_Textbox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.Process_Name_Textbox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.Process_Name_Textbox.CustomButton.TabIndex = 1;
            this.Process_Name_Textbox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Process_Name_Textbox.CustomButton.UseSelectable = true;
            this.Process_Name_Textbox.CustomButton.Visible = false;
            this.Process_Name_Textbox.Lines = new string[0];
            this.Process_Name_Textbox.Location = new System.Drawing.Point(23, 73);
            this.Process_Name_Textbox.MaxLength = 32767;
            this.Process_Name_Textbox.Name = "Process_Name_Textbox";
            this.Process_Name_Textbox.PasswordChar = '\0';
            this.Process_Name_Textbox.PromptText = "Process name";
            this.Process_Name_Textbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.Process_Name_Textbox.SelectedText = "";
            this.Process_Name_Textbox.SelectionLength = 0;
            this.Process_Name_Textbox.SelectionStart = 0;
            this.Process_Name_Textbox.ShortcutsEnabled = true;
            this.Process_Name_Textbox.Size = new System.Drawing.Size(155, 23);
            this.Process_Name_Textbox.Style = MetroFramework.MetroColorStyle.Red;
            this.Process_Name_Textbox.TabIndex = 0;
            this.Process_Name_Textbox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Process_Name_Textbox.UseSelectable = true;
            this.Process_Name_Textbox.WaterMark = "Process name";
            this.Process_Name_Textbox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.Process_Name_Textbox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.Process_Name_Textbox.TextChanged += new System.EventHandler(this.Process_Name_Textbox_TextChanged);
            // 
            // Choose_Process_Button
            // 
            this.Choose_Process_Button.Location = new System.Drawing.Point(184, 73);
            this.Choose_Process_Button.Name = "Choose_Process_Button";
            this.Choose_Process_Button.Size = new System.Drawing.Size(148, 23);
            this.Choose_Process_Button.Style = MetroFramework.MetroColorStyle.Blue;
            this.Choose_Process_Button.TabIndex = 2;
            this.Choose_Process_Button.Text = "Choose Process";
            this.Choose_Process_Button.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Choose_Process_Button.UseSelectable = true;
            this.Choose_Process_Button.Click += new System.EventHandler(this.Choose_Process_Button_Click);
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            // 
            // Inject_Button
            // 
            this.Inject_Button.Highlight = true;
            this.Inject_Button.Location = new System.Drawing.Point(23, 207);
            this.Inject_Button.Name = "Inject_Button";
            this.Inject_Button.Size = new System.Drawing.Size(180, 25);
            this.Inject_Button.Style = MetroFramework.MetroColorStyle.Red;
            this.Inject_Button.TabIndex = 4;
            this.Inject_Button.Text = "Inject";
            this.Inject_Button.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Inject_Button.UseSelectable = true;
            this.Inject_Button.Click += new System.EventHandler(this.Inject_Button_Click);
            // 
            // metroPanel1
            // 
            this.metroPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel1.Controls.Add(this.UI_DLL_List);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(112, 102);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(220, 99);
            this.metroPanel1.TabIndex = 8;
            this.metroPanel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // UI_DLL_List
            // 
            this.UI_DLL_List.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.UI_DLL_List.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UI_DLL_List.ForeColor = System.Drawing.SystemColors.Menu;
            this.UI_DLL_List.FormattingEnabled = true;
            this.UI_DLL_List.Location = new System.Drawing.Point(3, 3);
            this.UI_DLL_List.Name = "UI_DLL_List";
            this.UI_DLL_List.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.UI_DLL_List.Size = new System.Drawing.Size(212, 91);
            this.UI_DLL_List.TabIndex = 2;
            // 
            // Clear_DLL_List_Button
            // 
            this.Clear_DLL_List_Button.Location = new System.Drawing.Point(23, 160);
            this.Clear_DLL_List_Button.Name = "Clear_DLL_List_Button";
            this.Clear_DLL_List_Button.Size = new System.Drawing.Size(83, 41);
            this.Clear_DLL_List_Button.Style = MetroFramework.MetroColorStyle.Blue;
            this.Clear_DLL_List_Button.TabIndex = 11;
            this.Clear_DLL_List_Button.Text = "Clear";
            this.Clear_DLL_List_Button.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Clear_DLL_List_Button.UseSelectable = true;
            this.Clear_DLL_List_Button.Click += new System.EventHandler(this.Clear_DLL_List_Button_Click);
            // 
            // Remove_DLL_Button
            // 
            this.Remove_DLL_Button.Location = new System.Drawing.Point(23, 131);
            this.Remove_DLL_Button.Name = "Remove_DLL_Button";
            this.Remove_DLL_Button.Size = new System.Drawing.Size(83, 23);
            this.Remove_DLL_Button.Style = MetroFramework.MetroColorStyle.Blue;
            this.Remove_DLL_Button.TabIndex = 9;
            this.Remove_DLL_Button.Text = "Remove";
            this.Remove_DLL_Button.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Remove_DLL_Button.UseSelectable = true;
            this.Remove_DLL_Button.Click += new System.EventHandler(this.Remove_DLL_Button_Click);
            // 
            // Add_DLL_Button
            // 
            this.Add_DLL_Button.Location = new System.Drawing.Point(23, 102);
            this.Add_DLL_Button.Name = "Add_DLL_Button";
            this.Add_DLL_Button.Size = new System.Drawing.Size(83, 23);
            this.Add_DLL_Button.Style = MetroFramework.MetroColorStyle.Blue;
            this.Add_DLL_Button.TabIndex = 8;
            this.Add_DLL_Button.Text = "Add";
            this.Add_DLL_Button.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Add_DLL_Button.UseSelectable = true;
            this.Add_DLL_Button.Click += new System.EventHandler(this.Add_DLL_Button_Click);
            // 
            // Inject_Method_Combobox
            // 
            this.Inject_Method_Combobox.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.Inject_Method_Combobox.FontWeight = MetroFramework.MetroComboBoxWeight.Light;
            this.Inject_Method_Combobox.FormattingEnabled = true;
            this.Inject_Method_Combobox.ItemHeight = 19;
            this.Inject_Method_Combobox.Items.AddRange(new object[] {
            "Standard",
            "Manual Mapping",
            "Thread Hijacking"});
            this.Inject_Method_Combobox.Location = new System.Drawing.Point(209, 207);
            this.Inject_Method_Combobox.Name = "Inject_Method_Combobox";
            this.Inject_Method_Combobox.Size = new System.Drawing.Size(123, 25);
            this.Inject_Method_Combobox.Style = MetroFramework.MetroColorStyle.Blue;
            this.Inject_Method_Combobox.TabIndex = 12;
            this.Inject_Method_Combobox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Inject_Method_Combobox.UseSelectable = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 255);
            this.Controls.Add(this.Inject_Method_Combobox);
            this.Controls.Add(this.Inject_Button);
            this.Controls.Add(this.Clear_DLL_List_Button);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.Remove_DLL_Button);
            this.Controls.Add(this.Add_DLL_Button);
            this.Controls.Add(this.Choose_Process_Button);
            this.Controls.Add(this.Process_Name_Textbox);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "Sharp Injector";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.metroPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox Process_Name_Textbox;
        private MetroFramework.Controls.MetroButton Choose_Process_Button;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroButton Inject_Button;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroButton Remove_DLL_Button;
        private MetroFramework.Controls.MetroButton Add_DLL_Button;
        private MetroFramework.Controls.MetroButton Clear_DLL_List_Button;
        private System.Windows.Forms.ListBox UI_DLL_List;
        private MetroFramework.Controls.MetroComboBox Inject_Method_Combobox;
    }
}

