
using System.Collections.Generic;
using Godot;

public enum Rarity
	{
		Common = 1,
		Uncommon = 2,
		Rare = 3,
		Legendary = 4
	}


public static class RarityColors
{
	public static readonly Dictionary<Rarity, Color> colors = new Dictionary<Rarity, Color>
	{
		{ Rarity.Common, new Color(0.5f, 0.5f, 0.5f)},
		{ Rarity.Uncommon, new Color(0, 1, 0)},
		{ Rarity.Rare, new Color(0, 0, 1)},
		{ Rarity.Legendary, new Color(1, 1, 0)}
};
	
	public static Color GetColor(Rarity rarity)
	{
		return colors.ContainsKey(rarity) ? colors[rarity] : new Color();
	}
}
