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
            this.user = user;
            this.host = host;

            InitializeComponent();
            InitHttpClient();

            Icon = Icon.FromHandle(Properties.Resources.rook_white.GetHicon());
            Text = $"Game Selector - {user.Player.Name}";
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
                var games = await GetPlayerGamesAsync();
                lbYourGames.Items.Clear();
                lbYourGames.Items.AddRange(games.ToArray());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                txbMessages.Text = ex.Message;
            }
            try
            {
                var games = await GetNewGamesAsync();
                lbNewGames.Items.Clear();
                lbNewGames.Items.AddRange(games.ToArray());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                txbMessages.Text = ex.Message;
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
                txbMessages.Text = ex.Message;
            }
        }

        private async void btnJoin_Click(object sender, EventArgs e)
        {
            try
            {
                var game = await PostJoinGameAsync(txbGameId.Text);
                new ChessOnlineForm(user, host, new ChessGameState(game)).Show();
            }
            catch (Exception ex)
            {
                txbMessages.Text = ex.Message;
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
                txbMessages.Text = ex.Message;
            }
        }

        private async void btnRejoin_Click(object sender, EventArgs e)
        {
            try
            {
                var game = await GetGameStateAsync(lbYourGames.SelectedItem.ToString());
                if (game.GameInfo.White == null || game.GameInfo.Black == null)
                {
                    new ChessOnlineForm(user, host, game.GameInfo).Show();
                } else
                {
                    new ChessOnlineForm(user, host, game).Show();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                txbMessages.Text = ex.Message;
            }
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var game = rdbBots.Checked ?
                    await PostBotGameAsync() :
                    await PostNewGameAsync(new NewGameModel
                    {
                        PlayerColor = rdbWhite.Checked ? PieceColor.White : PieceColor.Black
                    });
                new ChessOnlineForm(user, host, game).Show();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                txbMessages.Text = ex.Message;
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

        private void lbYourGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRejoin.Enabled = lbYourGames.SelectedItem != null;
        }

        private async Task<List<string>> GetNewGamesAsync()
        {
            HttpResponseMessage response = await client.GetAsync("api/game/new");
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}\n{result}");
            }
            return JsonConvert.DeserializeObject<List<string>>(result);
        }

        private async Task<List<string>> GetActiveGamesAsync()
        {
            HttpResponseMessage response = await client.GetAsync("api/game");
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}\n{result}");
            }
            return JsonConvert.DeserializeObject<List<string>>(result);
        }

        private async Task<List<string>> GetPlayerGamesAsync()
        {
            HttpResponseMessage response = await client.GetAsync("api/game/my");
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}\n{result}");
            }
            return JsonConvert.DeserializeObject<List<string>>(result);
        }

        private async Task<ChessGameState> GetGameStateAsync(string gameId)
        {
            HttpResponseMessage response = await client.GetAsync($"api/game/{gameId}");
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}\n{result}");
            }
            return JsonConvert.DeserializeObject<ChessGameState>(result);
        }

        private async Task<ChessGameInfo> PostNewGameAsync(NewGameModel model)
        {
            HttpResponseMessage response = await client.PostAsync("api/game/new",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}\n{result}");
            }
            return JsonConvert.DeserializeObject<ChessGameInfo>(result);
        }

        private async Task<ChessGameInfo> PostBotGameAsync()
        {
            HttpResponseMessage response = await client.PostAsync("api/game/new/addbots", null);
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}\n{result}");
            }
            return JsonConvert.DeserializeObject<ChessGameInfo>(result);
        }

        private async Task<ChessGameInfo> PostJoinGameAsync(string gameId)
        {
            HttpResponseMessage response = await client.PostAsync($"api/game/new/{gameId}/join", null);
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}\n{result}");
            }
            return JsonConvert.DeserializeObject<ChessGameInfo>(result);
        }

        private void bthHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Column 'New Games' shows games that are not yet started. Select a Game ID and click on 'Join' to join the selected game.\n\n" +
                "Column 'Active Games' shows games that are in progress. Select a Game ID and click on 'Spectate' to spectate the selected game.\n\n" +
                "Column 'My Games' shows all your games. Select a Game ID and click on 'Rejoin' to rejoin the selected game.\n\n" +
                "To create a new game, in 'Create game' box select the color you wish to play as and click on the button 'Create'\n\n" +
                "Clicking the 'Refresh' button will refresh the game lists.\n\n" +
                "Clicking the 'Help' button will bring up this dialog again.",
                "Help me please...",
                MessageBoxButtons.OK,
                MessageBoxIcon.Question
                );
        }
    }
}
