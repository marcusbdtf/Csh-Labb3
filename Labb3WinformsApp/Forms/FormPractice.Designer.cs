namespace Labb3WinformsApp.Forms
{
    partial class FormPractice
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
            this.textBoxAns = new System.Windows.Forms.TextBox();
            this.labelQ = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonPractice = new System.Windows.Forms.Button();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.labelResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxAns
            // 
            this.textBoxAns.Location = new System.Drawing.Point(115, 117);
            this.textBoxAns.Name = "textBoxAns";
            this.textBoxAns.Size = new System.Drawing.Size(121, 23);
            this.textBoxAns.TabIndex = 0;
            this.textBoxAns.Text = "Enter Translation";
            this.textBoxAns.TextChanged += new System.EventHandler(this.textBoxAns_TextChanged);
            // 
            // labelQ
            // 
            this.labelQ.AutoSize = true;
            this.labelQ.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelQ.Location = new System.Drawing.Point(67, 77);
            this.labelQ.Name = "labelQ";
            this.labelQ.Size = new System.Drawing.Size(75, 13);
            this.labelQ.TabIndex = 1;
            this.labelQ.Text = "Press Practice";
            this.labelQ.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(270, 241);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 5;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonPractice
            // 
            this.buttonPractice.Location = new System.Drawing.Point(270, 212);
            this.buttonPractice.Name = "buttonPractice";
            this.buttonPractice.Size = new System.Drawing.Size(75, 23);
            this.buttonPractice.TabIndex = 6;
            this.buttonPractice.Text = "Practice";
            this.buttonPractice.UseVisualStyleBackColor = true;
            this.buttonPractice.Click += new System.EventHandler(this.buttonPractice_Click);
            // 
            // buttonEnter
            // 
            this.buttonEnter.Location = new System.Drawing.Point(127, 146);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(97, 23);
            this.buttonEnter.TabIndex = 7;
            this.buttonEnter.Text = "Guess";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Location = new System.Drawing.Point(155, 199);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(39, 15);
            this.labelResult.TabIndex = 8;
            this.labelResult.Text = "Result";
            // 
            // FormPractice
            // 
            this.AcceptButton = this.buttonEnter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 276);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.buttonPractice);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.labelQ);
            this.Controls.Add(this.textBoxAns);
            this.Name = "FormPractice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormPractice";
            this.Load += new System.EventHandler(this.FormPractice_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBoxAns;
        private Label labelQ;
        private Button buttonExit;
        private Button buttonPractice;
        private Button buttonEnter;
        private Label labelResult;
    }
}