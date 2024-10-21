using System;
using UnityEngine;

public class MainMenuCardsView : MonoBehaviour
{
	[SerializeField] private MainMenuManager _mainMenuManager;

	[Space(10)]
	[SerializeField] private Transform _suspectsContent;
	[SerializeField] private Transform _weaponsContent;
	[SerializeField] private Transform _locationsContent;

	[Space(10)]
	[SerializeField] private CardLine _cardPrefab;

	private void Start()
	{
		CreateCards();
	}

	private void CreateCards()
	{
		AddCardToView<Suspect>(_suspectsContent);
		AddCardToView<Weapon>(_weaponsContent);
		AddCardToView<Location>(_locationsContent);
	}

	private void AddCardToView<T>(Transform content) where T : Enum
	{
		T[] values = (T[])Enum.GetValues(typeof(T));
		foreach (T value in values)
		{
			if (value.Equals(default(T)))
				continue;

			CardLine card = Instantiate(_cardPrefab, content);
			card.Name.text = value.ToString();
			T cardValue = value;
			card.Toggle.onValueChanged.AddListener(isOn => _mainMenuManager.OnCardValueChanged(isOn, cardValue));

			card.gameObject.SetActive(true);
		}
	}
}
