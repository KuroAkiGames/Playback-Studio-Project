#pragma strict
var bullet : GameObject;
var muzzlePoint : Transform;

function Start () {

}

function Update () {
	if(Input.GetButtonDown("Fire3")) {
		var instance = Instantiate(bullet, muzzlePoint.position, 
muzzlePoint.rotation);
		instance.transform.Rotate(0,0,0);
        instance.rigidbody.AddForce(transform.forward * 1000);
        
        }
}