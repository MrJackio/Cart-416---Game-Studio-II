using UnityEngine;
using System.Collections;

public class RotateSample : MonoBehaviour
{	
	
	public bool rotateOnX;
	public bool rotateOnY;
	public bool rotateOnZ;
	
	public float maxAmount;
	public float minAmount;
	public float maxSpeed;
	public float minSpeed;
	public float maxDelay;
	public float minDelay;
	private string axis="";
	
	void Start(){
		rotateRandom();
	}
	
	void rotateRandom()
	{
		if (rotateOnX) axis = "x";
		if (rotateOnY) axis = "y";
		if (rotateOnZ) axis = "z";
		
		
		float amount = Random.Range(minAmount,maxAmount);
		float delay = Random.Range(minDelay,maxDelay);
		float speed = Random.Range(minSpeed,maxSpeed);
		
		iTween.RotateTo(gameObject, iTween.Hash(axis, amount,"speed",speed, "easeType", "easeInOutBack", "loopType", "none", "delay", delay, "oncomplete" , "rotateRandom"));
	}
}

