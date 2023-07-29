using MemoryMaui.ViewModels;

namespace MemoryMaui.Pages;

public partial class GamePage : ContentPage
{
	public GamePage(IServiceProvider provider)
	{
		InitializeComponent();
		this.BindingContext = new GameViewModel(provider);
	}
}