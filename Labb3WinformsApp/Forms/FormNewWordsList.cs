using ClassLibraryL3;
using Labb3WinformsApp.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Labb3WinformsApp
{
    public partial class FormNewWordsList : Form
    {
        public WordList WordList { get; set; }
        public string[] languages;
        public string listName;

        public FormNewWordsList()
        {
            InitializeComponent();
            buttonCreate.Enabled = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBoxListName.Text.Length > 0)
            {
                buttonCreate.Enabled = true;
            }
            else
            {
                buttonCreate.Enabled = false;
            }
        }
        private void textBoxLanguages_TextChanged(object sender, EventArgs e)
        {
            if (textBoxLanguages.Text.Length > 0)
            {
                buttonCreate.Enabled = true;
            }
            else
            {
                buttonCreate.Enabled = false;
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxListName.Text) || string.IsNullOrEmpty(textBoxLanguages.Text))
            {
                MessageBox.Show("List name or languages cannot be empty");
                return;
            }
            listName = textBoxListName.Text;
            languages = textBoxLanguages.Text.Split(' ');
            WordList = new WordList(listName, languages);
            DialogResult = DialogResult.OK;
        }

        private void labelLanguages_Click(object sender, EventArgs e)
        {

        }
        private void FormNewWordsList_Load(object sender, EventArgs e)
        {

        }
    }
}
