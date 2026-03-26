namespace TARpe24_Mobiilirakendused_Pusijainen;

public partial class ContactPage : ContentPage
{
	TableView tableView;
	ImageCell ic;
	EntryCell phoneMail;
	Button emailButton;

	public ContactPage()
	{

		ic = new ImageCell
		{
			ImageSource = ImageSource.FromFile("kitarr.png")
		};

		phoneMail = new EntryCell
		{
			Label = "Saada email või SMS",
			Placeholder = "Sisesta email või telefoninumber",
			Keyboard = Keyboard.Text
		};

		emailButton = new Button
		{
			Text = "Saada email"
		};

		emailButton.Clicked += Saada_email_Clicked;

		tableView = new TableView
		{
			Intent = TableIntent.Form,
			Root = new TableRoot
			{
				new TableSection("Kontaktid:")
				{
					new EntryCell
					{
						Label = "Nimi",
						Placeholder = "Sisesta nimi",
						Keyboard = Keyboard.Text
					},
					ic,
					new EntryCell
					{
						Label = "Email",
						Placeholder = "Sisesta email",
						Keyboard = Keyboard.Email
					},
					new EntryCell
					{
						Label = "Telefon",
						Placeholder = "Sisesta telefoninumber",
						Keyboard = Keyboard.Telephone
					},
					new EntryCell
					{
						Label = "Kirjeldus",
						Placeholder = "Kirjeldus",
						Keyboard = Keyboard.Text
					},
					phoneMail
				},

			}
		};

		HorizontalStackLayout hst = new HorizontalStackLayout
		{
			Children = {emailButton}
		};

		VerticalStackLayout vst = new VerticalStackLayout
		{
			Children = {tableView, hst}
		};
		Content = vst;
	}

    private async void Saada_email_Clicked(object? sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(phoneMail.Text)) return;
		var message = "Tere tulemast! Saadan email";
		EmailMessage e_mail = new EmailMessage()
		{
			Subject = phoneMail.Text,
			Body = message,
			BodyFormat = EmailBodyFormat.PlainText,
			To = new List<string>(new[] {phoneMail.Text})

		};

		if (Email.Default.IsComposeSupported)
		{
			await Email.Default.ComposeAsync(e_mail);
		}
		else
		{
			await DisplayAlertAsync("Viga", "Emaili saatmine pole selles seadmes toetatud", "OK");
		}
	}
}