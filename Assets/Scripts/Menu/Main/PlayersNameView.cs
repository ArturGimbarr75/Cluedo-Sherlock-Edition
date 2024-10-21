using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayersNameView : MonoBehaviour
{
    public List<PlayerLine> PlayerLines { get; private set; } = new List<PlayerLine>();

	[SerializeField] private PlayerLine _mainPlayerLine;
	[SerializeField] private PlayerLine _playerLinePrefab;
	[SerializeField] private Slider _playersCountSlider;

	private void Awake()
	{
		PlayerLines ??= new List<PlayerLine>();
	}

	private void Start()
	{
		_playerLinePrefab.gameObject.SetActive(false);
		PlayerLines.Add(_mainPlayerLine);

		var players = StreamingAssetsHelper.LoadPlayers();
		if (players.Count != 0)
		{
			int count = players.Count(players => players.active);
			SetNewCount(players.Count);
			for (int i = 0; i < players.Count; i++)
			{
				PlayerLines[i].PlayerName.text = players[i].name;
				PlayerLines[i].gameObject.SetActive(players[i].active);
			}

			SetNewCount(count);
			_playersCountSlider.value = count;
		}
		else
		{
			SetNewCount(2);
			_playersCountSlider.value = 2;
		}
	}

	public void SetNewCount(float count)
	{
		if (count < 2)
			count = 2;

		if (count > PlayerLines.Count)
			AddLines(count);

		for (int i = 0; i < PlayerLines.Count; i++)
		{
			PlayerLines[i].gameObject.SetActive(i < count);
		}
	}

	private void AddLines(float count)
	{
		for (int i = PlayerLines.Count; i < count; i++)
		{
			PlayerLine newPlayerLine = Instantiate(_playerLinePrefab, _playerLinePrefab.transform.parent);
			newPlayerLine.gameObject.SetActive(true);
			newPlayerLine.PlayerNumber.text = $"{i + 1}.";
			PlayerLines.Add(newPlayerLine);
		}
	}
}
