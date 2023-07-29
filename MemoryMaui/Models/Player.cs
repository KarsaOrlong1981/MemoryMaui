using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMaui.Models
{
    public partial class Player : ObservableObject 
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private bool isActive;

        [ObservableProperty]
        private int score;

        [ObservableProperty]
        private ImageSource avatar;

        [ObservableProperty]
        private Color borderColor = Colors.White;
    }
}
