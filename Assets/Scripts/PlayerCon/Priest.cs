using UnityEngine;
using System.Collections;
using XInputDotNetPure;
public class Priest : Ability {
	//note: other than the constructer and abilityupdate functions, there should be no other functions in this class
	public Priest(int playerHealth,float playerSpeed){
		healthSetMax (playerHealth);
		healthSet (playerHealth);
		speedSet (playerSpeed);
		speedSetMax (playerSpeed);
	}
	public override void abilityUpdate(GamePadState state,GamePadState prestate,GameObject obj){
		//"desc/desc"
		if (state.Buttons.A == ButtonState.Pressed && prestate.Buttons.A == ButtonState.Released) {


		}
		//"desc/desc"
		if (state.Buttons.B == ButtonState.Pressed && prestate.Buttons.B == ButtonState.Released) {


		}
		//"desc/desc"
		if (state.Buttons.X == ButtonState.Pressed && prestate.Buttons.X == ButtonState.Released) {


		}
		//"desc/desc"
		if (state.Buttons.Y == ButtonState.Pressed && prestate.Buttons.Y == ButtonState.Released) {

		}


	}


}
