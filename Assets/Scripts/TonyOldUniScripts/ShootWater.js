#pragma strict
var bullet : GameObject;
var muzzlePoint : Transform;

function Start () {

}

function Update () {
	if(Input.GetButton("Fire2")) {
		var instance = Instantiate(bullet, muzzlePoint.position, 
muzzlePoint.rotation);
		instance.transform.Rotate(45,0,0);
        instance.rigidbody.AddForce(transform.forward * 1000);
        
        }
}