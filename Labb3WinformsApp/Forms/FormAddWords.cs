using ClassLibraryL3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labb3WinformsApp.Forms
{
    public partial class FormAddWords : Form
    {
        public WordList WordList;

        public FormAddWords(WordList wordList)
        {
            InitializeComponent();
            WordList = wordList;

            if (wordList.Count() == 0)
            {
                string[] fillerWords = { "Replace", "Replace2", "Replace3", "Replace4" };
                string[] tempArray = new string[wordList.Languages.Length];
                for (int i = 0; i < wordList.Languages.Length; i++)
                {
                    tempArray[i] = fillerWords[i];
                }
                wordList.Add(tempArray);
            }
            ShowWordListDataGridView(wordList, 0);
        }
        private void FormAddWords_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Form1)this.Owner).RefreshWordList();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void ShowWordListDataGridView(WordList wordList, int languageIndex)
        {
            dataGridViewAdder.Columns.Clear();
            dataGridViewAdder.Rows.Clear();

            for (int i = 0; i < wordList.Languages.Length; i++)
            {
                var column = new DataGridViewTextBoxColumn();
                column.HeaderText = wordList.Languages[i];
                dataGridViewAdder.Columns.Add(column);
            }

            wordList.List(languageIndex, translations =>
            {
                // Create a list to hold the cell values
                List<string> cellValues = new List<string>();

                // Add each translation to the list
                for (int i = 0; i < translations.Length; i++)
                {
                    cellValues.Add(translations[i]);
                }

                // Add the list as a new row in the DataGridView
                dataGridViewAdder.Rows.Add(cellValues.ToArray());
            });
        }

        private static void WordPrinter(string[] array)
        {
            string str = "";
            foreach (string s in array)
            {
                str += $"{s}\t";
            }
            Console.WriteLine(str);
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            dataGridViewAdder.ReadOnly = false;

            var row = new DataGridViewRow();
            for (int i = 0; i < dataGridViewAdder.ColumnCount; i++)
            {
                row.Cells.Add(new DataGridViewTextBoxCell());
            }

            dataGridViewAdder.Rows.Add(row);

            foreach (DataGridViewRow existingRow in dataGridViewAdder.Rows)
            {
                foreach (DataGridViewCell cell in existingRow.Cells)
                {
                    if (existingRow.Index != dataGridViewAdder.RowCount - 1)
                    {
                        cell.ReadOnly = true;
                    }
                }
            }
            MessageBox.Show("New empty cells have been added. You can now enter new translations.");
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewAdder.SelectedRows)
            {
                dataGridViewAdder.Rows.RemoveAt(row.Index);
            }
        }
        private void buttonCloseSave_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void dataGridViewAdder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewAdder.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                    {
                        MessageBox.Show("Please fill in all the cells before saving.");
                        return;
                    }
                }
            }
            WordList.Clear();
            foreach (DataGridViewRow row in dataGridViewAdder.Rows)
            {
                string[] words = new string[dataGridViewAdder.ColumnCount];
                for (int i = 0; i < dataGridViewAdder.ColumnCount; i++)
                {
                    words[i] = row.Cells[i].Value.ToString();
                }
                WordList.Add(words);
            }
            WordList.Save();
            dataGridViewAdder.ReadOnly = true;
        }
        private void FormAddWords_Load(object sender, EventArgs e)
        {

        }
    }
}
