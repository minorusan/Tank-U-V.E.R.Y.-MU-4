using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.GamePlay.Settings;
using UnityEngine.UI;
using System;


namespace Core.GamePlay
{
	public class HealthBehaviour : MonoBehaviour
	{
		private float _currentHealth;
		public HealthSettings settings;

		public event Action<Vector3> didDieAtPosition;
		/// <summary>
		/// All monsters will trigger this event by death
		/// </summary>
		public static event Action<Vector3> didDieAtPositionGlobal;
		/// <summary>
		/// All monsters will trigger this event by death. Parameter is GO tag
		/// </summary>
		public event Action<float> didModifyHealthByValue;

		public Image healthImage;

		public float health
		{
			get
			{
				return _currentHealth;
			}
		}

		private void OnEnable()
		{
			_currentHealth = settings.health;
			healthImage.color = Color.green;
			healthImage.fillAmount = 1f;
		}

		public void SubstractHealth(float value)
		{
			value *= 1f - settings.defence;
			Heal (-value);
		}

		public void Heal(float value)
		{
			_currentHealth += value;
			healthImage.fillAmount = _currentHealth / settings.health;
			healthImage.color = Color.Lerp (Color.red, Color.green, _currentHealth / settings.health);

			if (_currentHealth > settings.health)
			{
				_currentHealth = settings.health;
			}

			if (didModifyHealthByValue != null)
			{
				didModifyHealthByValue (value);
			}

			if (_currentHealth < 0f)
			{
				if (didDieAtPosition != null)
				{
					didDieAtPosition (transform.position);
				}

				if (didDieAtPositionGlobal != null)
				{
					if (tag != "Player")
					{
						didDieAtPositionGlobal (transform.position);
					}
				}

				gameObject.SetActive (false);
			}
		}
	}
}