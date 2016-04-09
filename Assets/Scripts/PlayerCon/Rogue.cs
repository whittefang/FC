using UnityEngine;
using System.Collections;
using XInputDotNetPure;
public enum status {Normal = 0,Stealth = 1};
public class Rogue : Ability {
	private status playerState = status.Normal;


	public override void abilityUpdate(GamePadState state,GamePadState prestate){
		//ability defs here
		if (state.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released) {
			if (playerState == status.Normal) {

			} 
			else if (playerState == status.Stealth) {

			}
		}
		if (state.Buttons.B == ButtonState.Pressed && state.Buttons.B == ButtonState.Released) {
			if (playerState == status.Normal) {

			} 
			else if (playerState == status.Stealth) {

			}
		}
		if (state.Buttons.X == ButtonState.Pressed && state.Buttons.X == ButtonState.Released) {
			if (playerState == status.Normal) {

			} 
			else if (playerState == status.Stealth) {

			}
		}
		if (state.Buttons.Y == ButtonState.Pressed && state.Buttons.Y == ButtonState.Released) {
			if (playerState == status.Normal) {

			} 
			else if (playerState == status.Stealth) {

			}
		}
	}
	

}
