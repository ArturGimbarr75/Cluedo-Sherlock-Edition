using System.Collections.Generic;
using System.Linq;

public class PlayerInformation
{
    public string Name;
    public int KnownCardsCount => Suspects.Count(s => s.Value == OwnStatus.Own) + Weapons.Count(w => w.Value == OwnStatus.Own) + Locations.Count(l => l.Value == OwnStatus.Own);
	public int TotalCardsCount;

    public Dictionary<Suspect, OwnStatus> Suspects { get; private set; } = new();
	public Dictionary<Weapon, OwnStatus> Weapons { get; private set; } = new();
	public Dictionary<Location, OwnStatus> Locations { get; private set; } = new();

	public PlayerInformation(string name, int totalCardsCount = 0)
	{
		Name = name;
		TotalCardsCount = totalCardsCount;
	}

	public override bool Equals(object other)
	{
		return other is PlayerInformation player && Name == player.Name;
	}

	public override int GetHashCode()
	{
		return Name.GetHashCode();
	}
}
