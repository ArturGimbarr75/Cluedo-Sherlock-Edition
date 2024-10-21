using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayersWindowContent : MonoBehaviour
{
	[SerializeField] private GameManager _gameManager;

	[Space(10)]
	[SerializeField] private Transform _playersNameContent;
	[SerializeField] private Button _playerButtonPrefab;

	[Space(10)]
	[SerializeField] private Transform _suspectsContent;
	[SerializeField] private Transform _weaponsContent;
	[SerializeField] private Transform _roomsContent;
	[SerializeField] private CardInfo _cardButtonPrefab;

	[Space(10)]
	[SerializeField] private Sprite _ownsImage;
	[SerializeField] private Sprite _notOwnsImage;
	[SerializeField] private Sprite _unknownImage;

	private Dictionary<Suspect, CardInfo> _suspects = new();
	private Dictionary<Weapon, CardInfo> _weapons = new();
	private Dictionary<Location, CardInfo> _rooms = new();

	private void Start()
	{
		InitCards();
		InitPlayerButtons();

		ShowPlayerInfo(_gameManager.Players[0]);
	}

	private void InitPlayerButtons()
	{
		var players = _gameManager.Players;

		foreach (var player in players)
		{
			var playerButton = Instantiate(_playerButtonPrefab, _playersNameContent);
			playerButton.GetComponentInChildren<TMP_Text>().text = player.Name;
			var currentPlayer = player;
			playerButton.onClick.AddListener(() => ShowPlayerInfo(currentPlayer));
			playerButton.gameObject.SetActive(true);
		}
	}

	private void InitCards()
	{
		foreach (var suspect in GameManager.SUSPECTS)
		{
			if (suspect == Suspect.None)
				continue;

			var card = Instantiate(_cardButtonPrefab, _suspectsContent);
			card.CardText.text = suspect.ToString();
			card.gameObject.SetActive(true);
			_suspects.Add(suspect, card);
		}

		foreach (var weapon in GameManager.WEAPONS)
		{
			if (weapon == Weapon.None)
				continue;

			var card = Instantiate(_cardButtonPrefab, _weaponsContent);
			card.CardText.text = weapon.ToString();
			card.gameObject.SetActive(true);
			_weapons.Add(weapon, card);
		}

		foreach (var room in GameManager.LOCATIONS)
		{
			if (room == Location.None)
				continue;

			var card = Instantiate(_cardButtonPrefab, _roomsContent);
			card.CardText.text = room.ToString();
			card.gameObject.SetActive(true);
			_rooms.Add(room, card);
		}
	}

	private void ShowPlayerInfo(PlayerInformation player)
	{
		foreach (var (suspect, status) in player.Suspects)
		{
			if (suspect == Suspect.None)
				continue;

			_suspects[suspect].StateImage.sprite = status switch
			{
				OwnStatus.Own => _ownsImage,
				OwnStatus.NotOwn => _notOwnsImage,
				OwnStatus.Unknown => _unknownImage,
				_ => throw new System.NotImplementedException(),
			};
		}

		foreach (var (weapon, status) in player.Weapons)
		{
			if (weapon == Weapon.None)
				continue;

			_weapons[weapon].StateImage.sprite = status switch
			{
				OwnStatus.Own => _ownsImage,
				OwnStatus.NotOwn => _notOwnsImage,
				OwnStatus.Unknown => _unknownImage,
				_ => throw new System.NotImplementedException(),
			};
		}

		foreach (var (room, status) in player.Rooms)
		{
			if (room == Location.None)
				continue;

			_rooms[room].StateImage.sprite = status switch
			{
				OwnStatus.Own => _ownsImage,
				OwnStatus.NotOwn => _notOwnsImage,
				OwnStatus.Unknown => _unknownImage,
				_ => throw new System.NotImplementedException(),
			};
		}
	}
}
