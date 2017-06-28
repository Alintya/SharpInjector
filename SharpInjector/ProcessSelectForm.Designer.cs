namespace SharpInjector
{
    partial class ProcessSelectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Window_List_Button = new MetroFramework.Controls.MetroButton();
            this.Process_List_Button = new MetroFramework.Controls.MetroButton();
            this.Select_Button = new MetroFramework.Controls.MetroButton();
            this.Close_Button = new MetroFramework.Controls.MetroButton();
            this.SearchTextbox = new MetroFramework.Controls.MetroTextBox();
            this.Header_Minimize_Label = new MetroFramework.Controls.MetroLabel();
            this.Header_Title_Label = new MetroFramework.Controls.MetroLabel();
            this.Header_Close_Label = new MetroFramework.Controls.MetroLabel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.Process_ListView = new System.Windows.Forms.ListView();
            this.Header_Background_Panel = new MetroFramework.Controls.MetroPanel();
            this.Header_Line_Panel = new MetroFramework.Controls.MetroPanel();
            this.metroPanel1.SuspendLayout();
            this.Header_Background_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Window_List_Button
            // 
            this.Window_List_Button.Enabled = false;
            this.Window_List_Button.Location = new System.Drawing.Point(146, 333);
            this.Window_List_Button.Name = "Window_List_Button";
            this.Window_List_Button.Size = new System.Drawing.Size(118, 23);
            this.Window_List_Button.Style = MetroFramework.MetroColorStyle.Blue;
            this.Window_List_Button.TabIndex = 2;
            this.Window_List_Button.Text = "Window List";
            this.Window_List_Button.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Window_List_Button.UseSelectable = true;
            this.Window_List_Button.Click += new System.EventHandler(this.Window_List_Button_Click);
            // 
            // Process_List_Button
            // 
            this.Process_List_Button.Enabled = false;
            this.Process_List_Button.Location = new System.Drawing.Point(23, 333);
            this.Process_List_Button.Name = "Process_List_Button";
            this.Process_List_Button.Size = new System.Drawing.Size(117, 23);
            this.Process_List_Button.Style = MetroFramework.MetroColorStyle.Blue;
            this.Process_List_Button.TabIndex = 3;
            this.Process_List_Button.Text = "Process List";
            this.Process_List_Button.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Process_List_Button.UseSelectable = true;
            this.Process_List_Button.Click += new System.EventHandler(this.Process_List_Button_Click);
            // 
            // Select_Button
            // 
            this.Select_Button.Location = new System.Drawing.Point(23, 362);
            this.Select_Button.Name = "Select_Button";
            this.Select_Button.Size = new System.Drawing.Size(117, 23);
            this.Select_Button.Style = MetroFramework.MetroColorStyle.Blue;
            this.Select_Button.TabIndex = 4;
            this.Select_Button.Text = "Select";
            this.Select_Button.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Select_Button.UseSelectable = true;
            this.Select_Button.Click += new System.EventHandler(this.Select_Button_Click);
            // 
            // Close_Button
            // 
            this.Close_Button.Location = new System.Drawing.Point(146, 362);
            this.Close_Button.Name = "Close_Button";
            this.Close_Button.Size = new System.Drawing.Size(118, 23);
            this.Close_Button.Style = MetroFramework.MetroColorStyle.Blue;
            this.Close_Button.TabIndex = 5;
            this.Close_Button.Text = "Close";
            this.Close_Button.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Close_Button.UseSelectable = true;
            this.Close_Button.Click += new System.EventHandler(this.Close_Button_Click);
            // 
            // SearchTextbox
            // 
            // 
            // 
            // 
            this.SearchTextbox.CustomButton.Image = null;
            this.SearchTextbox.CustomButton.Location = new System.Drawing.Point(219, 1);
            this.SearchTextbox.CustomButton.Name = "";
            this.SearchTextbox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.SearchTextbox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.SearchTextbox.CustomButton.TabIndex = 1;
            this.SearchTextbox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.SearchTextbox.CustomButton.UseSelectable = true;
            this.SearchTextbox.CustomButton.Visible = false;
            this.SearchTextbox.Lines = new string[0];
            this.SearchTextbox.Location = new System.Drawing.Point(23, 304);
            this.SearchTextbox.MaxLength = 32767;
            this.SearchTextbox.Name = "SearchTextbox";
            this.SearchTextbox.PasswordChar = '\0';
            //this.SearchTextbox.PromptText = "Search Process";
            this.SearchTextbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.SearchTextbox.SelectedText = "";
            this.SearchTextbox.SelectionLength = 0;
            this.SearchTextbox.SelectionStart = 0;
            this.SearchTextbox.ShortcutsEnabled = true;
            this.SearchTextbox.Size = new System.Drawing.Size(241, 23);
            this.SearchTextbox.Style = MetroFramework.MetroColorStyle.Blue;
            this.SearchTextbox.TabIndex = 6;
            this.SearchTextbox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.SearchTextbox.UseSelectable = true;
            this.SearchTextbox.WaterMark = "Search Process";
            this.SearchTextbox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.SearchTextbox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.SearchTextbox.TextChanged += new System.EventHandler(this.SearchTextbox_TextChanged);
            // 
            // Header_Minimize_Label
            // 
            this.Header_Minimize_Label.AutoSize = true;
            this.Header_Minimize_Label.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.Header_Minimize_Label.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.Header_Minimize_Label.ForeColor = System.Drawing.Color.White;
            this.Header_Minimize_Label.Location = new System.Drawing.Point(242, 7);
            this.Header_Minimize_Label.Name = "Header_Minimize_Label";
            this.Header_Minimize_Label.Size = new System.Drawing.Size(19, 25);
            this.Header_Minimize_Label.TabIndex = 4;
            this.Header_Minimize_Label.Text = "_";
            this.Header_Minimize_Label.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Header_Minimize_Label.UseCustomBackColor = true;
            this.Header_Minimize_Label.UseCustomForeColor = true;
            this.Header_Minimize_Label.Click += new System.EventHandler(this.Header_Minimize_Label_Click);
            this.Header_Minimize_Label.MouseEnter += new System.EventHandler(this.Header_Minimize_Label_MouseEnter);
            this.Header_Minimize_Label.MouseLeave += new System.EventHandler(this.Header_Minimize_Label_MouseLeave);
            // 
            // Header_Title_Label
            // 
            this.Header_Title_Label.AutoSize = true;
            this.Header_Title_Label.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.Header_Title_Label.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.Header_Title_Label.ForeColor = System.Drawing.Color.White;
            this.Header_Title_Label.Location = new System.Drawing.Point(23, 19);
            this.Header_Title_Label.Name = "Header_Title_Label";
            this.Header_Title_Label.Size = new System.Drawing.Size(148, 25);
            this.Header_Title_Label.TabIndex = 3;
            this.Header_Title_Label.Text = "Process Selection";
            this.Header_Title_Label.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Header_Title_Label.UseCustomBackColor = true;
            this.Header_Title_Label.UseCustomForeColor = true;
            this.Header_Title_Label.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Header_Title_Label_MouseMove);
            // 
            // Header_Close_Label
            // 
            this.Header_Close_Label.AutoSize = true;
            this.Header_Close_Label.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.Header_Close_Label.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.Header_Close_Label.ForeColor = System.Drawing.Color.White;
            this.Header_Close_Label.Location = new System.Drawing.Point(264, 10);
            this.Header_Close_Label.Name = "Header_Close_Label";
            this.Header_Close_Label.Size = new System.Drawing.Size(20, 25);
            this.Header_Close_Label.TabIndex = 2;
            this.Header_Close_Label.Text = "x";
            this.Header_Close_Label.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Header_Close_Label.UseCustomBackColor = true;
            this.Header_Close_Label.UseCustomForeColor = true;
            this.Header_Close_Label.Click += new System.EventHandler(this.Header_Close_Label_Click);
            this.Header_Close_Label.MouseEnter += new System.EventHandler(this.Header_Close_Label_MouseEnter);
            this.Header_Close_Label.MouseLeave += new System.EventHandler(this.Header_Close_Label_MouseLeave);
            // 
            // metroPanel1
            // 
            this.metroPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel1.Controls.Add(this.Process_ListView);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(23, 58);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(241, 240);
            this.metroPanel1.TabIndex = 1;
            this.metroPanel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // Process_ListView
            // 
            this.Process_ListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.Process_ListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Process_ListView.ForeColor = System.Drawing.SystemColors.Menu;
            this.Process_ListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.Process_ListView.Location = new System.Drawing.Point(3, 3);
            this.Process_ListView.MultiSelect = false;
            this.Process_ListView.Name = "Process_ListView";
            this.Process_ListView.Size = new System.Drawing.Size(233, 232);
            this.Process_ListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.Process_ListView.TabIndex = 2;
            this.Process_ListView.UseCompatibleStateImageBehavior = false;
            this.Process_ListView.View = System.Windows.Forms.View.Details;
            this.Process_ListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Process_ListView_MouseDoubleClick);
            // 
            // Header_Background_Panel
            // 
            this.Header_Background_Panel.Controls.Add(this.Header_Line_Panel);
            this.Header_Background_Panel.Controls.Add(this.Header_Minimize_Label);
            this.Header_Background_Panel.Controls.Add(this.Header_Title_Label);
            this.Header_Background_Panel.Controls.Add(this.Header_Close_Label);
            this.Header_Background_Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header_Background_Panel.HorizontalScrollbarBarColor = true;
            this.Header_Background_Panel.HorizontalScrollbarHighlightOnWheel = false;
            this.Header_Background_Panel.HorizontalScrollbarSize = 10;
            this.Header_Background_Panel.Location = new System.Drawing.Point(0, 0);
            this.Header_Background_Panel.Name = "Header_Background_Panel";
            this.Header_Background_Panel.Size = new System.Drawing.Size(287, 52);
            this.Header_Background_Panel.TabIndex = 8;
            this.Header_Background_Panel.UseCustomBackColor = true;
            this.Header_Background_Panel.VerticalScrollbarBarColor = true;
            this.Header_Background_Panel.VerticalScrollbarHighlightOnWheel = false;
            this.Header_Background_Panel.VerticalScrollbarSize = 10;
            this.Header_Background_Panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Header_Background_Panel_MouseMove);
            // 
            // Header_Line_Panel
            // 
            this.Header_Line_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.Header_Line_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Header_Line_Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header_Line_Panel.HorizontalScrollbarBarColor = true;
            this.Header_Line_Panel.HorizontalScrollbarHighlightOnWheel = false;
            this.Header_Line_Panel.HorizontalScrollbarSize = 10;
            this.Header_Line_Panel.Location = new System.Drawing.Point(0, 0);
            this.Header_Line_Panel.Name = "Header_Line_Panel";
            this.Header_Line_Panel.Size = new System.Drawing.Size(287, 7);
            this.Header_Line_Panel.TabIndex = 7;
            this.Header_Line_Panel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Header_Line_Panel.UseCustomBackColor = true;
            this.Header_Line_Panel.VerticalScrollbarBarColor = true;
            this.Header_Line_Panel.VerticalScrollbarHighlightOnWheel = false;
            this.Header_Line_Panel.VerticalScrollbarSize = 10;
            this.Header_Line_Panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Header_Line_Panel_MouseMove);
            // 
            // ProcessSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.ClientSize = new System.Drawing.Size(287, 408);
            this.Controls.Add(this.Header_Background_Panel);
            this.Controls.Add(this.SearchTextbox);
            this.Controls.Add(this.Close_Button);
            this.Controls.Add(this.Select_Button);
            this.Controls.Add(this.Process_List_Button);
            this.Controls.Add(this.Window_List_Button);
            this.Controls.Add(this.metroPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "ProcessSelectForm";
            this.Text = "ProcessSelectForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProcessSelectForm_FormClosing);
            this.Load += new System.EventHandler(this.ProcessSelectForm_Load);
            this.metroPanel1.ResumeLayout(false);
            this.Header_Background_Panel.ResumeLayout(false);
            this.Header_Background_Panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroButton Window_List_Button;
        private MetroFramework.Controls.MetroButton Process_List_Button;
        private MetroFramework.Controls.MetroButton Select_Button;
        private MetroFramework.Controls.MetroButton Close_Button;
        private MetroFramework.Controls.MetroTextBox SearchTextbox;
        private MetroFramework.Controls.MetroLabel Header_Title_Label;
        private MetroFramework.Controls.MetroLabel Header_Close_Label;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroLabel Header_Minimize_Label;
        private MetroFramework.Controls.MetroPanel Header_Background_Panel;
        private System.Windows.Forms.ListView Process_ListView;
        private MetroFramework.Controls.MetroPanel Header_Line_Panel;
    }
}