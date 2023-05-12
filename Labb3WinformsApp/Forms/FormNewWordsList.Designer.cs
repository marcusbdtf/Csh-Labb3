namespace Labb3WinformsApp
{
    partial class FormNewWordsList
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
            this.labelListName = new System.Windows.Forms.Label();
            this.labelLanguages = new System.Windows.Forms.Label();
            this.textBoxLanguages = new System.Windows.Forms.TextBox();
            this.textBoxListName = new System.Windows.Forms.TextBox();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelListName
            // 
            this.labelListName.AutoSize = true;
            this.labelListName.Location = new System.Drawing.Point(12, 9);
            this.labelListName.Name = "labelListName";
            this.labelListName.Size = new System.Drawing.Size(60, 15);
            this.labelListName.TabIndex = 0;
            this.labelListName.Text = "List Name";
            this.labelListName.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelLanguages
            // 
            this.labelLanguages.AutoSize = true;
            this.labelLanguages.Location = new System.Drawing.Point(12, 73);
            this.labelLanguages.Name = "labelLanguages";
            this.labelLanguages.Size = new System.Drawing.Size(64, 15);
            this.labelLanguages.TabIndex = 1;
            this.labelLanguages.Text = "Languages";
            this.labelLanguages.Click += new System.EventHandler(this.labelLanguages_Click);
            // 
            // textBoxLanguages
            // 
            this.textBoxLanguages.Location = new System.Drawing.Point(12, 91);
            this.textBoxLanguages.Multiline = true;
            this.textBoxLanguages.Name = "textBoxLanguages";
            this.textBoxLanguages.Size = new System.Drawing.Size(227, 160);
            this.textBoxLanguages.TabIndex = 3;
            this.textBoxLanguages.TextChanged += new System.EventHandler(this.textBoxLanguages_TextChanged);
            // 
            // textBoxListName
            // 
            this.textBoxListName.Location = new System.Drawing.Point(12, 27);
            this.textBoxListName.Name = "textBoxListName";
            this.textBoxListName.Size = new System.Drawing.Size(100, 23);
            this.textBoxListName.TabIndex = 4;
            this.textBoxListName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(12, 271);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(75, 23);
            this.buttonCreate.TabIndex = 5;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(93, 271);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormNewWordsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 306);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.textBoxListName);
            this.Controls.Add(this.textBoxLanguages);
            this.Controls.Add(this.labelLanguages);
            this.Controls.Add(this.labelListName);
            this.Name = "FormNewWordsList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormNewWordsList";
            this.Load += new System.EventHandler(this.FormNewWordsList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelListName;
        private Label labelLanguages;
        private TextBox textBoxLanguages;
        private TextBox textBoxListName;
        private Button buttonCreate;
        private Button buttonCancel;
    }
}