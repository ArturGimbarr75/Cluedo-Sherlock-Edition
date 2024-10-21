using TMPro;
using UnityEngine;

public class PlayerLine : MonoBehaviour
{
    [field: SerializeField] public TMP_Text Number { get; private set; }
	[field: SerializeField] public TMP_InputField Name { get; private set; }
	[field: SerializeField] public TMP_InputField CardsCount { get; private set; }
}
