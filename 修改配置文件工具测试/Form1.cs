using System;
using System.IO;
using System.Text;

namespace 修改配置文件工具测试
{
    public partial class Form1 : Form
    {
        static Dictionary<string, string> dic = new Dictionary<string, string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char[] chs = { '"' };//以'"'分割
            string findname = "Function";
            string dir = Directory.GetCurrentDirectory();
            DirectoryInfo directoryInfo = new DirectoryInfo(dir);
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                if ((fileInfo.Name).EndsWith("json"))
                {
                    //Console.WriteLine(fileInfo.Name);//测试用
                    //Console.WriteLine((fileInfo.Name).Substring(0,(fileInfo.Name).Length-5));//测试用
                    string str = (fileInfo.Name).Substring(0, (fileInfo.Name).Length - 5);//截取字符串获得文件名 5为.json
                    if (str.Contains(findname))//确定想要的json文件
                    {
                        //Console.WriteLine(str);//测试用
                        string Path1 = dir + "\\";//拼凑路径
                        string Path = Path1 + str + ".json";//拼凑路径
                        //Console.WriteLine(Path);//测试用
                        string[] contents = File.ReadAllLines(Path, Encoding.Default);//获取一开始的文件
                        for (int i = 0; i < contents.Length; i++)
                        {
                            if (contents[i].Length == 1)
                            {
                                dic.Add(contents[i], contents[i]);
                            }
                            else
                            {
                                dic.Add((contents[i].Split(chs))[1], contents[i]);
                            }
                        }
                        //foreach (var item in dic)//测试用输出当前字典的值
                        //{
                        //    Console.WriteLine($"Key:{item.Key}, Value:{item.Value}");
                        //}
                        //Console.WriteLine("现在更改的文件名为：{0}", fileInfo.Name);
                        StorageAccountName(contents);//改时间方法
                        VNetName(contents);//改时间方法
                        File.WriteAllLines(Path, dic.Values);//重新写入（覆盖）
                        dic.Clear();
                    }
                }
            }
            MessageBox.Show("修改成功");
        }
        public static void StorageAccountName(string[] contents)
        {
            char[] chs = { '"' };//以'"'分割
            string[] newcontents = dic["StorageAccountName"].Split(chs);
            char[] charArray = newcontents[3].ToCharArray();
            judgeTime(charArray);
            string str = new string(charArray);
            newcontents[3] = str;
            string strNew = string.Join('"', newcontents);
            dic["StorageAccountName"] = strNew;
        }
        public static void VNetName(string[] contents)
        {
            char[] chs = { '"' };//以'"'分割
            string[] newcontents = dic["VNetName"].Split(chs);
            char[] charArray = newcontents[3].ToCharArray();
            judgeTime(charArray);
            string str = new string(charArray);
            newcontents[3] = str;
            string strNew = string.Join('"', newcontents);
            dic["VNetName"] = strNew;
        }
        public static void judgeTime(char[] charArray)
        {
            char[] chs2 = { '/' };
            string[] datetime = (DateTime.Now.ToLocalTime().ToString()).Split(chs2);
            if (datetime[1].Length == 1)
            {
                string str1 = datetime[1];
                string strNew1 = "0" + str1;
                datetime[1] = strNew1;
            }
            if (datetime[0].Length == 1)
            {
                string str2 = datetime[0];
                string strNew2 = "0" + str2;
                datetime[0] = strNew2;
            }
            char[] timeday = datetime[1].ToCharArray();
            charArray[charArray.Length - 1] = timeday[1];
            charArray[charArray.Length - 2] = timeday[0];
            char[] timemonth = datetime[0].ToCharArray();
            charArray[charArray.Length - 3] = timemonth[1];
            charArray[charArray.Length - 4] = timemonth[0];
        }
        List<string> list = new List<string>();
        private void Form1_Load(object sender, EventArgs e)
        {
            string dir = Directory.GetCurrentDirectory();
            DirectoryInfo directoryInfo = new DirectoryInfo(dir);
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                string str = (fileInfo.Name).Substring(0, (fileInfo.Name).Length - 5);//截取字符串获得文件名 5为.json
                listBox1.Items.Add(str);
                string Path1 = dir + "\\";//拼凑路径
                string Path = Path1 + str + ".json";//拼凑路径
                list.Add(Path);
            }
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            char[] chs = { '"' };//以'"'分割
            //要获得双击的文件所对应的全路径
            string path = list[listBox1.SelectedIndex];
            //using (FileStream fsRead = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
            //{
            //    byte[] buffer = new byte[1024 * 1024 * 5];
            //    int r = fsRead.Read(buffer, 0, buffer.Length);
            //    textBox1.Text = Encoding.Default.GetString(buffer, 0, r);
            //}
            string[] contents = File.ReadAllLines(path, Encoding.Default);
            for (int i = 0; i < contents.Length; i++)
            {
                if (contents[i].Length == 1)
                {
                    dic.Add(contents[i], contents[i]);
                }
                else
                {
                    dic.Add((contents[i].Split(chs))[1], contents[3]);
                }
            }
            //textBox1.Text = contents[0];
            //textBox2.Text = contents[1];
            //textBox3.Text = contents[2];
            //textBox4.Text = contents[3];
            //textBox5.Text = contents[4];
            //textBox6.Text = contents[5];
            //textBox7.Text = contents[6];
            //textBox8.Text = contents[7];
            //textBox9.Text = contents[8];
            //textBox10.Text = (contents[9].Split(chs))[0]+(contents[9].Split(chs))[1]+ (contents[9].Split(chs))[2];
            //textBox11.Text = (contents[9].Split(chs))[3];
            //textBox12.Text = (contents[10].Split(chs))[0]+(contents[10].Split(chs))[1]+(contents[10].Split(chs))[2];
            //textBox13.Text = (contents[10].Split(chs))[3];
            //textBox14.Text = contents[11];
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}