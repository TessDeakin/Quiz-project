using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace quiz
{
    public partial class Form1 : Form
    {
        private int tempnum = 0;
        public Form1(int totalscore)
        {
            tempnum = totalscore;
            InitializeComponent();
            LoadComboBox();
            label3.Text = " " +  totalscore;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string selectedItem = comboBox1.SelectedItem.ToString();
                Form2 quizForm = new Form2(selectedItem, this, tempnum);
                this.Hide();
                quizForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("no selection");
            }
        }

        private void LoadComboBox()
        {
            string[] lines = File.ReadAllLines("C:/Users/tessd/source/repos/quiz/quiz/bin/Debug/Topics.txt");
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    comboBox1.Items.Add(line);
                }
            }
        }
    }
}
