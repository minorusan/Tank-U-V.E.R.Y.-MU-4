using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.ObjectPooling;
using System.Runtime.InteropServices;


namespace Core.GamePlay.Weapons
{
	public class BasicStraightCannonBehaviour : AWeaponBaseBehaviour
	{
		private Dictionary<GameObject, Vector3> _projectileStartPositions = new Dictionary<GameObject, Vector3> ();
		private float _timeChanneled;
		private bool _isCharging;

		#region MonoBehaviour

		protected override void Update()
		{
			base.Update ();
			if (_isCharging)
			{
				_timeChanneled += Time.deltaTime;
				weaponStatusImage.fillAmount = _timeChanneled / settings.channelTime;
				weaponStatusImage.color = Color.Lerp (Color.white, Color.green, _timeChanneled / settings.channelTime);
			}
		}

		#endregion

		#region implemented abstract members of AWeaponBaseBehaviour

		protected override void UpdateProjectiles()
		{
			for (int i = 0; i < _activeProjectiles.Count; i++)
			{
				var currentPosition = _activeProjectiles [i].transform.position;
				if (Vector3.Distance (currentPosition, _projectileStartPositions [_activeProjectiles [i]]) > settings.range)
				{
					_activeProjectiles [i].gameObject.SetActive (false);
					_projectileStartPositions.Remove (_activeProjectiles [i]);
					_activeProjectiles.RemoveAt (i);
					continue;
				}

				currentPosition = Vector3.MoveTowards (currentPosition, currentPosition +
					_activeProjectiles [i].transform.forward, Time.deltaTime * settings.projectileSpeed);
				_activeProjectiles [i].transform.position = currentPosition;
			}
		}

		protected override void HandleShot()
		{
			_isCharging = true;
			weaponStatusImage.enabled = true;
		
		}

		protected override void HandleShotRelease()
		{
			_isCharging = false;
			weaponStatusImage.enabled = false;
			weaponStatusImage.fillAmount = 0f;
			weaponStatusImage.color = Color.white;
			var proj = PoolManager.Instance.ReuseObject (_projectile, transform.position, transform.rotation);

			var projectileCollision = proj.GetComponentInChildren <ProjectileCollisionBehaviour> ();
			projectileCollision.aoeRadius = settings.aoeRadius;
			projectileCollision.damage = settings.damage * _timeChanneled;
			projectileCollision.crashSound = settings.explodeSound;

			PlayerAudioBehaviour.PlaySound (settings.launchSound, transform.position);

			_projectileStartPositions.Add (proj, transform.position);
			_activeProjectiles.Add (proj);
			_timeChanneled = 0f;
		}

		protected override void UpdateCrosshair()
		{
			var newPosition = transform.position + (transform.forward.normalized * settings.range);
			RaycastHit hit;
			if (Physics.Raycast (transform.position, transform.forward, out hit, settings.range))
			{
				newPosition = hit.point;
			}

			_crossHair.transform.position = newPosition;
		}

		#endregion
	}
}