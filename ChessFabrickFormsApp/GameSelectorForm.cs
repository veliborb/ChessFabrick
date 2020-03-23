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
            tabControl.Enabled = false;
            btnRefresh.Enabled = false;
            btnJoin.Enabled = false;

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
            btnSearch_Click(sender, e);

            tabControl.Enabled = true;
            btnRefresh.Enabled = true;
            tabControl_SelectedIndexChanged(sender, e);
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            btnCreate.Enabled = false;

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

            btnCreate.Enabled = true;
        }

        private async void btnJoin_Click(object sender, EventArgs e)
        {
            btnJoin.Enabled = false;

            ChessGameInfo gameInfo = null;
            try
            {
                if (tabControl.SelectedTab == tabNewGames)
                {
                    gameInfo = await PostJoinGameAsync(lbNewGames.SelectedItem.ToString());
                }
                else if (tabControl.SelectedTab == tabActiveGames)
                {
                    var game = await GetGameStateAsync(lbActiveGames.SelectedItem.ToString());
                    gameInfo = game.GameInfo;
                }
                else if (tabControl.SelectedTab == tabYourGames)
                {
                    var game = await GetGameStateAsync(lbYourGames.SelectedItem.ToString());
                    gameInfo = game.GameInfo;
                }
                else if (tabControl.SelectedTab == tabSearchedGames)
                {
                    var game = await GetGameStateAsync(lbSearchedGames.SelectedItem.ToString());
                    gameInfo = game.GameInfo;
                    if ((gameInfo.Black == null && gameInfo.White?.Name != user.Player.Name) ||
                        (gameInfo.White == null && gameInfo.Black?.Name != user.Player.Name))
                    {
                        gameInfo = await PostJoinGameAsync(gameInfo.GameId);
                    }
                }
                new ChessOnlineForm(user, host, gameInfo).Show();
            } catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                txbMessages.Text = ex.Message;
            }

            btnJoin.Enabled = true;
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabNewGames)
            {
                btnJoin.Text = "Join";
                lbNewGames_SelectedIndexChanged(sender, e);
            }
            else if (tabControl.SelectedTab == tabActiveGames)
            {
                btnJoin.Text = "Spectate";
                lbActiveGames_SelectedIndexChanged(sender, e);
            }
            else if (tabControl.SelectedTab == tabYourGames)
            {
                btnJoin.Text = "Rejoin";
                lbYourGames_SelectedIndexChanged(sender, e);
            }
            else if (tabControl.SelectedTab == tabSearchedGames)
            {
                btnJoin.Text = "Join";
                lbSearch_SelectedIndexChanged(sender, e);
            }
        }

        private void lbNewGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnJoin.Enabled = lbNewGames.SelectedItem != null;
        }

        private void lbActiveGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnJoin.Enabled = lbActiveGames.SelectedItem != null;
        }

        private void lbYourGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnJoin.Enabled = lbYourGames.SelectedItem != null;
        }

        private void lbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnJoin.Enabled = lbSearchedGames.SelectedItem != null;
        }

        private void txbSearch_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Enabled = txbSearch.Text?.Length > 0;
        }

        private void txbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = !(
            //    (e.KeyValue >= 'a' && e.KeyValue <= 'z') ||
            //    (e.KeyValue >= 'A' && e.KeyValue <= 'Z') ||
            //    (e.KeyValue >= '0' && e.KeyValue <= '9') ||
            //    e.KeyValue == '-' || e.KeyValue == '_');
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lbSearchedGames.Items.Clear();
                if (txbSearch.Text?.Length > 0)
                {
                    var games = await GetPlayerGamesAsync(txbSearch.Text);
                    lbSearchedGames.Items.AddRange(games.ToArray());
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                txbMessages.Text = ex.Message;
            }
        }

        private void bthHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Tab 'New Games' shows games that are not yet started. Select a Game ID and click on 'Join' to join the selected game.\n\n" +
                "Tab 'Active Games' shows games that are in progress. Select a Game ID and click on 'Spectate' to join the selected game. In case you are logged in as one of the players of the game, you will join as a player. Otherwise you will only be able to spectate the game.\n\n" +
                "Tab 'My Games' shows all your games. Select a Game ID and click on 'Rejoin' to rejoin the selected game.\n\n" +
                "Tab 'Search' lets you search games by player's name. Select a Game ID and click on 'Join' to join the selected game. Depending on whether the game was already started you will join either as a spectator or as a player.\n\n" +
                "To create a new game, in 'Create game' box select the color you wish to play as and click on the button 'Create'\n\n" +
                "Clicking the 'Refresh' button will refresh the game lists.\n\n" +
                "Clicking the 'Help' button will bring up this dialog again.",
                "Help me please...",
                MessageBoxButtons.OK,
                MessageBoxIcon.Question
                );
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

        private async Task<List<string>> GetPlayerGamesAsync(string name)
        {
            HttpResponseMessage response = await client.GetAsync($"api/player/{name}/games");
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
    }
}
