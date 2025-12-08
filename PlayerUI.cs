using Godot;
using System;

public partial class PlayerUI : Control
{
	private ProgressBar healthBar;
	private Label scoreLabel;
	
	public override void _Ready() {
		healthBar = GetNode<ProgressBar>("VFlowContainer/health");
		scoreLabel = GetNode<Label>("VFlowContainer/score");
	}
	
	
	private int _maxHealth = 100;
	// Current health starts full
	private int _currentHealth = 100;
	public int score { get; private set; } = 0;

	// Combo system
	private int comboCount = 0;
	private float comboTimer = 0f;
	private float comboResetTime = 2f; // Seconds before combo resets

	// Kill streak system
	private int killStreak = 0;
	
	public override void _Process(double delta)
	{
		scoreLabel.Text = (int.Parse(scoreLabel.Text) + 1).ToString();
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
			scoreLabel.Text = "";
			GD.Print("Difficulty level: HIGH");
		}
		else if (score > 200)
		{
			GD.Print("Difficulty level: MEDIUM");
		}
	}
	
	public void UpdateHealth(int health) {
		return;
	}
	
	// Call this when taking damage
	public void TakeDamage(int damage)
	{
		_currentHealth -= damage;
		if (_currentHealth < 0)
			_currentHealth = 0;
		UpdateHealthBar();
	}

	// Call this when healing
	public void Heal(int amount)
	{
		_currentHealth += amount;
		if (_currentHealth > _maxHealth)
			_currentHealth = _maxHealth;
		UpdateHealthBar();
	}

	// Update the UI
	private void UpdateHealthBar(int health)
	{
		_currentHealth = health;
		healthBar.Value = (float)_currentHealth / _maxHealth * 100f;
	}
	
}
