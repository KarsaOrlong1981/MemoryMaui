using MemoryMaui.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMaui.Services
{
    public interface IGameService
    {
        ObservableCollection<Card> GetCards();
        event EventHandler RankingChangedEvent;
        void UpdateRanking();
        bool CheckForMatch(ObservableCollection<Card> flippedCardIds);
        bool CheckForWin();
        void SetCardsToDisabled(ObservableCollection<Card> currentCards);
        void SetCardsToEnabled(ObservableCollection<Card> currentCards);
    }
}
