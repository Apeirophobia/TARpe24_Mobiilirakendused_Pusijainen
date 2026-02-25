
namespace TARpe24_Mobiilirakendused_Pusijainen;

public partial class TrafficLight : ContentPage
{
    Frame redFrame;
    Frame yellowFrame;
    Frame greenFrame;
    Image downtown;
    List<Frame> frames;
    Button switchButton;
    VerticalStackLayout valgusfoor;
    VerticalStackLayout vsl;
    Label outputLabel;
    bool OutIn = false;
    Dictionary<string, string> Modes;
    string ChosenMode = "None";

    public TrafficLight()
	{
        // InitializeComponent();
        TapGestureRecognizer tap = new TapGestureRecognizer();
        TapGestureRecognizer redTap = new TapGestureRecognizer();
        TapGestureRecognizer yellowTap = new TapGestureRecognizer();
        TapGestureRecognizer greenTap = new TapGestureRecognizer();
        Modes = new Dictionary<string, string>
            {
                { "Red", "Seisa" },
                { "Yellow", "Valmistu" },
                { "Green", "Sõida"},
                { "None", "Vali värv" }
            };

        outputLabel = new Label
        {
            Text = "Lülita valgusfoor sisse",
            FontSize = 20,
            HorizontalTextAlignment = TextAlignment.Center
        };

        downtown = new Image
        {
            AnchorX = 0,
            AnchorY = 0,
            ScaleX = 1,
            ScaleY = 1,
            Source = ImageSource.FromUri(new Uri("https://media.istockphoto.com/id/1457198267/photo/pov-of-freight-train-coming-towards-the-camera.webp?s=2048x2048&w=is&k=20&c=Gf6cd0vRTySKU8QdSVxdF9zWI3bu1V4ee4CC4K1kBpg="))
        };

        redFrame = new Frame
        {
            BackgroundColor = Color.FromRgb(0, 0, 0),
            CornerRadius = 100,
            HeightRequest = 150,
            WidthRequest = 150,
            BorderColor = Color.FromRgb(0, 0, 0)
        };

        redFrame.GestureRecognizers.Add(redTap);
        redTap.Tapped += TurnRed;


        yellowFrame = new Frame
        {
            BackgroundColor = Color.FromRgb(0, 0, 0),
            CornerRadius = 100,
            HeightRequest = 150,
            WidthRequest = 150,
            BorderColor = Color.FromRgb(0, 0, 0),
        };

        yellowFrame.GestureRecognizers.Add(yellowTap);
        yellowTap.Tapped += TurnYellow;

        greenFrame = new Frame
        {
            BackgroundColor = Color.FromRgb(0, 0, 0),
            CornerRadius = 100,
            HeightRequest = 150,
            WidthRequest = 150,
            BorderColor = Color.FromRgb(0, 0, 0),
        };

        greenFrame.GestureRecognizers.Add(greenTap);
        greenTap.Tapped += TurnGreen;

        frames = new List<Frame> { redFrame, yellowFrame, greenFrame };


        switchButton = new Button
        {
            Text = "Turn on",
        };
        switchButton.GestureRecognizers.Add(tap);
        tap.Tapped += OnSwitch;

        redFrame.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(async () =>
            {
                await Task.WhenAll(
                    downtown.FadeTo(0.5, 150)
                );
                await Task.WhenAll(
                    downtown.FadeTo(1.0, 150)
                );
            })
        });

        valgusfoor = new VerticalStackLayout
        {
            Spacing = 20,
            Padding = 20,
            VerticalOptions = LayoutOptions.Center
        };

        for (int i = 0; i < frames.Count; i++)
        {
            valgusfoor.Add(frames[i]);
        }

        vsl = new VerticalStackLayout
        {
            Spacing = 20,
            VerticalOptions = LayoutOptions.Center,
            Children = { outputLabel, downtown, valgusfoor, switchButton }
        };

        Content = vsl;
        UpdateOutputText();
    }

    private void TurnRed(object? sender, EventArgs e)
    {
        if (OutIn == false)
        {
            redFrame.BackgroundColor = Color.FromRgb(0, 0, 0);
            return;
        }
        ChosenMode = "Red";
        redFrame.BackgroundColor = Color.FromRgb(255, 0, 0);
        TurnOff(ChosenMode);

        UpdateOutputText();

    }

    private void TurnYellow(object? sender, EventArgs e)
    {
        if (OutIn == false)
        {
            yellowFrame.BackgroundColor = Color.FromRgb(0, 0, 0);
            return;
        }
        ChosenMode = "Yellow";
        yellowFrame.BackgroundColor = Color.FromRgb(255, 255, 0);
        TurnOff(ChosenMode);

        UpdateOutputText();

    }

    private void TurnGreen(object? sender, EventArgs e)
    {
        if (OutIn == false)
        {
            greenFrame.BackgroundColor = Color.FromRgb(0, 0, 0);
            return;
        }
        ChosenMode = "Green";
        greenFrame.BackgroundColor = Color.FromRgb(0, 255, 0);
        TurnOff(ChosenMode);
        UpdateOutputText();
    }

    private void OnSwitch(object? sender, EventArgs e)
    {
        OutIn = !OutIn;
        switchButton.Text = $"Switch State: {OutIn}";
        if (!OutIn)
        {
            TurnOff("None");
            ChosenMode = "None";
        }

        UpdateOutputText();
    }

    private void TurnOff(string otherThan)
    {
        if (otherThan == "None")
        {
            redFrame.BackgroundColor = Color.FromRgb(0, 0, 0);
            yellowFrame.BackgroundColor = Color.FromRgb(0, 0, 0);
            greenFrame.BackgroundColor = Color.FromRgb(0, 0, 0);
            return;
        }

        if (otherThan == "Red")
        {
            yellowFrame.BackgroundColor = Color.FromRgb(0, 0, 0);
            greenFrame.BackgroundColor = Color.FromRgb(0, 0, 0);
        }

        else if (otherThan == "Yellow")
        {
            redFrame.BackgroundColor = Color.FromRgb(0, 0, 0);
            greenFrame.BackgroundColor = Color.FromRgb(0, 0, 0);
        }

        else if (otherThan == "Green")
        {
            redFrame.BackgroundColor = Color.FromRgb(0, 0, 0);
            yellowFrame.BackgroundColor = Color.FromRgb(0, 0, 0);
        }
    }
    private void UpdateOutputText()
    {
        if (OutIn == false)
        {
            outputLabel.Text = "Lülita valgusfoor sisse";
            return;
        }
        outputLabel.Text = Modes[ChosenMode];
    }
}