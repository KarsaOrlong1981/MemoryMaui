using MemoryMaui.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMaui.Services
{
    public class PlayerService : IPlayerService
    {
        public List<int> PlayerCounters { get; set; }
        public ObservableCollection<Player> Players { get; set; }

        //public void SetPlayerIsActive(ObservableCollection<Player> players)
        //{

        //}
        public PlayerService() 
        { 
            Players = new ObservableCollection<Player>();
        }
        public ObservableCollection<Player> GetPlayers()
        {
            Players[0].IsActive = true;
            return Players;
        }
        
        public void ResetPlayerValues()
        {
            if(Players != null)
            {
                foreach (Player player in Players)
                {
                    player.IsActive = false;
                    player.Score = 0;
                }
            }
            
        }

        public void IncreasePlayerCounter(ObservableCollection<Player> playerList)
        {
            var counter = 0;
            foreach (Player player in playerList)
            {   
                if (player.IsActive)
                {
                    PlayerCounters[counter]++;
                }
                counter++;
            }
        }

        public void SetNextPlayer(ObservableCollection<Player> players)
        {
            var counter = 0;
            foreach (var player in players)
            {
                if(player.IsActive)
                {
                    player.IsActive = false;
                    if (counter == players.Count - 1)
                        players[0].IsActive = true;
                    else
                        players[counter + 1].IsActive = true;
                    break;
                }
                if (counter == players.Count - 1)
                {
                    counter = 0;
                }
                else
                    counter++;
            }
        }
    }
}
