

public partial class healthBar : ProgressBar
{
}
public partial class HealthBar : Control
{
	// The progress bar node reference
	private TextureProgressBar _bar;

	// Max health is 100
	private int _maxHealth = 100;

	// Current health starts full
	private int _currentHealth = 100;

	public override void _Ready()
	{
		// Get the HealthBar node
		_bar = GetNode<TextureProgressBar>("HealthBar");
		UpdateHealthBar();
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
	private void UpdateHealthBar()
	{
		_bar.Value = (float)_currentHealth / _maxHealth * 100f;
	
	
