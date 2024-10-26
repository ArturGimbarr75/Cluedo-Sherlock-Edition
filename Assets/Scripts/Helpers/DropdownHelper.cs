using System.Collections.Generic;
using System.Linq;
using TMPro;

public static class DropdownHelper
{
	public static void InitializePlayersDropdowns(this TMP_Dropdown dropdown, GameManager gameManager, bool includeNone = true)
	{
		IEnumerable<string> players = gameManager.Players.Select(p => p.Name);

		if (includeNone)
			players = players.Prepend("None");

		dropdown.ClearOptions();
		dropdown.AddOptions(players.ToList());
	}

	public static void InitializeSuspectsDropdown(this TMP_Dropdown dropdown, bool includeNone = true)
	{
		dropdown.InitializeCardsDropdown(GameManager.SUSPECTS.Select(s => s.ToString()), includeNone);
	}

	public static void InitializeWeaponsDropdown(this TMP_Dropdown dropdown, bool includeNone = true)
	{
		dropdown.InitializeCardsDropdown(GameManager.WEAPONS.Select(w => w.ToString()), includeNone);
	}

	public static void InitializeLocationsDropdown(this TMP_Dropdown dropdown, bool includeNone = true)
	{
		dropdown.InitializeCardsDropdown(GameManager.LOCATIONS.Select(l => l.ToString()), includeNone);
	}

	public static void InitializeCardsDropdown(this TMP_Dropdown dropdown, IEnumerable<string> options, bool includeNone = true)
	{
		List<string> cards = new (options);

		if (includeNone)
			cards.Insert(0, "None");

		dropdown.ClearOptions();
		dropdown.AddOptions(cards);
	}
}
