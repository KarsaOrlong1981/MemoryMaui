using MemoryMaui.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMaui
{
    public static class PlayerData
    {
        public const string PlayersKey = "players_key";
        public const string RankingKey = "players_rankingKey";

        public static ObservableCollection<Player> GetPlayers()
        {
            string serializedPlayers = Preferences.Default.Get(PlayersKey, string.Empty);
            return JsonConvert.DeserializeObject<ObservableCollection<Player>>(serializedPlayers) ?? new ObservableCollection<Player>();
        }

        public static void SavePlayers(ObservableCollection<Player> players)
        {
            string serializedPlayers = JsonConvert.SerializeObject(players);
            Preferences.Default.Set(PlayersKey, serializedPlayers);
        }

        public static ObservableCollection<Ranking> GetRanking()
        {
            string serializedRanking = Preferences.Default.Get(RankingKey, string.Empty);
            return JsonConvert.DeserializeObject<ObservableCollection<Ranking>>(serializedRanking) ?? new ObservableCollection<Ranking>();
        }

        public static void SaveRanking(ObservableCollection<Ranking> ranking)
        {
            string serializedRanking = JsonConvert.SerializeObject(ranking);
            Preferences.Default.Set(RankingKey, serializedRanking);
        }
    }
}
