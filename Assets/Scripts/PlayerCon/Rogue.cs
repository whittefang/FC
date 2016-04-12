using UnityEngine;
using System.Collections;
using XInputDotNetPure;
public enum status {Normal = 0,Stealth = 1};
public class Rogue : Ability {
	//note: other than the constructer and abilityupdate functions, there should be no other functions in this class
	private status playerState = status.Normal;
	private float daggerState = .3f;
	private float dash = 3f;
	public Rogue(int playerHealth,float playerSpeed){
		healthSetMax (playerHealth);
		healthSet (playerHealth);
		speedSet (playerSpeed);
		speedSetMax (playerSpeed);
	}
	public override void abilityUpdate(GamePadState state,GamePadState prestate,GameObject obj){
		//"basic attack/back stab"
		if (state.Buttons.A == ButtonState.Pressed && prestate.Buttons.A == ButtonState.Released) {
			if (playerState == status.Normal && daggerState <= .3f) {
				daggerState = 1f;
			} 
			else if (playerState == status.Stealth && daggerState <= .3f) {
				playerState = status.Normal;
				daggerState = 1f;
			}
		}
		//"/poison bomb"
		if (state.Buttons.B == ButtonState.Pressed && prestate.Buttons.B == ButtonState.Released) {
			if (playerState == status.Normal) {

			} 
			else if (playerState == status.Stealth) {
				playerState = status.Normal;
			}
		}
		//"dash/stun"
		if (state.Buttons.X == ButtonState.Pressed && prestate.Buttons.X == ButtonState.Released) {
			if (playerState == status.Normal) {
				RaycastHit2D[] hits = Physics2D.RaycastAll((Vector2)obj.transform.position,new Vector2(state.ThumbSticks.Left.X,state.ThumbSticks.Left.Y),3);
				RaycastHit2D dashTo = hits[0];
				for(int i = 0; i < hits.Length && hits[i].distance < 3; i ++){
					dashTo = hits[i];
					if (hits [i].transform.name != obj.name)
						break;
				}
				Debug.DrawRay (obj.transform.position, new Vector3 (state.ThumbSticks.Left.X * 3, state.ThumbSticks.Left.Y * 3, 0));
				if (dashTo != null)
				obj.transform.position += new Vector3 (state.ThumbSticks.Left.X * dashTo.distance, state.ThumbSticks.Left.Y * dashTo.distance);

			} 
			else if (playerState == status.Stealth) {
				playerState = status.Normal;
				//status effect here
			}
		}
		//"Ult"
		if (state.Buttons.Y == ButtonState.Pressed && prestate.Buttons.Y == ButtonState.Released) {
			if (playerState == status.Normal) {
				playerState = status.Stealth;
			} 

		}
		obj.GetComponent<Children> ().childrenObj.transform.localPosition= new Vector3 (daggerState, 0, 0);
		if (daggerState > .3f)
			daggerState -= .06f;
	}
	

}
