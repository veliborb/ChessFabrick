using ChessFabrickCommons.Entities;
using ChessFabrickCommons.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessFabrickFormsApp
{
    public partial class AuthForm : Form
    {
        private HttpClient client;

        public AuthForm()
        {
            InitializeComponent();
            Text = "Chess Authentication";
            Icon = Icon.FromHandle(Properties.Resources.rook_black.GetHicon());
        }

        private void InitHttpClient(string host)
        {
            client?.Dispose();
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(host);
        }

        private void SetEnabled(bool enabled)
        {
            btnLogin.Enabled = enabled;
            btnRegister.Enabled = enabled;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            SetEnabled(false);

            try
            {
                labStatus.Text = "Logging in...";
                InitHttpClient(txbHost.Text);
                var user = await Authenticate("api/player/authenticate",
                    new AuthenticationModel { Name = txbName.Text, Password = txbPassword.Text }
                );
                labStatus.Text = $"Logged in as: {user.Player?.Name}";
                ShowGameSelectorForm(user);
            } catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                labStatus.Text = ex.Message;
            }

            SetEnabled(true);
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            SetEnabled(false);

            try
            {
                labStatus.Text = "Creating new player...";
                InitHttpClient(txbHost.Text);
                var user = await Authenticate("api/player/new",
                    new AuthenticationModel { Name = txbName.Text, Password = txbPassword.Text }
                );
                labStatus.Text = $"Created: {user.Player?.Name}";
                ShowGameSelectorForm(user);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                labStatus.Text = ex.Message;
            }

            SetEnabled(true);
        }

        private async Task<UserModel> Authenticate(string url, AuthenticationModel model)
        {
            HttpResponseMessage response = await client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")
            );
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}\n{result}");
            }
            return JsonConvert.DeserializeObject<UserModel>(result);
        }

        private void ShowGameSelectorForm(UserModel user)
        {
            var gameSelectorForm = new GameSelectorForm(user, client.BaseAddress);
            gameSelectorForm.FormClosed += (sender, e) => Show();
            Hide();
            gameSelectorForm.Show();
        }

        private void txbName_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = !(
            //    (e.KeyValue >= 'a' && e.KeyValue <= 'z') ||
            //    (e.KeyValue >= 'A' && e.KeyValue <= 'Z') ||
            //    (e.KeyValue >= '0' && e.KeyValue <= '9') ||
            //    e.KeyValue == '_');
        }

        private void txbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = !(
            //    (e.KeyValue >= 'a' && e.KeyValue <= 'z') ||
            //    (e.KeyValue >= 'A' && e.KeyValue <= 'Z') ||
            //    (e.KeyValue >= '0' && e.KeyValue <= '9') ||
            //    e.KeyValue == '_');
        }
    }
}
