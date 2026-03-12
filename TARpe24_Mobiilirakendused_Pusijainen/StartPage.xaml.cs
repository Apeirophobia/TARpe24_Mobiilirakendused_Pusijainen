using System.Runtime.Intrinsics.Arm;

namespace TARpe24_Mobiilirakendused_Pusijainen;

public partial class StartPage : ContentPage
{
	VerticalStackLayout vst;
	ScrollView sv;
	public List<ContentPage> Lehed = new List<ContentPage>() { new MainPage(), new TrafficLight(), new DateTimePage(), new StepperSliderPage(), new RGBPage(), new SnowmanPage(), new PopUpPage(), new Riddles(), new PickerImageGridPage() };
	public List<string> LeheNimed = new List<string>() { "Lehma Klikker", "Valgusfoor", "Aeg", "Stepper/Slider", "RGB Värvid", "Lumememm", "PopUpPage", "Mõistatused", "Piltide Valimine"};

	public StartPage()
	{
		// Title = "Avaleht";
		vst = new VerticalStackLayout() { Padding = 20, Spacing = 15 };
		for (int i = 0; i < Lehed.Count; i++)
		{
			Button nupp = new Button
			{
				Text = LeheNimed[i],
				FontSize = 36,
				FontFamily = "Segoe Script MT Bold",
				BackgroundColor = Colors.GreenYellow,
				TextColor = Colors.Black,
				CornerRadius = 10,
				HeightRequest = 60,
				ZIndex = i
			};

			vst.Add(nupp);
			nupp.Clicked += (sender, e) =>
			{
				var valik = Lehed[nupp.ZIndex];
				Navigation.PushAsync(valik);
			};

		}

		sv = new ScrollView { Content = vst };
		Content = sv;
	}
}