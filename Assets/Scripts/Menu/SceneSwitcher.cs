using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchScene(int buildIndex)
	{
		SceneManager.LoadScene(buildIndex);
	}
}
