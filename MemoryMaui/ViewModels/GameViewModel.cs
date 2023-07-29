using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MemoryMaui.Models;
using MemoryMaui.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMaui.ViewModels
{
    public partial class GameViewModel : ObservableObject
    {
        private IServiceProvider _serviceProvider;
        [ObservableProperty]
        private ObservableCollection<Card> cards;

        [ObservableProperty]
        private ObservableCollection<Player> players;

        [ObservableProperty]
        private Card card;

        [ObservableProperty]
        private Player winner;

        [ObservableProperty]
        private bool gameOver;

        [ObservableProperty]
        private bool canGoBack;

        private void SetPlayers()
        {
            Players = _serviceProvider.GetService<IPlayerService>().GetPlayers();
            var playerService = _serviceProvider.GetService<IPlayerService>();
            playerService.PlayerCounters = new List<int>();
            foreach (var player in Players)
            {
                playerService.PlayerCounters.Add(0);
            }
        }

        [RelayCommand]
        private async Task SelectionChanged()
        {
            var currentCard = Card;
            if (currentCard.IsEnabled && currentCard.ImagePath != "cross.png")
            {
                currentCard.IsFlipped = true;
                _serviceProvider.GetService<IGameService>().SetCardsToDisabled(Cards);




                var flippedCards = Cards.Where(c => c.IsFlipped).ToObservableCollection();

                if (flippedCards.Count == 2)
                {

                    await Task.Delay(3000);
                    //is matched prüfen, paar counter des player erhöhen wenn ismatched ist true und karten rausnehmen wenn is matched gleich false dann wieder herum drehen
                    if (_serviceProvider.GetService<IGameService>().CheckForMatch(flippedCards))
                    {
                        foreach (var card in flippedCards)
                        {
                            card.Backside = ImageSource.FromFile("cross.png");
                            card.ImagePath = "cross.png";
                            card.IsFlipped = false;
                            card.IsMatched = true;
                        }
                        var counter = 0;
                        foreach (var player in Players)
                        {
                            if (player.IsActive)
                            {
                                _serviceProvider.GetService<IPlayerService>().PlayerCounters[counter]++;
                                player.Score++;
                            }
                            counter++;
                        }
                        if (_serviceProvider.GetService<IGameService>().CheckForWin())
                        {
                            var winnerScore = Players.Max(x => x.Score);

                            for (int i = 0; i < Players.Count; i++)
                            {
                                if (winnerScore == Players[i].Score)
                                {
                                    Winner = Players[i];
                                    GameOver = true;
                                    UpdateRanking();
                                    //ResetGame();
                                    break;
                                }

                            }

                        }
                    }
                    else
                    {

                        foreach (var card in flippedCards)
                        {
                            card.IsFlipped = false;
                        }
                        _serviceProvider.GetService<IPlayerService>().SetNextPlayer(Players);
                    }
                    Card = new Card();
                }
                _serviceProvider.GetService<IGameService>().SetCardsToEnabled(Cards);
              
            }

        }

        private void UpdateRanking()
        {
            //var rankingList = PlayerData.GetRanking() ?? new ObservableCollection<Ranking>();
            var rankingList =new ObservableCollection<Ranking>();
            var savedRankingList = PlayerData.GetRanking();

            var orderedPlayerList = new ObservableCollection<Player>(Players.OrderByDescending(r => r.Score));
            int counter = 1; 
            if (savedRankingList.Count > 0)
            {
                for (int i = 0; i < orderedPlayerList.Count; i++)
                {
                    foreach (var r in savedRankingList)
                    {
                        if (r.Name == orderedPlayerList[i].Name)
                        {
                            r.Score += orderedPlayerList[i].Score;
                            r.Avatar = orderedPlayerList[i].Avatar;
                            r.GamesPlayed++;
                        }
                    }
                }
                var missingPlayers = new ObservableCollection<Ranking>();
                //wenn ein spieler noch nicht in rankingList registriert ist
                for (int i = 0; i < orderedPlayerList.Count; i++)
                {
                    bool playerFound = false;

                    for (int y = 0; y < savedRankingList.Count; y++)
                    {
                        if (savedRankingList[y].Name == orderedPlayerList[i].Name)
                        {
                            playerFound = true;
                            break;
                        }
                    }

                    if (!playerFound)
                    {
                        missingPlayers.Add(new Ranking
                        {
                            Avatar = orderedPlayerList[i].Avatar,
                            Name = orderedPlayerList[i].Name,
                            GamesPlayed = 1,
                            Score = orderedPlayerList[i].Score
                        });
                    }
                }
                if (missingPlayers.Count > 0)
                {
                    foreach (var item in missingPlayers)
                    {
                        savedRankingList.Add(item);
                    }
                }

                savedRankingList = new ObservableCollection<Ranking>(savedRankingList.OrderByDescending(r => r.Score));
                int rank = 1;
                foreach (var ranking in savedRankingList)
                {
                    ranking.Rank = rank;
                    rank++;
                }
                PlayerData.SaveRanking(savedRankingList);
            }
            else
            {
                foreach (var player in orderedPlayerList)
                {

                    //passiert nur beim ersten spiel überhaupt
                    var ranking = new Ranking { Rank = counter, Avatar = player.Avatar, Name = player.Name, Score = player.Score, GamesPlayed = 1 };
                    rankingList.Add(ranking);

                    counter++;
                }
                rankingList = new ObservableCollection<Ranking>(rankingList.OrderByDescending(r => r.Score));
                int rank = 1;
                foreach (var ranking in rankingList)
                {
                    ranking.Rank = rank;
                    rank++;
                }
                PlayerData.SaveRanking(rankingList);
            }

            _serviceProvider.GetService<IGameService>().UpdateRanking();
        }

       
        public GameViewModel(IServiceProvider provider)
        {
            _serviceProvider = provider;
            _serviceProvider.GetService<IPlayerService>().ResetPlayerValues();
            Cards = provider.GetService<IGameService>().GetCards();
            Card = new Card();
            Winner = new Player();
            SetPlayers();
        }
    }
}
