using MemoryMaui.Pages;
using MemoryMaui.Services;

namespace MemoryMaui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        
        Routing.RegisterRoute("gamePage", typeof(GamePage));
       
    }

    
}
