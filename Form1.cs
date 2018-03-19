using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anno
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private String title = "Untitled";  //保存打开的文件的标题
        Encoding ec = Encoding.UTF8;          //设置文本的格式为 UTF-8
        private int line = 2;
        private int len = 1;
        private string[] annostring = new string[6000];
        private string[] initstring = new string[6000];

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "") { MessageBox.Show("请先打开文件"); }
            else
            {
                FileStream fs = new FileStream(this.Text, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, ec);
                int num = line;
                while (num > 0 && num < len + 1)
                {
                    richTextBox1.Text = sr.ReadLine();
                    num--;
                }
                if (line < len + 1) { textBox1.Text = line.ToString(); }
                else
                {
                    textBox1.Text = (line - 1).ToString(); MessageBox.Show("已经是最后一句");
                }
                if (line < len + 1) { line++; }
                richTextBox3.Text = "";
                richTextBox4.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "") { MessageBox.Show("请先打开文件"); }
            else
            {
                FileStream fs = new FileStream(this.Text, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, ec);
                int num = line - 2;
                if (num == 0) { MessageBox.Show("已经是第一句"); }
                if (line > 2)
                {
                    textBox1.Text = (line - 2).ToString();
                }
                else { textBox1.Text = (line - 1).ToString(); }
                if (num > 0)
                {
                    line--;
                }
                while (num > 0)
                {
                    richTextBox1.Text = sr.ReadLine();
                    num--;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string aFilePath = @"D:\Anno\a.txt";
            string bFilePath = @"D:\Anno\b.txt";

            FileStream fsaMyfile = new FileStream(aFilePath, FileMode.Append, FileAccess.Write);
            StreamWriter swaMyfile = new StreamWriter(fsaMyfile);
            FileStream fsbMyfile = new FileStream(bFilePath, FileMode.Append, FileAccess.Write);
            StreamWriter swbMyfile = new StreamWriter(fsbMyfile);
            foreach (string i in annostring)
            {
                if (i != null) { swaMyfile.WriteLine(i); }
                else { continue; }
            }
            string[] relation = richTextBox5.Text.Split('\n');
            foreach (string i in relation)
            {
                if (i != null) { swbMyfile.WriteLine(i); }
                else { continue; }
            }
            swaMyfile.Flush();
            swaMyfile.Close();
            fsaMyfile.Close();
            swbMyfile.Flush();
            swbMyfile.Close();
            fsbMyfile.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            annostring[line] = richTextBox1.Text;
            richTextBox2.Text = null;
            foreach (string i in annostring)
            {

                if (i != null)
                {
                    if (richTextBox2.Text != "") { richTextBox2.Text = richTextBox2.Text + '\n' + i; ; }
                    else { richTextBox2.Text = i; }
                }
                else { continue; }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (richTextBox3.Text != "") { richTextBox3.Text = richTextBox3.Text + '\n' + richTextBox1.SelectedText; }
            else { richTextBox3.Text = richTextBox1.SelectedText; }
            richTextBox1.SelectedText = "[@" + richTextBox1.SelectedText + "#" + "D_targeted" + "*]";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();//撤销
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();//重做
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") { MessageBox.Show("请问打开文件"); }
            else
            {
                if (int.Parse(textBox1.Text) < len + 1)
                {
                    line = int.Parse(textBox1.Text);
                    FileStream fs = new FileStream(this.Text, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs, ec);
                    int num = line;
                    while (num > 0)
                    {
                        richTextBox1.Text = sr.ReadLine();
                        num--;
                    }
                    line++;
                }
                else { MessageBox.Show("超出了最大行数"); }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (richTextBox4.Text != "") { richTextBox4.Text = richTextBox4.Text + '\n' + richTextBox1.SelectedText; }
            else { richTextBox4.Text = richTextBox1.SelectedText; }
            richTextBox1.SelectedText = "[@" + richTextBox1.SelectedText + "#" + "adr" + "*]";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") { MessageBox.Show("请先打开文件"); }
            else
            {
                line = int.Parse(textBox1.Text);
                FileStream fs = new FileStream(this.Text, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, ec);
                int num = line;
                string str = "";
                while (num > 0)
                {
                    str = sr.ReadLine();
                    num--;
                }
                line++;
                string[] adr = new string[6];
                string[] drug = new string[6];
                if (richTextBox3.SelectedText != "" & richTextBox4.SelectedText != "")
                {
                    drug = richTextBox3.SelectedText.Split('\n');
                    adr = richTextBox4.SelectedText.Split('\n');
                    foreach (string i in adr)
                    {
                        foreach (string j in drug)
                        {
                            if (richTextBox5.Text != "") { richTextBox5.Text = richTextBox5.Text + '\n' + j + ' ' + i + ' ' + "特定" + ' ' + str; }
                            else { richTextBox5.Text = j + ' ' + i + ' ' + "特定" + ' ' + str; }
                        }
                    }
                }
                else { MessageBox.Show("请选择内容"); }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") { MessageBox.Show("请先打开文件"); }
            else
            {
                line = int.Parse(textBox1.Text);
                FileStream fs = new FileStream(this.Text, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, ec);
                int num = line;
                string str = "";
                while (num > 0)
                {
                    str = sr.ReadLine();
                    num--;
                }
                line++;
                string[] adr = new string[6];
                string[] drug = new string[6];
                if (richTextBox3.SelectedText != "" & richTextBox4.SelectedText != "")
                {
                    drug = richTextBox3.SelectedText.Split('\n');
                    adr = richTextBox4.SelectedText.Split('\n');
                    foreach (string i in adr)
                    {
                        foreach (string j in drug)
                        {
                            if (richTextBox5.Text != "") { richTextBox5.Text = richTextBox5.Text + '\n' + j + ' ' + i + ' ' + "怀疑" + ' ' + str; }
                            else { richTextBox5.Text = j + ' ' + i + ' ' + "怀疑" + ' ' + str; }
                        }
                    }
                }
                else { MessageBox.Show("请选择内容"); }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") { MessageBox.Show("请先打开文件"); }
            else
            {
                line = int.Parse(textBox1.Text);
                FileStream fs = new FileStream(this.Text, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, ec);
                int num = line;
                string str = "";
                while (num > 0)
                {
                    str = sr.ReadLine();
                    num--;
                }
                line++;
                string[] adr = new string[6];
                string[] drug = new string[6];
                if (richTextBox3.SelectedText != "" & richTextBox4.SelectedText != "")
                {
                    drug = richTextBox3.SelectedText.Split('\n');
                    adr = richTextBox4.SelectedText.Split('\n');
                    foreach (string i in adr)
                    {
                        foreach (string j in drug)
                        {
                            if (richTextBox5.Text != "") { richTextBox5.Text = richTextBox5.Text + '\n' + j + ' ' + i + ' ' + "否认" + ' ' + str; }
                            else { richTextBox5.Text = j + ' ' + i + ' ' + "否认" + ' ' + str; }
                        }
                    }
                }
                else { MessageBox.Show("请选择内容"); }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string aFilePath = @"D:\Anno\a.txt";
            string bFilePath = @"D:\Anno\b.txt";

            FileStream fsaMyfile = new FileStream(aFilePath, FileMode.Append, FileAccess.Write);
            StreamWriter swaMyfile = new StreamWriter(fsaMyfile);
            FileStream fsbMyfile = new FileStream(bFilePath, FileMode.Append, FileAccess.Write);
            StreamWriter swbMyfile = new StreamWriter(fsbMyfile);
            string[] anno= richTextBox2.Text.Split('\n');
            foreach (string i in anno)
            {
                if (i != null) { swaMyfile.WriteLine(i); }
                else { continue; }
            }
            string[] relation = richTextBox5.Text.Split('\n');
            foreach (string i in relation)
            {
                if (i != null) { swbMyfile.WriteLine(i); }
                else { continue; }
            }
            swaMyfile.Flush();
            swaMyfile.Close();
            fsaMyfile.Close();
            swbMyfile.Flush();
            swbMyfile.Close();
            fsbMyfile.Close();
        }

        private void opentoolStripButton_Click_1(object sender, EventArgs e)
        {
            /**
            * openFileDialog1 是在设计界面拖出来的控件 OpenFileDialog
            * 
            * 主要是打开 rtf 格式的文件
            */
            openFileDialog1.Filter = "文本文件|*.txt;*.html;*.docx;*.doc;*.rtf|所有文件|*.*"; //文件打开的过滤器
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                title = openFileDialog1.FileName;
                this.Text = title;                  //显示打开的文件名
                richTextBox1.Modified = false;
                string ext = title.Substring(title.LastIndexOf(".") + 1);//获取文件格式
                ext = ext.ToLower();
                FileStream fs = new FileStream(title, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, ec);

                if (ext == "rtf")  //如果后缀是 rtf 加载文件进来
                {
                    richTextBox1.LoadFile(title, RichTextBoxStreamType.RichText);
                    //string[] lines= sr.ReadLine().Split("患者".ToCharArray());
                    //richTextBox1.Text = lines[1];
                }
                else
                {
                    richTextBox1.Text = sr.ReadLine();
                    initstring[0] = richTextBox1.Text;
                    while (sr.ReadLine() != null)
                    {
                        len++;
                    }
                    label1.Text = "共" + len.ToString() + "行";
                    textBox1.Text = (line - 1).ToString();
                }
                fs.Close();
                sr.Close();
            }
        }

    }
}
