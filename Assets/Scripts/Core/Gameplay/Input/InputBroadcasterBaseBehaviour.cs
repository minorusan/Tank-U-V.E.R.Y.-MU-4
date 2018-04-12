using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Settings.GamePlay.Input;
using System;


namespace Core.GamePlay.Input
{
	public delegate void InputEventHandler (InputBroadcasterBaseBehaviour sender, MovementInputEventArgs args);
	public delegate void InteractionEventHandler (InputBroadcasterBaseBehaviour sender, InteractionInputEventArgs args);

	public class InputBroadcasterBaseBehaviour : MonoBehaviour
	{
		public Camera inputCamera;
		public AInputSettingsBase settings;

		/// <summary>
		/// Occurs when tank control input performed.
		/// </summary>
		public event InputEventHandler didPerformInput;
		public event Action didRealeaseInput;

		/// <summary>
		/// Occurs when mouse movement input performs.
		/// </summary>
		public event InputEventHandler didPerformMouseInput;
		public event InputEventHandler didPerformMouseDragInput;
		public event Action didPushShotButton;
		public event Action didReleaseShotButton;

		public event Action didSelectNextWeapon;
		public event Action didSelectPreviousWeapon;

		public event InteractionEventHandler didBeginHold;
		public event InteractionEventHandler didEndHold;
		public event InteractionEventHandler didPerformClick;


		protected virtual void Awake()
		{
			Debug.Assert (inputCamera != null, Strings.NoCameraFound);
		}

		protected void InvokeInputEvent(EMovementInputType type, MovementInputEventArgs args)
		{
			switch (type)
			{
				case EMovementInputType.mouse:
					{
						didPerformMouseInput (this, args);
						break;
					}
				case EMovementInputType.body:
					{
						didPerformInput (this, args);
						break;
					}
				case EMovementInputType.drag:
					{
						didPerformMouseDragInput (this, args);
						break;
					}
				case EMovementInputType.release:
					{
						didRealeaseInput ();
						break;
					}
			}	
		}

		protected void InvokeShootingEvent(EShootingEventType type)
		{
			switch (type)
			{
				case EShootingEventType.push:
					{
						didPushShotButton ();
						break;
					}
				case EShootingEventType.release:
					{
						didReleaseShotButton ();
						break;
					}
			}	
		}

		protected void InvokeSelectWeaponEvent(ESelectWeaponEvent type)
		{
			switch (type)
			{
				case ESelectWeaponEvent.next:
					{
						didSelectNextWeapon ();
						break;
					}
				case ESelectWeaponEvent.previous:
					{
						didSelectPreviousWeapon ();
						break;
					}
			}	
		}
	}
}

