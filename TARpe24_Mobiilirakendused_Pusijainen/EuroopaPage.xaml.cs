using System.Collections.ObjectModel;
using System.Net.Mime;

namespace TARpe24_Mobiilirakendused_Pusijainen;
/*
public class Riik
{
	public string Nimi { get; set; }
	public string Pealinn { get; set; }
	public int Rahvaarv { get; set; }
	public string Lipp { get; set; }
}

public partial class EuroopaPage : ContentPage
{
	ObservableCollection<Riik> riigid;
	ListView list;

	public EuroopaPage()
	{
		this.Title = "TRANS-OIROPA EXPRESS";

		riigid = new ObservableCollection<Riik>
		{
			new Riik
			{
				Nimi = "Serbia",
				Pealinn = "Belgrade",
				Rahvaarv = 6567783,
				Lipp = "cow_emoji.png"
            }
		};

		list = new ListView
		{
			HasUnevenRows = true,
			ItemsSource = riigid,
			SelectionMode = ListViewSelectionMode.Single
		};

		// list.ItemTapped 
		list.ItemTemplate = new DataTemplate(() =>
		{
			Image imgPilt = new Image
			{
				HeightRequest = 50,
				WidthRequest = 50,
				Aspect = Aspect.AspectFit,
				VerticalOptions = LayoutOptions.Center,
				Margin = new Thickness(0, 0, 10, 0) // Veeris
			};

			Label lblNimetus = new Label { FontSize = 18, FontAttributes = FontAttributes.Bold };
			lblNimetus.SetBinding(Label.TextProperty, "Nimetus");

			var textLayout = new VerticalStackLayout
			{
				//Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.Center,
				Children = {lblNimetus}
			};

			var rowLayout = new HorizontalStackLayout
			{
				//Orientation = StackOrientation.Horizontal,
				Padding = new Thickness(10),
				Children = { imgPilt, textLayout }
			};

			return new ViewCell { View = rowLayout };
		});

		this.Content = new VerticalStackLayout
		{
			Padding = new Thickness(10),
			Children =
			{
				list
			}
		};
		// InitializeComponent();
	}
}

*/