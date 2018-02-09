using AttentionTransferSpeedTest.BLL;
using AttentionTransferSpeedTest.DAL.DBO;
using AttentionTransferSpeedTest.DAL.Gateway;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace AttentionTransferSpeedTest
{
    public partial class Form1 : Form
    {
        private int X;
        private int Y;
        private AutoSizeFormClass asc = new AutoSizeFormClass();
        private System.Media.SoundPlayer s = new System.Media.SoundPlayer("resources/music/start_music.wav");
        private Thread t1;
        private Thread t2;
        private int[] Combination = new int[12];
        private int Correct = 0;
        private int Input = 0;
        private int ISI = 0;
        private int startTime;
        private int endTime;
        private Boolean isInput = false;
        private int[] sameComnination = new int[12];
        private int[] ISIS = { 1000, 618, 382, 236, 146, 90, 56 };
        private Boolean isRight = false;
        private int p;
        private int sp;
        private int trues = 0;
        private int falses = 0;
        private string[] Combinations = new string[140];
        private int[] ps = new int[140];
        private int[] Corrects = new int[140];
        private int[] Inputs = new int[140];
        private int[] RT = new int[140];
        private int[] realISIs = new int[140];        //定义全局变量
        public int currentCount = 0;
        private Thread t4;
        private Thread t5;
        private Thread t6;
        private Thread t7;
        private Thread t8;
        private Thread t9;
        private Thread t10;
        private User user = new User();
        private string path;

        private User GetUerInfo()
        {
            User user = new User();
            user.Name = textBox1.Text;
            user.Age = Convert.ToInt32(textBox2.Text);
            user.Tel = textBox3.Text;
            user.Sex = comboBox1.Text;
            user.Time = textBox4.Text + "年" + textBox5.Text + "月" + textBox6.Text + "日";
            return user;
        }

        private void PanelIsDisplay(int p)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel8.Visible = false;
            switch (p)
            {
                case 1:
                    {
                        panel1.Visible = true;
                    }
                    break;

                case 2:
                    {
                        panel2.Visible = true;
                    }
                    break;

                case 3:
                    {
                        panel3.Visible = true;
                    }
                    break;

                case 4:
                    {
                        panel4.Visible = true;
                    }
                    break;

                case 5:
                    {
                        panel5.Visible = true;
                    }
                    break;

                case 6:
                    {
                        panel6.Visible = true;
                    }
                    break;

                case 7:
                    {
                        panel7.Visible = true;
                    }
                    break;

                case 8:
                    {
                        panel8.Visible = true;
                    }
                    break;
            }
        }

        public Form1()
        {
            InitializeComponent();
            PanelIsDisplay(1);
            s.Play();
            panel1.BackgroundImage = Image.FromFile(Application.StartupPath + @"/resources/photos/main.jpg");
            panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        }

        private void setControls(float newx, float newy, Control cons)

        {
            //遍历窗体中的控件，重新设置控件的值

            foreach (Control con in cons.Controls)

            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });//获取控件的Tag属性值，并分割后存储字符串数组

                float a = Convert.ToSingle(mytag[0]) * newx;//根据窗体缩放比例确定控件的值，宽度

                con.Width = (int)a;//宽度

                a = Convert.ToSingle(mytag[1]) * newy;//高度

                con.Height = (int)(a);

                a = Convert.ToSingle(mytag[2]) * newx;//左边距离

                con.Left = (int)(a);

                a = Convert.ToSingle(mytag[3]) * newy;//上边缘距离

                con.Top = (int)(a);

                Single currentSize = Convert.ToSingle(mytag[4]) * newy;//字体大小

                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);

                if (con.Controls.Count > 0)

                {
                    setControls(newx, newy, con);
                }
            }
        }

        private void setTag(Control cons)

        {
            //遍历窗体中的控件

            foreach (Control con in cons.Controls)

            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;

                if (con.Controls.Count > 0)

                    setTag(con);
            }
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && comboBox1.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "")
            {
                user = GetUerInfo();
                Boolean iss = true;
                UserGateway userGateway = new UserGateway();
                List<User> Users = userGateway.SelectAllUser();
                if (Users != null)
                {
                    foreach (User users in Users)
                    {
                        string name = user.Name;
                        if (users.Name == name)
                        {
                            iss = false;
                            MessageBox.Show("姓名重复，请在姓名后添加序号");
                            textBox1.Text = "";
                        }
                    }
                }
                if (textBox1.Text != "")
                {
                    userGateway.InsertUser(user);
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/ep.png");
                    s.Stop();
                    s.SoundLocation = "resources/music/OperationGuide_music.wav";
                    s.Play();
                    PanelIsDisplay(3);
                }
            }
        }

        private int randomPoint()
        {
            Random rd = new Random();
            int x = rd.Next(1, 12);
            return x;
        }

        private static int[] RandArray(int[] arr)
        {
            int[] newarr = new int[arr.Length];
            int k = 0;
            while (k < arr.Length)
            {
                int temp = new Random().Next(0, arr.Length);
                if (arr[temp] != 0)
                {
                    newarr[k] = arr[temp];
                    k++;
                    arr[temp] = 0;
                }
            }
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(newarr[i]);
            }
            return newarr;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.NumPad1:
                    Input = 1;
                    break;

                case Keys.NumPad2:
                    Input = 2;
                    break;

                case Keys.NumPad3:
                    Input = 3;
                    break;

                case Keys.NumPad5:
                    Input = 5;
                    break;
            }
            if (Correct == Input && !isInput)
            {
                pictureBox14.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/y.png");
                pictureBox15.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/y.png");
                pictureBox28.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/y.png");
                endTime = System.Environment.TickCount;
                currentCount = endTime - startTime;
                label32.Text = "用时：" + currentCount;
                label33.Text = "用时：" + currentCount;
                label8.Text = "用时：" + currentCount;
                isInput = true;
                isRight = true;
                Thread.Sleep(200);
            }
            else if (Input != 0 && !isInput)
            {
                pictureBox14.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/n.png");
                pictureBox15.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/n.png");
                pictureBox28.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/n.png");

                endTime = System.Environment.TickCount;
                currentCount = endTime - startTime;
                label32.Text = "用时：" + currentCount;
                label33.Text = "用时：" + currentCount;
                label8.Text = "用时：" + currentCount;
                isInput = true;
                Thread.Sleep(200);
            }
        }

        private void test()
        {
            int[] arr = { 1, 1, 1, 2, 2, 2, 3, 3, 3, 5, 5, 5 };
            int[] arr1 = RandArray(arr);
            t2 = new Thread(() =>
            {
                Invoke(new Action(() =>
                {
                    label32.Text = "";
                    p = randomPoint();
                    pictureBox14.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/p" + p + ".png");
                    pictureBox2.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[0] + ".png");
                    pictureBox3.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[1] + ".png");
                    pictureBox4.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[2] + ".png");
                    pictureBox5.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[3] + ".png");
                    pictureBox6.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[4] + ".png");
                    pictureBox7.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[5] + ".png");
                    pictureBox8.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[6] + ".png");
                    pictureBox9.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[7] + ".png");
                    pictureBox10.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[8] + ".png");
                    pictureBox11.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[9] + ".png");
                    pictureBox12.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[10] + ".png");
                    pictureBox13.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[11] + ".png");
                    for (int i = 0; i < 12; i++)
                    {
                        Combination[i] = arr1[i];
                    }
                    Correct = arr1[p - 1];
                }));
                sameComnination = Combination;
                sp = p;
                Input = 0;
            });
            t2.IsBackground = true;
            t2.Start();
        }

        private void test2()
        {
            int[] arr = { 1, 1, 1, 2, 2, 2, 3, 3, 3, 5, 5, 5 };
            int[] arr1 = RandArray(arr);
            t7 = new Thread(() =>
            {
                Invoke(new Action(() =>
                {
                    pictureBox27.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox26.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox25.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox24.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox23.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox22.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox21.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox20.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox19.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox18.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox17.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox16.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                }));
            });
            t5 = new Thread(() =>
            {
                Invoke(new Action(() =>
                {
                    label33.Text = "";
                    p = randomPoint();
                    pictureBox15.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/p" + p + ".png");
                    pictureBox27.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[0] + ".png");
                    pictureBox26.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[1] + ".png");
                    pictureBox25.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[2] + ".png");
                    pictureBox24.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[3] + ".png");
                    pictureBox23.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[4] + ".png");
                    pictureBox22.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[5] + ".png");
                    pictureBox21.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[6] + ".png");
                    pictureBox20.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[7] + ".png");
                    pictureBox19.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[8] + ".png");
                    pictureBox18.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[9] + ".png");
                    pictureBox17.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[10] + ".png");
                    pictureBox16.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[11] + ".png");
                    for (int i = 0; i < 12; i++)
                    {
                        Combination[i] = arr1[i];
                    }
                    Correct = arr1[p - 1];
                }));
                Thread.Sleep(500);

                t7.IsBackground = true;
                t7.Start();
                sameComnination = Combination;
                sp = p;
                Input = 0;
            });

            t5.IsBackground = true;
            t5.Start();
        }

        private void test3()
        {
            int[] arr = { 1, 1, 1, 2, 2, 2, 3, 3, 3, 5, 5, 5 };
            int[] arr1 = RandArray(arr);

            t8 = new Thread(() =>
            {
                Invoke(new Action(() =>
                {
                    pictureBox40.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox39.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox38.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox37.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox36.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox35.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox34.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox33.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox32.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox31.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox30.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox29.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                }));
            });
            t9 = new Thread(() =>
            {
                Invoke(new Action(() =>
                {
                    label8.Text = "";
                    p = randomPoint();
                    pictureBox28.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/p" + p + ".png");
                }
                ));
                Thread.Sleep(28);
                //Invoke(new Action(() =>
                //{
                //    pictureBox28.Image = null;
                //}));

                Invoke(new Action(() =>
                {
                    //pictureBox28.Image = null;

                    pictureBox40.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[0] + ".png");
                    pictureBox39.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[1] + ".png");
                    pictureBox38.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[2] + ".png");
                    pictureBox37.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[3] + ".png");
                    pictureBox36.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[4] + ".png");
                    pictureBox35.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[5] + ".png");
                    pictureBox34.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[6] + ".png");
                    pictureBox33.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[7] + ".png");
                    pictureBox32.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[8] + ".png");
                    pictureBox31.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[9] + ".png");
                    pictureBox30.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[10] + ".png");
                    pictureBox29.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + arr1[11] + ".png");
                    for (int i = 0; i < 12; i++)
                    {
                        Combination[i] = arr1[i];
                    }
                    Correct = arr1[p - 1];
                }));
                Thread.Sleep(100);

                t8.IsBackground = true;
                t8.Start();
                sameComnination = Combination;
                sp = p;
                Input = 0;
            });

            t9.IsBackground = true;
            t9.Start();
        }

        private void testSame()
        {
            t2 = new Thread(() =>
            {
                Invoke(new Action(() =>
                {
                    label32.Text = "";
                    pictureBox14.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/p" + sp + ".png");
                    pictureBox2.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[0] + ".png");
                    pictureBox3.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[1] + ".png");
                    pictureBox4.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[2] + ".png");
                    pictureBox5.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[3] + ".png");
                    pictureBox6.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[4] + ".png");
                    pictureBox7.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[5] + ".png");
                    pictureBox8.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[6] + ".png");
                    pictureBox9.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[7] + ".png");
                    pictureBox10.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[8] + ".png");
                    pictureBox11.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[9] + ".png");
                    pictureBox12.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[10] + ".png");
                    pictureBox13.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[11] + ".png");
                }));
                Input = 0;
            });
            t2.IsBackground = true;
            t2.Start();
        }

        private void testSame2()
        {
            t7 = new Thread(() =>
            {
                Invoke(new Action(() =>
                {
                    pictureBox27.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox26.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox25.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox24.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox23.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox22.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox21.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox20.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox19.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox18.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox17.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                    pictureBox16.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/4.png");
                }));
            });
            t2 = new Thread(() =>
            {
                Invoke(new Action(() =>
                {
                    label33.Text = "";
                    pictureBox15.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/p" + sp + ".png");
                    pictureBox27.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[0] + ".png");
                    pictureBox26.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[1] + ".png");
                    pictureBox25.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[2] + ".png");
                    pictureBox24.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[3] + ".png");
                    pictureBox23.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[4] + ".png");
                    pictureBox22.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[5] + ".png");
                    pictureBox21.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[6] + ".png");
                    pictureBox20.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[7] + ".png");
                    pictureBox19.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[8] + ".png");
                    pictureBox18.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[9] + ".png");
                    pictureBox17.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[10] + ".png");
                    pictureBox16.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/" + sameComnination[11] + ".png");
                }));
                Input = 0;
                Thread.Sleep(500);
                t7.IsBackground = true;
                t7.Start();
            });

            t2.IsBackground = true;
            t2.Start();
        }

        private void Continue1_Click(object sender, EventArgs e)
        {
            s.Stop();
            s.SoundLocation = "resources/music/Continue1_music.wav";
            panel4.BackColor = Color.FromArgb(220, 220, 220);

            panel11.BackColor = Color.FromArgb(220, 220, 220);
            panel12.BackColor = Color.FromArgb(220, 220, 220);
            panel10.BackColor = Color.FromArgb(220, 220, 220);
            PanelIsDisplay(4);
            t4 = new Thread(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    test();
                    isInput = false;
                    isRight = false;
                    currentCount = 0;
                    startTime = System.Environment.TickCount;
                    Thread.Sleep(1000);
                    //isInput = true;

                    t2.Abort();

                    while (!isInput)
                    {
                    }
                    pictureBox2.Image = null;
                    pictureBox3.Image = null;
                    pictureBox4.Image = null;
                    pictureBox5.Image = null;
                    pictureBox6.Image = null;
                    pictureBox7.Image = null;
                    pictureBox8.Image = null;
                    pictureBox9.Image = null;
                    pictureBox10.Image = null;
                    pictureBox11.Image = null;
                    pictureBox12.Image = null;
                    pictureBox13.Image = null;
                    Thread.Sleep(2000);
                    while (!isRight)
                    {
                        pictureBox14.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/n.png");
                        Thread.Sleep(200);
                        testSame();
                        isInput = false;
                        startTime = System.Environment.TickCount;

                        while (!isInput)
                        {
                        }
                        pictureBox2.Image = null;
                        pictureBox3.Image = null;
                        pictureBox4.Image = null;
                        pictureBox5.Image = null;
                        pictureBox6.Image = null;
                        pictureBox7.Image = null;
                        pictureBox8.Image = null;
                        pictureBox9.Image = null;
                        pictureBox10.Image = null;
                        pictureBox11.Image = null;
                        pictureBox12.Image = null;
                        pictureBox13.Image = null;
                        Thread.Sleep(2000);
                    }

                    s.SoundLocation = "resources/music/di.wav";
                    if (i < 4) { s.Play(); }
                    Thread.Sleep(1000);
                }
                s.SoundLocation = "resources/music/Continue1_music.wav";
                s.Play();
            });
            t4.Start();
        }

        private void Continue2_Click(object sender, EventArgs e)
        {
            s.Stop();

            s.SoundLocation = "resources/music/Continue2_music.wav";
            t6 = new Thread(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    test2();
                    isInput = false;
                    isRight = false;
                    currentCount = 0;
                    startTime = System.Environment.TickCount;
                    Thread.Sleep(20);
                    while (!isInput)
                    {
                    }
                    pictureBox27.Image = null;
                    pictureBox26.Image = null;
                    pictureBox25.Image = null;
                    pictureBox24.Image = null;
                    pictureBox23.Image = null;
                    pictureBox22.Image = null;
                    pictureBox21.Image = null;
                    pictureBox20.Image = null;
                    pictureBox19.Image = null;
                    pictureBox18.Image = null;
                    pictureBox17.Image = null;
                    pictureBox16.Image = null;
                    Thread.Sleep(2000);

                    while (!isRight)
                    {
                        pictureBox14.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/n.png");
                        Thread.Sleep(200);
                        testSame2();
                        isInput = false;
                        startTime = System.Environment.TickCount;
                        while (!isInput)
                        {
                        }
                        pictureBox27.Image = null;
                        pictureBox26.Image = null;
                        pictureBox25.Image = null;
                        pictureBox24.Image = null;
                        pictureBox23.Image = null;
                        pictureBox22.Image = null;
                        pictureBox21.Image = null;
                        pictureBox20.Image = null;
                        pictureBox19.Image = null;
                        pictureBox18.Image = null;
                        pictureBox17.Image = null;
                        pictureBox16.Image = null;
                        Thread.Sleep(2000);
                    }
                    s.SoundLocation = "resources/music/di.wav";
                    if (i < 4) { s.Play(); }
                    Thread.Sleep(1000);
                    t2.Abort();
                    t7.Abort();
                }
                s.SoundLocation = "resources/music/Continue2_music.wav";
                s.Play();
            });
            t6.Start();
            t4.Abort();
            panel5.BackColor = Color.FromArgb(220, 220, 220);
            PanelIsDisplay(5);
        }

        private void Continue3_Click(object sender, EventArgs e)
        {
            s.Stop();
            s.SoundLocation = "resources/music/finish_music.wav";
            t6.Abort();
            t10 = new Thread(() =>
            {
                Boolean isco = true;
                int level = 0;
                int ts = 0;
                int tls = 0;
                int rs = 0;
                int fs = 0;
                while (isco)
                {
                    ISI = ISIS[level];

                    //测试开始
                    //Thread.Sleep(ISI);
                    s.SoundLocation = "resources/music/di.wav";
                    s.Play();
                    Thread.Sleep(1000);
                    Invoke(new Action(() =>
                    {
                        label8.Text = "";
                    }));
                    isInput = false;
                    isRight = false;
                    currentCount = 0;
                    startTime = System.Environment.TickCount;
                    test3();
                    if (isRight)
                    {
                        rs++;
                    }
                    else
                        fs++;

                    if (rs == 6 && fs == 0)
                    {
                        level++;
                        tls = 0;
                    }
                    if (tls == 19 && rs >= 10)
                    {
                        level++;
                        tls = 0;
                    }
                    if (ISI == 56 || (tls == 19 && rs < 10))
                    {
                        isco = false;
                    }

                    while (!isInput) { }
                    pictureBox40.Image = null;
                    pictureBox39.Image = null;
                    pictureBox38.Image = null;
                    pictureBox37.Image = null;
                    pictureBox36.Image = null;
                    pictureBox35.Image = null;
                    pictureBox34.Image = null;
                    pictureBox33.Image = null;
                    pictureBox32.Image = null;
                    pictureBox31.Image = null;
                    pictureBox30.Image = null;
                    pictureBox29.Image = null;
                    Thread.Sleep(ISI);
                    ps[ts] = sp;
                    Corrects[ts] = Correct;
                    Inputs[ts] = Input;
                    realISIs[ts] = ISI;
                    Combinations[ts] = (sameComnination[0].ToString() + sameComnination[1].ToString() + sameComnination[2].ToString() + sameComnination[3].ToString()
                        + sameComnination[4].ToString() + sameComnination[5].ToString() + sameComnination[6].ToString() + sameComnination[7].ToString() + sameComnination[8].ToString()
                        + sameComnination[9].ToString() + sameComnination[10].ToString() + sameComnination[11].ToString());
                    Invoke(new Action(() =>
                    {
                        if (label8.Text == "")
                            RT[ts] = 0;
                        else
                            RT[ts] = Convert.ToInt32(label8.Text.Replace("用时：", "").Trim());
                    }));
                    ts++;
                    tls++;
                }
                s.SoundLocation = "resources/music/finish_music.wav";
                s.Play();
                Invoke(new Action(() =>
                {
                    PanelIsDisplay(7);
                }
                ));
            });

            t10.Start();
            panel6.BackColor = Color.FromArgb(220, 220, 220);
            PanelIsDisplay(6);
        }

        private Questionnaire GetQuestionnaire()
        {
            Questionnaire questionnaire = new Questionnaire();
            questionnaire.psychiatricHistory = checkedListBox1.Text;
            questionnaire.Drink = checkedListBox2.Text;
            questionnaire.Insomnia = checkedListBox3.Text;
            questionnaire.Mood = checkedListBox4.Text;
            questionnaire.computerGame = checkedListBox5.Text;
            questionnaire.Exercise = checkedListBox6.Text;
            questionnaire.Driving = Convert.ToInt32(textBox7.Text);
            questionnaire.Accident = Convert.ToInt32(textBox8.Text);
            questionnaire.Others = richTextBox1.Text;
            questionnaire.Name = user.Name;
            return questionnaire;
        }

        private void Submit2_Click(object sender, EventArgs e)
        {
            Questionnaire questionnaire = GetQuestionnaire();
            QuestionnaireGateway questionnaireGateway = new QuestionnaireGateway();
            questionnaireGateway.InsertQuestionnaire(questionnaire);
            int i = 0;
            while (realISIs[i] != 0)
            {
                Result result = new Result();
                result.Num = i + 1;
                result.Name = user.Name;
                result.ISI = realISIs[i];
                result.Combination = Combinations[i];
                result.P = ps[i];
                result.Correct = Corrects[i];
                result.Input = Inputs[i];
                result.RT = RT[i];
                ResultGateway resultGateway = new ResultGateway();
                resultGateway.InsertResult(result);
                i++;
            }
            exportTxt txt = new exportTxt();
            ResultGateway resultsss = new ResultGateway();
            List<Result> results = resultsss.SelectAllResultByName(user.Name);
            txt.txt(user, results, path);
            Application.Exit();
            Application.Exit();
        }

        private void skip_Click(object sender, EventArgs e)
        {
            int i = 0;
            while (realISIs[i] != 0)
            {
                Result result = new Result();
                result.Num = i + 1;
                result.Name = user.Name;
                result.ISI = realISIs[i];
                result.Combination = Combinations[i];
                result.P = ps[i];
                result.Correct = Corrects[i];
                result.Input = Inputs[i];
                result.RT = RT[i];
                ResultGateway resultGateway = new ResultGateway();
                resultGateway.InsertResult(result);
                i++;
            }
            exportTxt txt = new exportTxt();
            ResultGateway resultsss = new ResultGateway();
            List<Result> results = resultsss.SelectAllResultByName(user.Name);
            txt.txt(user, results, path);
            Application.Exit();
        }

        private void start_Click_1(object sender, EventArgs e)
        {
            PanelIsDisplay(8);
            s.Stop();
            s.SoundLocation = "resources/music/bgm.wav";
            s.Play();
            ship2.Visible = false;
            t1 = new Thread(() =>
            {
                Thread.Sleep(60000);
                Invoke(new Action(() =>
                {
                    ship2.Visible = true;
                }));
            });
            t1.IsBackground = true;
            t1.Start();
            t2 = new Thread(() =>
            {
                for (int i = 0; i < 40; i++)
                {
                    for (int j = 1; j < 7; j++)
                    {
                        Invoke(new Action(() =>
                        {
                            panel8.BackgroundImage = Image.FromFile(Application.StartupPath + @"/resources/photos/bg" + j + ".jpg");
                            panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                        }));

                        Thread.Sleep(5000);
                    }
                }
            });
            t2.IsBackground = true;
            t2.Start();
        }

        private void ship2_Click(object sender, EventArgs e)
        {
            t1.Abort();
            t2.Abort();
            s.Stop();
            s.SoundLocation = "resources/music/info_music.wav";
            s.Play();
            PanelIsDisplay(2);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
        }
        private void 历史记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void 文件保存地址ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog paths = new FolderBrowserDialog();
            paths.ShowDialog();
            this.path = paths.SelectedPath;
        }

        private void 数据库设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}