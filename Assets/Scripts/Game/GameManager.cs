using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public List<PlayerInformation> Players { get; private set; }

	public static readonly Suspect[] SUSPECTS = (Suspect[])System.Enum.GetValues(typeof(Suspect));
	public static readonly Weapon[] WEAPONS = (Weapon[])System.Enum.GetValues(typeof(Weapon));
	public static readonly Location[] LOCATIONS = (Location[])System.Enum.GetValues(typeof(Location));

	public void Awake()
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
	}
}
