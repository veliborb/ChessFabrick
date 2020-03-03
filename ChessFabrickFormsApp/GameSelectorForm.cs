using ChessCommons;
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
    public partial class GameSelectorForm : Form
    {
        private UserModel user;
        private Uri host;

        private HttpClient client;

        public GameSelectorForm(UserModel user, Uri host)
        {
            InitializeComponent();
            this.user = user;
            this.host = host;
            InitHttpClient();
        }

        private void InitHttpClient()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = host;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
        }

        private void GameSelectorForm_Load(object sender, EventArgs e)
        {
            btnRefresh_Click(sender, e);
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                var games = await GetNewGamesAsync();
                lbNewGames.Items.Clear();
                lbNewGames.Items.AddRange(games.ToArray());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            try
            {
                var games = await GetActiveGamesAsync();
                lbActiveGames.Items.Clear();
                lbActiveGames.Items.AddRange(games.ToArray());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }

        private async void btnJoin_Click(object sender, EventArgs e)
        {
            try
            {
                var game = await PostJoinGameAsync(txbGameId.Text);
                new ChessOnlineForm(user, host, game).Show();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }

        private async void btnSpectate_Click(object sender, EventArgs e)
        {
            try
            {
                var game = await GetGameStateAsync(lbActiveGames.SelectedItem.ToString());
                new ChessOnlineForm(user, host, game).Show();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var game = await PostNewGameAsync(new NewGameModel
                {
                    PlayerColor = rdbWhite.Checked ? PieceColor.White : PieceColor.Black
                });
                new ChessOnlineForm(user, host, game).Show();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }

        private void txbGameId_TextChanged(object sender, EventArgs e)
        {
            btnJoin.Enabled = txbGameId.Text.Length > 0;
        }

        private void lbNewGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbGameId.Text = lbNewGames.SelectedItem?.ToString();
        }

        private void lbActiveGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSpectate.Enabled = lbActiveGames.SelectedItem != null;
        }

        private async Task<List<string>> GetNewGamesAsync()
        {
            HttpResponseMessage response = await client.GetAsync("api/chess/new");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            return JsonConvert.DeserializeObject<List<string>>(result);
        }

        private async Task<List<string>> GetActiveGamesAsync()
        {
            HttpResponseMessage response = await client.GetAsync("api/chess/game");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            return JsonConvert.DeserializeObject<List<string>>(result);
        }

        private async Task<ChessGameState> GetGameStateAsync(string gameId)
        {
            HttpResponseMessage response = await client.GetAsync($"api/chess/game/{gameId}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            return JsonConvert.DeserializeObject<ChessGameState>(result);
        }

        private async Task<ChessGameInfo> PostNewGameAsync(NewGameModel model)
        {
            HttpResponseMessage response = await client.PostAsync("api/chess/new",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            return JsonConvert.DeserializeObject<ChessGameInfo>(result);
        }

        private async Task<ChessGameState> PostJoinGameAsync(string gameId)
        {
            HttpResponseMessage response = await client.PostAsync($"api/chess/game/{gameId}/join", null);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            return JsonConvert.DeserializeObject<ChessGameState>(result);
        }
    }
}
