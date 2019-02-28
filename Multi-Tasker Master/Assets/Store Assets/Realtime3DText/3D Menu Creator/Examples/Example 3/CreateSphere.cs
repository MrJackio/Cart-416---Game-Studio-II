using UnityEngine;
using System.Collections;

public class CreateSphere : MonoBehaviour {
	public void createSphere()
	{
		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.AddComponent<Rigidbody>();
		sphere.GetComponent<Collider>().material = new PhysicMaterial();
		sphere.GetComponent<Collider>().material.bounciness = 1;
	}
	public void createCube()
	{
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.AddComponent<Rigidbody>();
		cube.GetComponent<Collider>().material = new PhysicMaterial();
		cube.GetComponent<Collider>().material.bounciness = 1;
	}
	public void createCapsule()
	{
		GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		capsule.AddComponent<Rigidbody>();
		capsule.GetComponent<Collider>().material = new PhysicMaterial();
		capsule.GetComponent<Collider>().material.bounciness = 1;
	}
}
