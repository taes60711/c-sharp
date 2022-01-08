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

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form1 f1;
        int total;
        string ImagePath, MoneyPath, DirectoryPath;
        function f = new function();
        public Form2(Form1 f1)
        {
            this.f1 = f1;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            UserInfo.Text = " ID :          " + f1.LoginedUser.Id+"\n Name :  "+f1.LoginedUser.Name;
            DirectoryPath = f.GetCurrentPath(@"\Project\" + f1.LoginedUser.Id);
            MoneyPath = f.GetCurrentPath(@"\Project\" + f1.LoginedUser.Id + @"\dollar.txt");
            ImagePath = f.GetCurrentPath(@"\Project\" + f1.LoginedUser.Id + @"\1.png");

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            if (File.Exists(ImagePath))
                pictureBox1.Image = Image.FromFile(ImagePath);
            else
                pictureBox1.Image = WindowsFormsApp1.Properties.Resources._1;

            f.CreateDirectory(DirectoryPath);

            if (!File.Exists(MoneyPath))
                f.Writefile(MoneyPath, "0", false);
  
            total = LoadUserMoney();
            listBox1.Items.Add("目前總餘額 : " + total);
        }

        private void button2_Click(object sender, EventArgs e)
        { getoutmoney(1, "存入", getmoney,comboBox1); }

        private void button3_Click(object sender, EventArgs e)
        { getoutmoney(-1, "取出", outmoney, comboBox2); }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                MessageBox.Show("請在ListBox內點選任一選項");
            else if (listBox1.SelectedIndex == 0)
                MessageBox.Show("第一項不可選擇，請選擇其他項目或先進行存入或取出的動作");
            else
            {
                int GetLayerIndex = listBox1.SelectedIndex / 2;
                int SelectedStartLayerIndex = 0;

                if (listBox1.SelectedIndex % 2 == 0)
                    SelectedStartLayerIndex = (2 * (GetLayerIndex - 1)) + 1;
                else
                    SelectedStartLayerIndex = (2 * GetLayerIndex) + 1;

                for (int i = 0; i < 2; i++)
                    listBox1.Items.RemoveAt(SelectedStartLayerIndex);

                listBox1.Items[0] = "目前總餘額 : " + total.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("目前總餘額 : " + total);
        }


        private void 存入ToolStripMenuItem_Click(object sender, EventArgs e)
        { getoutmoney(1, "存入"); }

        private void 取出ToolStripMenuItem_Click(object sender, EventArgs e)
        { getoutmoney(-1, "取出"); }

        void getoutmoney(int plus, string getorout)
        {
            Form3 form3 = new Form3();

            form3.button1.Text = getorout;
            form3.label4.Text = getorout;
            form3.ShowDialog();

            if (form3.f.GetCancel())
                totalcalculate(plus, getorout, form3.textBox1, form3.comboBox1);
        }

        void getoutmoney(int plus, string getorout, TextBox textBox, ComboBox comboBox)
        {
            Form4 form4 = new Form4();
           
            if (textBox.Text != "")
            {
                form4.label5.Text = getorout;
                form4.ShowDialog();

                if (form4.f.GetCancel())
                    totalcalculate(plus,getorout,textBox,comboBox);
            }
            else
                MessageBox.Show("請至少輸入1筆以上的資料");
        }

        void totalcalculate(int plus , string getorout, TextBox textBox, ComboBox comboBox)
        {
            int temptotal = total;
            if (temptotal + int.Parse(textBox.Text) * (comboBox.SelectedIndex + 1) * plus < 0)
                MessageBox.Show("餘額不足");
            else
            {
                total = total + int.Parse(textBox.Text) * (comboBox.SelectedIndex + 1) * plus;
                listBox1.Items.Add(getorout + textBox.Text + " 元    共" + (comboBox.SelectedIndex + 1).ToString() + " 筆");
                listBox1.Items.Add("目前餘額 : " + total);
                listBox1.Items[0] = "目前總餘額 : " + total.ToString();

                f.Writefile(MoneyPath, total.ToString(), false);
            }
        }

        int LoadUserMoney()
        {
            List<string> money = f.Readfile(MoneyPath);
            return int.Parse(money[0]);
        }

        private void 登出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files (JPG,PNG,GIF)|*.JPG;*.PNG;*.GIF";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
               string ChoicePath = System.IO.Path.GetFullPath(openFileDialog1.FileName);

                if (!File.Exists(ImagePath))
                    File.Copy(ChoicePath, ImagePath);
                else
                {
                    if (pictureBox1.Image != null)
                        pictureBox1.Image.Dispose();

                    File.Delete(ImagePath);
                    File.Copy(ChoicePath, ImagePath);
                }

                pictureBox1.Image = Image.FromFile(ImagePath);
                
            }
        }

        private bool beginMove = false;
        private int currentXPosition;
        private int currentYPosition;

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X; //滑鼠的 X 座標為當前窗體左上角 X 座標參考
                currentYPosition = MousePosition.Y; //滑鼠的 Y 座標為當前窗體左上角 Y 座標參考
            }
        }

        private void menuStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            if (beginMove)
            {
                this.Left += MousePosition.X - currentXPosition; //根據滑鼠的 X 座標確定窗體的 X 座標
                this.Top += MousePosition.Y - currentYPosition; //根據滑鼠的 Y 座標確定窗體的 Y 座標
                currentXPosition = MousePosition.X;
                currentYPosition = MousePosition.Y;
            }
        }

        private void menuStrip1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentXPosition = 0; //設定初始狀態
                currentYPosition = 0; //設定初始狀態
                beginMove = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X; //滑鼠的 X 座標為當前窗體左上角 X 座標參考
                currentYPosition = MousePosition.Y; //滑鼠的 Y 座標為當前窗體左上角 Y 座標參考
            }
        }

        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (beginMove)
            {
                this.Left += MousePosition.X - currentXPosition; //根據滑鼠的 X 座標確定窗體的 X 座標
                this.Top += MousePosition.Y - currentYPosition; //根據滑鼠的 Y 座標確定窗體的 Y 座標
                currentXPosition = MousePosition.X;
                currentYPosition = MousePosition.Y;
            }
        }

        private void listBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentXPosition = 0; //設定初始狀態
                currentYPosition = 0; //設定初始狀態
                beginMove = false;
            }
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X; //滑鼠的 X 座標為當前窗體左上角 X 座標參考
                currentYPosition = MousePosition.Y; //滑鼠的 Y 座標為當前窗體左上角 Y 座標參考
            }
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (beginMove)
            {
                this.Left += MousePosition.X - currentXPosition; //根據滑鼠的 X 座標確定窗體的 X 座標
                this.Top += MousePosition.Y - currentYPosition; //根據滑鼠的 Y 座標確定窗體的 Y 座標
                currentXPosition = MousePosition.X;
                currentYPosition = MousePosition.Y;
            }
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentXPosition = 0; //設定初始狀態
                currentYPosition = 0; //設定初始狀態
                beginMove = false;
            }
        }


    }
}
