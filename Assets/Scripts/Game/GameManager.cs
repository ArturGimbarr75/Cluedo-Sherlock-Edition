using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public List<PlayerInformation> Players { get; private set; }

	public static readonly Suspect[] SUSPECTS = (Suspect[])System.Enum.GetValues(typeof(Suspect));
	public static readonly Weapon[] WEAPONS = (Weapon[])System.Enum.GetValues(typeof(Weapon));
	public static readonly Location[] LOCATIONS = (Location[])System.Enum.GetValues(typeof(Location));

	private void Awake()
	{
		LoadPlayers();
	}

	private void LoadPlayers()
	{
		var players = StreamingAssetsHelper.LoadPlayers();
		Players = new(players.Count);

		foreach (var (name, active, cardsCount) in players)
		{
			if (active)
				Players.Add(new(name, cardsCount));
		}

		PlayerInformation mainPlayer = Players[0];
		var (suspects, weapons, locations) = StreamingAssetsHelper.LoadCards();

		foreach (Suspect suspect in SUSPECTS)
		{
			if (suspects.HasFlag(suspect))
				mainPlayer.Suspects.Add(suspect, OwnStatus.Own);
			else
				mainPlayer.Suspects.Add(suspect, OwnStatus.NotOwn);

			for (int i = 1; i < Players.Count; i++)
			{
				if (suspects.HasFlag(suspect))
					Players[i].Suspects.Add(suspect, OwnStatus.NotOwn);
				else
					Players[i].Suspects.Add(suspect, OwnStatus.Unknown);
			}
		}

		foreach (Weapon weapon in WEAPONS)
		{
			if (weapons.HasFlag(weapon))
				mainPlayer.Weapons.Add(weapon, OwnStatus.Own);
			else
				mainPlayer.Weapons.Add(weapon, OwnStatus.NotOwn);

			for (int i = 1; i < Players.Count; i++)
			{
				if (weapons.HasFlag(weapon))
					Players[i].Weapons.Add(weapon, OwnStatus.NotOwn);
				else
					Players[i].Weapons.Add(weapon, OwnStatus.Unknown);
			}
		}

		foreach (Location location in LOCATIONS)
		{
			if (locations.HasFlag(location))
				mainPlayer.Rooms.Add(location, OwnStatus.Own);
			else
				mainPlayer.Rooms.Add(location, OwnStatus.NotOwn);

			for (int i = 1; i < Players.Count; i++)
			{
				if (locations.HasFlag(location))
					Players[i].Rooms.Add(location, OwnStatus.NotOwn);
				else
					Players[i].Rooms.Add(location, OwnStatus.Unknown);
			}
		}
	}
}
