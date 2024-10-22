using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsOnGridWindow : MonoBehaviour
{
	[SerializeField] private GameManager _gameManager;

	[Space(10)]
	[SerializeField] private Transform _suspectsContent;
	[SerializeField] private Transform _weaponsContent;
	[SerializeField] private Transform _roomsContent;
	[SerializeField] private CardInfo _cardButtonPrefab;

	[Space(10)]
	[SerializeField] private Sprite _ownsImage;
	[SerializeField] private Sprite _notOwnsImage;
	[SerializeField] private Sprite _unknownImage;

	private Dictionary<Suspect, Image> _suspects = new();
	private Dictionary<Weapon, Image> _weapons = new();
	private Dictionary<Location, Image> _rooms = new();

	private void Awake()
	{
		InitCards();
	}

	private void Start()
	{
		UpdateCards();
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
			_suspects.Add(suspect, card.StateImage);
		}

		foreach (var weapon in GameManager.WEAPONS)
		{
			if (weapon == Weapon.None)
				continue;

			var card = Instantiate(_cardButtonPrefab, _weaponsContent);
			card.CardText.text = weapon.ToString();
			card.gameObject.SetActive(true);
			_weapons.Add(weapon, card.StateImage);
		}

		foreach (var room in GameManager.LOCATIONS)
		{
			if (room == Location.None)
				continue;

			var card = Instantiate(_cardButtonPrefab, _roomsContent);
			card.CardText.text = room.ToString();
			card.gameObject.SetActive(true);
			_rooms.Add(room, card.StateImage);
		}
	}

	public void UpdateCards()
	{
		if (_gameManager.Players.Count == 0)
			return;

		if (_suspects.Count == 0 || _weapons.Count == 0 || _rooms.Count == 0)
			return;

		var players = _gameManager.Players;

		foreach (Suspect suspect in GameManager.SUSPECTS)
		{
			if (suspect == Suspect.None)
				continue;

			bool allNotOwns = true;

			foreach (var player in players)
			{
				if (player.Suspects.TryGetValue(suspect, out var status))
				{
					if (status == OwnStatus.Own)
					{
						_suspects[suspect].sprite = _notOwnsImage;
						break;
					}

					if (status == OwnStatus.Unknown)
					{
						_suspects[suspect].sprite = _unknownImage;
						allNotOwns = false;
						break;
					}
				}
			}

			if (allNotOwns)
				_suspects[suspect].sprite = _ownsImage;
		}

		foreach (Weapon weapon in GameManager.WEAPONS)
		{
			if (weapon == Weapon.None)
				continue;

			bool allNotOwns = true;

			foreach (var player in players)
			{
				if (player.Weapons.TryGetValue(weapon, out var status))
				{
					if (status == OwnStatus.Own)
					{
						_weapons[weapon].sprite = _notOwnsImage;
						break;
					}

					if (status == OwnStatus.Unknown)
					{
						_weapons[weapon].sprite = _unknownImage;
						allNotOwns = false;
						break;
					}
				}
			}

			if (allNotOwns)
				_weapons[weapon].sprite = _ownsImage;
		}

		foreach (Location location in GameManager.LOCATIONS)
		{
			if (location == Location.None)
				continue;

			bool allNotOwns = true;

			foreach (var player in players)
			{
				if (player.Locations.TryGetValue(location, out var status))
				{
					if (status == OwnStatus.Own)
					{
						_rooms[location].sprite = _notOwnsImage;
						break;
					}

					if (status == OwnStatus.Unknown)
					{
						_rooms[location].sprite = _unknownImage;
						allNotOwns = false;
						break;
					}
				}
			}

			if (allNotOwns)
				_rooms[location].sprite = _ownsImage;
		}
	}
}
