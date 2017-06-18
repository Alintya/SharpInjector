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
            this.ListBox = new System.Windows.Forms.ListBox();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroListView1 = new MetroFramework.Controls.MetroListView();
            this.Window_List_Button = new MetroFramework.Controls.MetroButton();
            this.Process_List_Button = new MetroFramework.Controls.MetroButton();
            this.Select_Button = new MetroFramework.Controls.MetroButton();
            this.Close_Button = new MetroFramework.Controls.MetroButton();
            this.SearchTextbox = new MetroFramework.Controls.MetroTextBox();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListBox
            // 
            this.ListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.ListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListBox.ForeColor = System.Drawing.SystemColors.Menu;
            this.ListBox.FormattingEnabled = true;
            this.ListBox.Location = new System.Drawing.Point(3, 3);
            this.ListBox.Name = "ListBox";
            this.ListBox.Size = new System.Drawing.Size(206, 156);
            this.ListBox.Sorted = true;
            this.ListBox.TabIndex = 0;
            // 
            // metroPanel1
            // 
            this.metroPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel1.Controls.Add(this.metroListView1);
            this.metroPanel1.Controls.Add(this.ListBox);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(23, 33);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(241, 240);
            this.metroPanel1.TabIndex = 1;
            this.metroPanel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroListView1
            // 
            this.metroListView1.AutoArrange = false;
            this.metroListView1.BackColor = System.Drawing.SystemColors.MenuText;
            this.metroListView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.metroListView1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.metroListView1.ForeColor = System.Drawing.Color.Beige;
            this.metroListView1.FullRowSelect = true;
            this.metroListView1.Location = new System.Drawing.Point(3, 3);
            this.metroListView1.MultiSelect = false;
            this.metroListView1.Name = "metroListView1";
            this.metroListView1.OwnerDraw = true;
            this.metroListView1.Size = new System.Drawing.Size(233, 231);
            this.metroListView1.TabIndex = 2;
            this.metroListView1.UseCompatibleStateImageBehavior = false;
            this.metroListView1.UseSelectable = true;
            this.metroListView1.UseStyleColors = true;
            this.metroListView1.View = System.Windows.Forms.View.Details;
            // 
            // Window_List_Button
            // 
            this.Window_List_Button.Enabled = false;
            this.Window_List_Button.Location = new System.Drawing.Point(146, 308);
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
            this.Process_List_Button.Location = new System.Drawing.Point(23, 308);
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
            this.Select_Button.Location = new System.Drawing.Point(23, 337);
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
            this.Close_Button.Location = new System.Drawing.Point(146, 337);
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
            this.SearchTextbox.Location = new System.Drawing.Point(23, 279);
            this.SearchTextbox.MaxLength = 32767;
            this.SearchTextbox.Name = "SearchTextbox";
            this.SearchTextbox.PasswordChar = '\0';
            this.SearchTextbox.PromptText = "Search Process";
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
            // ProcessSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 383);
            this.Controls.Add(this.SearchTextbox);
            this.Controls.Add(this.Close_Button);
            this.Controls.Add(this.Select_Button);
            this.Controls.Add(this.Process_List_Button);
            this.Controls.Add(this.Window_List_Button);
            this.Controls.Add(this.metroPanel1);
            this.DisplayHeader = false;
            this.MaximizeBox = false;
            this.Name = "ProcessSelectForm";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "ProcessSelectForm";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.ProcessSelectForm_Load);
            this.metroPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ListBox;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroButton Window_List_Button;
        private MetroFramework.Controls.MetroButton Process_List_Button;
        private MetroFramework.Controls.MetroButton Select_Button;
        private MetroFramework.Controls.MetroButton Close_Button;
        private MetroFramework.Controls.MetroTextBox SearchTextbox;
        private MetroFramework.Controls.MetroListView metroListView1;
    }
}