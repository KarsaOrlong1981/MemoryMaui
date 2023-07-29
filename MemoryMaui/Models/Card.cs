using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMaui.Models
{
    public partial class Card : ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string imagePath;
        [ObservableProperty]
        private ImageSource source;
        [ObservableProperty]
        private ImageSource backside = ImageSource.FromFile("back.jpg");
        [ObservableProperty]
        private bool isFlipped;
        [ObservableProperty]
        private bool isMatched;
        [ObservableProperty]
        private bool isEnabled = true;

    }
}
