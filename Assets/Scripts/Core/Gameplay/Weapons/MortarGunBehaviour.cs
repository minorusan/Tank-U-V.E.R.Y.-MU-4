using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.ObjectPooling;
using System.Runtime.InteropServices;
using Utils;


namespace Core.GamePlay.Weapons
{
	public class MortarGunBehaviour : AWeaponBaseBehaviour
	{
		private float _timeChanneled;
		private bool _isReloading;

		#region MonoBehaviour

		protected override void Update()
		{
			base.Update ();
			if (_isReloading)
			{
				_timeChanneled += Time.deltaTime;
				weaponStatusImage.fillAmount = _timeChanneled / settings.channelTime;
				weaponStatusImage.color = Color.Lerp (Color.white, Color.green, _timeChanneled / settings.channelTime);
				if (weaponStatusImage.fillAmount >= 1f)
				{
					_isReloading = false;
					weaponStatusImage.enabled = false;

				}
			}
		}

		#endregion

		#region implemented abstract members of AWeaponBaseBehaviour

		protected override void UpdateProjectiles()
		{
			
		}

		protected override void HandleShot()
		{

		}

		protected override void HandleShotRelease()
		{
			if (_isReloading)
			{
				return;
			}
			_isReloading = true;
			weaponStatusImage.enabled = true;
			weaponStatusImage.fillAmount = 0f;
			weaponStatusImage.color = Color.white;
			var proj = PoolManager.Instance.ReuseObject (_projectile, transform.position, transform.rotation);

			var projectileCollision = proj.GetComponentInChildren <ProjectileCollisionBehaviour> ();
			projectileCollision.aoeRadius = settings.aoeRadius;
			projectileCollision.damage = settings.damage;
			projectileCollision.crashSound = settings.explodeSound;

			PlayerAudioBehaviour.PlaySound (settings.launchSound, transform.position);

			var target = _crossHair.transform.position;
			float shotHeight = target.magnitude * settings.multiplier;

			proj.GetComponent <Rigidbody> ().velocity = PhysicsHelpers.velocityForBasketBallThrow (transform.position, target, shotHeight);
			_timeChanneled = 0f;
		}

		protected override void UpdateCrosshair()
		{
			var newPosition = transform.position + (transform.forward.normalized * settings.range);
			newPosition.y = 1;
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