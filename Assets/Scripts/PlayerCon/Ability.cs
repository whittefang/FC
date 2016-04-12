using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public abstract class Ability{
	/**
	 * @param state Current gamepad state
	 * @param prestate Previos gamepad state
	 * @param obj The GameObject that the calling script is attached to
	 * 
	 * This function deffinition is ment to be overwritten over
	 */
	public abstract void abilityUpdate(GamePadState state,GamePadState prestate, GameObject obj);






	/************************************************************************************************************************************************
	 * Health Controller
	 * 
	 * all health related functions are preceaded with the word health
	 * 
	 * 
	 ************************************************************************************************************************************************/
	private int health = 0;
	private int maxHealth = 0;
	public bool isDead(){
		return (health == 0);
	}
	public int healthGet(){
		return health;
	}
	public void healthSetMax(int a){
		maxHealth = a;
	}
	public void healthSet(int a){
		if (a > maxHealth)
			health = maxHealth;
		health = a;
	}
	public void healthChange(int a){
		if (health + a > maxHealth)
			health = maxHealth;
		if (health + a <= 0)
			health = 0;
		health += a;
	}
	public void healthOverheal(int a){
		if(a > 0)
		health += a;
	}
	/************************************************************************************************************************************************
	 * Speed Controller
	 * 
	 * all speed related functions are preceaded with the word speed
	 * 
	 * 
	 ************************************************************************************************************************************************/
	private float speed = 0;
	private float maxSpeed = 0;
	public float speedGet(){
		return speed;
	}
	public void speedSet(float a){
		speed = a;
	}
	public void speedSetMax(float a){
		maxSpeed = a;
	}
	public void speedChange(float a){
		if (speed + a > maxSpeed)
			speed = maxSpeed;
		if (speed + a < 0)
			speed = 0;
		speed += a;
	}
	public void speedUp(int a){
		if(a > 0)
		speed += a;
	}

	/************************************************************************************************************************************************
	 * Debuff Controller
	 * 
	 * all Debuff related functions are preceaded with the word Debuff
	 * 
	 * 
	 ************************************************************************************************************************************************/
	public enum debuffs {poison = 0, slowness = 1,fear = 3};
	private List<debuffs> currentDebuffs;
	public List<debuffs> debuffGetCurrent(){
		return currentDebuffs;
	}
	public int debuffGetNum(debuffs a){
		int temp = 0;
		foreach (debuffs de in currentDebuffs)
			if (de == a)
				temp++;
		return temp;
	}
	public void debuffAdd(debuffs a){
		currentDebuffs.Add (a);
	}
	public bool debuffRemove(debuffs a){
		return currentDebuffs.Remove (a);
	}
	public void debuffClear(){
		currentDebuffs.Clear ();
	}
}
