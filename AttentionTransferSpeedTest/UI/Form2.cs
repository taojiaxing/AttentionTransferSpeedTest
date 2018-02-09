using AttentionTransferSpeedTest.BLL;
using AttentionTransferSpeedTest.DAL.DBO;
using AttentionTransferSpeedTest.DAL.Gateway;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AttentionTransferSpeedTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = DataBindingByList1();
        }
        private ArrayList DataBindingByList1()
        {
            ArrayList Al = new ArrayList();
            UserGateway userGateway = new UserGateway();
            List<User> users = userGateway.SelectAllUser();
            foreach(User user in users)
            {
                Al.Add(user);
            }
            return Al;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private string path;
        private void button1_Click(object sender, EventArgs e)
        {
            UserGateway userGateway = new UserGateway();
            User user = userGateway.SelectUserByName(this.textBox1.Text.Trim());
            exportTxt txt = new exportTxt();
            ResultGateway resultsss = new ResultGateway();
            List<Result> results = resultsss.SelectAllResultByName(user.Name);
            txt.txt(user, results,"");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog paths = new FolderBrowserDialog();
            paths.ShowDialog();
            this.path = paths.SelectedPath;
        }
    }
}
