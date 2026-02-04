namespace TARpe24_Mobiilirakendused_Pusijainen
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Petted {count} time";
            else
                CounterBtn.Text = $"Petted {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
