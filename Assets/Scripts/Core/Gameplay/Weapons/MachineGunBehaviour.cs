using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.ObjectPooling;
using System.Runtime.InteropServices;
using Utils;


namespace Core.GamePlay.Weapons
{
	public class MachineGunBehaviour: AWeaponBaseBehaviour
	{
		private enum EMachineGunState
		{
			idle,
			fire,
			reloading
		}

		private EMachineGunState _state;
		private float _timeChanneled;
		private bool _isReloading;

		private bool _beganLaunch;
		private float _fireRate;

		protected override void OnEnable()
		{
			base.OnEnable ();
			_state = EMachineGunState.idle;
		}

		#region MonoBehaviour

		protected override void Update()
		{
			base.Update ();
			switch (_state)
			{
				case EMachineGunState.idle:
					break;
				case EMachineGunState.fire:
					FireIfNeeded ();
					break;
				case EMachineGunState.reloading:
					Reload ();
					break;
				
				default:
					break;
			}
					
		}

		#endregion

		#region implemented abstract members of AWeaponBaseBehaviour

		private void Reload()
		{
			_timeChanneled += Time.deltaTime;
			weaponStatusImage.fillAmount = _timeChanneled / settings.channelTime;
			weaponStatusImage.color = Color.Lerp (Color.white, Color.green, _timeChanneled / settings.channelTime);
			if (weaponStatusImage.fillAmount >= 1f)
			{
				_isReloading = false;
				_state = EMachineGunState.idle;
				weaponStatusImage.fillAmount = 0f;
				_timeChanneled = 0f;
				weaponStatusImage.enabled = false;
			}
		}

		private void FireIfNeeded()
		{
			if (_fireRate <= 0f)
			{
				_fireRate = settings.projectileSpeed;
				var proj = PoolManager.Instance.ReuseObject (_projectile, transform.position, transform.rotation);

				var projectileCollision = proj.GetComponentInChildren <ProjectileCollisionBehaviour> ();
				projectileCollision.aoeRadius = settings.aoeRadius;
				projectileCollision.damage = settings.damage;
				projectileCollision.crashSound = settings.explodeSound;

				PlayerAudioBehaviour.PlaySound (settings.launchSound, transform.position);

				var target = _crossHair.transform.position;
				target.x += UnityEngine.Random.Range (-0.5f, 0.5f);
				target.z += UnityEngine.Random.Range (-0.5f, 0.5f);

				float shotHeight = target.magnitude * settings.multiplier;

				proj.GetComponent <Rigidbody> ().velocity = PhysicsHelpers.velocityForBasketBallThrow (transform.position, target, shotHeight);
			}

			_timeChanneled += Time.deltaTime;
			weaponStatusImage.fillAmount = _timeChanneled / settings.channelTime;
			weaponStatusImage.color = Color.Lerp (Color.white, Color.red, _timeChanneled / settings.channelTime);
			if (weaponStatusImage.fillAmount >= 1f)
			{
				_state = EMachineGunState.reloading;
				_timeChanneled = 0f;
			}

			_fireRate -= Time.deltaTime;
		}

		protected override void UpdateProjectiles()
		{
			
		}

		protected override void HandleShot()
		{
			if (_state == EMachineGunState.reloading)
			{
				return;
			}

			_state = EMachineGunState.fire;
			weaponStatusImage.enabled = true;
		}

		protected override void HandleShotRelease()
		{
			if (_state == EMachineGunState.reloading)
			{
				return;
			}
			if (weaponStatusImage.fillAmount < 1f)
			{
				_state = EMachineGunState.idle;
			}
			
			_isReloading = true;

			weaponStatusImage.fillAmount = 0f;
			weaponStatusImage.color = Color.white;

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