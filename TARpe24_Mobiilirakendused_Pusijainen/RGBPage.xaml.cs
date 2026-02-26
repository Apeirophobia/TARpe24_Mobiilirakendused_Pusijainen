using Microsoft.Maui.Layouts;

namespace TARpe24_Mobiilirakendused_Pusijainen;

public partial class RGBPage : ContentPage
{
	Frame colorFrame;

	Frame rFrame;
	Slider rSlider;
	Label rLabel;

	Frame gFrame;
	Slider gSlider;
	Label gLabel;

	Frame bFrame;
	Slider bSlider;
	Label bLabel;

	Stepper alphaStepper;
	Label aLabel;

	AbsoluteLayout al;
	public RGBPage()
	{
		// InitializeComponent();

		colorFrame = new Frame
		{
			BackgroundColor = Colors.LightGray
		};

		rFrame = new Frame
		{
			WidthRequest = 20,
			HeightRequest = 20,
			BackgroundColor = Colors.LightGray
		};

		rSlider = new Slider
		{
			Minimum = 0,
			Maximum = 255,
			HorizontalOptions = LayoutOptions.Center,
			WidthRequest = 200
		};

		rLabel = new Label
		{
			FontSize = 12,
			WidthRequest = 50,
            Text = $"Red: {rSlider.Value}"
        };

        gFrame = new Frame
        {
            WidthRequest = 20,
            HeightRequest = 20,
            BackgroundColor = Colors.LightGray
        };

        gSlider = new Slider
		{
			Minimum = 0,
			Maximum = 255,
			HorizontalOptions = LayoutOptions.Center,
			WidthRequest = 200
		};

        gLabel = new Label
        {
            FontSize = 12,
            WidthRequest = 50,
            Text = $"Green: {gSlider.Value}"
        };


        bFrame = new Frame
        {
            WidthRequest = 20,
            HeightRequest = 20,
            BackgroundColor = Colors.LightGray
        };

        bSlider = new Slider
		{
			Minimum = 0,
			Maximum = 255,
			HorizontalOptions = LayoutOptions.Center,
			WidthRequest = 200
		};

        bLabel = new Label
        {
            FontSize = 12,
            WidthRequest = 50,
            Text = $"Blue: {bSlider.Value}"
        };

		alphaStepper = new Stepper
		{
			Minimum = 0,
			Maximum = 255,
			Increment = 5,
			Value = 255,
		};

		aLabel = new Label
        {
            FontSize = 12,
            WidthRequest = 50,
            Text = $"Alpha: {alphaStepper.Value}"
        };

        rSlider.ValueChanged += SliderValueChanged;
		gSlider.ValueChanged += SliderValueChanged;
		bSlider.ValueChanged += SliderValueChanged;
		alphaStepper.ValueChanged += SliderValueChanged;
		

		al = new AbsoluteLayout { Children = { colorFrame, alphaStepper, aLabel, rFrame, rSlider, rLabel, gFrame, gSlider, gLabel, bFrame, bSlider, bLabel,  } };
		List<View> controls = new List<View> { colorFrame, alphaStepper, rSlider, gSlider, bSlider,  };
		List<View> frames = new List<View> { colorFrame, aLabel, rFrame, gFrame, bFrame };
		List<View> outputLabels = new List<View> { colorFrame, aLabel, rLabel, gLabel, bLabel};
		for (int i = 0; i < controls.Count; i++)
		{
			double yKoht = 0.15 + (double)i * 0.15;
			AbsoluteLayout.SetLayoutBounds(controls[i], new Rect(0.5, yKoht, 300, 60));
			AbsoluteLayout.SetLayoutFlags(controls[i], AbsoluteLayoutFlags.PositionProportional);

			if (i > 0)
			{
				if (i > 1)
				{
                    AbsoluteLayout.SetLayoutBounds(frames[i], new Rect(0.4, yKoht - 0.02, 50, 60));
                    AbsoluteLayout.SetLayoutFlags(frames[i], AbsoluteLayoutFlags.PositionProportional);
                }
				

				AbsoluteLayout.SetLayoutBounds(outputLabels[i], new Rect(0.5, yKoht + 0.05, 100, 60));
                AbsoluteLayout.SetLayoutFlags(outputLabels[i], AbsoluteLayoutFlags.PositionProportional);

            }
        }
		Content = al;

	}

	public void SliderValueChanged(object? sender, ValueChangedEventArgs e)
	{
		colorFrame.BackgroundColor = Color.FromRgba((int)rSlider.Value, (int)gSlider.Value, (int)bSlider.Value, (int)alphaStepper.Value);
		if (sender == rSlider)
		{
            rLabel.Text = String.Format("Red: {0}", (int)e.NewValue);
			rFrame.BackgroundColor = Color.FromRgb((int)e.NewValue, 0, 0);
        }
		if (sender == gSlider)
		{
            gLabel.Text = String.Format("Green: {0}", (int)e.NewValue);
			gFrame.BackgroundColor = Color.FromRgb(0, (int)e.NewValue, 0);
        }
		if (sender == bSlider)
		{
            bLabel.Text = String.Format("Blue: {0}", (int)e.NewValue);
			bFrame.BackgroundColor = Color.FromRgb(0, 0, (int)e.NewValue);
        }

		if (sender == alphaStepper)
		{
			aLabel.Text = String.Format("Alpha: {0}", (int)e.NewValue);
		}

    }
}