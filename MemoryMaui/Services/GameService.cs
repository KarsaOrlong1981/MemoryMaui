using MemoryMaui.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMaui.Services
{
    public class GameService : IGameService
    {
        private ObservableCollection<Card> cards;
        private Random random = new Random();

        public event EventHandler RankingChangedEvent;

        public ObservableCollection<Card> GetCards()
        {
            cards = new ObservableCollection<Card>();
            AddCards();
            ShuffleCards();
            return cards;

        }
        public void UpdateRanking()
        {

            RankingChangedEvent.Invoke(this, EventArgs.Empty);
        }
        public void SetCardsToDisabled(ObservableCollection<Card> currentCards)
        {
            var flippedCards = currentCards.Where(c => c.IsFlipped).ToList();
            if (flippedCards.Count > 1)
            {
                foreach (Card card in currentCards)
                {
                    card.IsEnabled = false;
                }
            }
           
        }
        public void SetCardsToEnabled(ObservableCollection<Card> currentCards)
        {
           
                foreach (Card card in currentCards)
                {
                    card.IsEnabled = true;
                }
      
           
        }
        public bool CheckForMatch(ObservableCollection<Card> flippedCardIds)
        {
            if (flippedCardIds.Count != 2)
                return false;

            var firstCard = cards.FirstOrDefault(c => c.Id == flippedCardIds[0].Id);
            var secondCard = cards.FirstOrDefault(c => c.Id == flippedCardIds[1].Id);

            if (firstCard == null || secondCard == null)
                return false;

            return firstCard.ImagePath == secondCard.ImagePath;
        }

        public bool CheckForWin()
        {
            return cards.All(c => c.IsMatched);
        }

        private void ShuffleCards()
        {
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Card value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }

        private void AddCards()
        {
            cards.Add(new Card { Id = 1, ImagePath = "dedede.png" });
            cards.Add(new Card { Id = 2, ImagePath = "dedede.png" });
            cards.Add(new Card { Id = 3, ImagePath = "dino.jpg" });
            cards.Add(new Card { Id = 4, ImagePath = "dino.jpg" });
            cards.Add(new Card { Id = 5, ImagePath = "dinos.jpg" });
            cards.Add(new Card { Id = 6, ImagePath = "dinos.jpg" });
            cards.Add(new Card { Id = 7, ImagePath = "snakecard.jpg" });
            cards.Add(new Card { Id = 8, ImagePath = "snakecard.jpg" });
            cards.Add(new Card { Id = 9, ImagePath = "wolfcard.jpg" });
            cards.Add(new Card { Id = 10, ImagePath = "wolfcard.jpg" });
            cards.Add(new Card { Id = 11, ImagePath = "ninjacard.jpg" });
            cards.Add(new Card { Id = 12, ImagePath = "ninjacard.jpg" });
            cards.Add(new Card { Id = 13, ImagePath = "tmcard.jpg" });
            cards.Add(new Card { Id = 14, ImagePath = "tmcard.jpg" });
            cards.Add(new Card { Id = 15, ImagePath = "soniccard.jpg" });
            cards.Add(new Card { Id = 16, ImagePath = "soniccard.jpg" });
            cards.Add(new Card { Id = 17, ImagePath = "kirby.png" });
            cards.Add(new Card { Id = 18, ImagePath = "kirby.png" });
            cards.Add(new Card { Id = 19, ImagePath = "affecard.jpg" });
            cards.Add(new Card { Id = 20, ImagePath = "affecard.jpg" });
            cards.Add(new Card { Id = 21, ImagePath = "hundhosecard.jpg" });
            cards.Add(new Card { Id = 22, ImagePath = "hundhosecard.jpg" });
            cards.Add(new Card { Id = 23, ImagePath = "minecraft.jpg" });
            cards.Add(new Card { Id = 24, ImagePath = "minecraft.jpg" });
            cards.Add(new Card { Id = 25, ImagePath = "monster.jpg" });
            cards.Add(new Card { Id = 26, ImagePath = "monster.jpg" });
            cards.Add(new Card { Id = 27, ImagePath = "monstertruck.jpg" });
            cards.Add(new Card { Id = 28, ImagePath = "monstertruck.jpg" });
            cards.Add(new Card { Id = 29, ImagePath = "monstertruckblue.jpg" });
            cards.Add(new Card { Id = 30, ImagePath = "monstertruckblue.jpg" });
            cards.Add(new Card { Id = 31, ImagePath = "sonic2card.jpg" });
            cards.Add(new Card { Id = 32, ImagePath = "sonic2card.jpg" });
            cards.Add(new Card { Id = 33, ImagePath = "peppa.jpg" });
            cards.Add(new Card { Id = 34, ImagePath = "peppa.jpg" });
            cards.Add(new Card { Id = 35, ImagePath = "peppageorge.jpg" });
            cards.Add(new Card { Id = 36, ImagePath = "peppageorge.jpg" });
            cards.Add(new Card { Id = 37, ImagePath = "pica.png" });
            cards.Add(new Card { Id = 38, ImagePath = "pica.png" });
            cards.Add(new Card { Id = 39, ImagePath = "powerrangers.jpg" });
            cards.Add(new Card { Id = 40, ImagePath = "powerrangers.jpg" });
            cards.Add(new Card { Id = 41, ImagePath = "tmntcard.jpg" });
            cards.Add(new Card { Id = 42, ImagePath = "tmntcard.jpg" });
            cards.Add(new Card { Id = 43, ImagePath = "smurf.jpg" });
            cards.Add(new Card { Id = 44, ImagePath = "smurf.jpg" });
            cards.Add(new Card { Id = 45, ImagePath = "spidy.jpg" });
            cards.Add(new Card { Id = 46, ImagePath = "spidy.jpg" });
            cards.Add(new Card { Id = 47, ImagePath = "stone.png" });
            cards.Add(new Card { Id = 48, ImagePath = "stone.png" });

            foreach (var card in cards)
            {
                card.Source = ImageSource.FromFile(card.ImagePath);
            }
        }

       
    }
}
