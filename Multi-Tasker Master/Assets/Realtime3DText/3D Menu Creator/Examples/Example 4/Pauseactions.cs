using UnityEngine;
using System.Collections;

public class Pauseactions : MonoBehaviour {

	void OnGUI()
	{
		GUI.Label(new Rect(Screen.width/2 - 75,Screen.height/10, 150, 25),new GUIContent("Press Esc key to Pause"));
	}
	
	public void moveSphere()
	{
		transform.Translate(new Vector3(0,0,0.1f));
	}
	
	public void printHello(string textToPrint)
	{
		print(textToPrint);
	}
	
}
