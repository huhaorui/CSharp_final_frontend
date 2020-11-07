﻿using System;
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
            timer1.Stop();
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
            if (Win(result.Split(":")[1]) == Global.seat)
            {
                MessageBox.Show("你赢了");
                Global.GameBegin = false;
                button_ready.Visible = true;
            }
            else if (Win(result.Split(":")[1]) == 3 - Global.seat)
            {
                MessageBox.Show("你输了");
                Global.GameBegin = false;
                button_ready.Visible = true;
            }
            else if (Win(result.Split(":")[1]) == 3)
            {
                MessageBox.Show("平局");
                Global.GameBegin = false;
                button_ready.Visible = true;

            }
            timer1.Start();


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
        private bool WinForSomeOne(string status, char number)
        {
            for (var i = 0; i < 3; i++)
            {
                if (status[i] == number && status[i + 3] == number && status[i + 6] == number)
                {
                    return true;
                }
            }

            for (var i = 0; i < 3; i++)
            {
                if (status[3 * i] == number && status[3 * i + 1] == number && status[3 * i + 2] == number)
                {
                    return true;
                }
            }

            if (status[0] == number && status[4] == number && status[8] == number)
            {
                return true;
            }

            if (status[2] == number && status[4] == number && status[6] == number)
            {
                return true;
            }

            return false;
        }

        private int Win(string status)
        {
            if (WinForSomeOne(status, '1'))
            {
                return 1;
            }
            else if (WinForSomeOne(status, '2'))
            {
                return 2;
            }
            else if (status.IndexOf("0") == -1)
            {
                return 3;
            }
            else
            {
                return 0;
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
                button_standup.Enabled = true;
                button_ready.Text = "准备";
            }
            else
            {
                button_standup.Enabled = false;
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
            else if (!Global.GameBegin)
            {
                Global.GameBegin = true;
                MessageBox.Show("游戏开始");
                button_ready.Visible = false;
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
                return;
            }
            label_desknumber.Text = result.Split(":")[0];
            ShowStatus(result.Split(":")[1]);
            judge(result.Split(":")[2]);
            ready(result.Split(":")[3]);
            timer1.Interval = 300;
        }

        private async void button_ready_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            string url = Url.Header + Url.ready;
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("uid", Global.uid),
                new KeyValuePair<string, string>("password", Global.password),
                new KeyValuePair<string, string>("attribute", button_ready.Text == "准备" ? "ready" : "unready")
            };
            var response = await httpClient.PostAsync(new Uri(url), new FormUrlEncodedContent(parameters));
            var result = await response.Content.ReadAsStringAsync();
            button_ready.Text = button_ready.Text == "准备" ? "取消" : "准备";
        }
        private async void Press(int key)
        {
            HttpClient httpClient = new HttpClient();
            string url = Url.Header + Url.press;
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("uid", Global.uid),
                new KeyValuePair<string, string>("password", Global.password),
                new KeyValuePair<string, string>("key", key.ToString())
            };
            var response = await httpClient.PostAsync(new Uri(url), new FormUrlEncodedContent(parameters));
            var result = await response.Content.ReadAsStringAsync();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Press(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Press(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Press(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Press(3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Press(4);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Press(5);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Press(6);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Press(7);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Press(8);
        }

        private async void button_standup_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            string url = Url.Header + Url.EnterDesk;
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("uid", Global.uid));
            parameters.Add(new KeyValuePair<string, string>("password", Global.password));
            parameters.Add(new KeyValuePair<string, string>("seat", Global.seat.ToString()));

            parameters.Add(new KeyValuePair<string, string>("attribute", "standup"));

            var response = await httpClient.PostAsync(new Uri(url), new FormUrlEncodedContent(parameters));
            var result = await response.Content.ReadAsStringAsync();
            if (result.Equals("OK"))
            {

                FormFindDesk formDesk = new FormFindDesk();
                formDesk.Show();
                this.Dispose();
            }
        }


        private void FormDesk_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
