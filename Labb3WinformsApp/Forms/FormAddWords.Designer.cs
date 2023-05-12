namespace Labb3WinformsApp.Forms
{
    partial class FormAddWords
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
            this.labelAddHeader = new System.Windows.Forms.Label();
            this.dataGridViewAdder = new System.Windows.Forms.DataGridView();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonCloseSave = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAdder)).BeginInit();
            this.SuspendLayout();
            // 
            // labelAddHeader
            // 
            this.labelAddHeader.AutoSize = true;
            this.labelAddHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelAddHeader.Location = new System.Drawing.Point(104, 9);
            this.labelAddHeader.Name = "labelAddHeader";
            this.labelAddHeader.Size = new System.Drawing.Size(133, 30);
            this.labelAddHeader.TabIndex = 0;
            this.labelAddHeader.Text = "Word Adder";
            this.labelAddHeader.Click += new System.EventHandler(this.label1_Click);
            // 
            // dataGridViewAdder
            // 
            this.dataGridViewAdder.AllowUserToAddRows = false;
            this.dataGridViewAdder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAdder.Location = new System.Drawing.Point(1, 42);
            this.dataGridViewAdder.Name = "dataGridViewAdder";
            this.dataGridViewAdder.ReadOnly = true;
            this.dataGridViewAdder.RowTemplate.Height = 25;
            this.dataGridViewAdder.Size = new System.Drawing.Size(341, 222);
            this.dataGridViewAdder.TabIndex = 1;
            this.dataGridViewAdder.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAdder_CellContentClick);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(104, 270);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 3;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(12, 270);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonCloseSave
            // 
            this.buttonCloseSave.Location = new System.Drawing.Point(12, 331);
            this.buttonCloseSave.Name = "buttonCloseSave";
            this.buttonCloseSave.Size = new System.Drawing.Size(103, 30);
            this.buttonCloseSave.TabIndex = 5;
            this.buttonCloseSave.Text = "Close";
            this.buttonCloseSave.UseVisualStyleBackColor = true;
            this.buttonCloseSave.Click += new System.EventHandler(this.buttonCloseSave_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(254, 335);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // FormAddWords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 377);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCloseSave);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.dataGridViewAdder);
            this.Controls.Add(this.labelAddHeader);
            this.Name = "FormAddWords";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Words";
            this.Load += new System.EventHandler(this.FormAddWords_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAdder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelAddHeader;
        private DataGridView dataGridViewAdder;
        private Button buttonRemove;
        private Button buttonAdd;
        private Button buttonCloseSave;
        private Button buttonSave;
    }
}