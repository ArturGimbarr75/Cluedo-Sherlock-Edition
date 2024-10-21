using System.Collections.Generic;
using UnityEngine;

public class PlayersOnGridWindow : MonoBehaviour
{
	[SerializeField] private GameManager _gameManager;

	[Space(10)]
	[SerializeField] private Transform _playersContent;
	[SerializeField] private PlayerGridColumn _playerGridColumnPrefab;

	private Dictionary<PlayerInformation, PlayerGridColumn> _players = new();

	private void Awake()
	{
		InitPlayers();
	}

	private void Start()
	{
		UpdatePlayers();
	}

	private void InitPlayers()
	{
		foreach (var player in _gameManager.Players)
		{
			var playerColumn = Instantiate(_playerGridColumnPrefab, _playersContent);
			playerColumn.SetPlayerName(player.Name);
			playerColumn.gameObject.SetActive(true);
			playerColumn.SetCardsState(player);

			_players.Add(player, playerColumn);
		}
	}

	public void UpdatePlayers()
	{
		foreach (var (player, playerColumn) in _players)
		{
			playerColumn.SetCardsState(player);
		}
	}
}
