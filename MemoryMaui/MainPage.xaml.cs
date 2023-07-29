using MemoryMaui.ViewModels;

namespace MemoryMaui;

public partial class MainPage : ContentPage
{
	public MainPage(IServiceProvider provider)
	{
		InitializeComponent();
		this.BindingContext = new MainViewModel(provider);
	}
}

