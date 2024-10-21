using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    public string Name;
    public int KnownCardsCount => Suspects.Count + Weapons.Count + Rooms.Count;
	public int TotalCardsCount;

    public Dictionary<Suspect, OwnStatus> Suspects { get; private set; } = new();
	public Dictionary<Weapon, OwnStatus> Weapons { get; private set; } = new();
	public Dictionary<Location, OwnStatus> Rooms { get; private set; } = new();

	public PlayerInformation(string name, int totalCardsCount = 0)
	{
		Name = name;
		TotalCardsCount = totalCardsCount;
	}
}
