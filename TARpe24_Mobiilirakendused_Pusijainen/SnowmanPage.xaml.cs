using Microsoft.Maui.Layouts;


namespace TARpe24_Mobiilirakendused_Pusijainen;

public partial class SnowmanPage : ContentPage
{
	BoxView bucket;

	Frame head;
	Frame upperBody;
	Frame lowerBody;

    Label opacityLabel;
    Slider opacitySlider;

    Picker pickle;
    Button pickleButton;

    Stepper tempoStepper;

    List<View> parts;

    AbsoluteLayout al;
	
	public SnowmanPage()
	{
		//InitializeComponent();
		bucket = new BoxView
		{
			BackgroundColor = Colors.Gray,
			HeightRequest = 125,
			WidthRequest = 125,
			CornerRadius = 0,
            ZIndex = 4
		};

        head = new Frame
        {
            BackgroundColor = Colors.LightBlue,
            HeightRequest = 125,
            WidthRequest = 125,
            CornerRadius = 125,
            BorderColor = Colors.Black,
            ZIndex = 3
        };


        upperBody = new Frame
        {
            BackgroundColor = Colors.LightBlue,
            HeightRequest = 150,
            WidthRequest = 150,
            CornerRadius = 150,
            BorderColor = Colors.Black,
            ZIndex = 2
        };

        lowerBody = new Frame
        {
            BackgroundColor = Colors.LightBlue,
            HeightRequest = 175,
            WidthRequest = 175,
            CornerRadius = 175,
            BorderColor = Colors.Black,
            ZIndex = 1
        };


        opacitySlider = new Slider
        {
            Minimum = 0,
            Maximum = 1,
            Value = 1,
            WidthRequest = 200,
            HorizontalOptions = LayoutOptions.Center
        };

        opacityLabel = new Label
        {
            Text = $"{opacitySlider.Value}",
            WidthRequest = 200,
            HorizontalOptions = LayoutOptions.Center
        };

        tempoStepper = new Stepper
        {
            Minimum = 60,
            Maximum = 180,
            WidthRequest = 100,
            Value = 130 // Call Me Maybe
        };

        var items = new List<string> {"Peida", "Näita", "Muuda värvi", "Sulata", "Tantsi"};

        pickle = new Picker
        {
            WidthRequest = 200,
            ItemsSource = items
        };

        pickleButton = new Button
        {
            WidthRequest = 100,
            Text = "Aktiveeri"
        };


        opacitySlider.ValueChanged += SetOpacity;
        pickleButton.Pressed += OnPickerSelected;

        al = new AbsoluteLayout { Children = { bucket, head ,upperBody, lowerBody, opacitySlider, opacityLabel, pickle, pickleButton} };
        parts = new List<View> { bucket, head, upperBody, lowerBody };
        
        AbsoluteLayout.SetLayoutBounds(parts[0], new Rect(0.35, 0.15, parts[0].Width, parts[0].Height));
        AbsoluteLayout.SetLayoutFlags(parts[0], AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(parts[1], new Rect(0.35, 0.29, parts[1].Width, parts[1].Height));
        AbsoluteLayout.SetLayoutFlags(parts[1], AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(parts[2], new Rect(0.35, 0.54, parts[2].Width, parts[2].Height));
        AbsoluteLayout.SetLayoutFlags(parts[2], AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(parts[3], new Rect(0.35, 0.86, parts[3].Width, parts[3].Height));
        AbsoluteLayout.SetLayoutFlags(parts[3], AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(opacitySlider, new Rect(0.65, 0.29, opacitySlider.Width, 50));
        AbsoluteLayout.SetLayoutFlags(opacitySlider, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(opacityLabel, new Rect(0.65, 0.35, opacityLabel.Width, 50));
        AbsoluteLayout.SetLayoutFlags(opacityLabel, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(pickle, new Rect(0.65, 0.40, pickle.Width, 50));
        AbsoluteLayout.SetLayoutFlags(pickle, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(pickleButton, new Rect(0.65, 0.50, pickleButton.Width, 50));
        AbsoluteLayout.SetLayoutFlags(pickleButton, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(tempoStepper, new Rect(0.65, 0.6, tempoStepper.Width, 50));
        AbsoluteLayout.SetLayoutFlags(tempoStepper, AbsoluteLayoutFlags.PositionProportional);


        Content = al;
	}

    public void SetOpacity(object? sender, ValueChangedEventArgs e)
    {
        opacityLabel.Text = $"{opacitySlider.Value}";
        for (int i = 0; i < parts.Count; i++)
        {
            parts[i].Opacity = (double)e.NewValue;
        }
    }

    public void OnPickerSelected(object? sender, EventArgs e)
    {
        if (pickle.SelectedItem == null)
        {
            return;
        }
        // opacityLabel.Text = pickle.SelectedItem.ToString();

        switch (pickle.SelectedItem.ToString())
        {
            case "Peida":
                Hide();
                break;
            case "Näita":
                Show();
                break;
            case "Muuda värvi":
                ChangeColor();
                break;
            case "Sulata":
                Melt();
                break;
            case "Tantsi":
                Dance();
                break;
        }
    }

    public void Hide()
    {
        for (int i = 0; i < parts.Count; i++)
        {
            parts[i].IsVisible = false;
        }
    }

    public void Show()
    {
        for (int i = 0; i < parts.Count; i++)
        {
            parts[i].IsVisible = true;
            parts[i].Opacity = opacitySlider.Value;
        }
    }

    public void ChangeColor()
    {
        Random rand = new Random();
        int r = rand.Next(0, 255);
        int g = rand.Next(0, 255);
        int b = rand.Next(0, 255);

        for (int i = 1; i < parts.Count; i++)
        {
            parts[i].BackgroundColor = Color.FromRgb(r, g, b); 
        }
    }

    public async void Melt()
    {
        for (int i = 0; i < parts.Count; i++)
        {
            parts[i].FadeToAsync(0, 1000);
        }
    }

    public async void Dance()
    {
        double bpm = tempoStepper.Value;
        double interval = Math.Round((double)(60 / bpm) * 1000, 0);
        uint animLen = (uint)interval;

        for (int i = 0; i < 4; i++)
        {
            await Task.WhenAll(
            bucket.RotateToAsync(15, animLen, easing: Easing.Linear),
            //head.RotateToAsync(15, 200),
            //upperBody.RotateToAsync(15, 200),
            //lowerBody.RotateToAsync(15, 200),

            head.TranslateToAsync(-15, -10, animLen, Easing.Linear),
            upperBody.TranslateToAsync(-25, -20, animLen, Easing.Linear),
            lowerBody.TranslateToAsync(-35, -30, animLen, Easing.Linear)
            );

            await Task.WhenAll(
                bucket.RotateToAsync(0, animLen, Easing.Linear),
                //head.RotateToAsync(15, 200),
                //upperBody.RotateToAsync(15, 200),
                //lowerBody.RotateToAsync(15, 200),

                head.TranslateToAsync(0, 0, animLen, Easing.Linear),
                upperBody.TranslateToAsync(0, 0, animLen, Easing.Linear),
                lowerBody.TranslateToAsync(0, 0, animLen, Easing.Linear)
                );

            await Task.WhenAll(
                bucket.RotateToAsync(-15, animLen, Easing.Linear),
                //head.RotateToAsync(15, 200),
                //upperBody.RotateToAsync(15, 200),
                //lowerBody.RotateToAsync(15, 200),

                head.TranslateToAsync(15, -10, animLen, Easing.Linear),
                upperBody.TranslateToAsync(25, -20, animLen, Easing.Linear),
                lowerBody.TranslateToAsync(35, -30, animLen, Easing.Linear)
                );

            await Task.WhenAll(
                bucket.RotateToAsync(0, animLen),
                //head.RotateToAsync(15, 200),
                //upperBody.RotateToAsync(15, 200),
                //lowerBody.RotateToAsync(15, 200),

                head.TranslateToAsync(0, 0, animLen, Easing.Linear),
                upperBody.TranslateToAsync(0, 0, animLen, Easing.Linear),
                lowerBody.TranslateToAsync(0, 0, animLen, Easing.Linear)
                );
        }

    }
}