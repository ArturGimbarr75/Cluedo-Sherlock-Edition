using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public List<PlayerInformation> Players { get; private set; }
	public List<Request> Requests { get; private set; } = new();

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

		InitSuspects(mainPlayer, suspects);
		InitWeapons(mainPlayer, weapons);
		InitLocations(mainPlayer, locations);
	}

	private void InitSuspects(PlayerInformation mainPlayer, Suspect suspects)
	{
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

	private void InitWeapons(PlayerInformation mainPlayer, Weapon weapons)
	{
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
	}

	private void InitLocations(PlayerInformation mainPlayer, Location locations)
	{
		foreach (Location location in LOCATIONS)
		{
			if (locations.HasFlag(location))
				mainPlayer.Locations.Add(location, OwnStatus.Own);
			else
				mainPlayer.Locations.Add(location, OwnStatus.NotOwn);

			for (int i = 1; i < Players.Count; i++)
			{
				if (locations.HasFlag(location))
					Players[i].Locations.Add(location, OwnStatus.NotOwn);
				else
					Players[i].Locations.Add(location, OwnStatus.Unknown);
			}
		}
	}

	private void UpdateStates()
	{
		bool updated = true;

		while (updated)
		{
			updated = false;

			foreach (PlayerInformation information in Players)
			{
				updated |= UpdateStates(information);
			}
		}
	}

	private bool UpdateStates(PlayerInformation player)
	{
		bool updated = false;

		foreach (Request request in Requests)
		{
			OwnStatus suspectStatus = player.Suspects[request.Suspect];
			OwnStatus weaponsStatus = player.Weapons[request.Weapon];
			OwnStatus locationStatus = player.Locations[request.Location];

			if (suspectStatus == OwnStatus.NotOwn && weaponsStatus == OwnStatus.NotOwn && locationStatus == OwnStatus.Unknown)
			{
				AddLocation(player, request.Location);
				updated = true;
			}

			if (suspectStatus == OwnStatus.NotOwn && weaponsStatus == OwnStatus.Unknown && locationStatus == OwnStatus.NotOwn)
			{
				AddWeapon(player, request.Weapon);
				updated = true;
			}

			if (suspectStatus == OwnStatus.Unknown && weaponsStatus == OwnStatus.NotOwn && locationStatus == OwnStatus.NotOwn)
			{
				AddSuspect(player, request.Suspect);
				updated = true;
			}
		}

		return updated;
	}

	public void AddRequest(Request request)
	{
		Requests.Add(request);

		foreach (PlayerInformation information in request.PlayersWithoutCards)
		{
			information.Suspects[request.Suspect] = OwnStatus.NotOwn;
			information.Weapons[request.Weapon] = OwnStatus.NotOwn;
			information.Locations[request.Location] = OwnStatus.NotOwn;
		}

		UpdateStates();
	}

	public void AddSuspect(PlayerInformation player, Suspect suspect)
	{
		player.Suspects[suspect] = OwnStatus.Own;

		foreach (PlayerInformation information in Players)
		{
			if (information != player)
				information.Suspects[suspect] = OwnStatus.NotOwn;
		}

		UpdateStates();
	}

	public void AddWeapon(PlayerInformation player, Weapon weapon)
	{
		player.Weapons[weapon] = OwnStatus.Own;

		foreach (PlayerInformation information in Players)
		{
			if (information != player)
				information.Weapons[weapon] = OwnStatus.NotOwn;
		}

		UpdateStates();
	}

	public void AddLocation(PlayerInformation player, Location location)
	{
		player.Locations[location] = OwnStatus.Own;

		foreach (PlayerInformation information in Players)
		{
			if (information != player)
				information.Locations[location] = OwnStatus.NotOwn;
		}

		UpdateStates();
	}
}
