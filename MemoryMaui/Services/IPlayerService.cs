using MemoryMaui.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMaui.Services
{
    public interface IPlayerService
    {
        List<int> PlayerCounters { get; set; }
        ObservableCollection<Player> Players { get; set; }

        void ResetPlayerValues();
        ObservableCollection<Player> GetPlayers();

        //void SetPlayerIsActive(ObservableCollection<Player> players);

        void SetNextPlayer(ObservableCollection<Player> list);  

        void IncreasePlayerCounter(ObservableCollection<Player> playerList);
       
    }
}
