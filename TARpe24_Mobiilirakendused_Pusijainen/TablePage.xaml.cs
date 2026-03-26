
namespace TARpe24_Mobiilirakendused_Pusijainen;

public partial class TablePage : ContentPage
{
	TableView tableView;
	SwitchCell sc;
	ImageCell ic;
	TableSection ctrls;
	TableSection fotoSection;
	public TablePage()
	{
		sc = new SwitchCell { Text = "Näita veel" };
		sc.OnChanged += Sc_OnChanged;
		ic = new ImageCell
		{
			ImageSource = ImageSource.FromFile("cow_emoji.png"),
			Text = "Foto nimetus",
			Detail = "Foto nimetus"
		};

		ctrls = new TableSection()
		{
			new ViewCell
			{
				View = new HorizontalStackLayout
				{
					Children = { new Button { Text = "Helista" }, new Button { Text = "Saada SMS"}, new Button { Text = "Saada Email"} }
				} 
			}
		};

		fotoSection = new TableSection();

		tableView = new TableView()
		{
			Intent = TableIntent.Form,
			Root = new TableRoot
			{
				new TableSection("Kontaktandmed:")
				{
					new EntryCell
					{
						Label = "Telefon",
						Placeholder = "Sisesta tel. number",
						Keyboard = Keyboard.Telephone
					},
					new EntryCell
					{
						Label = "Email",
						Placeholder = "Sisesta email",
						Keyboard = Keyboard.Email
					},
					sc
				},
				fotoSection,
				ctrls
			}
		};

		Content = tableView;
	}

	private void Sc_OnChanged(object sender, ToggledEventArgs e)
	{
		if (e.Value)
		{
			fotoSection.Title = "Foto";
			fotoSection.Add(ic);
			sc.Text = "Peida";
		}
		else
		{
			fotoSection.Title = "";
			fotoSection.Remove(ic);
			sc.Text = "Näita veel";
		}
	}
}