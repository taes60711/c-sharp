using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public function f = new function();
        private void Form3_Load(object sender, EventArgs e)
        {
            label3.Text = f.Restart(16, 5);
            comboBox1.SelectedIndex = 0;
        }

        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1.PerformClick();
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && (comboBox1.SelectedIndex + 1) > 0)
            {
                if (textBox2.Text == label3.Text)
                {
                    f.SetCancel(true);
                    this.Close();
                }
                else
                    MessageBox.Show("驗證碼輸入錯誤");

            }
            else
                MessageBox.Show("請至少輸入1筆以上的資料");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text = "剩餘 " + f.Gettimer().ToString() + " 秒";

            if (f.Gettimer() < 0)
                this.Close();
        }

        private bool beginMove = false;
        private int currentXPosition;
        private int currentYPosition;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (beginMove)
            {
                this.Left += MousePosition.X - currentXPosition; //根據滑鼠的 X 座標確定窗體的 X 座標
                this.Top += MousePosition.Y - currentYPosition; //根據滑鼠的 Y 座標確定窗體的 Y 座標
                currentXPosition = MousePosition.X;
                currentYPosition = MousePosition.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X; //滑鼠的 X 座標為當前窗體左上角 X 座標參考
                currentYPosition = MousePosition.Y; //滑鼠的 Y 座標為當前窗體左上角 Y 座標參考
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
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
