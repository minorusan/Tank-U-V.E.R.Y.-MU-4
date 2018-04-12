using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.GamePlay.Input;

using UnityInput = UnityEngine.Input;
using Settings.GamePlay.Input;


namespace Core.GamePlay.Input
{
	public class DesktopInputBroadcasterBehaviour : InputBroadcasterBaseBehaviour
	{
		private Vector2 _previousMouseDrag;
		private float _mouseDragTrashHold;
		private bool _didBeginShooting;
		private bool _prevFrameGotInput;

		private MovementInputEventArgs _cachedArgs = new MovementInputEventArgs ();
		private MovementInputEventArgs _cachedMuseRotationArgs = new MovementInputEventArgs ();

		public BoxCollider2D inputCollider;

		public bool catchesMouseMovement;
		public bool catchesBodyMovement;

		#region MonoBehaviour

		protected override void Awake()
		{
			base.Awake ();
			_mouseDragTrashHold = (settings as DesktopInputSettings).mouseDragTrashHold;
		}

		private void Update()
		{
			HandleMouseInputIfNeeded ();
			HandleBodyInputIfNeeded ();
			HandleRotationIfNeeded ();
			HandleShootingIfNeeded ();
			HandleWeaponSelectionIfNeeded ();
		}

		#endregion

		#region Common code

		private void HandleShootingIfNeeded()
		{
			var vert = new int[100];

			var containsList = new List<int> (vert.Length);
			var constainsSet = new HashSet<int> (containsList);

			bool shootKey = UnityInput.GetKey (settings.inputSceme.shootKey);
			if (shootKey && !_didBeginShooting)
			{
				InvokeShootingEvent (EShootingEventType.push);
				_didBeginShooting = true;
			}

			if (!shootKey && _didBeginShooting)
			{
				InvokeShootingEvent (EShootingEventType.release);
				_didBeginShooting = false;
			}
		}

		private void HandleRotationIfNeeded()
		{
			if (UnityInput.GetMouseButton (settings.inputSceme.beginRotateButtonIndex))
			{
				Cursor.visible = false;
				var diff = Mathf.Abs (UnityInput.mousePosition.x - _previousMouseDrag.x);
				if (diff > _mouseDragTrashHold)
				{
					_cachedMuseRotationArgs.horisontalWorldInputValue = UnityInput.mousePosition.x > _previousMouseDrag.x ? 1f : -1f;
				}
				else
				{
					_cachedMuseRotationArgs.horisontalWorldInputValue = 0f;
				}

				_previousMouseDrag.x = UnityInput.mousePosition.x;
				InvokeInputEvent (EMovementInputType.drag, _cachedMuseRotationArgs);
			}
			else
			{
				Cursor.visible = true;
			
				_previousMouseDrag = Vector2.zero;
				_cachedMuseRotationArgs.horisontalWorldInputValue = 0;
				_cachedMuseRotationArgs.verticalWorldInputValue = 0;
			}
		}

		private void HandleBodyInputIfNeeded()
		{
			if (catchesBodyMovement == false)
			{
				return;
			}

			_cachedArgs.horisontalWorldInputValue = 0f;
			_cachedArgs.verticalWorldInputValue = 0f;

			bool right = UnityInput.GetKey (settings.inputSceme.right);
			bool left = UnityInput.GetKey (settings.inputSceme.left);
			bool horisontal = left || right;

			bool forward = UnityInput.GetKey (settings.inputSceme.forward);
			bool back = UnityInput.GetKey (settings.inputSceme.back);
			bool vertical = back || forward;

			float hInputValue = 0f;
			float vInputValue = 0f;

			if (horisontal)
			{
				hInputValue += right ? 1f : 0f;
				hInputValue -= left ? 1f : 0f;
				_cachedArgs.horisontalWorldInputValue = hInputValue;
			}

			if (vertical)
			{
				vInputValue += forward ? 1f : 0f;
				vInputValue -= back ? 1f : 0f;
				_cachedArgs.verticalWorldInputValue = vInputValue;
			}

			if (vertical || horisontal)
			{
				InvokeInputEvent (EMovementInputType.body, _cachedArgs);
				_prevFrameGotInput = true;
			}
			else
			{
				if (_prevFrameGotInput)
				{
					InvokeInputEvent (EMovementInputType.release, _cachedArgs);
					_prevFrameGotInput = false;
				}
			}

		}

		private void HandleWeaponSelectionIfNeeded()
		{
			if (UnityInput.GetKeyDown (settings.inputSceme.swapWeaponsSetup.swapBack))
			{
				InvokeSelectWeaponEvent (ESelectWeaponEvent.previous);
				return;
			}

			if (UnityInput.GetKeyDown (settings.inputSceme.swapWeaponsSetup.swapForward))
			{
				InvokeSelectWeaponEvent (ESelectWeaponEvent.next);
			}
		}

		private void HandleMouseInputIfNeeded()
		{
			if (catchesMouseMovement == false)
			{
				return;
			}

			var input = UnityEngine.Input.mousePosition;
			var worldInput = inputCamera.ScreenToWorldPoint (input);

			_cachedArgs.verticalWorldInputValue = worldInput.y;
			_cachedArgs.horisontalWorldInputValue = worldInput.x;

			_cachedArgs.verticalScreenInputValue = input.y;
			_cachedArgs.horisontaScreenlInputValue = input.x;

			InvokeInputEvent (EMovementInputType.mouse, _cachedArgs);
		}

		#endregion
	}
}