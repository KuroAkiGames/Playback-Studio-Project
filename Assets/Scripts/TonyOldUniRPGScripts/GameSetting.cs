using UnityEngine;
using System.Collections;

public class GameSetting : MonoBehaviour {

	void Awake(){
				DontDestroyOnLoad (this);
		}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SaveChracterData() {
		//PlayerPrefs.SetString (""); For MMO usage setting he characters name not used for RoM or SAO UW
		}
	void LoadCharacterData(){
		}
}
