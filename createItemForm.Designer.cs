namespace FeedCreator.NET
{
    partial class createItemForm
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.titleBox = new System.Windows.Forms.TextBox();
            this.descriptionBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linkBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.authorEmail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.SourceGroupBox = new System.Windows.Forms.GroupBox();
            this.sourceURL = new System.Windows.Forms.TextBox();
            this.sourceText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SourceGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title";
            // 
            // titleBox
            // 
            this.titleBox.Location = new System.Drawing.Point(137, 34);
            this.titleBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.titleBox.Name = "titleBox";
            this.titleBox.Size = new System.Drawing.Size(404, 22);
            this.titleBox.TabIndex = 1;
            // 
            // descriptionBox
            // 
            this.descriptionBox.Location = new System.Drawing.Point(137, 75);
            this.descriptionBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(404, 117);
            this.descriptionBox.TabIndex = 2;
            this.descriptionBox.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 85);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 208);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Link";
            // 
            // linkBox
            // 
            this.linkBox.Location = new System.Drawing.Point(137, 204);
            this.linkBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.linkBox.Name = "linkBox";
            this.linkBox.Size = new System.Drawing.Size(404, 22);
            this.linkBox.TabIndex = 5;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(184, 523);
            this.okButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 28);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(323, 523);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 28);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 252);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Author (Email)";
            // 
            // authorEmail
            // 
            this.authorEmail.Location = new System.Drawing.Point(137, 249);
            this.authorEmail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.authorEmail.Name = "authorEmail";
            this.authorEmail.Size = new System.Drawing.Size(404, 22);
            this.authorEmail.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 306);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Publication Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(144, 302);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(277, 22);
            this.dateTimePicker1.TabIndex = 11;
            // 
            // SourceGroupBox
            // 
            this.SourceGroupBox.Controls.Add(this.sourceURL);
            this.SourceGroupBox.Controls.Add(this.sourceText);
            this.SourceGroupBox.Controls.Add(this.label7);
            this.SourceGroupBox.Controls.Add(this.label6);
            this.SourceGroupBox.Location = new System.Drawing.Point(27, 372);
            this.SourceGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SourceGroupBox.Name = "SourceGroupBox";
            this.SourceGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SourceGroupBox.Size = new System.Drawing.Size(516, 101);
            this.SourceGroupBox.TabIndex = 12;
            this.SourceGroupBox.TabStop = false;
            this.SourceGroupBox.Text = "Source";
            // 
            // sourceURL
            // 
            this.sourceURL.Location = new System.Drawing.Point(157, 62);
            this.sourceURL.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sourceURL.Name = "sourceURL";
            this.sourceURL.Size = new System.Drawing.Size(349, 22);
            this.sourceURL.TabIndex = 3;
            // 
            // sourceText
            // 
            this.sourceText.Location = new System.Drawing.Point(157, 21);
            this.sourceText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sourceText.Name = "sourceText";
            this.sourceText.Size = new System.Drawing.Size(349, 22);
            this.sourceText.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 71);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "URL";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 25);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Text";
            // 
            // createItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 587);
            this.ControlBox = false;
            this.Controls.Add(this.SourceGroupBox);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.authorEmail);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.linkBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.titleBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "createItemForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Feed Item";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.createItemForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.SourceGroupBox.ResumeLayout(false);
            this.SourceGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        public System.Windows.Forms.TextBox titleBox;
        public System.Windows.Forms.RichTextBox descriptionBox;
        public System.Windows.Forms.TextBox linkBox;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox authorEmail;
        private System.Windows.Forms.GroupBox SourceGroupBox;
        public System.Windows.Forms.TextBox sourceURL;
        public System.Windows.Forms.TextBox sourceText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label5;
    }
}