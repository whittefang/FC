using UnityEngine;
using System.Collections;
using XInputDotNetPure;
public abstract class Ability{
	public int abilityState = 0;
	public abstract void abilityUpdate(GamePadState state,GamePadState prestate);
}
