using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Utils
{
	public class RestartSceneBehaviour : MonoBehaviour
	{
		public void RestartGame()
		{
			SceneManager.LoadSceneAsync (0);
		}
	}
}