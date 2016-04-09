using UnityEngine;
using System.Collections;
using XInputDotNetPure;
public class Rogue : Ability {
	public override void abilityUpdate(GamePadState state,GamePadState prestate){
		//ability defs here
		if (state.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released) {

		}
		if (state.Buttons.B == ButtonState.Pressed && state.Buttons.B == ButtonState.Released) {

		}
		if (state.Buttons.X == ButtonState.Pressed && state.Buttons.X == ButtonState.Released) {

		}
		if (state.Buttons.Y == ButtonState.Pressed && state.Buttons.Y == ButtonState.Released) {

		}
	}
	

}
