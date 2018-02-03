using AttentionTransferSpeedTest;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AttentionTransferSpeedTest
{
    public partial class Form1 : Form
    {
        private int X;
        private int Y;
        private AutoSizeFormClass asc = new AutoSizeFormClass();
        System.Media.SoundPlayer s = new System.Media.SoundPlayer("resources/music/start_music.wav");
        private void PanelIsDisplay(int p)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
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
            }
        }
        public Form1()
        {
            InitializeComponent();
            PanelIsDisplay(1);
           
            s.Play();
            this.Load += new EventHandler(Form1_Load);
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

        private void Form1_Load(object sender, EventArgs e)

        {
            this.Resize += new EventHandler(Form1_Resize);//窗体调整大小时引发事件

            X = this.Width;//获取窗体的宽度

            Y = this.Height;//获取窗体的高度

            setTag(this);//调用方法
        }

        private void Form1_Resize(object sender, EventArgs e)

        {
            float newx = (this.Width) / X; //窗体宽度缩放比例

            float newy = this.Height / Y;//窗体高度缩放比例

            setControls(newx, newy, this);//随窗体改变控件大小

            this.Text = this.Width.ToString() + " " + this.Height.ToString();//窗体标题栏文本

            start.Left = (this.Width - start.Width) / 2;

            label1.Left = (this.Width - label1.Width) / 2;
            label2.Left = (this.Width - label2.Width) / 2;

            label1.Top = Convert.ToInt32(this.Top + this.Height * 0.2);

            label2.Top = Convert.ToInt32(label2.Top + 10);

            start.Top = Convert.ToInt32(label2.Top + this.Height * 0.2);
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            PanelIsDisplay(3);
        }

        private void Continue1_Click(object sender, EventArgs e)
        {
            PanelIsDisplay(4);
        }

        private void Continue2_Click(object sender, EventArgs e)
        {
            PanelIsDisplay(5);
        }

        private void Continue3_Click(object sender, EventArgs e)
        {
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
            PanelIsDisplay(2);
            s.Stop();
            s.SoundLocation = "resources/music/info_music.wav";
            s.Play();
        }
    }
}