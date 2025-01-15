#pragma strict
var bullet : GameObject;
var muzzlePoint : Transform;
var explode: Transform;
function Start () {

}

function OnCollisionEnter(collision : Collision){
	Instantiate(explode, transform.position, transform.rotation);
	Destroy (gameObject);
	}
function Update () {
	if(Input.GetButtonDown("Fire1")) {
		var instance = Instantiate(bullet, muzzlePoint.position, 
muzzlePoint.rotation);
		instance.transform.Rotate(0,0,0);
        instance.rigidbody.AddForce(transform.forward * 1000);
        
        }
}