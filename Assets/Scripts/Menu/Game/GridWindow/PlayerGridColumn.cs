using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGridColumn : MonoBehaviour
{
	[SerializeField] private TMP_Text _playerName;

	[Space(10)]
	[SerializeField] private Transform _suspectsContent;
	[SerializeField] private Transform _weaponsContent;
	[SerializeField] private Transform _roomsContent;
	[SerializeField] private GameObject _imageHolderPrefab;

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

	private void InitCards()
	{
		foreach (var suspect in GameManager.SUSPECTS)
		{
			if (suspect == Suspect.None)
				continue;

			var card = Instantiate(_imageHolderPrefab, _suspectsContent);
			card.gameObject.SetActive(true);
			Image image = card.transform.GetChild(0).GetComponent<Image>();
			_suspects.Add(suspect, image);
		}

		foreach (var weapon in GameManager.WEAPONS)
		{
			if (weapon == Weapon.None)
				continue;

			var card = Instantiate(_imageHolderPrefab, _weaponsContent);
			card.gameObject.SetActive(true);
			Image image = card.transform.GetChild(0).GetComponent<Image>();
			_weapons.Add(weapon, image);
		}

		foreach (var room in GameManager.LOCATIONS)
		{
			if (room == Location.None)
				continue;

			var card = Instantiate(_imageHolderPrefab, _roomsContent);
			card.gameObject.SetActive(true);
			Image image = card.transform.GetChild(0).GetComponent<Image>();
			_rooms.Add(room, image);
		}
	}

	public void SetPlayerName(string name)
	{
		_playerName.text = name;
	}

	public void SetCardsState(PlayerInformation information)
	{
		if (information == null)
			return;

		if (_suspects.Count == 0 || _weapons.Count == 0 || _rooms.Count == 0)
			return;

		foreach (var suspect in _suspects)
		{
			if (information.Suspects.TryGetValue(suspect.Key, out OwnStatus status))
			{
				suspect.Value.sprite = status switch
				{
					OwnStatus.Own => _ownsImage,
					OwnStatus.NotOwn => _notOwnsImage,
					OwnStatus.Unknown => _unknownImage,
					_ => throw new System.ArgumentOutOfRangeException(nameof(status), status, null)
				};
			}
		}

		foreach (var weapon in _weapons)
		{
			if (information.Weapons.TryGetValue(weapon.Key, out OwnStatus status))
			{
				weapon.Value.sprite = status switch
				{
					OwnStatus.Own => _ownsImage,
					OwnStatus.NotOwn => _notOwnsImage,
					OwnStatus.Unknown => _unknownImage,
					_ => throw new System.ArgumentOutOfRangeException(nameof(status), status, null)
				};
			}
		}

		foreach (var room in _rooms)
		{
			if (information.Locations.TryGetValue(room.Key, out OwnStatus status))
			{
				room.Value.sprite = status switch
				{
					OwnStatus.Own => _ownsImage,
					OwnStatus.NotOwn => _notOwnsImage,
					OwnStatus.Unknown => _unknownImage,
					_ => throw new System.ArgumentOutOfRangeException(nameof(status), status, null)
				};
			}
		}
	}
}
