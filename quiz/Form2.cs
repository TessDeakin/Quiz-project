using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quiz
{
    public partial class Form2 : Form
    {
        public string quizname;
        private Form1 parent;
        private MMQuiz[] Questionlist;
        private int NumberofQuestions = 0;
        private int CurrentQuestionIndex = 1;
        private int score = 0;
        private bool QuestionAnswered = false;
        private int ansnum = 0;
        private int ts = 0;

        System.Timers.Timer timer;
        int m, s;

        struct MMQuiz
        {
            public string question;
            public string answera;
            public string answerb;
            public string answerc;
            public string answerd;
            public string correct;
        }

        public Form2(string selectedItem, Form1 caller, int totalscore)
        {
            ts = totalscore;
            this.quizname = selectedItem;
            this.parent = caller;
            InitializeComponent();
            Questionlist = new MMQuiz[MaxQuestions + 1];
            NumberofQuestions = LoadQuizData(ref Questionlist, quizname);
        }

        const int MaxQuestions = 10;

        static int LoadQuizData(ref MMQuiz[] Questionlist, string quizname)
        {
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(quizname + ".txt");
                int QuestionCount = 0;
                while (file.EndOfStream == false)
                {
                    MMQuiz ThisQuiz;
                    ThisQuiz.question = file.ReadLine();
                    ThisQuiz.answera = file.ReadLine();
                    ThisQuiz.answerb = file.ReadLine();
                    ThisQuiz.answerc = file.ReadLine();
                    ThisQuiz.answerd = file.ReadLine();
                    ThisQuiz.correct = file.ReadLine();
                    QuestionCount += 1;
                    Questionlist[QuestionCount] = ThisQuiz;
                }
                file.Close();
                return QuestionCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading quiz data: " + ex.Message);
                return 0;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Visible = true;
            label1.Text = quizname;
            UpdateScoreDisplay();
            DisplayQuestion(CurrentQuestionIndex, ref Questionlist);
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        private void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                s++;
                if (s == 60)
                {
                    m++;
                    s = 0;
                }
                if (s == 30 && ansnum < 2)
                {
                    timer.Stop();
                    MessageBox.Show("Time up!");
                    check("x", CurrentQuestionIndex, ref Questionlist);
                    MessageBox.Show("Quiz completed, you scored: " + score + " / " + NumberofQuestions);
                    button6.Visible = false;
                }
                label5.Text = "Time: " + m + ":" + s;
            }));
        }

        private void UpdateScoreDisplay()
        {
            label4.Visible = true;
            label4.Text = score + " / " + NumberofQuestions;
        }

        private void DisplayQuestion(int questionnum, ref MMQuiz[] questionlist)
        {
            textBox1.Text = questionlist[questionnum].question;
            textBox2.Text = questionlist[questionnum].answera;
            textBox3.Text = questionlist[questionnum].answerb;
            textBox4.Text = questionlist[questionnum].answerc;
            textBox5.Text = questionlist[questionnum].answerd;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 goback = new Form1(ts);
            goback.ShowDialog();
            this.Close();
            parent.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ansnum++;
            check(button2.Text, CurrentQuestionIndex, ref Questionlist);
        }

        static void end(int num, System.Timers.Timer timer)
        {
            if (num == 2)
            {
                timer.Stop();
            }
        }

        private void check(string answer, int questionnum, ref MMQuiz[] questionlist)
        {
            end(ansnum, timer);
            if (answer == questionlist[questionnum].correct)
            {
                score++;
                ts++;
                if (CurrentQuestionIndex - 1 == NumberofQuestions)
                {
                    QuestionAnswered = true;
                }
                MessageBox.Show("Correct!");
            }
            else
            {
                MessageBox.Show("Sorry the correct answer is: " + questionlist[questionnum].correct);
                if (CurrentQuestionIndex-1 == NumberofQuestions)
                {
                    QuestionAnswered= true;
                }
            }
            CurrentQuestionIndex++;
            if (CurrentQuestionIndex - 1 == NumberofQuestions)
            {
                button6.Visible = true;
                button6.Text = "Finish Quiz";
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            ansnum++;
            check(button3.Text, CurrentQuestionIndex, ref Questionlist);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ansnum++;
            check(button4.Text, CurrentQuestionIndex, ref Questionlist);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ansnum++;
            check(button5.Text, CurrentQuestionIndex, ref Questionlist);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UpdateScoreDisplay();
            DisplayQuestion(CurrentQuestionIndex, ref Questionlist);
            if (CurrentQuestionIndex - 1 == NumberofQuestions)
            {
                MessageBox.Show("Quiz completed, you scored: " + score + " / " + NumberofQuestions);
                timer.Stop();
                button6.Visible = false;
            }
        }
    }
}
