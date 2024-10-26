using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveWindowInitializer : MonoBehaviour
{
	[SerializeField] private GameManager _gameManager;

	[Space(10)]
	[SerializeField] private GameObject _playerLinePrefab;
	[SerializeField] private Transform _linesContent;

	[Space(10)]
    [SerializeField] private TMP_Dropdown _suspectsDropdown;
	[SerializeField] private TMP_Dropdown _weaponsDropdown;
	[SerializeField] private TMP_Dropdown _locationsDropdown;

	[Space(10)]
	[SerializeField] private TMP_Dropdown _requesterDropdown;
	[SerializeField] private TMP_Dropdown _responderDropdown;

	private Dictionary<PlayerInformation, bool> _playersWithoutCards = new();

	private void Awake()
	{
		InitializeDropdowns();
		InitializePlayers();

		ResetUI();
	}

	private void InitializePlayers()
	{
		var players = _gameManager.Players;

		foreach (var player in players)
		{
			GameObject playerLine = Instantiate(_playerLinePrefab, _linesContent);
			playerLine.GetComponentInChildren<TMP_Text>().text = player.Name;
			playerLine.GetComponentInChildren<Toggle>().onValueChanged.AddListener(isOn => _playersWithoutCards[player] = isOn);
			playerLine.gameObject.SetActive(true);

			_playersWithoutCards.Add(player, false);
		}
	}

	private void InitializeDropdowns()
	{
		_suspectsDropdown.InitializeSuspectsDropdown();
		_weaponsDropdown.InitializeWeaponsDropdown();
		_locationsDropdown.InitializeLocationsDropdown();

		_requesterDropdown.InitializePlayersDropdowns(_gameManager);
		_responderDropdown.InitializePlayersDropdowns(_gameManager);
	}

	public void SendRequest()
	{
		Request request = new();

		if (_requesterDropdown.value == 0)
			return;
		else
		{
			string requesterName = _requesterDropdown.options[_requesterDropdown.value].text;
			request.Requester = _gameManager.Players.FirstOrDefault(p => p.Name == requesterName);
		}

		if (_responderDropdown.value != 0)
		{
			string responderName = _responderDropdown.options[_responderDropdown.value].text;
			request.Responder = _gameManager.Players.FirstOrDefault(p => p.Name == responderName);
		}

		request.Suspect = (Suspect)Enum.Parse(typeof(Suspect), _suspectsDropdown.options[_suspectsDropdown.value].text);
		request.Weapon = (Weapon)Enum.Parse(typeof(Weapon), _weaponsDropdown.options[_weaponsDropdown.value].text);
		request.Location = (Location)Enum.Parse(typeof(Location), _locationsDropdown.options[_locationsDropdown.value].text);

		if (request.Suspect == Suspect.None || request.Weapon == Weapon.None || request.Location == Location.None)
			return;

		if (_playersWithoutCards.Any(p => p.Value))
			request.PlayersWithoutCards = _playersWithoutCards.Where(p => p.Value).Select(p => p.Key).ToArray();
		else
			request.PlayersWithoutCards = Array.Empty<PlayerInformation>();

		ResetUI();

		_gameManager.AddRequest(request);
	}

	private void ResetUI()
	{
		_suspectsDropdown.value = 0;
		_weaponsDropdown.value = 0;
		_locationsDropdown.value = 0;
		_requesterDropdown.value = 0;
		_responderDropdown.value = 0;
		foreach (var player in _gameManager.Players)
			_playersWithoutCards[player] = false;
	}
}
