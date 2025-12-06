using Godot;

public partial class BottomUI : Control
{
	[Export] public Label PlayerLabel1;
	[Export] public Label PlayerLabel2;
	[Export] public Label PlayerLabel3;
	[Export] public Label PlayerLabel4;

	public override void _Ready()
	{
		// Set player names
		PlayerLabel1.Text = "Player 1";
		PlayerLabel2.Text = "Player 2";
		PlayerLabel3.Text = "Player 3";
		PlayerLabel4.Text = "Player 4";

		// Center text horizontally
		PlayerLabel1.HorizontalAlignment = HorizontalAlignment.Center;
		PlayerLabel2.HorizontalAlignment = HorizontalAlignment.Center;
		PlayerLabel3.HorizontalAlignment = HorizontalAlignment.Center;
		PlayerLabel4.HorizontalAlignment = HorizontalAlignment.Center;

		// Center text vertically
		PlayerLabel1.VerticalAlignment = VerticalAlignment.Center;
		PlayerLabel2.VerticalAlignment = VerticalAlignment.Center;
		PlayerLabel3.VerticalAlignment = VerticalAlignment.Center;
		PlayerLabel4.VerticalAlignment = VerticalAlignment.Center;
	}

	// Optional: change a player name dynamically
	public void SetPlayerName(int index, string name)
	{
		switch (index)
		{
			case 1: PlayerLabel1.Text = name; break;
			case 2: PlayerLabel2.Text = name; break;
			case 3: PlayerLabel3.Text = name; break;
			case 4: PlayerLabel4.Text = name; break;
		}
	}
}
