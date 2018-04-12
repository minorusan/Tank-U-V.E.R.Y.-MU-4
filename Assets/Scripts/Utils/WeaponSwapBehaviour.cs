using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.GamePlay.Input;


namespace Core.Utils
{
	public class WeaponSwapBehaviour : MonoBehaviour
	{
		private int _currentIndexBackingField;

		private int _currentIndex
		{
			get
			{
				return _currentIndexBackingField;
			}
			set
			{
				_currentIndexBackingField = value;
				if (_currentIndexBackingField > weapons.Length - 1)
				{
					_currentIndexBackingField = 0;
				}

				if (_currentIndexBackingField < 0)
				{
					_currentIndexBackingField = weapons.Length - 1;
				}
			}

		}

		public GameObject[] weapons;

		private void Awake()
		{
			FindObjectOfType<InputBroadcasterBaseBehaviour> ().didSelectNextWeapon += OnNextWeaponSelected;
			FindObjectOfType<InputBroadcasterBaseBehaviour> ().didSelectPreviousWeapon += OnPreviousWeponSelected;
		}

		void OnPreviousWeponSelected()
		{
			_currentIndex--;
			SetWeapon ();
		}

		void OnNextWeaponSelected()
		{
			_currentIndex++;
			SetWeapon ();
		}

		private void SetWeapon()
		{
			PlayerAudioBehaviour.PlaySound (EAudioEventType.reload, transform.position);
			for (int i = 0; i < weapons.Length; i++)
			{
				weapons [i].SetActive (false);
			}

			weapons [_currentIndex].SetActive (true);
		}
	}
}