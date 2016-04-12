using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour{
	public enum playerClass {Rogue = 0,Mage = 1,Priest = 2,Warrior = 3};
	public PlayerIndex controllerNum;
	public playerClass Class;
	private Ability script;

	private GamePadState state;
	private GamePadState prestate;
	// Use this for initialization
	void Start () {
		state = GamePad.GetState (controllerNum);
		prestate = state;
		switch(Class)
		{
		case playerClass.Rogue:
			script = new Rogue (100,.1f);
			break;
		case playerClass.Mage:
			script = new Mage (100,.1f);
			break;
		case playerClass.Priest:
			script = new Priest (100,.1f);
			break;
		case playerClass.Warrior:
			script = new Warrior (100,.1f);
			break;
		}
	}

	// this is just movement updates and abylity update
	void FixedUpdate () {
		state = GamePad.GetState (controllerNum);
		transform.position += new Vector3 (script.speedGet() * state.ThumbSticks.Left.X, script.speedGet() * state.ThumbSticks.Left.Y, 0);
		float x = state.ThumbSticks.Left.X;
		float y = state.ThumbSticks.Left.Y;
		float radius = Mathf.Sqrt (Mathf.Pow (x, 2) + Mathf.Pow (y, 2));
		if(!(x == 0 && y ==0))
			transform.rotation = Quaternion.Euler(new Vector3(
				0,0,y >0 ?
				Vector3.Angle(new Vector3(radius,0,0), new Vector3(x,y,0)):
				Vector3.Angle(new Vector3(radius,0,0), new Vector3(x,y,0))*-1)
			);
		script.abilityUpdate (state,prestate,gameObject);
		prestate = state;
	}



	/**
	 * when doing anything to effect another player call this function
	 */
	public Ability effect(){
		return script;
	}
}
