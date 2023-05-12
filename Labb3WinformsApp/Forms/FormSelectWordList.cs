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
    public partial class FormSelectWordList : Form
    {
        Form1 f1;
        public WordList wordlist { get; set; }
        public FormSelectWordList()
        {
            this.f1 = new Form1();
            InitializeComponent();
            listBoxFiles.Items.AddRange(WordList.GetLists());

        }    
        private void FormSelectWordList_Load(object sender, EventArgs e)
        {
            listBoxFiles.Items.Clear();
            listBoxFiles.Items.AddRange(WordList.GetLists());
            
        }

        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBoxFiles_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listBoxFiles?.SelectedItem == null)
            {
                MessageBox.Show("No file selected");
                return;
            }

            listBoxFileLanguages.Items.Clear();
            string text = listBoxFiles.GetItemText(listBoxFiles.SelectedItem);
            this.wordlist = WordList.LoadList(text);

            if (wordlist == null || wordlist.Languages == null)
            {
                MessageBox.Show("No wordlist or languages found");
                return;
            }

            listBoxFileLanguages.Items.AddRange(wordlist.Languages);
            labelLanguages.Text = $"Words in file: {wordlist.Count() * wordlist.Languages.Length}";
            buttonOk.Enabled = true;
        }
        private void labelLanguages_Click(object sender, EventArgs e)
        {

        }
        private void FormSelectWordList_Load_1(object sender, EventArgs e)
        {

        }
    }
}
