using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Settings.GamePlay.Input;


namespace Core.GamePlay.Input
{
	
	public class PlayerInputBehaviour : MonoBehaviour
	{
		private float _sensevity;
		private float _queuedRotation;
		private bool _canPlayAccelerate = true;
		private Vector3 _quedVelocityGain;

		private InputBroadcasterBaseBehaviour _inputBroadcaster;

		#region MonoBehaviour

		private void Awake()
		{
			_inputBroadcaster = FindObjectOfType<InputBroadcasterBaseBehaviour> ();
			_inputBroadcaster.didPerformInput += OnControlInputWasPerformed;
			_inputBroadcaster.didPerformMouseDragInput += OnMouseDragInputWasPerformed;
			_inputBroadcaster.didRealeaseInput += OnInputReleased;
			_sensevity = (_inputBroadcaster.settings as DesktopInputSettings).mouseSensevity;
		}

		private void FixedUpdate()
		{
			var velocty = Vector3.zero;
			var forward = transform.forward;
			transform.Translate ((forward + _quedVelocityGain) * _quedVelocityGain.magnitude * _inputBroadcaster.settings.speedMultiplier);

			_quedVelocityGain *= _inputBroadcaster.settings.speedDump;
		}

		private void Update()
		{
			transform.Rotate (new Vector3 (0f, _queuedRotation, 0f));
			_queuedRotation = 0f;
		}

		#endregion

		#region Events

		private void OnControlInputWasPerformed(InputBroadcasterBaseBehaviour sender, MovementInputEventArgs args)
		{
			if (Mathf.Approximately (args.horisontalWorldInputValue, 0f) && Mathf.Approximately (args.verticalWorldInputValue, 0f))
			{
				_quedVelocityGain = Vector3.zero;
			}

			if (_canPlayAccelerate)
			{
				PlayerAudioBehaviour.PlaySound (EAudioEventType.accelerate, transform.position);
				_canPlayAccelerate = false;
			}

			_quedVelocityGain.x += args.horisontalWorldInputValue;
			_quedVelocityGain.z += args.verticalWorldInputValue;
		}

		void OnInputReleased()
		{
			_canPlayAccelerate = true;
		}


		void OnMouseDragInputWasPerformed(InputBroadcasterBaseBehaviour sender, MovementInputEventArgs args)
		{
			_queuedRotation = args.horisontalWorldInputValue * _sensevity;
		}

		#endregion

	}
}

