using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	static int maxHealth = 3289;	//Maximum health Limit *Note Kirito UW HP
	static int curHealth = 3289;	//Current displayed Health
	
	static float healthBarLength;	// HP bar Length *Note to make Max Cur and HP Bar appear in inspector, place a public instead of static

	//public Texture healthBarTexture;

	// Use this for initialization
	void Start () {

		healthBarLength = Screen.width / 2;
	}
	
	// Update is called once per frame
	void Update () {
			AddjustCurrentHealth (0);
		//HP Damage Taking Tester
		if(Input.GetKeyDown(KeyCode.Alpha1)){
				curHealth = curHealth - 1000;
			}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			curHealth = curHealth + 300;
			SaveHealth();
		}
	}
	
	void OnGUI() {
		GUI.Box(new Rect(10, 10, healthBarLength, 20), curHealth + "/" + maxHealth);
		if (curHealth == 0) {
				GUI.Box (new Rect (0, 0, Screen.width, Screen.height), "Game Over");
		}
	}
	
	public void AddjustCurrentHealth(int adj){
		curHealth += adj;
		
		if(curHealth < 1)
			curHealth = 0;
		
		if (curHealth > maxHealth)
			curHealth = maxHealth;
		
		if(maxHealth < 1)
			maxHealth = 1;
		
		healthBarLength = (Screen.width / 2) * (curHealth / (float)maxHealth);
		
		if (curHealth <= 1) {
			//Destroy (gameObject); 	*it breaks it...
		}
	}
	void SaveHealth(){PlayerPrefs.SetInt("HP",curHealth);}
}