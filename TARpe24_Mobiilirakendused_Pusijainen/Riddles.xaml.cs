using Microsoft.Maui.Layouts;

namespace TARpe24_Mobiilirakendused_Pusijainen;

public partial class Riddles : ContentPage
{
	int points;
	Button startButton;
	AbsoluteLayout al;
	public Riddles()
	{
		points = 0;

		startButton = new Button
		{
			Text = "Sisene mõistatuste telki",
			WidthRequest = 200,
			HeightRequest = 100,
			VerticalOptions = LayoutOptions.Start,
			HorizontalOptions = LayoutOptions.Center
		};

		BackgroundImageSource = "moistatustetelk.png";

		startButton.Clicked += startButton_Clicked;


		al = new AbsoluteLayout
		{
			Children = { startButton }
		};

		AbsoluteLayout.SetLayoutBounds(startButton, new Rect(0.7, 0.4, startButton.Width, startButton.Height));
		AbsoluteLayout.SetLayoutFlags(startButton, AbsoluteLayoutFlags.PositionProportional);
		Content = al;

	}

	public async void Finish()
	{
        bool valik = await DisplayAlertAsync("Mõistatused said otsa", $"Kahjuks mõistatused said otsa \nPunktid: {points}/4",
            "Proovi uuesti", "Välju mõistatuste telgist");
		if (valik)
        {
            StartRiddles();
            return;
        }
        BackgroundImageSource = "moistatustetelk.png";
        startButton.IsVisible = true;

    }

    public async void FourthRiddle()
	{
		string vastus = await DisplayPromptAsync("Mõistatus 4", "Kes on kõige parem muruniiduk?");
		
		if (vastus == null)
		{
			Finish();
			return;
		}

		if (vastus.ToLower().TrimStart().TrimEnd() == "lehm")
		{
			points += 1;
		}
		Finish();
	}

	public async void ThirdRiddle()
	{
		string valik = await DisplayActionSheetAsync("Söö ära ... ja ole nagu Dalai-Lama", "Loobu", null, "põhiseadused", "kooki", "muru", "lehma");
        if (valik == null)
        {
            FourthRiddle();
            return;
        }

        if (valik == "põhiseadused")
		{
			points += 1;
		}
		FourthRiddle();
	}

	public async void SecondRiddle()
	{
		string valik = await DisplayActionSheetAsync("Kuuba juured, aga pesitseb Lasnamäel", "Loobu", null, "Kuusk", "Seen", "Enrique", "Papagoi");
        if (valik == null)
        {
            ThirdRiddle();
            return;
        }

        if (valik == "Enrique")
		{
			points += 1;
		}
		ThirdRiddle();
	}

	public async void FirstRiddle()
	{
		bool valik = await DisplayAlertAsync("Mõistatus 1", "Pisa torn on tehtud pitsast?", "Jah", "Ei");

        if (valik == null)
        {
            SecondRiddle();
            return;
        }

        if (valik == false)
		{
			points += 1;
		}

		SecondRiddle();
	}

	public async void startButton_Clicked(object? sender, EventArgs e)
	{

		startButton.IsVisible = false;
        StartRiddles();
	}

	public async void StartRiddles()
	{
        points = 0;
        BackgroundImageSource = "lehmnostardamus.png";

        await DisplayPromptAsync("Tere!", "Mis on sinu nimi?");
		FirstRiddle();
	}
}