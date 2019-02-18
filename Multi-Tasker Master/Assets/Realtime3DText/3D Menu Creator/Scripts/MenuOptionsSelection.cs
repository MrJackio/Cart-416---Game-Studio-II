//using UnityEngine;
//using System.Collections;
//
//public class MenuOptionsSelection : MonoBehaviour {
//
//	
//	public Shader shader;
//	public string onClickFunctionCalled;
//	private Shader originalShader;
//	
//	
//	void Start()
//	{
//		originalShader = renderer.material.shader;
//		
//	}
//	
//	
//	 void OnMouseEnter() {
//        renderer.material.shader = shader;
//	}
//	
//	 void OnMouseExit() {
//        renderer.material.shader = originalShader;
//	}
//	
//	void OnMouseDown()
//	{
//		Invoke(onClickFunctionCalled,0);
//	}
//	
//}
//






using UnityEngine;
using System.Collections;

/** 
 *	A simple event dispatcher - allows to listen to events in one GameObject from another GameObject
 *
 *  Author: Bartek Drozdz (bartek [at] everyday3d [dot] com)
 *
 *  Usage:
 *	Add this script to the object that is supposed to dispatch events. 
 *  In another objects follow this pattern to register as listener at intercept events:
 
 	void Start () {
		EventDispatcher ev = GameObject.Find("someObject").GetComponent<EventDispatcher>();
		ev.MouseDown += ListeningFunction; // Register the listener (and experience the beauty of overloaded operators!)
	}

	void ListeningFunction (GameObject e) {
		e.transform.Rotate(20, 0, 0); // 'e' is the game object that dispatched the event
		e.GetComponent<EventDispatcher>().MouseDown -= ListeningFunction; // Remove the listener
	}
	
 *  This class does not implement all standards events, nor does it allow dispatching custom events, 
 *  but you shold have no problem adding all the other methods.
 */
public class MenuOptionsSelection : MonoBehaviour
{

	public delegate void EventHandler (GameObject e);
	public delegate void CollisionHandler (GameObject e, Collision c);

	public event EventHandler MouseOver;
	void OnMouseOver ()
	{
		if (MouseOver != null)
			MouseOver (this.gameObject);
	}

	public event EventHandler MouseDown;
	void OnMouseDown ()
	{
		if (MouseDown != null)
			MouseDown (this.gameObject);
	}

	public event EventHandler MouseEnter;
	void OnMouseEnter ()
	{
		if (MouseEnter != null)
			MouseEnter (this.gameObject);
	}


	public event EventHandler MouseExit;
	void OnMouseExit ()
	{
		if (MouseExit != null)
			MouseExit (this.gameObject);
	}

	public event EventHandler BecameVisible;
	void OnBecameVisible ()
	{
		if (BecameVisible != null)
			BecameVisible (this.gameObject);
	}

	public event EventHandler BecameInvisible;
	void OnBecameInvisible ()
	{
		if (BecameInvisible != null)
			BecameInvisible (this.gameObject);
	}

	public event CollisionHandler CollisionEnter;
	void OnCollisionEnter (Collision c)
	{
		if (CollisionEnter != null)
			CollisionEnter (this.gameObject, c);
	}

	public event CollisionHandler CollisionExit;
	void OnCollisionExit (Collision c)
	{
		if (CollisionExit != null)
			CollisionExit (this.gameObject, c);
	}
	
}