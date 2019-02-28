using System;
using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class ArraysIndex {
	public int[] indexes;
}

[System.Serializable]
public class events
{
	
	public TextWriterVisual.OnEventType type;
	
	public iTweens[] tweens = new iTweens[0];
}



[System.Serializable]
public class iTweens
{
	public TextWriterVisual.TweenType type;
	
	public float delay = 0;
	
	public string[] keys;
	
 	public int[] indexes;
	
	public string[] metadatas;
	
	public int[] ints;
	
	public float[] floats;
	
	public bool[] bools;
	
	public string[] strings;
	
	public Vector3[] vector3s;
	
	public Color[] colors;
		
	public Space[] spaces;
	
	public iTween.EaseType[] easeTypes;
	
	public iTween.LoopType[] loopTypes;
	
	public GameObject[] gameObjects;
	
	public Transform[] transforms;

	public ArraysIndex[] vector3Arrays;
	
	public ArraysIndex[] transformArrays;
}

[System.Serializable]
public class TextWriterVisual : MonoBehaviour {
	
	public int[] eventIndex= new int[8];
	
	public events[] events1 = new events[0];
	
	public bool cleanOnceGlobal = false;
	public bool cleanOnceTween = false;
	public bool cleanOnceComponent = false;
	
	public GameObject[] components;
	public GameObject menuObject;
	public int menuComponentsIndex = 0;
	public string controlName;
	public string[] menuComponentsTitles;
	public Vector3 positionSpace = new Vector3(0,-0.8f,0);
	public Vector3 letterSpace;
	public int fontsize = 50;
	public Dictionary<string,List<Real3DText.Fonts.fontDesc>> fontlist;
	public int fontResolution = 3;
	public string[] fontsName;
	public List<FontStyle[]> fontStyle;
	public int fontNameIndex;
	public int fontStyleIndex;
	public float depth = 0.2f;
	public Shader fontShader;
	public Color32 fontcolor = Color.white;
	public int[] selectedValue = new int[8];
	public bool[] eventEnabled = new bool[8]{true,true,true,false,false,false,false,false};
	
	public bool singlePositionSpace;
	public bool singleFontSize;
	public bool singleFontType;
	public bool singleFontStyle;
	public bool singleLetterSpace;
	public bool singleColor;
	public bool singleShader;
	public bool singleEventsShader;
	public bool singleEventsAudio;
	public bool singleEventsTarget;
	public bool singleEventsFunctions;
	public bool singleEventsFunctionsParam;
	public bool singleEventsUseItween;
	
	
	public justifyType justify = justifyType.Left;
	
	public AudioClip[] eventsAudio = new AudioClip[8];

	public Shader[] eventsShader = new Shader[8];
	
	public GameObject[] eventsTarget = new GameObject[8];
	public string[] eventsFunction = new string[8];
	
	public string[] eventsParam = new string[8];
//	public float[] eventsScaleAmount = new float[8];
//	public Color[] eventsColor = new Color[8];
	
	public string[] events = new string[8]{"On Mouse Enter","On Mouse Exit","On Mouse Down","On Mouse Over","Collision Enter","Collision Exit","Became Visible","Became Invisible"};
	
	public bool[] useiTween = new bool[8];
	
	public bool pauseMenu = false;
	
	public bool isPaused = false;
	
	public Transform mainCamera;
	public Vector3 distance = Vector3.zero;
	public float cameraFieldOfView = 35;
	private float previousFieldOfView;
	
	public Texture2D logo;
	public Texture2D logoGP;
	public Texture2D logoCP;
	public Texture2D logoiTween;
//	public fontType fonttype = fontType.Arial;
//	public fontStyle fontstyle = fontStyle.Normal;
	
	public enum justifyType {
		Left,
		Center,
		Rigth,
	}
	
//	public enum fontStyle {
//		Normal,
//		Bold,
//		Italic,
//		BoldItalic,
//	}
	
	public enum fontType {
		Arial,
//		Calibri,
//		TimesNewRoman,
//		ComicSanMS,
	}
	
	public enum OnEventType {
		OnMouseEnter,
		OnMouseExit,
		OnMouseDown,
		OnMouseOver,
		CollisionEnter,
		CollisionExit,
		BecameVisible,
		BecameInvisible,
	}
	
	public enum TweenType {
		ColorFrom,
		ColorTo,
		ColorUpdate,
		FadeFrom,
		FadeTo,
		FadeUpdate,
		LookFrom,
		LookTo,
		LookUpdate,
		MoveAdd,
		MoveBy,
		MoveFrom,
		MoveTo,
		MoveUpdate,
		PunchPosition,
		PunchRotation,
		PunchScale,
		RotateAdd,
		RotateBy,
		RotateFrom,
		RotateTo,
		RotateUpdate,
		ScaleAdd,
		ScaleBy,
		ScaleFrom,
		ScaleTo,
		ScaleUpdate,
		ShakePosition,
		ShakeRotation,
		ShakeScale,
	}
	
	void Start()
	{
		if (pauseMenu)
			hide();
		Time.timeScale = 1;
		AudioListener.volume = 1;
		Cursor.visible = true;
	}
	
	void Update()
	{
		if (pauseMenu)
		{
			if (Input.GetKeyUp(KeyCode.Escape))
			{
				pause();
//				if (!isPaused)
//				{
//					Time.timeScale = 0;
//					if (!mainCamera) mainCamera = Camera.mainCamera.transform;
//					transform.parent = mainCamera.transform;
//					transform.localEulerAngles = new Vector3(0,180,0);
//					transform.localPosition = distance;
//					isPaused = true;
//					show();
//					
//				}
//				else
//				{
//					isPaused = false;
//					hide();
//					Time.timeScale = 1;		
//					transform.parent = null;
//				}
			}
		}
	}
	
	public void hide()
	{
		Cursor.visible = false;		
		Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
			BoxCollider[] boxcolliders = gameObject.GetComponentsInChildren<BoxCollider>();
			
			foreach(Renderer render in renderers)
				render.enabled = false;
			
			foreach(BoxCollider boxcollider in boxcolliders)
				boxcollider.enabled = false;
	}
	
	public void show()
	{Cursor.visible = true;		
		Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
			BoxCollider[] boxcolliders = gameObject.GetComponentsInChildren<BoxCollider>();
			
			foreach(Renderer render in renderers)
				render.enabled = true;
			
			foreach(BoxCollider boxcollider in boxcolliders)
				boxcollider.enabled = true;
	}
	
	public void pause()
	{
		if (pauseMenu)
		{
			if (!isPaused)
			{
				Time.timeScale = 0;
				if (!mainCamera) mainCamera = Camera.main.transform;
				previousFieldOfView =mainCamera.GetComponent<Camera>().fieldOfView;
				mainCamera.GetComponent<Camera>().fieldOfView = cameraFieldOfView;
				transform.parent = mainCamera.transform;
				transform.localEulerAngles = new Vector3(0,180,0);
				transform.localPosition = distance;
				isPaused = true;
				show();
				
			}
			else
			{
				transform.parent = null;
				mainCamera.GetComponent<Camera>().fieldOfView = previousFieldOfView;
				isPaused = false;
				hide();
				Time.timeScale = 1;					
			}
		}
	}
	
	void OnGUI()
	{}
}