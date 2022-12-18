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
        public Wordlist WordList;

        public FormAddWords(Wordlist wordList)
        {
            InitializeComponent();
            WordList = wordList;

            // Add temporary words to the WordList object if it is empty
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


        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void ShowWordListDataGridView(Wordlist wordList, int languageIndex)
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
                var row = new DataGridViewRow();

                // Add cell for each translation
                for (int i = 0; i < translations.Length; i++)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = translations[i] });
                }

                // Add the row to the DataGridView
                dataGridViewAdder.Rows.Add(row);
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
            // last row index
            int lastRowIndex = dataGridViewAdder.RowCount - 1;

            //  words from last row of the DataGrid
            string[] words = new string[dataGridViewAdder.ColumnCount];
            for (int j = 0; j < dataGridViewAdder.ColumnCount; j++)
            {
                words[j] = dataGridViewAdder.Rows[lastRowIndex].Cells[j].Value.ToString();
            }

            WordList.Add(words);
            ShowWordListDataGridView(WordList, 0);
            MessageBox.Show("Added");
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Remove the selected rows from the DataGridView control
            foreach (DataGridViewRow row in dataGridViewAdder.SelectedRows)
            {
                dataGridViewAdder.Rows.RemoveAt(row.Index);
            }
        }

        private void buttonCloseSave_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

}
