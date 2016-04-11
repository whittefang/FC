using UnityEngine;
using System.Collections;
using XInputDotNetPure;
public enum playerClass {Rogue = 0,Mage = 1,Priest = 2,Warrior = 3};
public class PlayerController : MonoBehaviour {
	public PlayerIndex controllerNum;
	public playerClass Class;
	private Ability script;
	public float playerSpeed = 1;
	private GamePadState state;
	private GamePadState prestate;
	// Use this for initialization
	void Start () {
		state = GamePad.GetState (controllerNum);
		prestate = state;
		switch(Class)
		{
		case playerClass.Rogue:
			script = new Rogue ();
			break;
		case playerClass.Mage:
			break;
		case playerClass.Priest:
			break;
		case playerClass.Warrior:
			break;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		state = GamePad.GetState (controllerNum);
		transform.position += new Vector3 (playerSpeed * state.ThumbSticks.Left.X, playerSpeed * state.ThumbSticks.Left.Y, 0);
		float x = state.ThumbSticks.Left.X;
		float y = state.ThumbSticks.Left.Y;
		float radius = Mathf.Sqrt (Mathf.Pow (x, 2) + Mathf.Pow (y, 2));
		if(!(x == 0 && y ==0))
			transform.rotation = Quaternion.Euler(new Vector3(
				0,0,y >0 ?
				Vector3.Angle(new Vector3(radius,0,0), new Vector3(x,y,0)):
				Vector3.Angle(new Vector3(radius,0,0), new Vector3(x,y,0))*-1)
			);
		script.abilityUpdate (state,prestate);
		prestate = state;
	}
}
