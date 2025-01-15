#pragma strict
var sSceneName: String;

function Start () {
	yield WaitForSeconds (26);
	{
		if(sSceneName!=""){
			Application.LoadLevel(sSceneName);
		}
	}
}

function Update () {
	if(Input.GetKeyDown(KeyCode.Escape)){
		if(sSceneName!=""){
			Application.LoadLevel(sSceneName);
		}
	}
}