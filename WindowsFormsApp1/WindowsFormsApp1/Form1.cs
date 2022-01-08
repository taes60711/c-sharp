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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            
        }

        public struct User
        {
            public string Id;
            public string Pwd;
            public string Name;
        }

        function f = new function();
        // 登入/註冊_Btn
        private void button2_Click(object sender, EventArgs e)
        {
            LoadUserData();
 
            if (StateChoice == 1)
            {
                 f.CreateDirectory(f.GetCurrentPath(@"\Project"));
                
                if (!CreateId_Check(UserAccount.Text))
                {
                    if (UserAccount.Text != "" && UserPwd.Text != "" && UserId.Text != "")
                    {
                        f.Writefile(f.GetCurrentPath(@"\Project\user.txt"),
                            UserAccount.Text + "," + UserPwd.Text + "," + UserId.Text,
                            true);
                        MessageBox.Show("註冊成功");
                    }
                    else
                        MessageBox.Show("內容不得有空白");
                }
                else
                    MessageBox.Show("帳號已存在");

            }
            else if (StateChoice == 2)
            {
                if (Login_Check(UserAccount.Text, UserPwd.Text))
                {
                    this.Hide(); 
                    Form2 form2 = new Form2(this);
                    form2.Show();
                }
                else
                    MessageBox.Show("帳號或密碼錯誤");
            }
        }
        public List<User> userlist = new List<User>();
        public User LoginedUser = new User();
        void LoadUserData()
        {
            List<string> getid = f.Readfile(f.GetCurrentPath(@"\Project\user.txt"));

            for (int i = 0; i < getid.Count; i++)
            {
                string[] Currentgetid = getid[i].Split(',');

                User user = new User();
                user.Id = Currentgetid[0];
                user.Pwd = Currentgetid[1];
                user.Name = Currentgetid[2];
                userlist.Add(user);
            }
        }

       

        
        bool Login_Check(string id , string pwd)
        {
            bool CanLogin = false;
            for (int i = 0; i < userlist.Count; i++)
            {
                if (userlist[i].Id == id && userlist[i].Pwd == pwd)
                {
                    CanLogin = true;
                    LoginedUser = userlist[i];
                    break;
                }  
            }
            
            return CanLogin;
        }
      
        bool CreateId_Check(string id)
        {
            bool hasid = false;
            for (int i = 0; i < userlist.Count; i++)
            {
                if (userlist[i].Id == id)
                    hasid = true;
            }

            return hasid;
        }


       
        private void 註冊ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StateChoice = StageChange(1, "註冊", true);
            radioButton1.Checked = true;
        }

        private void 登入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StateChoice = StageChange(2, "登入", false);
            radioButton2.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                StateChoice = StageChange(1, "註冊", true);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                StateChoice = StageChange(2, "登入", false);
        }

        public int StageChange(int StateChoice, string stage, bool stagebl)
        {
            this.button2.Text = stage;
            label4.Text = stage;
            label5.Visible = stagebl;
            UserId.Visible = stagebl;

            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

            return StateChoice;
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button2.PerformClick();
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        public TextBox UserAccount, UserPwd, UserId;

        public int StateChoice = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            UserAccount = textBox2;
            UserPwd = textBox3;
            UserId = textBox4;

            radioButton2.Checked = !radioButton2.Checked;

            if (radioButton2.Checked)
                StateChoice = StageChange(2, "登入", false);

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

       

     
    }
}
