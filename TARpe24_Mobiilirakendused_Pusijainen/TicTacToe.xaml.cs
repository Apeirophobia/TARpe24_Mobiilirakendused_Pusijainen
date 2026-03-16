
namespace TARpe24_Mobiilirakendused_Pusijainen;

public partial class TicTacToe : ContentPage
{
	Grid mainGrid = new Grid();
    Image x = new Image { Source = "x.png" };
    Image o = new Image { Source = "o.png" };
	bool XO = false;

    public TicTacToe()
	{

		for (int i = 0; i < 3; i++)
		{
			mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
			mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
		}

		for (int row = 0; row < 3; row++)
		{
			for (int column = 0; column < 3; column++)
			{
				BoxView box = new BoxView
				{
					BackgroundColor  = Colors.Black
				};

				mainGrid.Add(box, column, row);

				int r = row;
				int c = column;
				TapGestureRecognizer tap = new TapGestureRecognizer();
				tap.Tapped += async (sender, args) =>
				{
					box.BackgroundColor = Colors.Black;
					await DisplayAlertAsync("Koordinaadid", $"Rida: {r+1} Veerg: {c+1}", "Selge");
					
					ReplaceBoxWithAnImage(mainGrid.IndexOf(box), r, c);
				};

				box.GestureRecognizers.Add(tap);
			}
		}

        Content = mainGrid;
		
	}

	public async void ReplaceBoxWithAnImage(int index, int row, int column)
	{
		mainGrid.RemoveAt(index);
        Image x = new Image { Source = "x.png",  };
        Image o = new Image { Source = "o.png" };

		if (!XO)
		{
            mainGrid.Add(x, column, row);

        }
        else if (XO)
		{
			mainGrid.Add(o, column, row);
		}

		XO = !XO;
		/*
        BoxView box = new BoxView
        {
            BackgroundColor = Colors.Red
        };

        mainGrid.Add(box, column, row);
		*/
        return;
	}

	public async void CheckHorizontalRows()
	{
		List<int> combinationOne = new List<int>() { 0, 1, 2};
		bool xWin = false;
		bool oWin = false;

		foreach (int ix in combinationOne)
		{
			xWin = false;
			if (mainGrid.Children[ix] is Image)
			{
				xWin = true;
			}
		}
	}
}