var goPlayer: GameObject;
var sSceneName: String;
var iTriggerDistance = 1;

function Update () {

	if(Vector3.Distance(transform.position,goPlayer.transform.position)<iTriggerDistance){
		if(sSceneName!=""){
			Application.LoadLevel(sSceneName);
		}
	}
	//Debug.Log("Distance to " + sSceneName + " door is " + Vector3.Distance(transform.position,goPlayer.transform.position) + " units away");
}