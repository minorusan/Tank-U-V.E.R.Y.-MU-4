using System.Collections;
using System.Collections.Generic;
using Core.GamePlay;
using UnityEngine;


namespace Core.AI
{
	[CreateAssetMenu (fileName = "New attack state", menuName = "AIStates/Attack state")]
	public sealed class AttackStateDefinition : AIStateBase
	{
		public float attackDistanse = 3f;
		public float damage;

		#region implemented abstract members of AIStateBase

		public override void OnAnimationCallback()
		{
			GameGlobalsBehaviour.playerHealth.SubstractHealth (damage);
			PlayerAudioBehaviour.PlaySound (EAudioEventType.zombieAttack, _body.transform.position);
		}

		public override AIStateBase TransitTo()
		{
			return transitions [0];
		}

		public override bool IsCompleted()
		{
			var dist = Vector3.Distance (_body.transform.position, GameGlobalsBehaviour.player.transform.position);
			return dist > attackDistanse;
		}

		public override void OnEnter(GameObject fragileBody)
		{
			_body = fragileBody;
			_animator = _body.GetComponentInChildren <Animator> ();
			_animator.SetTrigger ("attacks");
		}

		public override void Update()
		{
			var position = GameGlobalsBehaviour.player.transform.GetChild (0).position;
			position.y = 0.3f;
			_body.transform.LookAt (position);
			var rotation = _body.transform.rotation.eulerAngles;
			rotation.x = 0f;
			_body.transform.rotation = Quaternion.Euler (rotation);
		}

		public override void OnLeave()
		{
			
		}

		#endregion
		
	}
}