using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.GamePlay.Settings;
using Core.GamePlay.Input;
using UnityEngine.UI;
using Core.ObjectPooling;


namespace Core.GamePlay.Weapons
{
	public abstract class AWeaponBaseBehaviour : MonoBehaviour
	{
		protected List<GameObject> _activeProjectiles = new List<GameObject> (20);
		private InputBroadcasterBaseBehaviour _input;

		protected GameObject _crossHair
		{
			get;
			private set;
		}

		protected GameObject _projectile
		{
			get;
			private set;
		}

		public Image weaponStatusImage;
		public WeaponSettings settings;

		protected virtual void Awake()
		{
			_crossHair = Instantiate (settings.crosshairPrefab, transform);
			_input = FindObjectOfType<InputBroadcasterBaseBehaviour> ();
			_projectile = settings.projectile;

			PoolManager.Instance.CreatePool (_projectile, 20);
		}

		protected virtual void OnEnable()
		{
			_input.didPushShotButton += OnShootingBegan;
			_input.didReleaseShotButton += OnShootingEnded;
		}

		private void OnDisable()
		{
			_input.didPushShotButton -= OnShootingBegan;
			_input.didReleaseShotButton -= OnShootingEnded;
		}

		protected virtual void Update()
		{
			UpdateProjectiles ();
			UpdateCrosshair ();
		}

		void OnShootingEnded()
		{
			HandleShotRelease ();
		}

		void OnShootingBegan()
		{
			HandleShot ();
		}

		protected abstract void UpdateCrosshair();

		protected abstract void UpdateProjectiles();

		protected abstract void HandleShot();

		protected abstract void HandleShotRelease();
	}
}