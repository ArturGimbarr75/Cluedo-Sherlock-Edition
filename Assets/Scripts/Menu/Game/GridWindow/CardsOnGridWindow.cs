using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsOnGridWindow : MonoBehaviour
{
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

	private void Start()
	{
		InitCards();
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
}
