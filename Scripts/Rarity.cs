
using System.Collections.Generic;
using Godot;

public enum Rarity
	{
		Common = 10,
		Uncommon = 16,
		Rare = 19,
		Legendary = 20
	}


public static class RarityColors
{
	public static readonly Dictionary<Rarity, Color> colors = new Dictionary<Rarity, Color>
	{
		{ Rarity.Common, new Color(0.5f, 0.5f, 0.5f, 1.0f)},
		{ Rarity.Uncommon, new Color(0, 1, 0, 1.0f)},
		{ Rarity.Rare, new Color(0, 0, 1, 1.0f)},
		{ Rarity.Legendary, new Color(1, 1, 0, 1.0f)}
};
	
	public static Color GetColor(Rarity rarity)
	{
		return colors.ContainsKey(rarity) ? colors[rarity] : new Color();
	}
}
