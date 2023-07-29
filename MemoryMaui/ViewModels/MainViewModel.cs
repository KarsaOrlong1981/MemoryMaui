
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MemoryMaui.Models;
using MemoryMaui.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMaui.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private IServiceProvider provider;

        public MainViewModel(IServiceProvider provider)
        {
            //this.SelectedCurrentPlayer = new object();
            this.player = new Player();
            this.ObjectPlayerList = new ObservableCollection<object>();
            this.PlayerList = new ObservableCollection<Player>();
            this.AvatarList = new ObservableCollection<ImageSource>();
            this.CurrentPlayer = new ObservableCollection<object>();
            CreateAvatarList();
            this.provider = provider;
            provider.GetService<IGameService>().RankingChangedEvent += MainViewModel_RankingChangedEvent;
            GetData();
        }

        private void MainViewModel_RankingChangedEvent(object sender, EventArgs e)
        {
            RankingList = PlayerData.GetRanking();
            ResetGame();
        }
        private void ResetGame()
        {
            provider.GetService<IPlayerService>().ResetPlayerValues();
        }
        private void GetData()
        {
            if (Preferences.Default.ContainsKey(PlayerData.PlayersKey))
            {
                //this.provider.GetService<IPlayerService>().Players = PlayerData.GetPlayers();
                this.PlayerList = PlayerData.GetPlayers();
            }
            else
                this.provider.GetService<IPlayerService>().Players = this.PlayerList;
            if (PlayerList.Count > 1)
            {
                CanStartGame = true;
            }
            if (Preferences.Default.ContainsKey(PlayerData.RankingKey))
            {
                this.RankingList = PlayerData.GetRanking();
            }
            else
                this.RankingList = new ObservableCollection<Ranking>();
        }

        [ObservableProperty]
        private ObservableCollection<Ranking> rankingList;

        [ObservableProperty]
        private string playerName;

        [ObservableProperty]
        private bool canStartGame;

        [ObservableProperty]
        private bool canWriteName;

        [ObservableProperty]
        private bool isAvatar;

        [ObservableProperty]
        private bool choosePlayer;

        [ObservableProperty]
        private ObservableCollection<Player> playerList;

        private ObservableCollection<object> currentPlayer;

        public ObservableCollection<object> CurrentPlayer { get => currentPlayer; set => SetProperty(ref currentPlayer, value); }

        private ObservableCollection<object> objectPlayerList;
        public ObservableCollection<object> ObjectPlayerList { get => objectPlayerList; set => SetProperty(ref objectPlayerList, value); }

        [ObservableProperty]
        private ObservableCollection<ImageSource> avatarList;

        [ObservableProperty]
        private ImageSource avatar;

        [ObservableProperty]
        private Player selectedPlayer;

        [ObservableProperty]
        private object selectedCurrentPlayer;

        private Player player;
        private bool isEditMode;

        [RelayCommand]
        private void GotoGamePage()
        {
            PlayerData.SavePlayers(PlayerList);
            foreach (var player in PlayerList)
            {
                var obj = player as object;
                ObjectPlayerList.Add(obj);
            }
            //provider.GetService<IPlayerService>().Players = PlayerList;
            ChoosePlayer = true;
            
        }

        [RelayCommand]
        private async Task GotoGamePage2()
        {
            provider.GetService<IPlayerService>().Players.Clear();
            if(CurrentPlayer.Count > 1)
            {
                foreach (var item in CurrentPlayer)
                {
                    if (item is Player player)
                    {
                        provider.GetService<IPlayerService>().Players.Add(player);
                    }
                }
                CurrentPlayer = new ObservableCollection<object>();
                ChoosePlayer = false;
                await Shell.Current.GoToAsync("gamePage");
            }
            else
                await App.Current.MainPage.DisplayAlert("Achtung", "Bitte mindestens 2 Spieler wählen zum fortfahren.", "Ok");

        }

        [RelayCommand]
        private void VersusCom()
        {
            

        }

        [RelayCommand]
        private void MultipleSelectionChanged()
        {
            //if (CurrentPlayer.Count > 0)
            //{
               
            //    //var player = CurrentPlayer[CurrentPlayer.Count - 1] as Player;
            //    //if (player.BorderColor == Colors.Green)
            //    //{
            //    //    player.BorderColor = Colors.White;
            //    //}
            //    //else
            //    //    player.BorderColor = Colors.Green;
            //}
            //else
            //{
            //    foreach (var item in PlayerList)
            //    {
            //        item.BorderColor = Colors.White;
            //    }
            //}
        }

        [RelayCommand]
        private void AddNewPlayer()
        {
            CanWriteName = true;
           
        }

        [RelayCommand]
        private void NameCompleted()
        {
            if (isEditMode)
            {
                SelectedPlayer.Name = PlayerName;
            }
            else
              this.player.Name = PlayerName;
            CanWriteName = false;
            IsAvatar = true;
        }

        [RelayCommand]
        private void AvatarChanged()
        {
            IsAvatar = false;
            CanWriteName = false;
            if (Avatar != null)
            {
                if (isEditMode)
                {
                    SelectedPlayer.Avatar = Avatar;
                    SelectedPlayer = null;
                    
                }
                else
                {
                    this.player.Avatar = Avatar;

                    PlayerList.Add(this.player);
                   
                }
               

                if (PlayerList.Count > 1)
                    CanStartGame = true;
                PlayerName = "";
                isEditMode = false;
                PlayerData.SavePlayers(PlayerList);
            }
            
            player = new Player();
            
        }

        [RelayCommand]
        private void SelectedPlayerChange()
        {
            if (!isEditMode && SelectedPlayer != null)
            {
                ShowDeleteOrEditAlert();
            }
            Avatar = null;
           
        }

        private async void ShowDeleteOrEditAlert()
        {
            bool isDeleteSelected = await App.Current.MainPage.DisplayAlert("Auswahl treffen", "Möchten Sie löschen oder bearbeiten?", "Löschen", "Bearbeiten");
            isEditMode = false;

            if (isDeleteSelected)
            {
                PlayerList.Remove(SelectedPlayer);
                if (PlayerList.Count < 2)
                {
                    CanStartGame = false;

                }
                PlayerData.SavePlayers(PlayerList);
            }
            else
            {
                PlayerName = SelectedPlayer.Name;
                isEditMode = true;
                IsAvatar = true;
                CanWriteName = true;

            }
        }
        private void CreateAvatarList()
        {
            AvatarList = new ObservableCollection<ImageSource>
            {
                ImageSource.FromFile("koalaavatar.jpg"),
                ImageSource.FromFile("mineavatar.jpg"),
                ImageSource.FromFile("poweravatar.jpg"),
                ImageSource.FromFile("spidyavatar.png"),
                ImageSource.FromFile("spongeavatar.jpg"),
                ImageSource.FromFile("monsteravatar.jpg"),
                ImageSource.FromFile("tigerente.jpg"),
                ImageSource.FromFile("avataravatar.jpg"),
                ImageSource.FromFile("sonicavatar.jpg"),
                ImageSource.FromFile("roboavatar.jpg"),
                ImageSource.FromFile("robo2avatar.jpg"),
                ImageSource.FromFile("endermanavatar.png"),
                ImageSource.FromFile("ninjaavatar.jpg"),
                ImageSource.FromFile("ninja2avatar.jpg"),
                ImageSource.FromFile("raphaelavatar.jpg"),
                ImageSource.FromFile("leonardoavatar.jpg"),
                ImageSource.FromFile("shredaravatar.png")


            };
        }
    }
}
