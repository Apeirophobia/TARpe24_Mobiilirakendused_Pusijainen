using System.Collections.ObjectModel;

namespace TARpe24_Mobiilirakendused_Pusijainen;

public class Riik
{
    public string Nimi { get; set; }
    public string Pealinn { get; set; }
    public int Rahvaarv { get; set; }
    public string Lipp { get; set; }
}

public partial class RiikPage : ContentPage
{
    ObservableCollection<Riik> riigid;
    ListView list;
    Entry entryNimetus, entryPealinn, entryRahvaarv;

    string valitudPildiTee = "";
    Label lblValitudPilt;

    public RiikPage()
    {
        this.Title = "TRANS-OIROPA EXPRESS";

        // Algandmete laadimine
        riigid = new ObservableCollection<Riik>
        {
            new Riik
            {
                Nimi = "Serbia",
                Pealinn = "Belgrade",
                Rahvaarv = 6567783,
                Lipp = "serbia.png"
            }
        };

        // SISESTUSVÄLJAD

        entryNimetus = new Entry { Placeholder = "Nimi" };
        entryPealinn = new Entry { Placeholder = "Pealinn" };
        entryRahvaarv = new Entry { Placeholder = "Rahvaarv" };

        // PILDI VALIMISE KONTROLLID
        Button btnValiPilt = new Button 
        { Text = "Vali pilt galleriist", 
            BackgroundColor = Colors.LightBlue };

        btnValiPilt.Clicked += BtnValiPilt_Clicked;
        lblValitudPilt = new Label
        {
            Text = "Vali pilt galleeriist",
            FontSize = 12,
            TextColor = Colors.Gray
        };

        // Lisamine ja kustutamine

        Button btnLisa = new Button { Text = "Lisa riik", BackgroundColor = Colors.LightGreen };
        btnLisa.Clicked += Lisa_Clicked;

        Button btnKustuta = new Button { Text = "Kustuta valitud riik", BackgroundColor  = Colors.Red};
        btnKustuta.Clicked += Kustuta_Clicked;
        
        // LISTVIEW JA SELLE KUJUNDUS
        list = new ListView
        {
            HasUnevenRows = true,
            ItemsSource = riigid,
            SelectionMode = ListViewSelectionMode.Single
        };

        // list.ItemTapped 
        list.ItemTemplate = new DataTemplate(() =>
        {
            Image imgPilt = new Image
            {
                HeightRequest = 50,
                WidthRequest = 50,
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 10, 0) // Veeris
            };

            imgPilt.SetBinding(Image.SourceProperty, "Lipp");


            Label lblNimetus = new Label { FontSize = 18, FontAttributes = FontAttributes.Bold };
            lblNimetus.SetBinding(Label.TextProperty, "Nimi");

            Label lblPealinn = new Label { TextColor = Colors.Gray };
            lblPealinn.SetBinding(Label.TextProperty, "Pealinn");

            Label lblRahvaarv = new Label { TextColor = Colors.DarkBlue, FontAttributes = FontAttributes.Bold };
            lblRahvaarv.SetBinding(Label.TextProperty, new Binding("Rahvaarv", stringFormat: "{0}"));

            var textLayout = new VerticalStackLayout
            {
                //Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Center,
                Children = { lblNimetus, lblPealinn, lblRahvaarv }
            };

            var rowLayout = new HorizontalStackLayout
            {
                //Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(10),
                Children = { imgPilt, textLayout }
            };

            return new ViewCell { View = rowLayout };
        });

        this.Content = new VerticalStackLayout
        {
            Padding = new Thickness(10),
            Children =
            {
                entryNimetus,
                entryPealinn,
                entryRahvaarv,
                btnValiPilt,
                lblValitudPilt,
                btnLisa,
                btnKustuta,
                list
            }
        };
        // InitializeComponent();
    }

    // pildi galeriist
    private async void BtnValiPilt_Clicked(object sender, EventArgs e)
    {
        try
        {
            var photo = await MediaPicker.Default.PickPhotoAsync();

            if (photo != null)
            {
                valitudPildiTee = photo.FullPath;
                lblValitudPilt.Text = $"Valitud: {photo.FileName}";
                lblValitudPilt.TextColor = Colors.Green;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Viga", "Pildi valimine ebaõnnestus " + ex.Message, "OK");
        }
    }

    private void Lisa_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(entryNimetus.Text) && !string.IsNullOrWhiteSpace(entryPealinn.Text))
        {
            int rahvaarv = 0;
            int.TryParse(entryRahvaarv.Text, out rahvaarv);

            string pildiNimi = string.IsNullOrWhiteSpace(valitudPildiTee) ? "cow_emoji.png" : valitudPildiTee;

            riigid.Add(new Riik
            {
                Nimi = entryNimetus.Text,
                Pealinn = entryPealinn.Text,
                Rahvaarv = rahvaarv,
                Lipp = pildiNimi
            });

            entryNimetus.Text = "";
            entryPealinn.Text = "";
            entryRahvaarv.Text = "";

            valitudPildiTee = "";
            lblValitudPilt.Text = "Pilti pole valitud (kastuatakse vaikimisi pilti)";
            lblValitudPilt.TextColor = Colors.Gray;
        }
        else
        {
            DisplayAlertAsync("Viga", "Palun täida vähemalt Nimi ja Pealinna välja!", "OK");
        }
    }

    private async void Kustuta_Clicked(object sender, EventArgs e)
    {
        Riik valitudRiik = list.SelectedItem as Riik;

        if (valitudRiik != null)
        {
            bool vastus = await DisplayAlertAsync("Kinnitus", $"Kas oled kindel, et soovid {valitudRiik.Nimi}, kustutada?", "Jah", "Ei");
            
            if (vastus == true)
            {
                riigid.Remove(valitudRiik);
                list.SelectedItem = null;
            }
            
        }
        else
        {
            await DisplayAlertAsync("Viga", "Palun vali nimekirjast riik, mida soovid kustutada.", "OK");
        }
    }
}