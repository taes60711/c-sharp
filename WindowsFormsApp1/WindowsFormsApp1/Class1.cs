using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    public class function
    {
        int timer;
        int tempSecond;
        bool ChangeSecondbool;
        bool cancel;

        public string Restart(int basenumber, int getnumber)
        {
            timer = 60;
            tempSecond = 0;
            ChangeSecondbool = false;
            cancel = false;

            return CreateRandom(basenumber, getnumber);
        }

        public string CreateRandom(int basenumber, int getnumber)
        {
            List<string> RandList = new List<string>();
            Random myObject = new Random();
            for (int i = 0; i < getnumber; i++)
            {
                int ranNum = myObject.Next(1, basenumber);
                RandList.Add(Convert.ToString(ranNum, basenumber));
            }

            return string.Join("", RandList.ToArray());
        }

        public int Gettimer()
        {
            if (!ChangeSecondbool)
            {
                tempSecond = DateTime.Now.Second;
                ChangeSecondbool = true;
            }
            else
            {
                if (DateTime.Now.Second - tempSecond >= 1 || DateTime.Now.Second - tempSecond == -59)
                {
                    timer--;
                    ChangeSecondbool = false;
                }
            }

            return timer;
        }

        public bool GetCancel()
        {
            return cancel;
        }

        public void SetCancel(bool cancel)
        {
            this.cancel = cancel;
        }

        /// <summary>
        /// 
        /// </summary>

        string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public string GetCurrentPath(string path)
        {
            return Path + path;
        }

        public void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
        public void Writefile(string path, string content, bool overwrite)
        {
            StreamWriter sw = new StreamWriter(path, overwrite);
            sw.WriteLine(content);
            sw.Close();
        }

        public List<string> Readfile(string path)
        {
            List<string> getid = new List<string>();
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path);
                while (!sr.EndOfStream)
                {
                    getid.Add(sr.ReadLine());
                }
                sr.Close();
            }

            return getid;
        }
    }
}
