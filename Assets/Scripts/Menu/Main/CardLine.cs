using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardLine : MonoBehaviour
{
    [field: SerializeField] public TMP_Text Name { get; private set; }
	[field: SerializeField] public Toggle Toggle { get; private set; }
}
