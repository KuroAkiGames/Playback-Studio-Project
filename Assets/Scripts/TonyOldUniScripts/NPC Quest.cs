using UnityEngine;
using System.Collections;

public class NPCQuest : MonoBehaviour {
	public GUISkin talkSkin;
	public string[] answerButtons;
	public string[] Questions;
	public int RewardAmount;
	bool DisplayDialog = false;
	bool ActivateQuest = false;
	public bool QuestDone = false;

	public Texture texture1;
	public Texture texture2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUI.skin = talkSkin;
		GUILayout.BeginArea (new Rect(700, 600, 400, 400));
			if (DisplayDialog && !ActivateQuest){
			disableMouse();
			// if(disableMouseLook = true)

			//GUILayer.Label(Questions[0]);
			//GUILayer.Label(Questions[1]);
			if(GUILayout.Button (answerButtons[0])){
				ActivateQuest = true;
				QuestDone = false;
				DisplayDialog = false;
				//enableMouse();
			}
			if(GUILayout.Button (answerButtons[1])){
				DisplayDialog = false;
				//enableMouse();
			}
		}
		if(DisplayDialog && !ActivateQuest && QuestDone){
			disableMouse();
			//GUILayer.Label(Questions[2]);
			if(GUILayout.Button (answerButtons[2])){
				QuestCompleted ();
				DisplayDialog = false;
				//enableMouse();
			}
		}
		GUILayout.EndArea();

		if(ActivateQuest == true){
			//GUI.Box (new Rect (0, 0, Screen.width, Screen.height), "");
			//GUI.DrawTexture(new Rect (10,10,200, 150), texture1);
		}
	}
	void OnTriggerEnter(){
		DisplayDialog = false;
		//enableMouse();
	}
	private void DisableMouseLook(bool enable){
		//GameObject TPC = GameObject.FindWithTag("Player");

		//TPC.transform.GetComponent<CharacterMotor>().enabled = enable;
		//TPC.transform.GetComponent<MouseLook>().enabled = enable;
		//Camera.mainCamera.GetComponet<MouseLook>().enabled = enabled;
	}
	void QuestCompleted(){
		//PlayerGold.playerGold += RewardAmount;
		//GameObject.Destroy (Collider);
	}
		void disableMouse(){
		//GameObject player = GameObject.FindWithTag ("MainCamera");
		//cam.GetComponent<MouseLook>().enabled = true;
	}
}
