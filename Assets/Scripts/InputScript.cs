using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#

public class InputScript : MonoBehaviour {
	public delegate void buttonDelegate();
	buttonDelegate aButtonPress, bButtonPress, xButtonPress, yButtonPress, aButtonRelease, bButtonRelease, xButtonRelease, yButtonRelease;


	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	int playerNumber = 0;
	// Use this for initialization
	void Start () {

		playerIndex = (PlayerIndex)playerNumber;
	}

	// Update is called once per frame
	void FixedUpdate () {
		prevState = state;
		state = GamePad.GetState (playerIndex, GamePadDeadZone.None);

		// send input for movement(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);

		// Detect if a button was pressed this frame
		if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed) {
			aButtonPress ();
		}
		// Detect if a button was released this frame
		if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released) {
			aButtonRelease ();
		}

		// Detect if a button was pressed this frame
		if (prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed) {
			xButtonPress ();
		}
		// Detect if a button was released this frame
		if (prevState.Buttons.X == ButtonState.Pressed && state.Buttons.X == ButtonState.Released) {
			xButtonRelease ();
		}

		// Detect if a button was pressed this frame
		if (prevState.Buttons.Y == ButtonState.Released && state.Buttons.Y == ButtonState.Pressed) {
			yButtonPress ();
		}
		// Detect if a button was released this frame
		if (prevState.Buttons.Y == ButtonState.Pressed && state.Buttons.Y == ButtonState.Released) {
			yButtonRelease ();
		}

		// x button press
		if (state.Buttons.B == ButtonState.Pressed && prevState.Buttons.B == ButtonState.Released) {
			bButtonPress ();
		}
		// Detect if a button was released this frame
		if (prevState.Buttons.B == ButtonState.Pressed && state.Buttons.B == ButtonState.Released) {
			bButtonRelease ();
		}


	}

	// takes in function delegate and assigns them to appropriate buttons
	public void assignAButton(buttonDelegate aPress, buttonDelegate aRelease){
		aButtonPress = aPress;
		aButtonRelease = aRelease;
	}
	// takes in function delegate and assigns them to appropriate buttons
	public void assignBButton(buttonDelegate bPress, buttonDelegate bRelease){
		bButtonPress = bPress;
		bButtonRelease = bRelease;
	}
	// takes in function delegate and assigns them to appropriate buttons
	public void assignXButton(buttonDelegate xPress, buttonDelegate xRelease){
		xButtonPress = xPress;
		xButtonRelease = xRelease;
	}
	// takes in function delegate and assigns them to appropriate buttons
	public void assignYButton(buttonDelegate yPress, buttonDelegate yRelease){
		yButtonPress = yPress;
		yButtonRelease = yRelease;
	}

	public void SetPlayerNumber (int newNum){
		playerNumber = newNum;
	}
}
