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
    public partial class FormPractice : Form
    {
        private Wordlist _wordlist;
        private int _totalWords;
        private int _correctWords;
        Word word { get; set; }

        public FormPractice(Wordlist wordlist)
        {
            InitializeComponent();
            _wordlist = wordlist;
            word = _wordlist.GetWordToPractice();
            _totalWords = 0;
            _correctWords = 0;
        }
        private void label1_Click(object sender, EventArgs e) {}

        private void buttonPractice_Click(object sender, EventArgs e)
        {
            labelQ.Text = $"Translate '{word.Translations[word.FromLanguage]}' to {_wordlist.Languages[word.ToLanguage]}";
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            _totalWords++;

            string userAnswer = textBoxAns.Text?.ToLower();
            if (word.Translations[word.ToLanguage] == userAnswer)
            {
                _correctWords++;
                labelResult.Text = "Correct!";
            }
            else
            {
                labelResult.Text = "Incorrect";
            }
            textBoxAns.Clear();

            word = _wordlist.GetWordToPractice();
            labelQ.Text = $"Translate '{word.Translations[word.FromLanguage]}' to {_wordlist.Languages[word.ToLanguage]}";
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            string message = $"Amount of correct words: {_correctWords}/{_totalWords}\nPercentage: {((float)_correctWords / _totalWords) * 100:0.0}%";
            MessageBox.Show(message, "Practice Results");
            Close();
        }



        private void textBoxAns_TextChanged(object sender, EventArgs e){ }
    }
}
