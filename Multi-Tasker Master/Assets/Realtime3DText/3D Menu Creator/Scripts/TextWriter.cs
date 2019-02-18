using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System;
using Real3DText;

[System.Serializable]
public class TextWriter : MonoBehaviour{
	
	Real3DFont tempReal3DFont;
	public Texture2D logo;
	public events[] events1 = new events[0]; 
	public GameObject initialPosition;
	public Vector3 actualPosition = Vector3.zero;
	public Vector3 positionOffset = Vector3.zero;
	public int wordCharactersCount = 0;
	public string words;
	public string defaultWord = "";
	public Transform[] lettersTransforms = new Transform[1];
	public Vector3[] size = new Vector3[1];
	public Vector3 originalPosition;
	public bool initialOffsetDone = false;
	public float fontSize = 1;
//	public TextWriterVisual.fontType fonttype = TextWriterVisual.fontType.Arial;
//	public TextWriterVisual.fontStyle fontstyle = TextWriterVisual.fontStyle.Normal;
	
	public bool textDisplayed = false;
	public Vector3 letterSpace;
	public Vector3 positionSpace = new Vector3(0,-0.8f,0);
	
	public int[] selectedValue = new int[8];
	public bool[] eventEnabled = new bool[8]{true,true,true,false,false,false,false,false};
	public AudioClip[] eventAudio = new AudioClip[8];
	public Shader[] eventShader = new Shader[8];
	public GameObject[] eventTarget = new GameObject[8];
	public string[] eventFunction = new string[8];
	
	//Defaults
	public string[] eventFunctionParam = new string[8];
	public Color colorDefault = Color.white;
	public Color colorDefaultHighLight = Color.yellow;
	public bool[] useiTween = new bool[8]{ true,true,true,true,true,true,true,true};
	
	
	// Is this a input user contorl?
	public bool displayCursor = false;
	public float cursorSize;
	public bool inputControl = false;
	public Shader letterShader;
	public Material material;

	// Is this a menu title that needs to initialize delegates and sound?
	public bool initializeOnAwake = false;
	

	//Private variable
	private AudioSource mouseAudioSource = null;
	
	
	private Shader defaultShader;
	private Shader defaultHighLightShader;
	GameObject tempObject;
	
	private int Index1=0;
	private int Index2=0;
	
	public int[] eventIndex= new int[8];
	
	// iTween Variables
	Dictionary<string, object> Values {
		get { 
			if(null == values1) {
				DeserializeValues();
			}
			return values1;
		}
		set {
			values1 = value;
		}
	}
	
	Dictionary<string, object> values1;
	
		void SerializeValues() {
		var keyList = new List<string>();
		var indexList = new List<int>();
		var metadataList = new List<string>();
		
		var intList = new List<int>();
		var floatList = new List<float>();
		var boolList = new List<bool>();
		var stringList = new List<string>();
		var vector3List = new List<Vector3>();
		var colorList = new List<Color>();
		var spaceList = new List<Space>();
		var easeTypeList = new List<iTween.EaseType>();
		var loopTypeList = new List<iTween.LoopType>();
		var gameObjectList = new List<GameObject>();
		var transformList = new List<Transform>();
		var audioClipList = new List<AudioClip>();
		var audioSourceList = new List<AudioSource>();
		var vector3ArrayList = new List<ArraysIndex>();
		var transformArrayList = new List<ArraysIndex>();
	TextWriterVisual.TweenType type = events1[eventIndex[Index1]].tweens[Index2].type;			
		foreach(var pair in values1) {
			var mappings = TextWriterParamMapping.mappings[type];
			var valueType = mappings[pair.Key];
			if(typeof(int) == valueType) {
				AddToList<int>(keyList, indexList, intList, metadataList, pair);
			}
			if(typeof(float) == valueType) {
				AddToList<float>(keyList, indexList, floatList, metadataList, pair);
			}
			else if(typeof(bool) == valueType) {
				AddToList<bool>(keyList, indexList, boolList, metadataList, pair);
			}
			else if(typeof(string) == valueType) {
				AddToList<string>(keyList, indexList, stringList, metadataList, pair);
			}
			else if(typeof(Vector3) == valueType) {
				AddToList<Vector3>(keyList, indexList, vector3List, metadataList, pair);
			}
			else if(typeof(Color) == valueType) {
				AddToList<Color>(keyList, indexList, colorList, metadataList, pair);
			}
			else if(typeof(Space) == valueType) {
				AddToList<Space>(keyList, indexList, spaceList, metadataList, pair);
			}
			else if(typeof(iTween.EaseType) == valueType) {
				AddToList<iTween.EaseType>(keyList, indexList, easeTypeList, metadataList, pair);
			}
			else if(typeof(iTween.LoopType) == valueType) {
				AddToList<iTween.LoopType>(keyList, indexList, loopTypeList, metadataList, pair);
			}
			else if(typeof(GameObject) == valueType) {
				AddToList<GameObject>(keyList, indexList, gameObjectList, metadataList, pair);
			}
			else if(typeof(Transform) == valueType) {
				AddToList<Transform>(keyList, indexList, transformList, metadataList, pair);
			}
			else if(typeof(AudioClip) == valueType) {
				AddToList<AudioClip>(keyList, indexList, audioClipList, metadataList, pair);
			}
			else if(typeof(AudioSource) == valueType) {
				AddToList<AudioSource>(keyList, indexList, audioSourceList, metadataList, pair);
			}
			else if(typeof(Vector3OrTransform) == valueType) {
				if(pair.Value == null || typeof(Transform) == pair.Value.GetType()) {
					AddToList<Transform>(keyList, indexList, transformList, metadataList, pair.Key, pair.Value, "t");
				}
				else {
					AddToList<Vector3>(keyList, indexList, vector3List, metadataList, pair.Key, pair.Value, "v");
				}
			}
			else if(typeof(Vector3OrTransformArray) == valueType) {
				if(typeof(Vector3[]) == pair.Value.GetType()) {
					var value = (Vector3[])pair.Value;
					var vectorIndexes = new ArraysIndex();
					var indexArray = new int[value.Length];
					for(var i = 0; i < value.Length; ++i) {
						vector3List.Add((Vector3)value[i]);
						indexArray[i] = vector3List.Count - 1;
					}
					
					vectorIndexes.indexes = indexArray;
					AddToList<ArraysIndex>(keyList, indexList, vector3ArrayList, metadataList, pair.Key, vectorIndexes, "v");
				}
				else if(typeof(Transform[]) == pair.Value.GetType()) {
					var value = (Transform[])pair.Value;
					var transformIndexes = new ArraysIndex();
					var indexArray = new int[value.Length];
					for(var i = 0; i < value.Length; ++i) {
						transformList.Add((Transform)value[i]);
						indexArray[i] = transformList.Count - 1;
					}
					
					transformIndexes.indexes = indexArray;
					AddToList<ArraysIndex>(keyList, indexList, transformArrayList, metadataList, pair.Key, transformIndexes, "t");
				}
				else if(typeof(string) == pair.Value.GetType())
				{
					AddToList<string>(keyList, indexList, stringList, metadataList, pair.Key, pair.Value, "p");
				}
			}
		}
		
		events1[Index1].tweens[Index2].keys = keyList.ToArray();
		events1[Index1].tweens[Index2].indexes = indexList.ToArray();
		events1[Index1].tweens[Index2].metadatas = metadataList.ToArray();
		events1[Index1].tweens[Index2].ints = intList.ToArray();
		events1[Index1].tweens[Index2].floats = floatList.ToArray();
		events1[Index1].tweens[Index2].bools = boolList.ToArray();
		events1[Index1].tweens[Index2].strings = stringList.ToArray();
		events1[Index1].tweens[Index2].vector3s = vector3List.ToArray();
		events1[Index1].tweens[Index2].colors = colorList.ToArray();
		events1[Index1].tweens[Index2].spaces = spaceList.ToArray();
		events1[Index1].tweens[Index2].easeTypes = easeTypeList.ToArray();
		events1[Index1].tweens[Index2].loopTypes = loopTypeList.ToArray();
		events1[Index1].tweens[Index2].gameObjects = gameObjectList.ToArray();
		events1[Index1].tweens[Index2].transforms = transformList.ToArray();
		events1[Index1].tweens[Index2].vector3Arrays = vector3ArrayList.ToArray();
		events1[Index1].tweens[Index2].transformArrays = transformArrayList.ToArray();
	}
	
	void AddToList<T>(List<string> keyList, List<int> indexList, IList<T> valueList, List<string> metadataList, KeyValuePair<string, object> pair) {
		AddToList<T>(keyList, indexList, valueList, metadataList, pair.Key, pair.Value);
	}
	
	void AddToList<T>(List<string> keyList, List<int> indexList, IList<T> valueList, List<string> metadataList, KeyValuePair<string, object> pair, string metadata) {
		AddToList<T>(keyList, indexList, valueList, metadataList, pair.Key, pair.Value, metadata);
	}
	
	void AddToList<T>(List<string> keyList, List<int> indexList, IList<T> valueList, List<string> metadataList, string key, object value) {
		AddToList<T>(keyList, indexList, valueList, metadataList, key, value, null);
	}
	
	void AddToList<T>(List<string> keyList, List<int> indexList, IList<T> valueList, List<string> metadataList, string key, object value, string metadata) {
		keyList.Add(key);
		valueList.Add((T)value);
		indexList.Add(valueList.Count - 1);
		metadataList.Add(metadata);
	}
	
	void DeserializeValues() {
		values1 = new Dictionary<string, object>();
		TextWriterVisual.TweenType type = events1[eventIndex[Index1]].tweens[Index2].type;
		if(null == events1[eventIndex[Index1]].tweens[Index2].keys) {
			return;
		}
		
		for(var i = 0; i < events1[eventIndex[Index1]].tweens[Index2].keys.Length; ++i) {
			var mappings = TextWriterParamMapping.mappings[type];
			var valueType = mappings[events1[eventIndex[Index1]].tweens[Index2].keys[i]];
			int[] indexes = events1[eventIndex[Index1]].tweens[Index2].indexes;
			if(typeof(int) == valueType) {
				values1.Add(events1[eventIndex[Index1]].tweens[Index2].keys[i], events1[eventIndex[Index1]].tweens[Index2].ints[indexes[i]]);
			}
			else if(typeof(float) == valueType) {
				values1.Add(events1[eventIndex[Index1]].tweens[Index2].keys[i], events1[eventIndex[Index1]].tweens[Index2].floats[indexes[i]]);
			}
			else if(typeof(bool) == valueType) {
				values1.Add(events1[eventIndex[Index1]].tweens[Index2].keys[i], events1[eventIndex[Index1]].tweens[Index2].bools[indexes[i]]);
			}
			else if(typeof(string) == valueType) {
				values1.Add(events1[eventIndex[Index1]].tweens[Index2].keys[i], events1[eventIndex[Index1]].tweens[Index2].strings[indexes[i]]);
			}
			else if(typeof(Vector3) == valueType) {
				values1.Add(events1[eventIndex[Index1]].tweens[Index2].keys[i], events1[eventIndex[Index1]].tweens[Index2].vector3s[indexes[i]]);
			}
			else if(typeof(Color) == valueType) {
				values1.Add(events1[eventIndex[Index1]].tweens[Index2].keys[i], events1[eventIndex[Index1]].tweens[Index2].colors[indexes[i]]);
			}
			else if(typeof(Space) == valueType) {
				values1.Add(events1[eventIndex[Index1]].tweens[Index2].keys[i], events1[eventIndex[Index1]].tweens[Index2].spaces[indexes[i]]);
			}
			else if(typeof(iTween.EaseType) == valueType) {
				values1.Add(events1[eventIndex[Index1]].tweens[Index2].keys[i], events1[eventIndex[Index1]].tweens[Index2].easeTypes[indexes[i]]);
			}
			else if(typeof(iTween.LoopType) == valueType) {
				values1.Add(events1[eventIndex[Index1]].tweens[Index2].keys[i], events1[eventIndex[Index1]].tweens[Index2].loopTypes[indexes[i]]) ;
			}
			else if(typeof(GameObject) == valueType) {
				values1.Add(events1[eventIndex[Index1]].tweens[Index2].keys[i], events1[eventIndex[Index1]].tweens[Index2].gameObjects[indexes[i]]) ;
			}
			else if(typeof(Transform) == valueType) {
				values1.Add(events1[eventIndex[Index1]].tweens[Index2].keys[i], events1[eventIndex[Index1]].tweens[Index2].transforms[indexes[i]]) ;
			}
			else if(typeof(Vector3OrTransform) == valueType) {
				if("v" == events1[eventIndex[Index1]].tweens[Index2].metadatas[i]) {
					values1.Add(events1[eventIndex[Index1]].tweens[Index2].keys[i], events1[eventIndex[Index1]].tweens[Index2].vector3s[indexes[i]]) ;
				}
				else if("t" == events1[eventIndex[Index1]].tweens[Index2].metadatas[i]) {
					values1.Add(events1[eventIndex[Index1]].tweens[Index2].keys[i], events1[eventIndex[Index1]].tweens[Index2].transforms[indexes[i]]) ;
				}
			}
			else if(typeof(Vector3OrTransformArray) == valueType) {
				if("v" == events1[eventIndex[Index1]].tweens[Index2].metadatas[i]) {
					var arrayIndexes = events1[eventIndex[Index1]].tweens[Index2].vector3Arrays[indexes[i]];
					var vectorArray = new Vector3[arrayIndexes.indexes.Length];
					for(var idx = 0; idx < arrayIndexes.indexes.Length; ++idx) {
						vectorArray[idx] = events1[eventIndex[Index1]].tweens[Index2].vector3s[arrayIndexes.indexes[idx]];
					}
					
					values1.Add(events1[eventIndex[Index1]].tweens[Index2].keys[i], vectorArray);
				}
				else if("t" == events1[eventIndex[Index1]].tweens[Index2].metadatas[i]) {
					var arrayIndexes = events1[eventIndex[Index1]].tweens[Index2].transformArrays[indexes[i]];
					var transformArray = new Transform[arrayIndexes.indexes.Length];
					for(var idx = 0; idx < arrayIndexes.indexes.Length; ++idx) {
						transformArray[idx] = events1[eventIndex[Index1]].tweens[Index2].transforms[arrayIndexes.indexes[idx]];
					}
					
					values1.Add(events1[eventIndex[Index1]].tweens[Index2].keys[i], transformArray);
				}
				else if("p" == events1[eventIndex[Index1]].tweens[Index2].metadatas[i]) {
					values1.Add(events1[eventIndex[Index1]].tweens[Index2].keys[i], events1[eventIndex[Index1]].tweens[Index2].strings[indexes[i]]);
				}
			}
		}
	}

	public void Play(GameObject c) {
		StartCoroutine(StartEvent(c, Index1, Index2));
		
	}
	
		IEnumerator StartEvent(GameObject c, int i1,int i2) {
		if(events1[eventIndex[i1]].tweens[i2].delay > 0) yield return new WaitForSeconds(events1[eventIndex[i1]].tweens[i2].delay);
		Index1 = i1;
		Index2 = i2;
		DeserializeValues();
		TextWriterVisual.TweenType type = events1[eventIndex[i1]].tweens[i2].type;
		var optionsHash = new Hashtable();
		foreach(var pair in Values) {
			if("path" == pair.Key && pair.Value.GetType() == typeof(string)) {
				optionsHash.Add(pair.Key, iTweenPath.GetPath((string)pair.Value));
			}
			else {
				optionsHash.Add(pair.Key, pair.Value);
			}
		}
		
		switch(type) {
		case TextWriterVisual.TweenType.ColorFrom:
			iTween.ColorFrom(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.ColorTo:
			iTween.ColorTo(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.ColorUpdate:
			iTween.ColorUpdate(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.FadeFrom:
			iTween.FadeFrom(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.FadeTo:
			iTween.FadeTo(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.FadeUpdate:
			iTween.FadeUpdate(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.LookFrom:
			iTween.LookFrom(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.LookTo:
			iTween.LookTo(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.LookUpdate:
			iTween.LookUpdate(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.MoveAdd:
			iTween.MoveAdd(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.MoveBy:
			iTween.MoveBy(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.MoveFrom:
			iTween.MoveFrom(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.MoveTo:
			iTween.MoveTo(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.MoveUpdate:
			iTween.MoveUpdate(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.PunchPosition:
			iTween.PunchPosition(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.PunchRotation:
			iTween.PunchRotation(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.PunchScale:
			iTween.PunchScale(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.RotateAdd:
			iTween.RotateAdd(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.RotateBy:
			iTween.RotateBy(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.RotateFrom:
			iTween.RotateFrom(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.RotateTo:
			iTween.RotateTo(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.RotateUpdate:
			iTween.RotateUpdate(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.ScaleAdd:
			iTween.ScaleAdd(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.ScaleBy:
			iTween.ScaleBy(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.ScaleFrom:
			iTween.ScaleFrom(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.ScaleTo:
			iTween.ScaleTo(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.ScaleUpdate:
			iTween.ScaleUpdate(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.ShakePosition:
			iTween.ShakePosition(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.ShakeRotation:
			iTween.ShakeRotation(c, optionsHash);
			break;
		case TextWriterVisual.TweenType.ShakeScale:
			iTween.ShakeScale(c, optionsHash);
			break;
		default:
			throw new System.ArgumentException("Invalid tween type: " + type);
		}
	}
	
	
	void Awake()
	{
		defaultShader =  Shader.Find("Diffuse");
		defaultHighLightShader = Shader.Find("Self-Illumin/Diffuse");
		if (inputControl)
		{
			tempReal3DFont = gameObject.GetComponentInChildren<Real3DFont>();
			tempReal3DFont.vectorFont.toDictionary();
//			createCursor(tempReal3DFont);
			initializeSounds();
			
			words = defaultWord;
//			process(tempReal3DFont);
			
//			for (int i = 0; i < defaultWord.Length;i++)
//			{
//				TextWriter.write(gameObject, TextWriter.Hash("letter", defaultWord[i],"position",transform.position ,"rotation", transform.rotation,"letterspace", letterSpace,"fontsize", fontSize,"displaycursor",displayCursor,"lettershader", letterShader, "fontcolor",colorDefault));
//			}
		}
		
		if (initializeOnAwake == true)
		{
			initializeSounds ();
			MenuOptionsSelection tempMenuEvents = transform.GetComponentInChildren<MenuOptionsSelection>();
			
			for (int i =0; i < events1.Length; i++)
			{
				switch (events1[i].type.ToString())
				{
				case "OnMouseEnter":
					tempMenuEvents.MouseEnter += MouseEnter;
					break;
				
				case "OnMouseExit":
					tempMenuEvents.MouseExit += MouseExit;
					break;
				
				case "OnMouseDown":
					tempMenuEvents.MouseDown += MouseDown;
					
					break;
				
				case "OnMouseOver":
					tempMenuEvents.MouseOver += MouseOver;
					
					break;
				
				case "CollisionEnter":
					tempMenuEvents.CollisionEnter += CollisionEnter;
					
					break;
				
				case "CollisionExit":
					tempMenuEvents.CollisionExit += CollisionExit;
					
					break;
				
				case "BecameVisible":
					tempMenuEvents.BecameVisible += BecameVisible;
					
					break;
				
				case "BecameInvisible":
					tempMenuEvents.BecameInvisible += BecameInvisible;
					
					break;
				}
			}
		}
	}
	
	public int lastLength;
	public int currentLength;
	
	void Update()
	{
		if (inputControl)
		{
			lastLength = currentLength;
			foreach (char c in Input.inputString){
				int cr = c;
				if (cr == 8)
				{
					words = words.Substring(0,(words.Length-2<0?0:words.Length-1));
					if (words.Length == 0) words = "";
				}else words += c;
			}
			currentLength = words.Length;
			if (lastLength != currentLength){
				if (displayCursor)tempReal3DFont.textToPrint = words + "|";
				else tempReal3DFont.textToPrint = words;
				process(tempReal3DFont);
//				UpdateCursor(tempReal3DFont);	
			}
		}
	}
	
	
	void initializeSounds () {
		mouseAudioSource = CreateAudioSource(null,false , 0.8f, false,false , true, "MouseSounds");
	}
	
	void PlayAudio(AudioSource carAudio, AudioClip clip, float volume)
	{
		carAudio.clip = clip;
		carAudio.volume = volume;
		carAudio.PlayOneShot(clip);
	}
	
	private AudioSource CreateAudioSource (AudioClip clip, bool loop, float volume,bool play, bool playOnAwake, bool noAudioClip, string nameAudio) {
		GameObject go = new GameObject("audio" + nameAudio);
		go.transform.parent = this.transform;
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		go.AddComponent(typeof(AudioSource));
		
		if (!noAudioClip)go.GetComponent<AudioSource>().clip = clip;
		
		go.GetComponent<AudioSource>().loop = loop;
		go.GetComponent<AudioSource>().volume = volume;
		if (play)go.GetComponent<AudioSource>().Play();
		else go.GetComponent<AudioSource>().Stop();
		
		if (playOnAwake) go.GetComponent<AudioSource>().playOnAwake = true;
		else go.GetComponent<AudioSource>().playOnAwake = false;
		
		return go.GetComponent<AudioSource>();
	}
	
	
	public void MouseOver (GameObject c)
	{
		Index1 = 3;
		for (Index2=0; Index2 < events1[eventIndex[Index1]].tweens.Length; Index2++)
		{
			Play(c);
		}
		
		if (eventAudio[3]) PlayAudio( mouseAudioSource,eventAudio[3],1);
		if (!eventShader[3])
			eventShader[3] = defaultHighLightShader;
			
		foreach (Renderer render in c.GetComponentsInChildren<Renderer>())
			{
				render.material.shader = eventShader[3];
			}
		
		if (eventTarget[3] && eventFunction[3].Length !=0){
			if (eventFunctionParam[3] !=null)
				eventTarget[3].SendMessage((string)eventFunction[3], (object)eventFunctionParam[3],SendMessageOptions.DontRequireReceiver);
			else 
			eventTarget[3].SendMessage((string)eventFunction[3],SendMessageOptions.DontRequireReceiver);
		}
	}

	public void MouseDown (GameObject c)
	{
		Index1 = 2;
		for (Index2=0; Index2 < events1[eventIndex[Index1]].tweens.Length; Index2++)
		{
			Play(c);
		}
		
		
		if (eventAudio[2]) PlayAudio( mouseAudioSource,eventAudio[2],1);				
		if (!eventShader[2])
			eventShader[2] = defaultHighLightShader;
		foreach (Renderer render in c.GetComponentsInChildren<Renderer>())
			render.material.shader = eventShader[2];
		
		if (eventTarget[2] && eventFunction[2].Length !=0){
			if (eventFunctionParam[2] !=null)
				eventTarget[2].SendMessage((string)eventFunction[2],(object)eventFunctionParam[2]);
			else
				eventTarget[2].SendMessage((string)eventFunction[2],SendMessageOptions.DontRequireReceiver);
		}
	}

	public void MouseEnter (GameObject c)
	{
		Index1 = 0;
		for (Index2=0; Index2 < events1[eventIndex[Index1]].tweens.Length; Index2++)
		{
			Play(c);
		}
		
		
		if (eventAudio[0]) PlayAudio( mouseAudioSource,eventAudio[0],1);				
		
		if (!eventShader[0])
			eventShader[0] = defaultHighLightShader;
		
		foreach (Renderer render in c.GetComponentsInChildren<Renderer>())
			render.material.shader = eventShader[0];
		
		if (eventTarget[0] && eventFunction[0].Length !=0){
			if (eventFunctionParam[0] !=null)
				eventTarget[0].SendMessage((string)eventFunction[0],(object)eventFunctionParam[0]);
			else
				eventTarget[0].SendMessage((string)eventFunction[0],SendMessageOptions.DontRequireReceiver);
		}
	}


	public void MouseExit (GameObject c)
	{
		Index1 = 1;
		for (Index2=0; Index2 < events1[eventIndex[Index1]].tweens.Length; Index2++)
		{
			Play(c);
		}
		
		
		if (eventAudio[1]) PlayAudio( mouseAudioSource,eventAudio[1],1);				
		
		if (!eventShader[1])
			eventShader[1] = defaultShader;
		
		foreach (Renderer render in c.GetComponentsInChildren<Renderer>())
			render.material.shader = eventShader[1];
		
		if (eventTarget[1] && eventFunction[1].Length !=0){
			if (eventFunctionParam[1] !=null)
				eventTarget[1].SendMessage((string)eventFunction[1],(object)eventFunctionParam[1]);
			else
				eventTarget[1].SendMessage((string)eventFunction[1],SendMessageOptions.DontRequireReceiver);
		}
	}

	public void BecameVisible (GameObject c)
	{
		
		Index1 = 6;
		for (Index2=0; Index2 < events1[eventIndex[Index1]].tweens.Length; Index2++)
		{
			Play(c);
		}
		
		
		if (eventAudio[6]) PlayAudio( mouseAudioSource,eventAudio[6],1);		
		if (!eventShader[6])
			eventShader[6] = defaultShader;
					
		foreach (Renderer render in c.GetComponentsInChildren<Renderer>())
			render.material.shader = eventShader[6];
		
		
		if (eventTarget[6] && eventFunction[6].Length !=0){
			if (eventFunctionParam[6] !=null)
				eventTarget[6].SendMessage((string)eventFunction[6],(object)eventFunctionParam[6]);
			else
				eventTarget[6].SendMessage((string)eventFunction[6],SendMessageOptions.DontRequireReceiver);
		}
	}

	public void BecameInvisible (GameObject c)
	{
		Index1 = 7;
		for (Index2=0; Index2 < events1[eventIndex[Index1]].tweens.Length; Index2++)
		{
			Play(c);
		}
		
		if (eventAudio[7]) PlayAudio( mouseAudioSource,eventAudio[7],1);		
		if (!eventShader[7])
			eventShader[7] = defaultShader;
		foreach (Renderer render in c.GetComponentsInChildren<Renderer>())
			render.material.shader = eventShader[7];
		
		
		if (eventTarget[7] && eventFunction[7].Length !=0){
			if (eventFunctionParam[7] !=null)
				eventTarget[7].SendMessage((string)eventFunction[7],(object)eventFunctionParam[7]);
			else
				eventTarget[7].SendMessage((string)eventFunction[7],SendMessageOptions.DontRequireReceiver);
		}
	}

	public void CollisionEnter (GameObject e, Collision c)
	{
		
		Index1 = 4;
		for (Index2=0; Index2 < events1[eventIndex[Index1]].tweens.Length; Index2++)
		{
			Play(e);
		}
		
		if (eventAudio[4]) PlayAudio( mouseAudioSource,eventAudio[4],1);		
		if (!eventShader[4])
			eventShader[4] = defaultShader;					
		foreach (Renderer render in e.GetComponentsInChildren<Renderer>())
			render.material.shader = eventShader[4];
		
		
		if (eventTarget[4] && eventFunction[4].Length !=0){
			if (eventFunctionParam[4] !=null)
				eventTarget[4].SendMessage((string)eventFunction[4],(object)eventFunctionParam[4]);
			else
				eventTarget[4].SendMessage((string)eventFunction[4],SendMessageOptions.DontRequireReceiver);
		}
	}

	public void CollisionExit (GameObject e, Collision c)
	{
		Index1 = 5;
		for (Index2=0; Index2 < events1[eventIndex[Index1]].tweens.Length; Index2++)
		{
			Play(e);
		}
		
		
		
		if (eventAudio[5]) PlayAudio( mouseAudioSource,eventAudio[5],1);	
		if (!eventShader[5])
			eventShader[5] = defaultShader;				
		
		foreach (Renderer render in e.GetComponentsInChildren<Renderer>())
			render.material.shader = eventShader[5];		
		
		
		if (eventTarget[5] && eventFunction[5].Length !=0){
			if (eventFunctionParam[5] !=null)
				eventTarget[5].SendMessage((string)eventFunction[5],(object)eventFunctionParam[5]);
			else
				eventTarget[5].SendMessage((string)eventFunction[5],SendMessageOptions.DontRequireReceiver);
		}
	}
	
	
	
	// User Input method that returns String with the word captured from keyboard.
	
	/// <summary>
	/// Method to get the user keyboard input, it can be used on single call or every frame.  This method returns the user input in a string.
	/// </summary>
	/// <param name="controlName">
	/// <see cref="string"/> type.  Name of this control, used for future reference of this control.
	/// </param>
	/// <param name="rotationPositionObject">
	/// <see cref="GameObject"/> type to get the position and rotation from, and where the user input is displayed at.
	/// </param>
	public static string userInputString (string controlName, GameObject rotationPositionObject)
	{
		Hashtable hash = new Hashtable();
		
		hash.Add("position",rotationPositionObject.transform.position);
		hash.Add("rotation",rotationPositionObject.transform.rotation);
		
		return userInput(controlName, hash).words;
	}
	
	/// <summary>
	/// Method to get the user keyboard input, it can be used on single call or every frame.  This method returns the user input in a string.
	/// </summary>
	/// <param name="position">
	/// <see cref="Vector3"/> type.  Position in global space, where the user input is printed on screen.
	/// </param>
	/// <param name="rotation">
	/// <see cref="Quaternion"/> tyoe.  Rotation in global space, where the user input is printed on screen.
	/// </param>
	/// <param name="letterSpace">
	/// <see cref="Vector3"/> type.  Represents the space between letters. by default is X= 0.1f, y = 0, z = 0.
	/// </param>
	/// <param name="controlName">
	/// <see cref="string"/> type.  Name of this control, used for future reference of this control.
	/// </param>
	/// <param name="fontsize">
	/// <see cref="System.single"/> type.  The size of the font, by default is 1, smaller values than 1 decrease the font size.
	/// </param>
	/// <param name="displayCursor">
	/// <see cref="bool"/> type.  By default is true, this displays a cursor on the screen.
	/// </param>
	public static string userInputString (Vector3 position ,Quaternion rotation, Vector3 letterSpace, string controlName, float fontsize,bool displayCursor)
	{
		Hashtable hash = new Hashtable();
		
		hash.Add("position",position);
		hash.Add("rotation",rotation);
		hash.Add("letterSpace",letterSpace);
		hash.Add("fontsize",fontsize);
		hash.Add("displayCursor",displayCursor);
		
		return userInput(controlName, hash).words;
	}
	
	/// <summary>
	/// Method to get the user keyboard input, it can be used on single call or every frame.  This method returns the user input in a string.
	/// </summary>
	/// <param name="position">
	/// <see cref="Vector3"/> type.  Position in global space, where the user input is printed on screen.
	/// </param>
	/// <param name="rotation">
	/// <see cref="Quaternion"/> tyoe.  Rotation in global space, where the user input is printed on screen.
	/// </param>
	/// <param name="letterSpace">
	/// <see cref="Vector3"/> type.  Represents the space between letters. by default is X= 0.1f, y = 0, z = 0.
	/// </param>
	/// <param name="controlName">
	/// <see cref="string"/> type.  Name of this control, used for future reference of this control.
	/// </param>
	/// <param name="fontsize">
	/// <see cref="System.single"/> type.  The size of the font, by default is 1, smaller values than 1 decrease the font size.
	/// </param>
	/// <param name="displayCursor">
	/// <see cref="bool"/> type.  By default is true, this displays a cursor on the screen.
	/// </param>
	/// <param name="fontColor">
	/// <see cref="Color"/> type.  The font Color, by default is white.
	/// </param>
	public static string userInputString (Vector3 position ,Quaternion rotation, Vector3 letterSpace, string controlName, float fontsize,bool displayCursor, Color fontColor)
	{
		Hashtable hash = new Hashtable();
		
		hash.Add("position",position);
		hash.Add("rotation",rotation);
		hash.Add("letterSpace",letterSpace);
		hash.Add("fontsize",fontsize);
		hash.Add("displayCursor",displayCursor);
		hash.Add("fontColor",fontColor);
		
		return userInput(controlName, hash).words;
		
	}
	
	
	
	
	
	
	
	
	/// <summary>
	/// Method to get the user keyboard input, it can be used on single call or every frame.  This method returns the user input in a string.
	/// </summary>
	/// <param name="position">
	/// <see cref="Vector3"/> type.  Position in global space, where the user input is printed on screen.
	/// </param>
	/// <param name="rotation">
	/// <see cref="Quaternion"/> tyoe.  Rotation in global space, where the user input is printed on screen.
	/// </param>
	/// <param name="letterSpace">
	/// <see cref="Vector3"/> type.  Represents the space between letters. by default is X= 0.1f, y = 0, z = 0.
	/// </param>
	/// <param name="controlName">
	/// <see cref="string"/> type.  Name of this control, used for future reference of this control.
	/// </param>
	/// <param name="fontsize">
	/// <see cref="System.single"/> type.  The size of the font, by default is 1, smaller values than 1 decrease the font size.
	/// </param>
	/// <param name="displayCursor">
	/// <see cref="bool"/> type.  By default is true, this displays a cursor on the screen.
	/// </param>
	/// <param name="fontColor">
	/// <see cref="Color"/> type.  The font Color, by default is white.
	/// </param>
	/// <param name="fontShader">
	/// <see cref="Shader"/> type.  The font Shader, by default is Diffuse.
	/// </param>
	public static string userInputString (Vector3 position ,Quaternion rotation, Vector3 letterSpace, string controlName, float fontsize,bool displayCursor, Color fontColor, Shader fontShader)
	{
		Hashtable hash = new Hashtable();
		
		hash.Add("position",position);
		hash.Add("rotation",rotation);
		hash.Add("letterSpace",letterSpace);
		hash.Add("fontsize",fontsize);
		hash.Add("displayCursor",displayCursor);
		hash.Add("fontColor",fontColor);
		hash.Add("fontShader",fontShader);
		
		return userInput(controlName, hash).words;
	}
	
	
	
	// User Input method that returns the Class TextWriter.
	
	
	/// <summary>
	/// Method to get the user keyboard input, it can be used on single call or every frame.  This method returns the Class TextWriter.
	/// </summary>
	/// <param name="controlName">
	/// <see cref="string"/> type.  Name for this control, used for future reference of this control.
	/// </param>
	/// <param name="rotationPositionObject">
	/// <see cref="GameObject"/> type to get the position and rotation from, and where the user input is displayed at.
	/// </param>
	public static TextWriter userInput (string controlName, GameObject rotationPositionObject)
	{
		Hashtable hash = new Hashtable();
		
		hash.Add("position",rotationPositionObject.transform.position);
		hash.Add("rotation",rotationPositionObject.transform.rotation);
		
		return userInput(controlName, hash);
	}
	
	
	
	/// <summary>
	/// Method to get the user keyboard input, it can be used on single call or every frame.  This method returns the Class TextWriter.
	/// </summary>
	/// <param name="position">
	/// <see cref="Vector3"/> type.  Position in global space, where the user input is printed on screen.
	/// </param>
	/// <param name="rotation">
	/// <see cref="Quaternion"/> tyoe.  Rotation in global space, where the user input is printed on screen.
	/// </param>
	/// <param name="letterSpace">
	/// <see cref="Vector3"/> type.  Represents the space between letters. by default is X= 0.1f, y = 0, z = 0.
	/// </param>
	/// <param name="controlName">
	/// <see cref="string"/> type.  Name of this control, used for future reference of this control.
	/// </param>
	/// <param name="fontsize">
	/// <see cref="System.single"/> type.  The size of the font, by default is 1, smaller values than 1 decrease the font size.
	/// </param>
	/// <param name="displayCursor">
	/// <see cref="bool"/> type.  By default is true, this displays a cursor on the screen.
	/// </param>
	public static TextWriter userInput (Vector3 position ,Quaternion rotation, Vector3 letterSpace, string controlName, float fontsize,bool displayCursor)
	{
		Hashtable hash = new Hashtable();
		
		hash.Add("position",position);
		hash.Add("rotation",rotation);
		hash.Add("letterSpace",letterSpace);
		hash.Add("fontsize",fontsize);
		hash.Add("displayCursor",displayCursor);
		
		return userInput(controlName, hash);
	}
	
	/// <summary>
	/// Method to get the user keyboard input, it can be used on single call or every frame.  This method returns the Class TextWriter.
	/// </summary>
	/// <param name="position">
	/// <see cref="Vector3"/> type.  Position in global space, where the user input is printed on screen.
	/// </param>
	/// <param name="rotation">
	/// <see cref="Quaternion"/> tyoe.  Rotation in global space, where the user input is printed on screen.
	/// </param>
	/// <param name="letterSpace">
	/// <see cref="Vector3"/> type.  Represents the space between letters. by default is X= 0.1f, y = 0, z = 0.
	/// </param>
	/// <param name="controlName">
	/// <see cref="string"/> type.  Name of this control, used for future reference of this control.
	/// </param>
	/// <param name="fontsize">
	/// <see cref="System.single"/> type.  The size of the font, by default is 1, smaller values than 1 decrease the font size.
	/// </param>
	/// <param name="displayCursor">
	/// <see cref="bool"/> type.  By default is true, this displays a cursor on the screen.
	/// </param>
	/// <param name="fontColor">
	/// <see cref="Color"/> type.  The font Color, by default is white.
	/// </param>
	public static TextWriter userInput (Vector3 position ,Quaternion rotation, Vector3 letterSpace, string controlName, float fontsize,bool displayCursor, Color fontColor)
	{
		Hashtable hash = new Hashtable();
		
		hash.Add("position",position);
		hash.Add("rotation",rotation);
		hash.Add("letterSpace",letterSpace);
		hash.Add("fontsize",fontsize);
		hash.Add("displayCursor",displayCursor);
		hash.Add("fontColor",fontColor);
		
		return userInput(controlName, hash);
	}
	
	
	/// <summary>
	/// Method to get the user keyboard input, it can be used on single call or every frame.  This method returns the Class TextWriter.
	/// </summary>
	/// <param name="position">
	/// <see cref="Vector3"/> type.  Position in global space, where the user input is printed on screen.
	/// </param>
	/// <param name="rotation">
	/// <see cref="Quaternion"/> tyoe.  Rotation in global space, where the user input is printed on screen.
	/// </param>
	/// <param name="letterSpace">
	/// <see cref="Vector3"/> type.  Represents the space between letters. by default is X= 0.1f, y = 0, z = 0.
	/// </param>
	/// <param name="controlName">
	/// <see cref="string"/> type.  Name of this control, used for future reference of this control.
	/// </param>
	/// <param name="fontsize">
	/// <see cref="System.single"/> type.  The size of the font, by default is 1, smaller values than 1 decrease the font size.
	/// </param>
	/// <param name="displayCursor">
	/// <see cref="bool"/> type.  By default is true, this displays a cursor on the screen.
	/// </param>
	/// <param name="fontColor">
	/// <see cref="Color"/> type.  The font Color, by default is white.
	/// </param>
	/// <param name="fontShader">
	/// <see cref="Shader"/> type.  The font Shader, by default is Diffuse.
	/// </param>
	
	public static TextWriter userInput (Vector3 position ,Quaternion rotation, Vector3 letterSpace, string controlName, float fontsize,bool displayCursor, Color fontColor, Shader fontShader)
	{
		Hashtable hash = new Hashtable();
		
		hash.Add("position",position);
		hash.Add("rotation",rotation);
		hash.Add("letterSpace",letterSpace);
		hash.Add("fontsize",fontsize);
		hash.Add("displayCursor",displayCursor);
		hash.Add("fontColor",fontColor);
		hash.Add("fontShader",fontShader);
		
		return userInput(controlName, hash);
	}
	
	/// <summary>
	/// Method to get the user keyboard input, it can be used on single call or every frame.  This method returns the Class TextWriter.
	/// </summary>
	/// <param name="position">
	/// <see cref="Vector3"/> type.  Position in global space, where the user input is printed on screen.
	/// </param>
	/// <param name="rotation">
	/// <see cref="Quaternion"/> tyoe.  Rotation in global space, where the user input is printed on screen.
	/// </param>
	/// <param name="letterSpace">
	/// <see cref="Vector3"/> type.  Represents the space between letters. by default is X= 0.1f, y = 0, z = 0.
	/// </param>
	/// <param name="controlName">
	/// <see cref="string"/> type.  Name of this control, used for future reference of this control.
	/// </param>
	/// <param name="fontsize">
	/// <see cref="System.single"/> type.  The size of the font, by default is 1, smaller values than 1 decrease the font size.
	/// </param>
	/// <param name="displayCursor">
	/// <see cref="bool"/> type.  By default is true, this displays a cursor on the screen.
	/// </param>
	/// <param name="fontColor">
	/// <see cref="Color"/> type.  The font Color, by default is white.
	/// </param>
	/// <param name="fontShader">
	/// <see cref="Shader"/> type.  The font Shader, by default is Diffuse.
	/// </param>
	/// <param name="rotationPositionObject">
	/// <see cref="GameObject"/> type to get the position and rotation from, and where the user input is displayed at.
	/// </param>
	public static TextWriter userInput (string controlName, Hashtable hash)
	{
		hash = TextWriter.castParameters(hash);
		
		Vector3 position;
		Quaternion rotation;
		Vector3 letterSpace;
		float fontsize;
		bool displayCursor;
		Shader fontShader;
		Color fontColor;
			
		
		if(hash.ContainsKey("fontcolor")&& (hash["fontcolor"].GetType() == typeof(Color) || hash["fontcolor"].GetType() == typeof(Color32)))
			fontColor = (Color)hash["fontcolor"];	
		else fontColor = Color.white;
		
		
		if(hash.ContainsKey("rotationpositionobject")&& hash["rotationpositionobject"].GetType() == typeof(GameObject))
			rotation = ((GameObject)hash["rotationpositionobject"]).transform.rotation;	
		else if(hash.ContainsKey("rotation")&& hash["rotation"].GetType() == typeof(Quaternion))
			rotation = (Quaternion)hash["rotation"];	
		else rotation = new Quaternion(0,0,0,0);
		
		
		
		if(hash.ContainsKey("rotationpositionobject")&& hash["rotationpositionobject"].GetType() == typeof(GameObject))
			position = ((GameObject)hash["rotationpositionobject"]).transform.position;	
		else if(hash.ContainsKey("position")&& hash["position"].GetType() == typeof(Vector3))
			position = (Vector3)hash["position"];	
		else position = new Vector3(0,0,0);
		
		if(hash.ContainsKey("letterspace") && hash["letterspace"].GetType() == typeof(Vector3))
			letterSpace = (Vector3)hash["letterspace"];	
		else letterSpace = new Vector3(0.1f,0,0);
		
					
		if(hash.ContainsKey("fontsize") && hash["fontsize"].GetType() == typeof(float))
			fontsize = (float)hash["fontsize"];	
		else if(hash.ContainsKey("fontsize") && hash["fontsize"].GetType() == typeof(int))
			fontsize = (int)hash["fontsize"];	
		else fontsize = 1;
	
		if(hash.ContainsKey("displaycursor") && hash["displaycursor"].GetType() == typeof(bool))
			displayCursor = (bool)hash["displaycursor"];	
		else displayCursor = true;		
		
		if(hash.ContainsKey("fontshader") && hash["fontshader"].GetType() == typeof(Shader))
			fontShader = (Shader)hash["fontshader"];	
		else fontShader = Shader.Find("Diffuse");				
		
		TextWriter tempWriter;
		GameObject tempWordParent;
		GameObject Parent;
		if (!GameObject.Find(controlName + "_Word"))
		{
			tempWordParent = new GameObject();
			Parent = new GameObject();
			tempWordParent.transform.rotation = rotation;
			tempWordParent.transform.position = position;
			tempWordParent.name = controlName + "_Word";
			Parent.name = controlName + "_Parent";
			tempWordParent.transform.parent = Parent.transform;
		}
		else
		{
			Parent = GameObject.Find(controlName + "_Parent");
			tempWordParent = GameObject.Find(controlName + "_Word");
		}
		
		
		if (!GameObject.Find(controlName))
		{
			GameObject tempWord = new GameObject();
			if (displayCursor) tempWord=(GameObject)Instantiate(Resources.Load("Leters&Numbers/Arial/Normal/cursor") as GameObject);
			tempWord.transform.rotation = rotation;
			tempWord.transform.position = position;
			tempWord.name = controlName;
			tempWord.AddComponent<TextWriter>();
			tempWriter = tempWord.GetComponent<TextWriter>();
			if (displayCursor) tempWriter.displayCursor = true;
			else tempWriter.displayCursor = false;
			tempWriter.inputControl = true;
			tempWriter.letterShader = fontShader;
			tempWriter.fontSize = fontsize;
			tempWriter.letterSpace = letterSpace;
			if (fontColor != Color.clear)tempWriter.colorDefault = fontColor;
			tempWord.transform.parent = Parent.transform;
			if (displayCursor)tempWord.GetComponent<Renderer>().material.color = tempWriter.colorDefault;
			
		}
		else
		{
			GameObject tempWord = GameObject.Find(controlName);
			tempWriter = tempWord.GetComponent<TextWriter>();
			if (displayCursor) tempWriter.displayCursor = true;
			else tempWriter.displayCursor = false;
			tempWriter.inputControl = true;
			tempWriter.letterShader = fontShader;
			tempWriter.fontSize = fontsize;
			tempWriter.letterSpace = letterSpace;
			if (fontColor != Color.clear)tempWriter.colorDefault = fontColor;
			if (displayCursor)tempWord.GetComponent<Renderer>().material.color = tempWriter.colorDefault;
		}
		return tempWriter;
	}

	
	//Method to destroy the user input ONLY, this can't be used with MenuComponents!
	
	
	/// <summary>
	/// Method to destroy a control created with userInput Method (also the userInputString Method), it returns the user input in string.
	/// </summary>
	/// <param name="controlName">
	/// <see cref="string"/> type.  Name of the control to be destroyed.
	/// </param>
	public static string destroyUserInput(string controlName)
	{
		TextWriter tempWriter;
		GameObject Parent;
		string word = "";
		if (!GameObject.Find(controlName + "_Word"))
		{
			print ("the control " +controlName + " does not exits!");
			return null;
		}
		else
		{
			Parent = GameObject.Find(controlName + "_Parent");
		}
		
		
		if (!GameObject.Find(controlName))
		{
			print ("the control " +controlName + " does not exits!");
			return null;
		}
		else
		{
			GameObject tempWord = GameObject.Find(controlName);
			tempWriter = tempWord.GetComponent<TextWriter>();
			word = tempWriter.words;
		}
		DestroyImmediate(Parent);
		return word;
		
	}
	
	
	//This method is for internal use, it can also be used to Print variable of type Char ONLY! for printing string use displayString instead
	
	/// <summary>
	/// Method to print one single Char (it will print all the chars contained on the variable Words of the TextWriter Class) and add this char to string contained in the TextWriter Class (Words)
	/// </summary>
	/// <param name="letter">
	/// <see cref="Char"/> type.  Position in global space, where the Words variable contained in the TextWriter Class is printed on screen.
	/// </param>
	/// <param name="position">
	/// <see cref="Vector3"/> type.  Position in global space, where the Words variable contained in the TextWriter Class is printed on screen.
	/// </param>
	/// <param name="rotation">
	/// <see cref="Quaternion"/> tyoe.  Rotation in global space, where the Words variable contained in the TextWriter Class is printed on screen.
	/// </param>
	/// <param name="letterSpace">
	/// <see cref="Vector3"/> type.  Represents the space between letters. by default is X= 0.1f, y = 0, z = 0.
	/// </param>
	/// <param name="controlName">
	/// <see cref="string"/> type.  Name of this control, used for future reference of this control.
	/// </param>
	/// <param name="fontsize">
	/// <see cref="System.single"/> type.  The size of the font, by default is 1, smaller values than 1 decrease the font size.
	/// </param>
	/// <param name="displayCursor">
	/// <see cref="bool"/> type.  By default is true, this displays a cursor on the screen.
	/// </param>
	/// <param name="fontShader">
	/// <see cref="Shader"/> type.  The font Shader, by default is Diffuse.
	/// </param>
	public static TextWriter write (char letter,Vector3 position ,Quaternion rotation, Vector3 letterSpace, string controlName, float fontsize,bool displayCursor, Shader fontShader)
	{
		Hashtable hash = new Hashtable();
		
		hash.Add("letter",letter);
		hash.Add("position",position);
		hash.Add("rotation",rotation);
		hash.Add("letterSpace",letterSpace);
		hash.Add("fontsize",fontsize);
		hash.Add("displayCursor",displayCursor);
		hash.Add("fontShader",fontShader);
		
		return write( controlName,  hash);
	}
	
	
	/// <summary>
	/// Method to print one single Char (it will print all the chars contained on the variable Words of the TextWriter Class) and add this char to string contained in the TextWriter Class (Words)
	/// </summary>
	/// <param name="letter">
	/// <see cref="Char"/> type.  Position in global space, where the Words variable contained in the TextWriter Class is printed on screen.
	/// </param>
	/// <param name="position">
	/// <see cref="Vector3"/> type.  Position in global space, where the Words variable contained in the TextWriter Class is printed on screen.
	/// </param>
	/// <param name="rotation">
	/// <see cref="Quaternion"/> tyoe.  Rotation in global space, where the Words variable contained in the TextWriter Class is printed on screen.
	/// </param>
	/// <param name="letterSpace">
	/// <see cref="Vector3"/> type.  Represents the space between letters. by default is X= 0.1f, y = 0, z = 0.
	/// </param>
	/// <param name="controlName">
	/// <see cref="string"/> type.  Name of this control, used for future reference of this control.
	/// </param>
	/// <param name="fontsize">
	/// <see cref="System.single"/> type.  The size of the font, by default is 1, smaller values than 1 decrease the font size.
	/// </param>
	static TextWriter write (char letter,Vector3 position ,Quaternion rotation, Vector3 letterSpace, string controlName, float fontsize)
	{
		Hashtable hash = new Hashtable();
		
		hash.Add("letter",letter);
		hash.Add("position",position);
		hash.Add("rotation",rotation);
		hash.Add("letterSpace",letterSpace);
		hash.Add("fontsize",fontsize);
		
		return write( controlName,  hash);
	}	
	
	/// <summary>
	/// Method to print one single Char (it will print all the chars contained on the variable Words of the TextWriter Class) and add this char to string contained in the TextWriter Class (Words)
	/// </summary>
	/// <param name="letter">
	/// <see cref="Char"/> type.  Position in global space, where the Words variable contained in the TextWriter Class is printed on screen.
	/// </param>
	/// <param name="position">
	/// <see cref="Vector3"/> type.  Position in global space, where the Words variable contained in the TextWriter Class is printed on screen.
	/// </param>
	/// <param name="rotation">
	/// <see cref="Quaternion"/> tyoe.  Rotation in global space, where the Words variable contained in the TextWriter Class is printed on screen.
	/// </param>
	/// <param name="letterSpace">
	/// <see cref="Vector3"/> type.  Represents the space between letters. by default is X= 0.1f, y = 0, z = 0.
	/// </param>
	/// <param name="controlName">
	/// <see cref="string"/> type.  Name of this control, used for future reference of this control.
	/// </param>
	/// <param name="fontsize">
	/// <see cref="System.single"/> type.  The size of the font, by default is 1, smaller values than 1 decrease the font size.
	/// </param>
	/// <param name="displayCursor">
	/// <see cref="bool"/> type.  By default is true, this displays a cursor on the screen.
	/// </param>
	static TextWriter write (char letter,Vector3 position ,Quaternion rotation, Vector3 letterSpace, string controlName, float fontsize, bool displayCursor)
	{
		Hashtable hash = new Hashtable();
		
		hash.Add("letter",letter);
		hash.Add("position",position);
		hash.Add("rotation",rotation);
		hash.Add("letterSpace",letterSpace);
		hash.Add("fontsize",fontsize);
		hash.Add("displayCursor",displayCursor);
		
		return write( controlName,  hash);
	}	
	
	public static TextWriter write (string controlName, Hashtable hash)
	{
		GameObject tempWord;
		if (!GameObject.Find(controlName))
		{
			tempWord = new GameObject();
		}
		else
		{
			 tempWord = GameObject.Find(controlName);
		}
		return write(tempWord,hash);
	}
	
	
	/// <summary>
	/// Method to print one single Char (it will print all the chars contained on the variable Words of the TextWriter Class) and add this char to string contained in the TextWriter Class (Words)
	/// </summary>
	/// <param name="letter">
	/// <see cref="Char"/> type.  The char that would be printed on screen.
	/// </param>
	/// <param name="position">
	/// <see cref="Vector3"/> type.  Position in global space, where the letter variable is printed on screen.
	/// </param>
	/// <param name="rotation">
	/// <see cref="Quaternion"/> tyoe.  Rotation in global space,  where the letter variable is printed on screen.
	/// </param>
	/// <param name="letterSpace">
	/// <see cref="Vector3"/> type.  Represents the space between letters. by default is X= 0.1f, y = 0, z = 0.
	/// </param>
	/// <param name="fontsize">
	/// <see cref="System.single"/> type.  The size of the font, by default is 1, smaller values than 1 decrease the font size.
	/// </param>
	/// <param name="displayCursor">
	/// <see cref="bool"/> type.  By default is true, this displays a cursor on the screen.
	/// </param>
	/// <param name="fontShader">
	/// <see cref="Shader"/> type.  The font Shader, by default is Diffuse.
	/// </param>
	/// <param name="rotationPositionObject">
	/// <see cref="GameObject"/> type to get the position and rotation from, and where the user input is displayed at.
	/// </param>
	
	
	
	public static TextWriter write (GameObject controlName, Hashtable hash)
	{
		
		hash = TextWriter.castParameters(hash);
		
		TextWriter tempWriter;
		GameObject tempWordParent;
		
		char letter;
		Vector3 position;
		Quaternion rotation;
		Vector3 letterSpace;
		float fontsize;
		bool displayCursor;
		Material material = null;
		
			
		if(hash.ContainsKey("letter")&& hash["letter"].GetType() == typeof(char))
			letter = (char)hash["letter"];	
		else return null;
		
		if(hash.ContainsKey("rotationpositionpoject")&& hash["rotationpositionobject"].GetType() == typeof(GameObject))
			rotation = ((GameObject)hash["rotationpositionobject"]).transform.rotation;	
		else if(hash.ContainsKey("rotation")&& hash["rotation"].GetType() == typeof(Quaternion))
			rotation = (Quaternion)hash["rotation"];	
		else rotation = new Quaternion(0,0,0,0);
		
		
		
		if(hash.ContainsKey("rotationpositionobject")&& hash["rotationpositionobject"].GetType() == typeof(GameObject))
			position = ((GameObject)hash["rotationpositionobject"]).transform.position;	
		else if(hash.ContainsKey("position")&& hash["position"].GetType() == typeof(Vector3))
			position = (Vector3)hash["position"];	
		else position = new Vector3(0,0,0);
				
		
		if(hash.ContainsKey("letterspace") && hash["letterspace"].GetType() == typeof(Vector3))
			letterSpace = (Vector3)hash["letterspace"];	
		else letterSpace = new Vector3(0.1f,0,0);
		
					
		if(hash.ContainsKey("fontsize") && hash["fontsize"].GetType() == typeof(float))
			fontsize = (float)hash["fontsize"];	
		else if(hash.ContainsKey("fontsize") && hash["fontsize"].GetType() == typeof(int))
			fontsize = (int)hash["fontsize"];	
		else fontsize = 1;
	
		if(hash.ContainsKey("displaycursor") && hash["displaycursor"].GetType() == typeof(bool))
			displayCursor = (bool)hash["displaycursor"];	
		else displayCursor = true;		
		
		BoxCollider tempCollider;
			
		
		if (!controlName.GetComponent<TextWriter>())
		{
			GameObject tempWord = controlName;
			if (displayCursor) tempWord=(GameObject)Instantiate(Resources.Load("Leters&Numbers/Arial/Normal/cursor") as GameObject);
			tempWord.transform.rotation = rotation;
			tempWord.transform.position = position;
			tempWord.name = controlName.name;
			tempWord.AddComponent<TextWriter>();

			tempWriter = tempWord.GetComponent<TextWriter>();
		}
		else
		{
			GameObject tempWord = controlName;
			tempWord.transform.rotation = rotation;
			tempWord.transform.position = position;
			tempWriter = tempWord.GetComponent<TextWriter>();
			
		}
		
		
		
		if (!controlName.GetComponentInChildren<MenuOptionsSelection>())
		{
			tempWordParent = new GameObject();
			tempWordParent.transform.rotation = rotation;
			tempWordParent.transform.position = position;
			tempWordParent.AddComponent<MenuOptionsSelection>();
			tempWordParent.name = controlName.name + "_Word";
			tempCollider = tempWordParent.AddComponent<BoxCollider>();
			tempWordParent.transform.parent = tempWriter.transform;
			
			tempWriter.initialPosition.transform.parent = tempWriter.transform;
		}
		else
		{
			tempWordParent = controlName.GetComponentInChildren<MenuOptionsSelection>().gameObject;
			tempWordParent.transform.rotation = rotation;
			tempWordParent.transform.position = position;
			tempCollider = tempWordParent.GetComponent<BoxCollider>();
			tempWordParent.transform.parent = tempWriter.transform;
			
			if (displayCursor)tempWriter.initialPosition.GetComponent<Renderer>().material.color = tempWriter.colorDefault;
			tempWriter.initialPosition.transform.parent = tempWriter.transform;
		}
		
		
		
			int c = letter;
			
		
		
		
		if(hash.ContainsKey("fontcolor") && (hash["fontcolor"].GetType() == typeof(Color)||(hash["fontcolor"].GetType() == typeof(Color32))))
			tempWriter.colorDefault = (Color)hash["fontcolor"];	
		else tempWriter.colorDefault = Color.white;				
		
		if(hash.ContainsKey("fontshader") && hash["fontshader"].GetType() == typeof(Shader))
			tempWriter.letterShader = (Shader)hash["fontshader"];	
		else tempWriter.letterShader = Shader.Find("Diffuse");				
		
		if(hash.ContainsKey("material") && hash["material"].GetType() == typeof(Material))
			material = (Material)hash["material"];	
		else material = tempWriter.material;
		
			tempWriter.fontSize = fontsize;
		string tempLetter = Char.ToString(letter);
		if ((c < 32 || c >= 126) && c != 8&& c != 243 && c != 237&& c != 233&& c != 225
			&& c != 250&& c != 193&& c != 201&& c != 205&& c != 218&& c != 211&& c != 241&& c != 209)
					c = 32;
		switch (c){
		
		case 34:
			tempLetter = "quotes";
			goto default;
			
		case 42:
			tempLetter = "asterisk";
			goto default;
		
		case 46:
			tempLetter = "point";
			goto default;
		
		case 47:
			tempLetter = "backslash";
			goto default;
		
		case 58:
			tempLetter = "2points";
			goto default;
		
		case 60:
			tempLetter = "less";
			goto default;
		
		case 62:
			tempLetter = "more";
			goto default;
		
		case 63:
			tempLetter = "qmark";
			goto default;
		
		case 92:
			tempLetter = "slash";
			goto default;
		
		
		case 124:
			tempLetter = "pipe";
			goto default;
			
		case 8:
	    
			if (tempWriter.wordCharactersCount > 0)
			{
				tempWriter.words = tempWriter.words.Substring(0, tempWriter.words.Length - 1);
					DestroyImmediate(tempWriter.lettersTransforms[tempWriter.wordCharactersCount-1].gameObject);
				tempWriter.wordCharactersCount--;
				
				System.Array.Resize<Transform>(ref tempWriter.lettersTransforms, tempWriter.wordCharactersCount >0?tempWriter.wordCharactersCount:1);
				
				tempWriter.initialPosition.transform.localPosition += (new Vector3(tempWriter.size[tempWriter.wordCharactersCount].x,0,0) + letterSpace );
				if (tempWriter.wordCharactersCount ==0)tempWriter.initialPosition.transform.localPosition = Vector3.zero;
			}else tempWriter.initialPosition.transform.localPosition = Vector3.zero;
			
			tempCollider.size = TextWriter.calculateSize(tempWriter.gameObject);
			tempCollider.center = new Vector3(-tempCollider.size.x /2,tempCollider.size.y/2,0);
			
			return tempWriter;
			
	
		case 32:
			tempWriter.actualPosition = tempWriter.initialPosition.transform.position;
			
			tempWriter.wordCharactersCount++;
			
			
	
			if (tempWriter.wordCharactersCount > 1)
			{
				System.Array.Resize<Transform>(ref tempWriter.lettersTransforms, tempWriter.wordCharactersCount);
				System.Array.Resize<Vector3>(ref tempWriter.size, tempWriter.wordCharactersCount);
				tempWriter.size[tempWriter.wordCharactersCount-1] = new Vector3(0.2f * fontsize,0,0);
				tempWriter.lettersTransforms[tempWriter.wordCharactersCount-1] = new GameObject().transform;
				tempWriter.lettersTransforms[tempWriter.wordCharactersCount-1].name = "Space";
				tempWriter.lettersTransforms[tempWriter.wordCharactersCount-1].parent = tempWordParent.transform;
				
			}
			else
			{
				tempWriter.lettersTransforms[0] = new GameObject().transform;
				tempWriter.lettersTransforms[0].name = "Space";
				tempWriter.lettersTransforms[0].parent = tempWordParent.transform;
				
				tempWriter.size[0] = new Vector3(0.2f * fontsize,0,0);
			}
			
			tempWriter.words += letter;
			tempWriter.initialPosition.transform.localPosition += (-new Vector3(tempWriter.size[tempWriter.wordCharactersCount-1].x,0,0) - letterSpace);//+ letterSpace
			
			tempCollider.size = TextWriter.calculateSize(tempWriter.gameObject);
			tempCollider.center = new Vector3(-tempCollider.size.x /2,tempCollider.size.y/2,0);
			
			return tempWriter;
				
		
		case 13:
			return tempWriter;
			
		default:		
			if (char.IsLetterOrDigit(letter))
			{
				tempLetter = char.ToString(letter);
				if (char.IsLower(letter)) tempLetter = "L"+letter;
			}else
			{
				if ((c <= 32 || c >= 126) && c != 8)
					return tempWriter;
			}
				GameObject letterOBJ = Resources.Load("Leters&Numbers/"/*+tempWriter.fonttype.ToString()*/+"/"/*+tempWriter.fontstyle.ToString()*/+"/" + tempLetter) as GameObject;
				if (tempWriter.wordCharactersCount == 0 && !tempWriter.initialOffsetDone){
					tempWriter.initialPosition.transform.Translate(tempWriter.positionOffset);
					tempWriter.initialOffsetDone = true;
				}
				
				tempWriter.actualPosition = tempWriter.initialPosition.transform.position;
				
				
				GameObject tempOBJ = Instantiate(letterOBJ, tempWriter.initialPosition.transform.position,tempWriter.initialPosition.transform.rotation ) as GameObject;
				tempOBJ.transform.localScale = new Vector3(fontsize,fontsize,fontsize);
				tempWriter.wordCharactersCount++;
				
				
				
		
				if (tempWriter.wordCharactersCount > 1)
				{
				
					System.Array.Resize<Transform>(ref tempWriter.lettersTransforms, tempWriter.wordCharactersCount);
					System.Array.Resize<Vector3>(ref tempWriter.size, tempWriter.wordCharactersCount);
					tempWriter.size[tempWriter.wordCharactersCount-1] = calculateSize(tempOBJ);
					tempWriter.lettersTransforms[tempWriter.wordCharactersCount-1] = tempOBJ.transform;
					tempWriter.lettersTransforms[tempWriter.wordCharactersCount-1].parent = tempWordParent.transform;
						if(tempWriter.lettersTransforms[tempWriter.wordCharactersCount-1].GetComponent<Renderer>()){
						
						if (material != null)tempWriter.lettersTransforms[tempWriter.wordCharactersCount-1].GetComponent<Renderer>().sharedMaterial = material;
						tempWriter.lettersTransforms[tempWriter.wordCharactersCount-1].GetComponent<Renderer>().sharedMaterial.color = tempWriter.colorDefault;
						if(!tempWriter.inputControl)tempWriter.lettersTransforms[tempWriter.wordCharactersCount-1].GetComponent<Renderer>().sharedMaterial.shader = tempWriter.letterShader;
					}
						else foreach(Renderer render in tempWriter.lettersTransforms[tempWriter.wordCharactersCount-1].GetComponentsInChildren<Renderer>())
							{
								if (material != null)render.sharedMaterial = material;
								render.sharedMaterial.color = tempWriter.colorDefault;
								if(!tempWriter.inputControl)render.sharedMaterial.shader = tempWriter.letterShader;
							}
					
					
				}
				else
				{
					tempWriter.lettersTransforms[0] = tempOBJ.transform;
					tempWriter.lettersTransforms[0].parent = tempWordParent.transform;
					if(tempWriter.lettersTransforms[0].GetComponent<Renderer>()){
						
						if (material != null) tempWriter.lettersTransforms[0].GetComponent<Renderer>().sharedMaterial = material;
						tempWriter.lettersTransforms[0].GetComponent<Renderer>().sharedMaterial.color = tempWriter.colorDefault;
						if(!tempWriter.inputControl)tempWriter.lettersTransforms[0].GetComponent<Renderer>().sharedMaterial.shader = tempWriter.letterShader;
					}
					
					else foreach(Renderer render in tempWriter.lettersTransforms[0].GetComponentsInChildren<Renderer>())
					{
						if (material != null)render.sharedMaterial = material;
						render.sharedMaterial.color = tempWriter.colorDefault;
						if(!tempWriter.inputControl)render.sharedMaterial.shader = tempWriter.letterShader;
					}
					
					
					
					tempWriter.size[0] = calculateSize(tempOBJ);
				}
				
				tempWriter.words += letter;
				tempWriter.initialPosition.transform.localPosition += (- new Vector3(tempWriter.size[tempWriter.wordCharactersCount-1].x,0,0) - letterSpace);//+ letterSpace
			tempCollider.size = TextWriter.calculateSize(tempWriter.gameObject);
			tempCollider.center = new Vector3(-tempCollider.size.x /2,tempCollider.size.y/2,0);
			return tempWriter;
			
			}
	}
	
	
//	public static TextWriter clearWord (string controlName)
//	{
//		
//		TextWriter tempWriter;
//		
//		
//		
//		if (!GameObject.Find(controlName))
//		{
//			return null;
//		}
//		else
//		{
//			GameObject tempWord = GameObject.Find(controlName);
//			tempWriter = tempWord.GetComponent<TextWriter>();
//		}
//	    
//				tempWriter.words = "";
//		
//		foreach (Transform r in tempWriter.lettersTransforms)
//				DestroyImmediate(r.gameObject);
//			
//		tempWriter.wordCharactersCount = 0;
//			tempWriter.initialPosition.transform.localPosition = Vector3.zero;
//			return tempWriter;
//	}
	
	
	
	
	/// <summary>
	/// Method to calculate the size of all the meshes attached to this game object, inlcuding their childrens.
	/// </summary>
	/// <param name="letter">
	/// <see cref="GameObject"/> type.  Size is calculated for this GameObject.
	/// </param>
	public static Vector3 calculateSize(GameObject letter)
	{
		
		Vector3 size = new Vector3();
		Bounds bounds;
		Quaternion temprotation = letter.transform.rotation;
		letter.transform.rotation = Quaternion.Euler(Vector3.zero);
		bounds = new Bounds (letter.transform.position, Vector3.zero);

    	Renderer[] renderers = letter.GetComponentsInChildren<Renderer> ();

    	foreach (Renderer renderer in renderers)

    	{
   		     bounds.Encapsulate (renderer.bounds);
   		 }
		size.z = bounds.size.z;
		size.x = bounds.size.x;
		size.y = bounds.size.y;
		letter.transform.rotation = temprotation;
		return size;
	}
	
	
	/// <summary>
	/// Method to print a string at given position and rotation.
	/// </summary>
	/// <param name="controlName">
	/// <see cref="string"/> type.  Name of this control, used for future reference of this control.
	/// </param>
	/// <param name="word">
	/// <see cref="string"/> type.  The string that would be printed on screen.
	/// </param>
	/// <param name="position">
	/// <see cref="Vector3"/> type.  Position in global space, where the Word variable is printed on screen.
	/// </param>
	/// <param name="rotation">
	/// <see cref="Quaternion"/> tyoe.  Rotation in global space, where the Word variable is printed on screen
	/// </param>
	/// <param name="letterSpace">
	/// <see cref="Vector3"/> type.  Represents the space between letters. by default is X= 0.1f, y = 0, z = 0.
	/// </param>
	/// <param name="fontsize">
	/// <see cref="System.single"/> type.  The size of the font, by default is 1, smaller values than 1 decrease the font size.
	/// </param>
//	public static GameObject displayString(string word,Vector3 position ,Quaternion rotation, Vector3 letterSpace, string controlName, float fontsize, Material material, Shader shader)
//	{
//				
//		Hashtable hash = new Hashtable();
//		
//		hash.Add("word",word);
//		hash.Add("position",position);
//		hash.Add("rotation",rotation);
//		hash.Add("letterspace",letterSpace);
//		hash.Add("fontsize",fontsize);
//		hash.Add("material",material);
//		hash.Add("fontshader",shader);
//		
//		return displayString( controlName,  hash);
//	}
	
	
	/// <summary>
	/// Method to print a string at given position and rotation.
	/// </summary>
	/// <param name="controlName">
	/// <see cref="string"/> type.  Name of this control, used for future reference of this control.
	/// </param>
	/// <param name="word">
	/// <see cref="string"/> type.  The string that would be printed on screen.
	/// </param>
	/// <param name="position">
	/// <see cref="Vector3"/> type.  Position in global space, where the Word variable is printed on screen.
	/// </param>
	/// <param name="rotation">
	/// <see cref="Quaternion"/> tyoe.  Rotation in global space, where the Word variable is printed on screen
	/// </param>
	/// <param name="letterSpace">
	/// <see cref="Vector3"/> type.  Represents the space between letters. by default is X= 0.1f, y = 0, z = 0.
	/// </param>
	/// <param name="fontsize">
	/// <see cref="System.single"/> type.  The size of the font, by default is 1, smaller values than 1 decrease the font size.
	/// </param>
	/// <param name="displayCursor">
	/// <see cref="bool"/> type.  By default is true, this displays a cursor on the screen.
	/// </param>
	/// <param name="fontColor">
	/// <see cref="Color"/> type.  The font Color, by default is white.
	/// </param>
	/// <param name="fontShader">
	/// <see cref="Shader"/> type.  The font Shader, by default is Diffuse.
	/// </param>
	/// <param name="rotationPositionObject">
	/// <see cref="GameObject"/> type to get the position and rotation from, and where the user input is displayed at.
	/// </param>
//	public static GameObject displayString(string controlName, Hashtable hash)
//	{
//		
//		hash = TextWriter.castParameters(hash);
//		
//		string word;
//		Vector3 position;
//		Quaternion rotation;
//		Vector3 letterSpace;
//		float fontsize;
//		Shader fontShader;
//		Color fontColor;	
//		Material material;
//		
//		if(hash.ContainsKey("fontcolor")&& (hash["fontcolor"].GetType() == typeof(Color) || hash["fontcolor"].GetType() == typeof(Color32)))
//			fontColor = (Color)hash["fontcolor"];	
//		else fontColor = Color.white;
//		
//		if(hash.ContainsKey("word")&& hash["word"].GetType() == typeof(string))
//			word = (string)hash["word"];	
//		else return null;
//		
//		if(hash.ContainsKey("rotationpositionobject")&& hash["rotationpositionobject"].GetType() == typeof(GameObject))
//			rotation = ((GameObject)hash["rotationpositionobject"]).transform.rotation;	
//		else if(hash.ContainsKey("rotation")&& hash["rotation"].GetType() == typeof(Quaternion))
//			rotation = (Quaternion)hash["rotation"];	
//		else rotation = new Quaternion(0,0,0,0);
//		
//		
//		
//		if(hash.ContainsKey("rotationpositionobject")&& hash["rotationpositionobject"].GetType() == typeof(GameObject))
//			position = ((GameObject)hash["rotationpositionobject"]).transform.position;	
//		else if(hash.ContainsKey("position")&& hash["position"].GetType() == typeof(Vector3))
//			position = (Vector3)hash["position"];	
//		else position = new Vector3(0,0,0);
//		
//		if(hash.ContainsKey("letterspace") && hash["letterspace"].GetType() == typeof(Vector3))
//			letterSpace = (Vector3)hash["letterspace"];	
//		else letterSpace = new Vector3(0.1f,0,0);
//		
//					
//		if(hash.ContainsKey("fontsize") && hash["fontsize"].GetType() == typeof(float))
//			fontsize = (float)hash["fontsize"];	
//		else if(hash.ContainsKey("fontsize") && hash["fontsize"].GetType() == typeof(int))
//			fontsize = (int)hash["fontsize"];	
//		else fontsize = 1;
//	
//		if(hash.ContainsKey("fontshader") && hash["fontshader"].GetType() == typeof(Shader))
//			fontShader = (Shader)hash["fontshader"];	
//		else fontShader = Shader.Find("Diffuse");	
//		
//		if(hash.ContainsKey("material") && hash["material"].GetType() == typeof(Material))
//			material = (Material)hash["material"];	
//		else material = null;	
//		
//		
//		GameObject tempWord;
//				TextWriter tempWriter;
//		if (!GameObject.Find(controlName))
//		{
//			tempWord = new GameObject();
//			tempWord.transform.rotation = rotation;
//			tempWord.transform.position = position;
//			tempWord.name = controlName;
//			tempWord.AddComponent<TextWriter>();
//			tempWriter = tempWord.GetComponent<TextWriter>();
//			tempWriter.colorDefault = fontColor;
//			tempWriter.initialPosition = new GameObject();
//			tempWriter.initialPosition.transform.position = tempWriter.transform.position;
//			tempWriter.initialPosition.transform.rotation = tempWriter.transform.rotation;
//			tempWriter.initialPosition.name = "Position Reference";
//			tempWriter.initialPosition.transform.parent = tempWriter.transform;
//			
//		}
//		else
//		{
//			tempWord = GameObject.Find(controlName);
//			tempWriter = tempWord.GetComponent<TextWriter>();
//			tempWriter.colorDefault = fontColor;
//			tempWriter.initialPosition = tempWriter.gameObject;
//		}
//		
//		
//		if (!tempWriter.textDisplayed)
//		{
//			for (int i = 0; i< word.Length; i++)
//			{
//				write(controlName, Hash ("letter", word.Substring(i,1)[0],"position",position,"rotation",rotation,"letterspace",letterSpace,"fontsize", fontsize,"displaycursor", false,"fontshader", fontShader,"material",material) );
//			}
//			tempWriter.textDisplayed = true;
//		}
//		tempWord.transform.parent = tempWriter.transform;
//		return tempWriter.gameObject;
//	}
	
	
	
	/// <summary>
	/// Method to change the color of a control (this method can't be used for Menu components, you can change a single menu title with this method).
	/// </summary>
	/// <param name="controlName">
	/// <see cref="string"/> type.  Name of this control, used for future reference of this control.
	/// </param>
	/// <param name="color">
	/// <see cref="Color"/> type.  The new font color.
	/// </param>
	public static void fontColor(Color color, GameObject controlName)
	{
		TextWriter tempWriter;
			
			tempWriter = controlName.GetComponent<TextWriter>();
			tempWriter.colorDefault = color;
			foreach (Renderer renderer in controlName.GetComponentsInChildren<Renderer>())
			{
			if (renderer.sharedMaterial)
				renderer.sharedMaterial.color = color;
			}
	}
	
	
	/// <summary>
	/// Method to change the shader of a control (this method can't be used for Menu components, you can change a single menu title with this method).
	/// </summary>
	/// <param name="controlName">
	/// <see cref="string"/> type.  Name of this control, used for future reference of this control.
	/// </param>
	/// <param name="shader">
	/// <see cref="Shader"/> type.  The new font Shader.
	/// </param>
	public static void fontShader(Shader shader, GameObject controlName)
	{
		TextWriter tempWriter;
			
			tempWriter = controlName.GetComponent<TextWriter>();
			tempWriter.letterShader = shader;
			foreach (Renderer renderer in controlName.GetComponentsInChildren<Renderer>())
			{
			if (renderer.sharedMaterial)
				renderer.sharedMaterial.shader = shader;
			}
	}
	
	
	/// <summary>
	/// Method to get the shader of a control (this method can't be used for Menu components, you can get a single menu title with this method).
	/// </summary>
	/// <param name="controlName">
	/// <see cref="string"/> type.  Name of this control, used for future reference of this control.
	/// </param>
	public static Shader getFontShader(string controlName)
	{
		
		Shader tempShader = Shader.Find("Diffuse");
		if (!GameObject.Find(controlName + "_Word"))
		{
			print("This Object ("+ controlName +") does not exist!");
			
		}
		else
		{
			GameObject tempWord = GameObject.Find(controlName + "_Word");
			
		
			foreach (Renderer renderer in tempWord.GetComponentsInChildren<Renderer>())
			{
				tempShader = renderer.material.shader;
			}
		}
		return tempShader;
		
	}
	
	
	/// <summary>
	/// Method to get the color of a control (this method can't be used for Menu components, you can get a single menu title with this method).
	/// </summary>
	/// <param name="controlName">
	/// <see cref="string"/> type.  Name of this control, used for future reference of this control.
	/// </param>
	public static Color getFontColor(string controlName)
	{
		
		Color tempColor = Color.clear;
		if (!GameObject.Find(controlName + "_Word"))
		{
			print("This Object ("+ controlName +") does not exist!");
			
		}
		else
		{
			GameObject tempWord = GameObject.Find(controlName + "_Word");
			
		
			foreach (Renderer renderer in tempWord.GetComponentsInChildren<Renderer>())
			{
				tempColor = renderer.material.color;
			}
		}
		return tempColor;
	}

	
	
	static Hashtable castParameters(Hashtable hash)
	{
		
		Hashtable tempHash = new Hashtable(hash.Count);
		
		foreach(DictionaryEntry HashEntry in hash)
		{
			if (HashEntry.Value.GetType() == typeof(int[]))
			{
				float[] tempInt = new float[((int[])HashEntry.Value).Length];
				int i = 0;
				foreach(int intEntry in (int[])HashEntry.Value)
				{
					tempInt[i] = (float) intEntry;
					i++;
				}
				tempHash.Add(HashEntry.Key.ToString().ToLower(),tempInt);
			}
			else if (HashEntry.Value.GetType() == typeof(double[]))
			{
				float[] tempDouble = new float[((double[])HashEntry.Value).Length];
				int i = 0;
				foreach(double doubleEntry in (double[])HashEntry.Value)
				{
					tempDouble[i] = (float) doubleEntry;
					i++;
				}
				tempHash.Add(HashEntry.Key.ToString().ToLower(),tempDouble);
			}
			if (HashEntry.Value.GetType() == typeof(int))
			{
				tempHash.Add(HashEntry.Key.ToString().ToLower(),(float)(int)HashEntry.Value);
			}
			else if (HashEntry.Value.GetType() == typeof(double))
			tempHash.Add(HashEntry.Key,(float)(double)HashEntry.Value);
			else tempHash.Add(HashEntry.Key.ToString().ToLower(),HashEntry.Value);
		}
		return tempHash;
	}
	
	
	
	public static Hashtable Hash(params object[] parameters)
	{
		Hashtable parametersToReturn = new Hashtable(parameters.Length/2);
		if (parameters.Length %2 ==0) 
		{
		
			for(int i = 0 ; i < parameters.Length - 1; i+=2) 
			{
				parametersToReturn.Add(parameters[i], parameters[i+1]);
			}
			return parametersToReturn;
		}
		else
		{
			Debug.LogError("Number of parameters must be in pairs"); 
			return null;
		}
	}	

	
	private void apply_iTween(GameObject c, Hashtable hash)
	{
		hash = TextWriter.castParameters(hash);
		
		if (hash.ContainsKey("itweenmethod") && hash["itweenmethod"].GetType() == typeof(string))
		{
			switch((string)hash["itweenmethod"])
			{
			case "colorfrom":
				
				iTween.ColorFrom(c, hash);
				
				break;
				
				
			case "colorto":
				
				iTween.ColorTo(c, hash);
				break;
				
			case "colorupdate":
				
				iTween.ColorUpdate(c, hash);
				break;
				
			case "fadefrom":
				iTween.FadeFrom(c, hash);
				break;
				
			case "fadeto":
				iTween.FadeTo(c, hash);
				break;
				
			case "fadeupdate":
				iTween.FadeUpdate(c, hash);
				break;
				
			case "lookfrom":
				iTween.LookFrom(c, hash);
				break;
				
			case "lookto":
				iTween.LookTo(c, hash);
				break;
				
			case "lookupdate":
				iTween.LookUpdate(c, hash);
				break;
				
			case "moveadd":
				iTween.MoveAdd(c, hash);
				break;
				
			case "moveby":
				iTween.MoveBy(c, hash);
				break;
				
			case "movefrom":
				iTween.MoveFrom(c, hash);
				break;
				
			case "moveto":
				iTween.MoveTo(c, hash);
				break;
				
			case "moveupdate":
				iTween.MoveUpdate(c, hash);
				break;
				
			case "punchposition":
				iTween.PunchPosition(c, hash);
				break;
				
			case "punchrotation":
				iTween.PunchRotation(c, hash);
				break;
				
			case "punchscale":
				iTween.PunchScale(c, hash);
				break;
				
			case "rotateadd":
				iTween.RotateAdd(c, hash);
				break;
				
			case "rotateby":
				iTween.RotateBy(c, hash);
				break;
				
			case "rotatefrom":
				iTween.RotateFrom(c, hash);
				break;
				
			case "rotateto":
				iTween.RotateTo(c, hash);
				break;
				
			case "rotateupdate":
				iTween.RotateUpdate(c, hash);
				break;
				
			case "scaleadd":
				iTween.ScaleAdd(c, hash);
				break;
				
			case "scaleby":
				iTween.ScaleBy(c, hash);
				break;
				
			case "scalefrom":
				iTween.ScaleFrom(c, hash);
				break;
				
			case "scaleto":
				iTween.ScaleTo(c, hash);
				break;
				
			case "scaleupdate":
				iTween.ScaleUpdate(c, hash);
				break;
				
			case "shakeposition":
				iTween.ShakePosition(c, hash);
				break;
				
			case "shakerotation":
				iTween.ShakeRotation(c, hash);
				break;
				
			case "shakescale":
				iTween.ShakeScale(c, hash);
				break;
			}
		}
		

	}
	
	
	
	public void process(Real3DFont script)
	{
		if (script.individualFaceMaterials)
		{
			script.text.SubMeshCount = 3;
		}else script.text.SubMeshCount = 0;
		switch (script.meshType)
		{
		case Real3DFont.MeshType.Line:
			getMaterials(script);
			script.text = script.vectorFont.Extrude(script.textToPrint, script.depth,script.text,script.smoothingAngle);
			MeshRenderer meshR = script.gameObject.GetComponent<MeshRenderer>();
			if (!meshR){
				clearMeshes(script, true);
				meshR = script.gameObject.AddComponent<MeshRenderer>();
				script.gameObject.AddComponent<MeshFilter>();
			}
			if (meshR)
			{
				MeshFilter meshF = script.gameObject.GetComponent<MeshFilter>();
				DestroyImmediate(meshF.sharedMesh);
				meshF.sharedMesh = Real3DText.Fonts.ExtrudedText.meshLine(script.text);
				setMaterials(script);
			}
			break;
		case Real3DFont.MeshType.Word:
			getMaterials(script);
			clearMeshes(script, true);
			script.text = script.vectorFont.Extrude(script.textToPrint, script.depth,script.text,script.smoothingAngle);
			Real3DText.Fonts.ExtrudedText.meshWords(script.text, script.transform);
			setMaterials(script);
			break;
		case Real3DFont.MeshType.Letter:
			getMaterials(script);
			clearMeshes(script,false);
			script.text = script.vectorFont.Extrude(script.textToPrint, script.depth,script.text,script.smoothingAngle);
			Real3DText.Fonts.ExtrudedText.meshLetters(script.text, script.transform);
			setMaterials(script);
			break;
		}
	}
	
	public void getMaterials(Real3DFont script)
	{
		MeshRenderer[] tempRenderer = script.gameObject.GetComponentsInChildren<MeshRenderer>();
		if (script.text.TMaterial == null) 
			script.text.TMaterial = new Real3DText.Fonts.Text.Materials[tempRenderer.Length];
		System.Array.Resize<Real3DText.Fonts.Text.Materials>(ref script.text.material,tempRenderer.Length==0?1:tempRenderer.Length);
		int i = 0;
		foreach (MeshRenderer renderer in tempRenderer){
			
			CheckIfMaterialNeedsToBeCreated(ref script.text.TMaterial[i]);
			if (script.text.SubMeshCount == 0){
				script.text.TMaterial[i].material[0] = renderer.sharedMaterials[0];
				if (script.text.TMaterial[i].material[0]== null && i >0)
					script.text.TMaterial[i].material[0] = script.text.TMaterial[i-1].material[0];
			}
			else{
				if (renderer.sharedMaterials.Length ==3)
				{
					for (int i1 = 0;i1 < script.text.TMaterial[i].material.Length;i1++){
						script.text.TMaterial[i].material[i1] = renderer.sharedMaterials[i1];
					}
				}
				else{
					if (script.text.TMaterial[i].material[1] == null){
						script.text.TMaterial[i].material[0] = script.text.TMaterial[i].material[1] =
							script.text.TMaterial[i].material[2]= renderer.sharedMaterials[0];
					}
				}
			}
			i++;
		}
	}
	
	Material[] GetCopy(Material[] mat)
	{
		Material[] newArray = (Material[]) mat.Clone();
		return newArray;
	}
	
	public void setMaterials(Real3DFont script)
	{
		int r = 0;
		MeshRenderer[] tempRenderer = script.gameObject.GetComponentsInChildren<MeshRenderer>();
		if (script.text.TMaterial.Length != tempRenderer.Length)
		{
			System.Array.Resize<Real3DText.Fonts.Text.Materials>(ref script.text.material,tempRenderer.Length==0?1:tempRenderer.Length);
		}

		foreach (MeshRenderer renderer in tempRenderer){
//			MeshFilter meshF = renderer.GetComponent<MeshFilter>();
			if (script.text.SubMeshCount == 3)
			{
				if (script.text.TMaterial[r] == null || script.text.TMaterial[r].material.Length !=3 && r>0)
				{
					script.text.TMaterial[r] = new Real3DText.Fonts.Text.Materials();
					script.text.TMaterial[r].material = GetCopy(script.text.TMaterial[r-1].material);
				}
				renderer.sharedMaterials = GetCopy(script.text.TMaterial[r].material);
			}
			else{
				CheckIfMaterialNeedsToBeCreated(ref script.text.TMaterial[r]);
				Material[] tempMats = renderer.sharedMaterials;
				System.Array.Resize<Material>(ref tempMats,1);
				renderer.sharedMaterials = tempMats;
				if (script.text.TMaterial[r].material[0]==null && r >0)
					script.text.TMaterial[r].material[0] = script.text.TMaterial[r-1].material[0];
				renderer.sharedMaterial = script.text.TMaterial[r].material[0]; 
			}
			r++;
		}
	}
	
	void CheckIfMaterialNeedsToBeCreated(ref Real3DText.Fonts.Text.Materials mat)
	{
		if (mat == null || mat.material == null || mat.material.Length < 3)
		{
			mat = new Real3DText.Fonts.Text.Materials();
			mat.material = new Material[3];
		}
	}

	
	public void clearMeshes(Real3DFont script, bool destroyMesh)
	{
		MeshFilter[] tempChilds = script.gameObject.GetComponentsInChildren<MeshFilter>();
		for (int i = tempChilds.Length-1; i >=0 ;i--)// (MeshFilter meshfilter in tempChilds )
		{
			
			GameObject gameOBJ = tempChilds[i].gameObject;
			if (gameOBJ.GetHashCode()!= script.gameObject.GetHashCode())
			{
				if (destroyMesh){
					Mesh mesh = tempChilds[i].sharedMesh;
					DestroyImmediate(mesh,true);
				}
				DestroyImmediate(gameOBJ);
			}
			else {				
				if (tempChilds[i] != null){
					if (destroyMesh){
						Mesh mesh = tempChilds[i].sharedMesh;
						DestroyImmediate(mesh,true);
					}
					DestroyImmediate(tempChilds[i]);
				}
				if (gameOBJ.GetComponent<MeshRenderer>() != null){
					DestroyImmediate(gameOBJ.GetComponent<MeshRenderer>());	
				}
			} 
		}
	}
	
	
	
//public void getCursorSize(Real3DFont real3DFont)
//	{
//			if (real3DFont.textToPrint.Length == 0)
//			{
//				real3DFont.textToPrint = "A";
//				process(real3DFont);
//				this.cursorSize = real3DFont.renderer.bounds.extents.y;
//				real3DFont.textToPrint = "";
//				process(real3DFont);
//			}else this.cursorSize = real3DFont.renderer.bounds.extents.y;
//	}
//	
	
//public void UpdateCursor(Real3DFont real3DFont)
//	{
//		this.getCursorSize(real3DFont);
//		this.initialPosition.transform.localPosition = Vector3.zero;
//		this.initialPosition.transform.localScale = Vector3.one *this.cursorSize * 150;
//		this.initialPosition.transform.Translate(new Vector3(real3DFont.text.Width,0,0));
//	}
//public void createCursor(Real3DFont real3DFont)
//	{
//		if (this.displayCursor){
//					this.initialPosition = (GameObject)Instantiate(Resources.Load("cursor") as GameObject);
//					this.initialPosition.GetComponent<MeshRenderer>().sharedMaterial.color = this.colorDefault;
//					this.initialPosition.transform.parent = this.transform;
//					this.initialPosition.transform.eulerAngles = Vector3.zero;
//					this.UpdateCursor(real3DFont);
//				}else{
//					if (this.initialPosition)
//						DestroyImmediate(this.initialPosition);
//				}
//	}
	

}
