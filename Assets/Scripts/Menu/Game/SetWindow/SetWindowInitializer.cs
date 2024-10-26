using TMPro;
using UnityEngine;

public class SetWindowInitializer : MonoBehaviour
{
	[SerializeField] private GameManager _gameManager;

	[Space(10)]
	[SerializeField] private TMP_Dropdown _playersDropdown;

    [Header("Dropdowns")]
	[SerializeField] private TMP_Dropdown _suspectsDropdown;
	[SerializeField] private TMP_Dropdown _weaponsDropdown;
	[SerializeField] private TMP_Dropdown _locationsDropdown;

	private CardType _selectedCardType;

	private void Awake()
	{
		_selectedCardType = CardType.Suspect;
		InitializeDropdowns();
	}

	private void InitializeDropdowns()
	{
		_playersDropdown.InitializePlayersDropdowns(_gameManager);
		_suspectsDropdown.InitializeSuspectsDropdown();
		_weaponsDropdown.InitializeWeaponsDropdown();
		_locationsDropdown.InitializeLocationsDropdown();
	}

	private void ResetUI()
	{
		_playersDropdown.value = 0;
		_suspectsDropdown.value = 0;
		_weaponsDropdown.value = 0;
		_locationsDropdown.value = 0;
	}

	public void SetSuspectsCardType()
	{
		_selectedCardType = CardType.Suspect;
	}

	public void SetWeaponsCardType()
	{
		_selectedCardType = CardType.Weapon;
	}

	public void SetLocationsCardType()
	{
		_selectedCardType = CardType.Location;
	}

	public void SetCardToPlayer()
	{
		PlayerInformation player = _playersDropdown.GetSelectedPlayer(_gameManager);

		if (player is null)
		{
			return;
		}

		if (_selectedCardType == CardType.Suspect)
		{
			Suspect suspect = _suspectsDropdown.GetSelectedSuspect();
			if (suspect != Suspect.None)
			{
				_gameManager.AddSuspect(player, suspect);
				ResetUI();
			}
		}	
		else if (_selectedCardType == CardType.Weapon)
		{
			Weapon weapon = _weaponsDropdown.GetSelectedWeapon();
			if (weapon != Weapon.None)
			{
				_gameManager.AddWeapon(player, weapon);
				ResetUI();
			}
		}	
		else if (_selectedCardType == CardType.Location)
		{
			Location location = _locationsDropdown.GetSelectedLocation();
			if (location != Location.None)
			{
				_gameManager.AddLocation(player, location);
				ResetUI();
			}
		}
	}
}
