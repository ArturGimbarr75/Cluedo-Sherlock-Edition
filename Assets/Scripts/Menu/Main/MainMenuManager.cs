using System;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
	[SerializeField] private PlayersNameView _playersNameView;

	private Suspect _suspects;
	private Weapon _weapons;
	private Location _locations;

	private List<PlayerLine> _playerLines;

	private void Start()
	{
		_playerLines = _playersNameView.PlayerLines;
	}

	public void OnCardValueChanged(bool isOn, Enum value)
	{
		if (value is Suspect suspect)
		{
			if (isOn)
			{
				_suspects |= suspect; 
			}
			else
			{
				_suspects &= ~suspect;
			}
		}
		else if (value is Weapon weapon)
		{
			if (isOn)
			{
				_weapons |= weapon;
			}
			else
			{
				_weapons &= ~weapon;
			}
		}
		else if (value is Location location)
		{
			if (isOn)
			{
				_locations |= location;
			}
			else
			{
				_locations &= ~location;
			}
		}
		else
		{
			throw new ArgumentException("Unknown card type");
		}
	}

	public void OnPlayClick()
	{
		StreamingAssetsHelper.SavePlayers(_playerLines);
		StreamingAssetsHelper.SaveCards(_suspects, _weapons, _locations);
	}
}
