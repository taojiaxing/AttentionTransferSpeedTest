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

        //定义全局变量
        public int currentCount = 0;


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
            PanelIsDisplay(3);
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

        private void SetTextBoxText(string strValue)
        {
            //this.time = currentCount;
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
            if (Correct == Input&& !isInput)
            {
                pictureBox14.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/y.png");
                endTime = System.Environment.TickCount;
                currentCount = endTime - startTime;
                label32.Text = "用时：" + currentCount;
                isInput = true;
            }
            else if (Input != 0&&!isInput)
            {
                pictureBox14.Image = Image.FromFile(Application.StartupPath + @"/resources/photos/n.png");
                endTime = System.Environment.TickCount;
                currentCount = endTime - startTime;
                label32.Text = "用时：" + currentCount;
                isInput = true;
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
                    int p = randomPoint();
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
                Input = 0;
            });
            t2.IsBackground = true;
            t2.Start();
        }

        private void Continue1_Click(object sender, EventArgs e)
        {
            s.Stop();
            s.SoundLocation = "resources/music/Continue1_music.wav";
            panel4.BackColor = Color.FromArgb(220, 220, 220);
            panel10.BackColor = Color.FromArgb(220, 220, 220);
            PanelIsDisplay(4);
            Thread t4 = new Thread(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    
                    test();
                    isInput = false;
                    currentCount = 0;
                    startTime = System.Environment.TickCount;
                    Thread.Sleep(3000);
                    t2.Abort();
                }
            });
            t4.Start();
            s.Play();
        }

        private void Continue2_Click(object sender, EventArgs e)
        {
            s.Stop();
            s.SoundLocation = "resources/music/Continue2_music.wav";
            s.Play();
            PanelIsDisplay(5);
        }

        private void Continue3_Click(object sender, EventArgs e)
        {
            s.Stop();
            s.SoundLocation = "resources/music/finish_music.wav";
            s.Play();
            PanelIsDisplay(7);
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