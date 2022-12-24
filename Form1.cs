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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.DataVisualization.Charting;
using System.Net.NetworkInformation;

namespace TextAnalise
{
    public partial class Form1 : Form
    {
        string s;
        int[] N = new int[100];
        int countLet;
        int count_words;
        int end;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
            chart3.Series[0].Points.Clear();
            listBox1.Items.Clear();
            string[] al = { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я", " " };
            s = richTextBox1.Text.ToLower();
            countLet = 0;
            for (int i = 0; i < al.Length; i++)
            {
                N[i] = 0;
                for (int j = 0; j < s.Length; j++)
                {
                    if (al[i] == Convert.ToString(s[j]))
                        N[i]++;
                }
                if (N[i] > 0 & i != al.Length - 1)
                {
                    listBox1.Items.Add(al[i].ToUpper() + " - " + N[i]);// вывод вертикального списка
                    chart1.Series[0].Points.AddXY(al[i], N[i]);// вывод гистограммы
                }
                countLet += N[i];

            }
            count_words = N[33] + 1 ;
            for (int j = 0; j < s.Length; j++)// определяем где конец слово (точнее сколько предложений
                if ("!" == Convert.ToString(s[j]) || "." == Convert.ToString(s[j]) || "?" == Convert.ToString(s[j]))
                    end++;
            if (end == 0) // обрабатываем разные случаи и выводим средние значения 
            {
                end = 1;
                textBox2.Text = Convert.ToString(count_words / end);
                textBox3.Text = Convert.ToString(countLet / end);
            }
            else
            {
                textBox2.Text = Convert.ToString(count_words / end);
                textBox3.Text = Convert.ToString(countLet / end);
            }
            textBox4.Text = Convert.ToString(end);
            // определяем самое длиное слово
            string[] mass = s.Split(' ');
            string max_long_Slovo = "";
            int max = 0;
            foreach (string a in mass)
                if (a.Length > max)
                {
                    max = a.Length;
                    max_long_Slovo = a;
                }
            
            max_long_Slovo = max_long_Slovo.Replace(".", "").Replace("?" , "").Replace("!" , "").Replace(";", "").Replace(":", "");
            
            textBox5.Text = max_long_Slovo;// убираем ненужные символы и выводим самоле длинное слово

            int[] lengths = new int[max_long_Slovo.Length];// максимальная длинна слова ( нужно для графика как максимальное значение
            string[] words = Convert.ToString(s).Split(' ');// получаем массив из элементов строки где разделяем их с помошью пробела
            string b = "";//вспомогательная переменная так как в цикле нельзя менять переменную
            foreach(string w in words)// заполняем массив где индек означает( длину слова +1) ,а значение  количество таких слов
            {
                b = w.Replace(".", "").Replace("?", "").Replace("!", "");
                if (b.Length != 0)
                    lengths[b.Length - 1]++;
            }
            for (int i = 0; i < max_long_Slovo.Length; i++) // график зависимости количества слов от длины слова
                chart3.Series[0].Points.AddXY(i+1, lengths[i]);//
 

            int max_long_sentence = 0;                     // самое длинное предложение ( по количеству слов)
            string[] sentence = Convert.ToString(s).Split(new char[] { '!','.','?' }, StringSplitOptions.RemoveEmptyEntries);// разделяем строку на предложения , сразу убирая пустые элементы
            foreach(string s in sentence)
                if (s.Split(' ').Length > max_long_sentence)// далее разделив предложение по словам через пробелы , смотрим количесвто слов
                    max_long_sentence = s.Split(' ').Length;
            int[] sentence_lengths = new int[max_long_sentence];// максимальная длинна предложения ( нужно для графика как максимальное значение
            foreach (string s in sentence)                  // заполняем массив где индек означает( длину предложения +1) ,а значение  количество таких предложений
                if(s.Length != 0)
                    sentence_lengths[s.Split(' ').Length - 1]++;
            int[] ax2 = new int[max_long_sentence];
            for (int i = 0; i < ax2.Length; i++)// график зависимости количества предложения от длины предложения
                chart2.Series[0].Points.AddXY(i + 1, sentence_lengths[i]);
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read);
                if (fileStream != null)
                {
                    StreamReader streamreader = new StreamReader(fileStream);
                    richTextBox1.Text = streamreader.ReadToEnd();
                    fileStream.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(countLet);
        }


        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


