using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frontend
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label_password_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void button_submit_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            String url = Url.Header + Url.LoginUrl;
            String username = textBox_uid.Text;
            String password = textBox_password.Text;
            List<KeyValuePair<string, string>> paramList = new List<KeyValuePair<string, string>>();
            paramList.Add(new KeyValuePair<string, string>("uid", username));
            paramList.Add(new KeyValuePair<string, string>("password", password));
            var response = httpClient.PostAsync(new Uri(url), new FormUrlEncodedContent(paramList)).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (result.Equals("OK"))
            {
                Global.uid = textBox_uid.Text;
                Global.password = textBox_password.Text;
                this.Hide();
                var formFindDesk = new FormFindDesk();
              
                formFindDesk.Show();
            }
            httpClient.Dispose();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}