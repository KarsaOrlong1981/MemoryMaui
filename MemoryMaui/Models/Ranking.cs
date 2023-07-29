using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMaui.Models
{
    public partial class Ranking : ObservableObject
    {
        [ObservableProperty]
        private int rank;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int score;

        [ObservableProperty]
        private int gamesPlayed;

        [ObservableProperty]
        private ImageSource avatar;
    }
}
