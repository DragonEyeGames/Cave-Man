using Godot;
using System;

public partial class Score : Label
{
	// Total points
	public int score { get; private set; } = 0;

	// Combo system
	private int comboCount = 0;
	private float comboTimer = 0f;
	private float comboResetTime = 2f; // Seconds before combo resets

	// Kill streak system
	private int killStreak = 0;

	public override void _Process(double delta)
	{
		// Combo timer countdown
		if (comboCount > 0)
		{
			comboTimer -= (float)delta;

			if (comboTimer <= 0)
			{
				comboCount = 0;
			}
		}

		// Points for staying alive
		AddScore((int)(1 * delta)); // 1 point per second alive
	}

	// Call this when player kills an enemy
	public void OnEnemyKilled()
	{
		killStreak++;
		comboCount++;

		comboTimer = comboResetTime;

		int basePoints = 10;
		int comboBonus = comboCount * 2;
		int killStreakBonus = killStreak * 3;

		int totalEarned = basePoints + comboBonus + killStreakBonus;

		AddScore(totalEarned);
	}

	// Reset streak when player gets hit
	public void OnPlayerHit()
	{
		killStreak = 0;
	}

	// Add points
	private void AddScore(int amount)
	{
		score += amount;
		GD.Print("Score: " + score);

		// Increase game craziness based on score
		AdjustGameDifficulty();
	}

	// Makes more objects appear as score increases
	private void AdjustGameDifficulty()
	{
		if (score > 1000)
		{GD.Print("Difficulty level: CHAOS MODE");
			// spawn more enemies, objects, etc.
		}
		else if (score > 500)
		{
			this.Text = "";
			GD.Print("Difficulty level: HIGH");
		}
		else if (score > 200)
		{
			GD.Print("Difficulty level: MEDIUM");
		}
	}
}
