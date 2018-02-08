using AttentionTransferSpeedTest.DAL.DBO;
using System;
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
        private int countTime;
        //定义全局变量
        public int currentCount = 0;
        private Thread t4;
        private Thread t5;
        private Thread t6;
        private Thread t7;
        private Thread t8;

        private User GetUerInfo()
        {
            User user = new User();
            user.Name = textBox1.Text;
            user.Age = Convert.ToInt32(textBox2.Text);
            user.Tel = Convert.ToInt32(textBox3.Text);
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
            pictureBox1.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/ep.png");
            s.Stop();
            s.SoundLocation = "resources/music/OperationGuide_music.wav";
            s.Play();
            PanelIsDisplay(3);
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
                endTime = System.Environment.TickCount;
                currentCount = endTime - startTime;
                label32.Text = "用时：" + currentCount;
                label33.Text = "用时：" + currentCount;
                isInput = true;
                isRight = true;
                Thread.Sleep(200);
                
            }
            else if (Input != 0 && !isInput)
            {
                pictureBox14.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/n.png");
                pictureBox15.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/n.png");
                endTime = System.Environment.TickCount;
                currentCount = endTime - startTime;
                label32.Text = "用时：" + currentCount;
                label33.Text = "用时：" + currentCount;
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
                    pictureBox10.Image = Image.FromFile(Application.StartupPath +@"/resources/photos/" + sameComnination[8] + ".png");
                    pictureBox11.Image = Image.FromFile(Application.StartupPath +@"/resources/photos/" + sameComnination[9] + ".png");
                    pictureBox12.Image = Image.FromFile(Application.StartupPath +@"/resources/photos/" + sameComnination[10] + ".png");
                    pictureBox13.Image = Image.FromFile(Application.StartupPath +@"/resources/photos/" + sameComnination[11] + ".png");
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
                    isInput = true;

                    t2.Abort();
                    //label32.Text = "";
                    
                    //pictureBox14.Image = null;
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
                        Thread.Sleep(1000);
                        isInput = true;

                        //pictureBox14.Image = null;
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
                for (int i = 0; i < 10; i++)
                {
                    test2();
                    isInput = false;
                    isRight = false;
                    currentCount = 0;
                    startTime = System.Environment.TickCount;
                    Thread.Sleep(1000);
                    isInput = true;
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
                        Thread.Sleep(1000);
                        isInput = true;
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
            s.Play();
            t6.Abort();
            panel6.BackColor = Color.FromArgb(220, 220, 220);
            PanelIsDisplay(6);
        }

        private void Submit2_Click(object sender, EventArgs e)
        {
        }

        private void skip_Click(object sender, EventArgs e)
        {
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
                Thread.Sleep(6000);
                Invoke(new Action(() =>
                {
                    ship2.Visible = true;
                }));
            });
            t1.IsBackground = true;
            t1.Start();
            t2 = new Thread(() =>
            {
                for (int i = 0; i < 10000; i++)
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
    }
}