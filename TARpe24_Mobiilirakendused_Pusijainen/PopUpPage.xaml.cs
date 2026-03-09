

namespace TARpe24_Mobiilirakendused_Pusijainen;

public partial class PopUpPage : ContentPage
{
	public PopUpPage()
	{
		// InitializeComponent();
		
		// 1. Loome esimese nuppu (Lihtne  teade)
		Button alertButton = new Button
		{
			Text = "Teade",
			VerticalOptions = LayoutOptions.Start,
			HorizontalOptions = LayoutOptions.Center
		};

		// Seome klikkimise funktsiooniga
		alertButton.Clicked += AlertButton_Clicked;

		// 2. Loome teise nupu (Kinnitus)
		Button alertYesNoButton = new Button
		{
			Text = "Jah või Ei",
			VerticalOptions = LayoutOptions.Start,
			HorizontalOptions = LayoutOptions.Center
		};

		alertYesNoButton.Clicked += AlertYesNoButton_Clicked;

		// 3. Loome kolmanda nupu (Valikmenüü)
		Button alertListButton = new Button
		{
			Text = "Valik",
			VerticalOptions = LayoutOptions.Start,
			HorizontalOptions = LayoutOptions.Center
		};

		alertListButton.Clicked += AlertListButton_Clicked;

		// 4. Paigutame kõik nupud ekraanile üksteise alla

		Content = new VerticalStackLayout
		{
			Spacing = 20, // Jätab nuppude vahele 20 pikslit vaba ruumi 
			Padding = new Thickness(0, 50, 0, 0), // Lükkab sisu veidi ülevalt alla
			Children = { alertButton, alertYesNoButton, alertListButton }
		};
	}

	public async void AlertButton_Clicked(object? sender, EventArgs e)
	{
		await DisplayAlertAsync("Teade", "Reinla arvab, et ma olen narkoman", "OK");
	}

	public async void AlertYesNoButton_Clicked(object? sender, EventArgs e)
	{
		// Küsime kasutajalt kinnitust (tagastab true või false)
		bool result = await DisplayAlertAsync("Kinnitus", "Kas oled kindel", "Olen Kindel", "Ei ole kindel");

		// Kuvame uue teate vastavalt sellele, mida kasutaja valis
		// (result ? "Jah" : "Ei") tähendab: kui result on true siis kirjutada "Jah", muidu "Ei".
		await DisplayAlertAsync("Teade", (result ? "Naiss" : "Lohh"), "OK");
	}
	
	public async void AlertListButton_Clicked(object? sender, EventArgs e)
	{

		string action = await DisplayActionSheetAsync("Mida teha?", "Loobu", null, "Tantsida", "Nutta", "Tappa ennast ära");

		if (action != null && action != "Loobu")
		{
			await DisplayAlertAsync("Valik", "Sa valisid tegevuse: " + action, "OK");
		}
	}

}