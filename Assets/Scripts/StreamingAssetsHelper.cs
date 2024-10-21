using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class StreamingAssetsHelper
{
	private readonly static string PLAYERS_PATH = Path.Combine(Application.streamingAssetsPath, "Players.json");
	private readonly static string CARDS_PATH = Path.Combine(Application.streamingAssetsPath, "Cards.txt");

	public static void SavePlayers(List<PlayerLine> playerLines)
	{
		File.WriteAllLines(PLAYERS_PATH, playerLines
										.Select(p => $"{p.Name.text} {p.gameObject.activeSelf} {p.CardsCount.text}")
										.Prepend($"{playerLines.Count}"));
	}

	public static List<(string name, bool active, int cardsCount)> LoadPlayers()
	{
		if (!File.Exists(PLAYERS_PATH))
			return new();

		string[] lines = File.ReadAllLines(PLAYERS_PATH);
		int count = int.Parse(lines[0]);
		List<(string name, bool active, int cardsCount)> players = new(count);

		for (int i = 1; i < count + 1; i++)
		{
			string[] player = lines[i].Split(' ');
			players.Add((string.Join(" ", player[0..^2]), bool.Parse(player[^2]), int.Parse(player[^1])));
		}

		return players;
	}

	public static void SaveCards(Suspect suspects, Weapon weapons, Location locations)
	{
		File.WriteAllText(CARDS_PATH, $"{(int)suspects} {(int)weapons} {(int)locations}");
	}

	public static (Suspect suspects, Weapon weapons, Location locations) LoadCards()
	{
		if (!File.Exists(CARDS_PATH))
			return (0, 0, 0);

		string[] cards = File.ReadAllText(CARDS_PATH).Split(' ');
		return ((Suspect)int.Parse(cards[0]), (Weapon)int.Parse(cards[1]), (Location)int.Parse(cards[2]));
	}
}
