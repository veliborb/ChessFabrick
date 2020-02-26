using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ChessFabrickFormsApp
{
    public partial class ConnectionForm : Form
    {
        private static readonly int BUFFER_SIZE = 128;

        private delegate void SafeCallDelegate(string text);

        private string HostName => txbHost.Text;
        private string ProxyName => txbPort.Text;
        private string Message => txbOutbox.Text;

        private HubConnection connection;

        public ConnectionForm()
        {
            InitializeComponent();
            txbOutbox.MaxLength = BUFFER_SIZE;
        }

        private void AppendMessage(string text)
        {
            if (txbInbox.InvokeRequired)
            {
                var d = new SafeCallDelegate(AppendMessage);
                txbInbox.Invoke(d, new object[] { text });
            }
            else
            {
                txbInbox.Text += $"\r\n{text}";
            }
        }

        private void SetStatus(string text)
        {
            if (labStatus.InvokeRequired)
            {
                var d = new SafeCallDelegate(SetStatus);
                labStatus.Invoke(d, new object[] { text });
            }
            else
            {
                labStatus.Text = text;
            }
        }

        private void Connect()
        {
            connection = new HubConnectionBuilder().WithUrl(HostName).Build();
            connection.On<string, string>("ReceiveMessage", (name, message) => { AppendMessage($"{name}: {message}"); });
            OpenConnection();
        }

        private async void OpenConnection()
        {
            await connection.StartAsync();
        }

        private async void SendMesage()
        {
            await connection.SendAsync("SendMessage", "WinForms", Message);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMesage();
        }
    }
}
