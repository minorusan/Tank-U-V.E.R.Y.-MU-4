using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace Core.GamePlay.Input
{
	public enum ESelectWeaponEvent
	{
		previous,
		next
	}

	public enum EShootingEventType
	{
		push,
		release
	}

	public enum EMovementInputType
	{
		mouse,
		body,
		drag,
		release
	}

	public enum EInteractionInputType
	{
		click,
		beganHold,
		endHold
	}

	public class Strings
	{
		public static readonly string NoCameraFound = "InputBroadcasterBaseBehaviour::No camera found.";
	}

	//This may fire up frequently and to avoid unnesessary allocations instance should
	//be cached. For this reasongs, incapsulations is treated as martyr for performance.
	public class MovementInputEventArgs:EventArgs
	{
		public float verticalWorldInputValue;
		public float horisontalWorldInputValue;

		public float verticalScreenInputValue;
		public float horisontaScreenlInputValue;
	}

	public class InteractionInputEventArgs:EventArgs
	{
		private Vector2 _screenInput;
		private Vector3 _worldInput;

		public Vector2 screenInput
		{
			get
			{
				return _screenInput;
			}
		}

		public Vector3 worldInput
		{
			get
			{
				return _worldInput;
			}
		}

		public InteractionInputEventArgs (Vector2 screen, Vector3 world)
		{
			_screenInput = screen;
			_worldInput = world;
		}
	}
}