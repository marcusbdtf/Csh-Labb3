using ClassLibraryL3;
using Labb3WinformsApp.Forms;
using System.Drawing.Text;
using System.Text;

namespace Labb3WinformsApp
{
    public partial class Form1 : Form
    {
        private Wordlist wordList;

        public string textBoxListName { get; set; }
        public string[] textBoxLanguages { get; set; }

        public Form1()
        {
            InitializeComponent();

        }

        private void selectWordsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formSelectWordlist = new FormSelectWordList())
            {
                if (formSelectWordlist.ShowDialog() == DialogResult.OK)
                {
                    wordList = formSelectWordlist.wordlist;
                    ShowWordListDataGridView(wordList, 0);
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = wordList;
        }

        private void ShowWordListDataGridView(Wordlist wordListName, int languageIndex)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            // Create a column for each language in the word list
            for (int i = 0; i < wordList.Languages.Length; i++)
            {
                var column = new DataGridViewTextBoxColumn();
                column.HeaderText = wordList.Languages[i];
                dataGridView1.Columns.Add(column);
            }

            wordList.List(languageIndex, translations =>
            {
                var row = new DataGridViewRow();
                WordPrinter(translations);
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = translations[j] });
                }

                dataGridView1.Rows.Add(row);
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

        private void newWordsListToolStripMenuItemNewWord_Click(object sender, EventArgs e)
        {
            if (wordList != null)
            {
                wordList.Clear();
            }
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            using (var formNewWordlist = new FormNewWordsList())
            {
                if (formNewWordlist.ShowDialog() == DialogResult.OK)
                {
                    wordList = formNewWordlist.WordList;
                    using (var formAddWords = new FormAddWords(wordList))
                    {
                        formAddWords.ShowDialog();
                    }
                    ShowWordListDataGridView(wordList, 0);
                }
            }
        }

        private void buttonStartTraining_Click(object sender, EventArgs e)
        {
            if (wordList == null)
            {
                MessageBox.Show("Please select a word list first");
                return;
            }

            FormPractice formPractice = new FormPractice(wordList);
            formPractice.Show();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wordList.Clear();
            int rowCount = dataGridView1.RowCount;
            int columnCount = dataGridView1.ColumnCount;

            for (int i = 0; i < rowCount; i++)
            {
                string[] words = new string[columnCount];
                for (int j = 0; j < columnCount; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        words[j] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                    else
                    {
                        words[j] = "";
                    }
                }
                wordList.Add(words);
            }
            wordList.Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wordList != null)
            {
                FormAddWords form = new FormAddWords(wordList);
                form.Show();

            }
        }
    }
}