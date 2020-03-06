using ChessCommons;
using ChessFabrickCommons.Entities;
using ChessFabrickCommons.Models;
using ChessFabrickFormsApp.Properties;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
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
using System.Windows.Forms.VisualStyles;

namespace ChessFabrickFormsApp
{
    public partial class ChessOnlineForm : Form
    {
        private UserModel user;
        private Uri host;

        private string gameId;
        private ChessGameState gameState;
        private PieceColor? playerColor;

        private Board board;
        private Tuple<int, int> selectedField;
        private List<Tuple<int, int>> possibleMoves;

        private ChessFieldBox[,] fieldBoxes = new ChessFieldBox[8, 8];

        private HttpClient client;
        private HubConnection connection;

        public ChessOnlineForm(UserModel user, Uri host, ChessGameInfo gameInfo)
        {
            this.user = user;
            this.host = host;
            this.gameId = gameInfo.GameId;
            if (gameInfo.White?.Name == user.Player.Name)
            {
                playerColor = PieceColor.White;
            }
            else if (gameInfo.Black?.Name == user.Player.Name)
            {
                playerColor = PieceColor.Black;
            }

            InitializeComponent();
            InitializeBoard();
            InitHttpClient();
            InitHubConnection();

            Icon = Icon.FromHandle(Properties.Resources.knight_white.GetHicon());
            Text = $"Chess - {user.Player.Name}";
        }

        public ChessOnlineForm(UserModel user, Uri host, ChessGameState gameState)
            : this(user, host, gameState.GameInfo)
        {
            this.gameState = gameState;
        }

        private void InitializeBoard()
        {
            panTable.SuspendLayout();
            for (int i = 0; i < 8; ++i)
            {
                var labColumn = new Label();
                labColumn.Location = new Point(20 + 53 * i, 0);
                labColumn.AutoSize = false;
                labColumn.Size = new Size(50, 20);
                labColumn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                labColumn.Text = ((char)('A' + i)).ToString();
                panTable.Controls.Add(labColumn);

                var labRow = new Label();
                labRow.Location = new Point(0, 20 + 53 * i);
                labRow.AutoSize = false;
                labRow.Size = new Size(20, 50);
                labRow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                labRow.Text = ((char)('8' - i)).ToString();
                panTable.Controls.Add(labRow);

                for (int j = 0; j < 8; ++j)
                {
                    var fieldBox = new ChessFieldBox();
                    fieldBox.Location = new Point(20 + 53 * i, 20 + 53 * (7 - j));
                    fieldBox.BackColor = (i + j) % 2 == 0 ? Color.Peru : Color.Cornsilk;
                    fieldBox.Click += FieldBox_Click;
                    fieldBox.Tag = Tuple.Create(i, j);
                    panTable.Controls.Add(fieldBox);
                    fieldBoxes[i, j] = fieldBox;
                }
            }
            panTable.ResumeLayout(false);
        }

        private void InitHttpClient()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = host;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
        }

        private void InitHubConnection()
        {
            connection = new HubConnectionBuilder()
                .WithUrl(host.AbsoluteUri + "hub/chess", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(user.Token);
                })
                .Build();
            connection.On<string>("Test", (message) => { Console.WriteLine($"{message}"); });
            connection.On<ChessGameState>("OnBoardChanged", (board) => OnBoardChanged(board) );
            connection.On<string, string, ChessGameState>("OnPieceMoved", (from, to, board) => OnBoardChanged(board));
            connection.On<ChessGameState>("OnPlayerJoined", (board) => OnBoardChanged(board));
        }

        private async void ChessForm_Load(object sender, EventArgs e)
        {
            try
            {
                await connection.StartAsync();
            } catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                MessageBox.Show(ex.Message, "Unable to connect to the server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            try
            {
                if (playerColor == null)
                {
                    labPlayerColor.Text = "Spectating";
                    cfbPlayerColor.Image = Resources.eye;
                    gameState = await connection.InvokeAsync<ChessGameState>("SpectateGame", gameId);
                    Console.WriteLine($"SpectateGame: {JsonConvert.SerializeObject(gameState)}");
                }
                else
                {
                    labPlayerColor.Text = "Your color";
                    cfbPlayerColor.Image = PieceImageUtils.Pawn(playerColor.Value);
                    if (gameState != null)
                    {
                        gameState = await connection.InvokeAsync<ChessGameState>("JoinGame", gameId);
                        Console.WriteLine($"JoinGame: {JsonConvert.SerializeObject(gameState)}");
                    }
                }
            } catch (HubException ex)
            {
                Console.Error.WriteLine(ex);
                txbMessage.Text = ex.Message;
            }
            UpdateBoard();
        }

        private void UpdateBoard()
        {
            board = new Board();
            try
            {
                board.PerformMoves(gameState.GameInfo.MoveHistory);
            }
            catch (Exception ex)
            {
                txbMessage.Text = ex.Message;
            }
            RefreshViews();
        }

        private void RefreshViews()
        {
            labPlayerNames.Text = $"White: \n{gameState?.GameInfo?.White?.Name} \n\nBlack: \n{gameState?.GameInfo?.Black?.Name}";

            if (gameState == null)
            {
                btnAddBot.Visible = true;
                return;
            }

            btnAddBot.Visible = false;

            if (gameState.IsCheckmate)
            {
                panTable.Enabled = false;
                labPlaying.Text = "Victory";
                cfbPlaying.Image = PieceImageUtils.King(gameState.OnTurn.Other());
            }
            else if (gameState.IsDraw)
            {
                panTable.Enabled = false;
                labPlaying.Text = "Draw";
                cfbPlaying.Image = null;
            }
            else
            {
                panTable.Enabled = true;
                labPlaying.Text = "Now playing";
                cfbPlaying.Image = PieceImageUtils.Pawn(gameState.OnTurn);
            }

            pieceListPanelBlack.SetPieceChars(gameState.CapturedPieces.Where(c => c >= 'a' && c <= 'z').ToList());
            pieceListPanelWhite.SetPieceChars(gameState.CapturedPieces.Where(c => c >= 'A' && c <= 'Z').ToList());

            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    var fieldBox = fieldBoxes[i, j];
                    fieldBox.SuspendLayout();
                    fieldBox.Style = ChessFieldBox.BoxStyle.None;
                    fieldBox.Image = board[i, j]?.Image();
                }
            }

            if (gameState.CheckingPieces?.Count > 0)
            {
                var king = board.King(board.TurnColor);
                fieldBoxes[king.X, king.Y].Style = ChessFieldBox.BoxStyle.Checked;
                foreach (var piece in gameState.CheckingPieces)
                {
                    var field = ChessGameUtils.FieldFromString(piece);
                    fieldBoxes[field.Item1, field.Item2].Style = ChessFieldBox.BoxStyle.Checking;
                }
            }

            if (selectedField != null)
            {
                fieldBoxes[selectedField.Item1, selectedField.Item2].Style = ChessFieldBox.BoxStyle.Selected;
                if (possibleMoves != null)
                {
                    var selectedPiece = board[selectedField.Item1, selectedField.Item2];
                    foreach (var field in possibleMoves)
                    {
                        fieldBoxes[field.Item1, field.Item2].Style =
                            board[field.Item1, field.Item2] != null || (selectedPiece is Pawn && selectedPiece.X != field.Item1) ?
                            ChessFieldBox.BoxStyle.Attack : ChessFieldBox.BoxStyle.Move;
                    }
                }
            }

            foreach (var fieldBox in fieldBoxes)
            {
                fieldBox.ResumeLayout(false);
                fieldBox.Invalidate();
            }
        }

        private async void FieldBox_Click(object sender, EventArgs e)
        {
            var field = (sender as ChessFieldBox).Tag as Tuple<int, int>;
            if (selectedField == null)
            {
                selectedField = field;
                var selectedPiece = board[selectedField.Item1, selectedField.Item2];
                if (selectedPiece?.Color != playerColor || selectedPiece?.Color != gameState.OnTurn)
                {
                    selectedField = null;
                }
                if (selectedField != null)
                {
                    try
                    {
                        var moves = await connection.InvokeAsync<List<string>>("GetPieceMoves", gameId,
                            ChessGameUtils.FieldToString(selectedField.Item1, selectedField.Item2)
                        );
                        possibleMoves = new List<Tuple<int, int>>(moves.Count);
                        foreach (var move in moves)
                        {
                            possibleMoves.Add(ChessGameUtils.FieldFromString(move));
                        }
                    } catch (HubException ex)
                    {
                        selectedField = null;
                        possibleMoves = null;
                        Console.Error.WriteLine(ex);
                        txbMessage.Text = ex.Message;
                    }
                }
            }
            else
            {
                if (possibleMoves.Contains(field))
                {
                    try
                    {
                        gameState = await connection.InvokeAsync<ChessGameState>("MovePiece", gameId,
                            ChessGameUtils.FieldToString(selectedField.Item1, selectedField.Item2),
                            ChessGameUtils.FieldToString(field.Item1, field.Item2)
                        );
                    } catch (HubException ex)
                    {
                        selectedField = null;
                        Console.Error.WriteLine(ex);
                        txbMessage.Text = ex.Message;
                    }
                }
                selectedField = null;
                possibleMoves = null;
            }
            UpdateBoard();
        }

        private void OnBoardChanged(ChessGameState board)
        {
            Console.WriteLine($"OnBoardChanged: {JsonConvert.SerializeObject(board)}");
            if (board?.GameInfo?.GameId != gameId)
            {
                return;
            }
            gameState = board;
            selectedField = null;
            possibleMoves = null;
            UpdateBoard();
        }

        private async void btnAddBot_Click(object sender, EventArgs e)
        {
            try
            {
                var game = await PostAddBot();
                gameState = new ChessGameState(game);
                UpdateBoard();
            }
            catch (Exception ex)
            {
                txbMessage.Text = ex.Message;
                Console.Error.WriteLine(ex);
            }
        }

        private async Task<ChessGameInfo> PostAddBot()
        {
            HttpResponseMessage response = await client.PostAsync($"api/game/new/{gameId}/addbot", null);
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
