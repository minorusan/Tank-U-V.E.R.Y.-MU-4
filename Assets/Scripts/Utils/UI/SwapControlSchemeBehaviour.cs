using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Settings.GamePlay.Input;


namespace Utils.UI
{
	public class SwapControlSchemeBehaviour : MonoBehaviour
	{
		private int _currentScemeIndex;

		private int _currentIndex
		{
			get
			{
				return _currentScemeIndex;
			}
			set
			{
				_currentScemeIndex = value;
				if (_currentScemeIndex > inputScemes.Length - 1)
				{
					_currentScemeIndex = 0;
				}

				if (_currentScemeIndex < 0)
				{
					_currentScemeIndex = inputScemes.Length - 1;
				}
			}

		}

		public AInputSettingsBase inputSettings;
		public InputSchemeSettings[] inputScemes;
		public GameObject[] uiScemeRepresentation;

		void Start()
		{
			int selectedScheme = PlayerPrefs.GetInt ("scheme");
			inputSettings.inputSceme = inputScemes [selectedScheme];
			for (int i = 0; i < uiScemeRepresentation.Length; i++)
			{
				uiScemeRepresentation [i].SetActive (false);
			}
			uiScemeRepresentation [selectedScheme].SetActive (true);
		}

		public void SwapSceme()
		{
			_currentIndex++;
			inputSettings.inputSceme = inputScemes [_currentIndex];
			for (int i = 0; i < uiScemeRepresentation.Length; i++)
			{
				uiScemeRepresentation [i].SetActive (false);
			}
			uiScemeRepresentation [_currentIndex].SetActive (true);
			PlayerPrefs.SetInt ("scheme", _currentIndex);
		}
			
	}
}