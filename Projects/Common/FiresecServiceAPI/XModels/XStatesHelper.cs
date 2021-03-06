﻿using System.Collections;
using System.Collections.Generic;
using XFiresecAPI;

namespace FiresecAPI.XModels
{
	public static class XStatesHelper
	{
		public static int XStateTypeToPriority(XStateType stateType)
		{
			switch (stateType)
			{
				case XStateType.Fire1:
				case XStateType.Fire2:
					return 0;

				case XStateType.Attention:
					return 1;

				case XStateType.Failure:
					return 2;

				case XStateType.Ignore:
					return 4;

				case XStateType.Test:
					return 6;

				case XStateType.Off:
				case XStateType.TurningOn:
				case XStateType.TurningOff:
				case XStateType.TurnOn:
				case XStateType.CancelDelay:
				case XStateType.TurnOff:
				case XStateType.Stop:
				case XStateType.ForbidStart:
				case XStateType.TurnOnNow:
				case XStateType.TurnOffNow:
				case XStateType.Reserve1:
				case XStateType.Reserve2:
					return 6;

				case XStateType.Norm:
				case XStateType.On:
					return 7;
			}
			return 7;
		}

		public static List<XStateType> StatesFromInt(int intValue)
		{
			var states = new List<XStateType>();
			var bitArray = new BitArray(new int[1] { intValue });
			for (int bitIndex = 0; bitIndex < bitArray.Count; bitIndex++)
			{
				var b = bitArray[bitIndex];
				if (b)
				{
					var stateTupe = (XStateType)bitIndex;
					states.Add(stateTupe);
				}
			}
			return states;
		}

		public static StateType XStateTypesToState(List<XStateType> states)
		{
			var minPriority = 7;
			foreach (var state in states)
			{
				var priority = XStateTypeToPriority(state);
				if (priority < minPriority)
				{
					minPriority = priority;
				}
			}
			StateType stateType = (StateType)minPriority;
			return stateType;
		}
	}
}