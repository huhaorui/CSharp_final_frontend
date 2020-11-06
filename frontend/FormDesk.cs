using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace frontend
{
    public partial class FormDesk : Form
    {
        public FormDesk()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            string url = Url.Header + Url.status;
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("uid", Global.uid));
            parameters.Add(new KeyValuePair<string, string>("password", Global.password));
            var response = await httpClient.PostAsync(new Uri(url), new FormUrlEncodedContent(parameters));
            var result = await response.Content.ReadAsStringAsync();
            if (result.Equals("NG"))
            {
                MessageBox.Show("未找到游戏");
                return;
            }
            label_desknumber.Text = result.Split(":")[0];
            ShowStatus(result.Split(":")[1]);
            judge(result.Split(":")[2]);
            ready(result.Split(":")[3]);
        }
        private void SetStatus(Button button, char status)
        {
            if (status != '0')
            {
                button.Text = status.ToString();
                button.Enabled = false;
            }
            else
            {
                button.Enabled = true;
                button.Text = "";
            }


        }
        private void ShowStatus(string status)
        {
            SetStatus(button1, status[0]);
            SetStatus(button2, status[1]);
            SetStatus(button3, status[2]);
            SetStatus(button4, status[3]);
            SetStatus(button5, status[4]);
            SetStatus(button6, status[5]);
            SetStatus(button7, status[6]);
            SetStatus(button8, status[7]);
            SetStatus(button9, status[8]);

        }
        private void judge(string status)
        {
            if (int.Parse(status) != Global.seat)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
            }
        }
        private void ready(string status)
        {
            if (status[Global.seat - 1] == '0')
            {
                button_ready.Text = "准备";
            }
            else
            {
                button_ready.Text = "取消";
            }
            if (!status.Equals("11"))
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
            }
        }
        private async void FormDesk_Load(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            string url = Url.Header + Url.status;
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("uid", Global.uid));
            parameters.Add(new KeyValuePair<string, string>("password", Global.password));
            var response = await httpClient.PostAsync(new Uri(url), new FormUrlEncodedContent(parameters));
            var result = await response.Content.ReadAsStringAsync();
            if (result.Equals("NG"))
            {
                MessageBox.Show("未找到游戏");
                return;
            }
            label_desknumber.Text = result.Split(":")[0];
            ShowStatus(result.Split(":")[1]);
            judge(result.Split(":")[2]);
            ready(result.Split(":")[3]);
        }

        private async void button_ready_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            string url = Url.Header + Url.ready;
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("uid", Global.uid));
            parameters.Add(new KeyValuePair<string, string>("password", Global.password));
            parameters.Add(new KeyValuePair<string, string>("attribute", button_ready.Text == "准备" ? "ready" : "unready"));
            var response = await httpClient.PostAsync(new Uri(url), new FormUrlEncodedContent(parameters));
            var result = await response.Content.ReadAsStringAsync();
            button_ready.Text = button_ready.Text == "准备" ? "取消" : "准备";
        }
    }
}
