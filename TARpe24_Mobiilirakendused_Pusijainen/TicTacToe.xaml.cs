
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
					//await DisplayAlertAsync("Koordinaadid", $"Rida: {r+1} Veerg: {c+1}", "Selge");
					
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
        Image x = new Image { Source = "x.png", ZIndex = 0};
        Image o = new Image { Source = "o.png", ZIndex = 1};

		if (!XO)
		{
            mainGrid.Add(x, column, row);
			//if (mainGrid.GetRow(x) == 0)
			//{
			// CheckHorizontalRows(mainGrid.GetRow(x));
			// CheckVerticalRows(row, column);
			CheckGameState(row, column);
			
			//}
        }
        else if (XO)
		{
			mainGrid.Add(o, column, row);
            //if (mainGrid.GetRow(o) == 0)
            //{
            //CheckHorizontalRows(mainGrid.GetRow(o));
            //}
            CheckGameState(row, column);

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

	public async Task< List<bool>> CheckHorizontalRows(int rowIndex)
	{
		List<int> combinationOne = new List<int>() {0, 1, 2, 3, 4, 5, 6, 7, 8};
		bool xWin = true;
		bool oWin = true;

		int imageCount = 0;

        foreach (int ix in combinationOne) // Checks every cell
		{

            if (mainGrid.Children[ix] is Image) // if cell is an image
			{
                if (mainGrid.GetRow(mainGrid.Children[ix]) == rowIndex) // Checks if looping cell is in selected row
				{
                    //await DisplayAlertAsync("Excuse me", $"Found an image: {mainGrid.Children[ix].ZIndex}", "OK"); // Displays alert with image's ZIndex (0 = X; 1 = O)
					imageCount += 1; // Add to image counter to check if row is full
					if (mainGrid.Children[ix].ZIndex == 1) // If row contains O, xWin = false
					{
						xWin = false;
					}

					if (mainGrid.Children[ix].ZIndex == 0) // If row contains X, oWin = false
					{
						oWin = false;
					}
                }

            }
		}


		if (imageCount < 3) // if row is not full, both bools set to false.
		{
			xWin = false;
			oWin = false;
		}

        List<bool> winStates = new List<bool>() { xWin, oWin };

        // await DisplayAlertAsync("Excuse me", $"X: {xWin} O: {oWin}", "OK"); // Display current state
		return winStates;
	}

	public async Task<List<bool>> CheckVerticalRows(int rowIndex, int columnIndex)
	{
        List<int> combinationOne = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        bool xWin = true;
        bool oWin = true;
        int imageCount = 0;

        foreach (int ix in combinationOne) // Checks every cell
        {

            if (mainGrid.Children[ix] is Image) // if cell is an image
            {
                if (mainGrid.GetColumn(mainGrid.Children[ix]) == columnIndex) // Checks if looping cell is in selected row
                {
                    //if (mainGrid.GetRow(mainGrid.Children[ix]) == rowIndex)
					//{
						//await DisplayAlertAsync("Excuse me", $"Found an image: {mainGrid.Children[ix].ZIndex}", "OK");
                    //}
                    imageCount += 1; // Add to image counter to check if row is full
                    if (mainGrid.Children[ix].ZIndex == 1) // If column contains O, xWin = false
                    {
                        xWin = false;
                    }

                    if (mainGrid.Children[ix].ZIndex == 0) // If column contains X, oWin = false
                    {
                        oWin = false;
                    }
                }
            }
        }

        if (imageCount < 3) // if row is not full, both bools set to false.
        {
            xWin = false;
            oWin = false;
        }

        List<bool> winStates = new List<bool>() { xWin, oWin };

        // await DisplayAlertAsync("Excuse me", $"X: {xWin} O: {oWin}", "OK"); // Display current state
        return winStates;
    }

	public async void CheckDiagonalRows()
	{
        List<int> combinationOne = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        bool xWin = true;
        bool oWin = true;
        int imageCount = 0;

		foreach (var child in mainGrid.Children)
		{
			if (mainGrid.GetRow(child) == 0 && mainGrid.GetColumn(child) == 0)
			{
				if (child.ZIndex == 1) // If column contains O, xWin = false
				{
					xWin = false;
				}

				if (child.ZIndex == 0) // If column contains X, oWin = false
				{
					oWin = false;
				}
				continue;
			}
			else if (mainGrid.GetRow(child) == 1 && mainGrid.GetColumn(child) == 1)
			{
				if (child.ZIndex == 1) // If column contains O, xWin = false
				{
					xWin = false;
				}

				if (child.ZIndex == 0) // If column contains X, oWin = false
				{
					oWin = false;
				}
				continue;
			}
			else if (mainGrid.GetRow(child) == 2 && mainGrid.GetColumn(child) == 2)
			{
                if (child.ZIndex == 1) // If column contains O, xWin = false
                {
                    xWin = false;
                }

                if (child.ZIndex == 0) // If column contains X, oWin = false
                {
                    oWin = false;
                }
            }
		}

	}

	public async void CheckGameState(int rowIndex, int columnIndex)
	{
		List<bool> winStates = new List<bool>() { false, false };
		List<bool> winStatesHorizontal;
		List<bool> winStatesVertical;
		List<bool> winStatesDiagonal;
		winStatesHorizontal = await CheckHorizontalRows(rowIndex);
		if (winStatesHorizontal[0] == true)
		{
			winStates[0] = true;
		}
		else if (winStatesHorizontal[1] == true)
		{
			winStates[1] = true;
		}

		winStatesVertical = await CheckVerticalRows(rowIndex, columnIndex);

		if (winStatesVertical[0] == true)
		{
			winStates[0] = true;
		}
		else if (winStatesVertical[1] == true)
		{
			winStates[1] = true;
		}
		// await DisplayAlertAsync("Excuse me", $"X: {winStates[0]} O: {winStates[1]}", "OK"); // Display current state

		if (winStates[0] == true || winStates[1] == true)
		{
			if (winStates[0])
			{
				GameOver("Player 1 has won");
			} else if (winStates[1])
			{
                GameOver("Player 2 has won");
            }
            return;
        }
    }

	public async void GameOver(string msg)
	{
        await DisplayAlertAsync("Game over", msg, "Start over");
        mainGrid.Clear();
		FillGrid();
    }

    public void FillGrid()
	{
        XO = false;

        for (int row = 0; row < 3; row++)
        {
            for (int column = 0; column < 3; column++)
            {
                BoxView box = new BoxView
                {
                    BackgroundColor = Colors.Black
                };

                mainGrid.Add(box, column, row);

                int r = row;
                int c = column;
                TapGestureRecognizer tap = new TapGestureRecognizer();
                tap.Tapped += async (sender, args) =>
                {
                    box.BackgroundColor = Colors.Black;
                    //await DisplayAlertAsync("Koordinaadid", $"Rida: {r+1} Veerg: {c+1}", "Selge");

                    ReplaceBoxWithAnImage(mainGrid.IndexOf(box), r, c);
                };

                box.GestureRecognizers.Add(tap);
            }
        }
    }

}