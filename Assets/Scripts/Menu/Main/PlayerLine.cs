using TMPro;
using UnityEngine;

public class PlayerLine : MonoBehaviour
{
    [field: SerializeField] public TMP_Text PlayerNumber { get; private set; }
	[field: SerializeField] public TMP_InputField PlayerName { get; private set; }
}
