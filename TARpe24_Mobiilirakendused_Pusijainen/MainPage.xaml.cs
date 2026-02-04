using AndroidX.RecyclerView.Widget;
using System.Runtime.InteropServices.ObjectiveC;

namespace TARpe24_Mobiilirakendused_Pusijainen
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        bool LeftRight = false;
        bool finished = false;
        
        public MainPage()
        {
            InitializeComponent();
            ResetBtn.BackgroundColor = Color.FromRgba(255, 69, 0, 0);
            Cow.Scale = 0.1;

        }
        private void ResetScore(object? sender, EventArgs e)
        {
            count = 0;
            CounterBtn.Text = $"Pet me";
            Cow.Rotation = 0;
            Cow.IsVisible = true;
            ResetBtn.BackgroundColor = Color.FromRgba(255, 69, 0, 0);
            Cow.Scale = 0.1;

            if (LeftRight == false)
            {
                Cow.HorizontalOptions = LayoutOptions.End;
            }
            else if (LeftRight == true)
            {
                Cow.HorizontalOptions = LayoutOptions.Start;

            }

            LeftRight = !LeftRight;
            finished = false;
            /*
            var rnd = new Random();
            var rndColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            BackgroundColor = rndColor; // värvi muutmine*/
            SemanticScreenReader.Announce(CounterBtn.Text);
        }
        private void OnCounterClicked(object? sender, EventArgs e)
        {
            if (finished == true) return;
            count++;

            if (count == 1)
                CounterBtn.Text = $"Petted {count} time";
            else
                CounterBtn.Text = $"Petted {count} times";

            Cow.Rotation += 40;

            if (count == 10)
            {
                CounterBtn.Text = $"Cow has gone for a walk. Click reset!";
                Cow.IsVisible = false;
                finished = true;
            }

            double countF = count;
            double percent = countF / 10;
            double aF = Math.Round(percent * 255, 0);
            int a = Convert.ToInt32(aF);
            ResetBtn.BackgroundColor = Color.FromRgba(255, 69, 0, a);

            Cow.Scale = countF / 10;
            
            Console.WriteLine(a.ToString());
            Console.WriteLine(ResetBtn.BackgroundColor.ToString());

            SemanticScreenReader.Announce(CounterBtn.Text);

        }
    }
}
