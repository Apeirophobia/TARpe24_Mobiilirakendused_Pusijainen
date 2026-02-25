using Microsoft.Maui.Layouts;

namespace TARpe24_Mobiilirakendused_Pusijainen;

public partial class RGBPage : ContentPage
{
	Frame colorFrame;
	Slider rSlider;
	Slider gSlider;
	Slider bSlider;
	AbsoluteLayout al;
	public RGBPage()
	{
		// InitializeComponent();

		colorFrame = new Frame
		{
			BackgroundColor = Colors.LightGray
		};
		rSlider = new Slider
		{
			Minimum = 0,
			Maximum = 255,
			HorizontalOptions = LayoutOptions.Center
		};

		gSlider = new Slider
		{
			Minimum = 0,
			Maximum = 255,
			HorizontalOptions = LayoutOptions.Center
		};

		bSlider = new Slider
		{
			Minimum = 0,
			Maximum = 255,
			HorizontalOptions = LayoutOptions.Center
		};

		al = new AbsoluteLayout { Children = { colorFrame, rSlider, gSlider, bSlider } };
		List<View> controls = new List<View> { colorFrame, rSlider, gSlider, bSlider };
		for (int i = 0; i < controls.Count; i++)
		{
			double yKoht = 0.2 + (double)i * 0.2;
			AbsoluteLayout.SetLayoutBounds(controls[i], new Rect(0.5, yKoht, 300, 60));
			AbsoluteLayout.SetLayoutFlags(controls[i], AbsoluteLayoutFlags.PositionProportional);
		}
		Content = al;

	}
}