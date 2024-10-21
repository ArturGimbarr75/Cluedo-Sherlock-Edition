using TMPro;
using UnityEngine;

public class SliderText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

	public void SetText(float value)
	{
		_text.text = value.ToString();
	}
}
