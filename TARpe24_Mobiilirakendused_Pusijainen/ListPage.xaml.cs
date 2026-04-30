using System.Collections.ObjectModel;
using System.Runtime.InteropServices.ObjectiveC;

namespace TARpe24_Mobiilirakendused_Pusijainen;

// andmemudel
public class Telefon
{
	public string Nimetus { get; set; }
	public string Tootja { get; set; }
	public int Hind { get; set; }
	public string Pilt { get; set; } // Hoiab pildi nime või seadme failiteed
}

public partial class ListPage : ContentPage
{
	//Globaalsed muutujad 
	ObservableCollection<Telefon> telefons;
	ListView list;
	Entry entryNimetus, entryTootja, entryHind;

	// Muutujad pildi valimise jaoks
	string valitudPildiTee = "";
	Label lblValitudPilt;

	public ListPage()
	{
		this.Title = "Telefonide haldus";

		// Algandmete  laadimine
		telefons = new ObservableCollection<Telefon>
		{
			new Telefon { Nimetus = "Samsung Galaxy S22 Ultra", Tootja="Samsung", Hind=1349, Pilt="cow_emoji.png"},
			new Telefon { Nimetus = "Xiaomi Mi 11 Lite 5G NE", Tootja="Xiaomi", Hind=399, Pilt="cow_emoji.png"},
			new Telefon { Nimetus = "iPhone 13 Mini", Tootja="Apple", Hind=1179, Pilt="lehmnostardamus.png"},
		};


		// 1. SISESTUSVÄLJAD
		entryNimetus = new Entry { Placeholder = "Telefoni mudel (nt iPhone 14)" };
		entryTootja = new Entry { Placeholder = "Tootja (nt Apple)" };
		entryHind = new Entry { Placeholder = "Hind (täisarv)", Keyboard = Keyboard.Numeric };

		// 2. PILDI VALIMISE KONTROLLID
		Button btnValiPilt = new Button { Text = "Vali pilt galleriist", BackgroundColor = Colors.LightBlue };
		btnValiPilt.Clicked += BtnValiPilt_Clicked;

		lblValitudPilt = new Label { Text = "Pilti pole valitud (kasutatakse vaikimisi pilti)", FontSize = 12, TextColor = Colors.Gray };

		// 3. LISAMISE JA KUSTUTAMISE NUPUD
		Button btnLisa = new Button { Text = "Lisa telefon", BackgroundColor = Colors.LightGreen };
		btnLisa.Clicked += Lisa_Clicked;

		Button btnKustuta = new Button { Text = "Kustuta valitud telefon", BackgroundColor = Colors.LightPink };
		btnKustuta.Clicked += Kustuta_Clicked;

		// 4. LISTVIEW JA SELLE KUJUNDUS

		list = new ListView
		{
			HasUnevenRows = true,
			ItemsSource = telefons,
			SelectionMode = ListViewSelectionMode.Single
		};

		list.ItemTapped += List_ItemTapped;

		list.ItemTemplate = new DataTemplate(() =>
		{
			// Pildi element
			Image imgPilt = new Image
			{
				HeightRequest = 50,
				WidthRequest = 50,
				Aspect = Aspect.AspectFit,
				VerticalOptions = LayoutOptions.Center,
				Margin = new Thickness(0, 0, 10, 0) // Veeris paremal
			};
			imgPilt.SetBinding(Image.SourceProperty, "Pilt");

			// Tekstide elemendid
			Label lblNimetus = new Label { FontSize = 18, FontAttributes = FontAttributes.Bold };
			lblNimetus.SetBinding(Label.TextProperty, "Nimetus");

			Label lblTootja = new Label { TextColor = Colors.Gray };
			lblTootja.SetBinding(Label.TextProperty, "Tootja");

			Label lblHind = new Label { TextColor = Colors.DarkBlue, FontAttributes = FontAttributes.Bold };
			lblHind.SetBinding(Label.TextProperty, new Binding("Hind", stringFormat: "{0} €"));

			var textLayout = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.Center,
				Children = { lblNimetus, lblTootja, lblHind }
			};

			var rowLayout = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness(10),
				Children = { imgPilt, textLayout }
			};

			return new ViewCell { View = rowLayout};
		});

		// 5. PANEME KÕIK LEHELE KOKKU
		this.Content = new StackLayout
		{
			Padding = new Thickness(10),
			Children =
			{
				entryNimetus,
				entryTootja,
				entryHind,
				btnValiPilt, // Uus nupp galerii jaoks
				lblValitudPilt, // Tagasiside silt
				btnLisa,
				btnKustuta,
				list
			}
		};

		// InitializeComponent();
	}

	// --------- EVENT HANDLERS -----------

	// Pildi valimine galeriist
	private async void BtnValiPilt_Clicked(object sender, EventArgs e)
	{
		try
		{
			var photo = await MediaPicker.Default.PickPhotoAsync();

			if (photo != null)
			{
				valitudPildiTee = photo.FullPath; // Jätame asukohta meelde
				lblValitudPilt.Text = $"Valitud: {photo.FileName}";
				lblValitudPilt.TextColor = Colors.Green;
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Viga", "Pildi valimine ebaõnnestus" + ex.Message, "OK");
		}
	}

	private void Lisa_Clicked(object sender, EventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(entryNimetus.Text) && !string.IsNullOrWhiteSpace(entryTootja.Text))
		{
			int hind = 0;
			int.TryParse(entryHind.Text, out hind);

			// Kui pilti ei valitud, kasutame vaikimisi faili
			string pildiNimi = string.IsNullOrWhiteSpace(valitudPildiTee) ? "cow_emoji.png" : valitudPildiTee;

			telefons.Add(new Telefon
			{
				Nimetus = entryNimetus.Text,
				Tootja = entryTootja.Text,
				Hind = hind,
				Pilt = pildiNimi
			});

			// Puhastame väljad uue sisestuse jaoks
			entryNimetus.Text = "";
			entryTootja.Text = "";
			entryHind.Text = "";

			// Lähtestame pildi valiku oleku
			valitudPildiTee = "";
			lblValitudPilt.Text = "Pilti pole valitud (kasutatakse vaikimisi pilti)";
			lblValitudPilt.TextColor = Colors.Gray;
		}
		else
		{
			DisplayAlert("Viga", "Palun täida vähemalt mudeli ja tootja välja!", "OK");
		}
	}

	// Uue telefoni lisamine
	private async void Kustuta_Clicked(object Sender, EventArgs e)
	{
		Telefon valitudTelefon = list.SelectedItem as Telefon;

		if (valitudTelefon != null)
		{
			bool vastus = await DisplayAlert("Kinnitus", $"Kas oled kindel, et soovid mudeli {valitudTelefon.Nimetus} kustutada?", "Jah", "Ei");

			if (vastus == true)
			{
				telefons.Remove(valitudTelefon);
				list.SelectedItem = null;
			}
		}
		else
		{
			await DisplayAlert("Viga", "Palun vali nimekirjast telefon, mida soovid kustutada.", "OK");
		}

		// Loendis reale vajutamine
	}

	private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
	{
		Telefon valitudTelefon = e.Item as Telefon;

		if (valitudTelefon != null)
		{
			await DisplayAlert("Telefoni info", $"Tootja: {valitudTelefon.Tootja}\nMudel: {valitudTelefon.Nimetus}\nHind: {valitudTelefon.Hind} €", "Sulge");
		}
	}
}