using UnityEngine;
using System.Collections;
using XInputDotNetPure;
public abstract class Ability{

	public abstract void abilityUpdate(GamePadState state,GamePadState prestate);
}
