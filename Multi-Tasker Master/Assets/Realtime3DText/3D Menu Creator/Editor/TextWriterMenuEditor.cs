using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Reflection;


[System.Serializable ]
	class tempVector3{
		public float x;
		public float y;
		public float z;
	}

[System.Serializable ]
	class tempVector4{
		public float x;
		public float y;
		public float z;
		public float w;
	}


[InitializeOnLoad]
[CustomEditor(typeof(TextWriterVisual))]
public class TextWriterMenuEditor : Editor {

	public enum Activity{
		redraw,
		changeColor,
		ChangeShader,
		ChangeFont,
		Process,
		All,
		ChangeFontAndProcess,
		none
	}
	public SerializedObject textWriterVisual;
	public SerializedProperty events;
	
	Dictionary<string, object> values;
	
	
	public TextWriterVisual script;
	public Rect windowGlobalParameters = new Rect(100,100,500,500);	
	TextWriterGlobalWindow windowGlobal;
	TextWriterComponentWindow windowComponent;
	bool[] MenuTitlegotFocused;
	
	
	
	[MenuItem("3D Menu Creator/Add 3DMenu")]
    static void Add3DMenu () {
		GameObject newMenuObject;
		
		
		if(Selection.activeGameObject != null) {
			newMenuObject = Selection.activeGameObject;
			if (newMenuObject.GetComponent<TextWriterVisual>()||newMenuObject.GetComponent<TextWriter>()|| newMenuObject.name != "GameObject")
			{
				int decision ;
				if (newMenuObject.GetComponent<TextWriterVisual>()||newMenuObject.GetComponent<TextWriter>())
				{
					decision = EditorUtility.DisplayDialogComplex("Warning!", "This GameObject is a 3DMenu component already, Do you want to replace it? \n If you replace it, all current settings and titles would be cleared!","Yes", "No", "Create New Menu");
				}else decision = EditorUtility.DisplayDialogComplex("Warning!", "Did you want to add 3DMenu component to the GameObject "+newMenuObject.name+" ?","Yes", "No", "Create New GameObject");
					
				switch (decision)
				{
				case 0:
					if (newMenuObject.GetComponent<TextWriterVisual>())
						DestroyImmediate(newMenuObject.GetComponent<TextWriterVisual>());
					if (newMenuObject.GetComponent<TextWriter>())
						DestroyImmediate(newMenuObject.GetComponent<TextWriter>());
					break;
				case 1:
					return;
				case 2:
					newMenuObject = new GameObject();
					break;
				}
			} 
		}else{
		
		newMenuObject = new GameObject();
		}
		
		
		
		newMenuObject.AddComponent(typeof(TextWriterVisual));
		
		TextWriterVisual tempScript = newMenuObject.GetComponent<TextWriterVisual>();
		
		tempScript.fontShader = Shader.Find("Diffuse");
		
		LoadNSave tempSave = new LoadNSave();
		tempSave.events = new Hashtable();
		SaveLoad.currentFilePath = "TextWriterMenus.DAT";
		int index = 0;
		if (System.IO.File.Exists(SaveLoad.currentFilePath))
		{
			tempSave = SaveLoad.Load(SaveLoad.currentFilePath);
			index = (int)tempSave.events["MenuIndex"]+1;
		}else{
			tempSave.events.Add("MenuIndex" ,index);
		}
		tempScript.controlName = "Menu "+index;
		tempSave.events.Add("Menu" +index, tempScript.controlName);
		tempSave.events["MenuIndex"]=index;
		
		SaveLoad.Save(tempSave);
		tempSave.events.Clear();
		newMenuObject.name = tempScript.controlName;
		
		
				
		SerializedObject tempTextWriterVisual = new SerializedObject(tempScript);
		SerializedProperty tempEvents = tempTextWriterVisual.FindProperty("events1");
		
		tempEvents.serializedObject.Update();
		tempEvents.arraySize = 3;
		tempEvents.serializedObject.ApplyModifiedProperties();
		
		
		//Global Defaults
		tempScript.events1[0].type = TextWriterVisual.OnEventType.OnMouseEnter;
		tempScript.eventsShader[0]= Shader.Find("Specular");
		tempScript.eventIndex[0] = 0;
		tempScript.events1[1].type = TextWriterVisual.OnEventType.OnMouseExit;
		tempScript.eventsShader[1]= Shader.Find("Diffuse");
		tempScript.eventIndex[1] = 1;
		tempScript.events1[2].type = TextWriterVisual.OnEventType.OnMouseDown;
		tempScript.eventsShader[2]= Shader.Find("Diffuse");
		tempScript.eventIndex[2] = 2;
		
    }
	
	public void changeChildsName()
	{
		TextWriter[] childsTitles = script.gameObject.GetComponentsInChildren<TextWriter>();
		MenuOptionsSelection[] childsTitlesWord = script.gameObject.GetComponentsInChildren<MenuOptionsSelection>();
		
		script.gameObject.name = script.controlName;
		int i = 0;
		foreach(TextWriter child in childsTitles)
		{
			child.gameObject.name = script.controlName +" Title " + i;
			i++;
		}
		
		i = 0;
		foreach(MenuOptionsSelection child in childsTitlesWord)
		{
			child.gameObject.name = script.controlName +" Title " + i + "_Word";
			i++;
		}
	}
	
	
	public override void OnInspectorGUI ()
	{
		GUI.changed = false;
		GUILayout.Space(5);
		GUILayout.BeginHorizontal();
		GUILayout.Space(25);
		GUILayout.Label(script.logo,GUILayout.Width(245));
		GUILayout.EndHorizontal();
		
		if (script.controlName != script.gameObject.name)
		{
			script.controlName = script.gameObject.name;
			changeChildsName();
		}
		
		GUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Menu Name","Name of the Menu, this has to be Unique GameObject name"));
		string menuName = EditorGUILayout.TextField(script.controlName, GUILayout.Width(100));
		if (menuName != script.controlName)
		{
			if (!GameObject.Find(menuName)){
				script.controlName = menuName;
				changeChildsName();
			}
			
			else EditorUtility.DisplayDialog("Warning", "The name "+ menuName + " is already taken by another object, please use a unique name", "Ok");
		}
		GUILayout.EndHorizontal();
		
		
		
		GUILayout.BeginHorizontal();
		GUILayout.Label("Save & Load templates");
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		if(GUILayout.Button(new GUIContent("Save", "Press to save actual settings"),EditorStyles.miniButtonLeft,GUILayout.Width(50)))
		{
			saveSettings();
		}
		if(GUILayout.Button(new GUIContent("Load", "Press to load settings"),EditorStyles.miniButtonRight,GUILayout.Width(50)))
		{
			int decision = EditorUtility.DisplayDialogComplex("Warning!", "Are you sure you want to load settings? \n this would clear your current settings.","Yes", "No","Save First");
			switch(decision)
			{
			case 0:
				loadSettings();		
				break;
				
			case 1:
				break;
				
			case 2:
				saveSettings();
				loadSettings();
				break;
			}
		
		}
		
		
		
		GUILayout.EndHorizontal();
		
		
		
		
		
		GUILayout.Space(10);
		
		GUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Is the Menu a Pause Menu?","Select this option, if you want to activate this menu as a Pause Menu, it would be activated if the enduser press esc key"));
		script.pauseMenu = EditorGUILayout.Toggle(script.pauseMenu, GUILayout.Width(15));
		GUILayout.EndHorizontal();
		
		GUILayout.Space(5);
		
		if (script.pauseMenu)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label(new GUIContent("Camera Transform","Add the camera where you want this Pause menu to appear, this is optional.  If the camera is not provided, the Menu will show on the MainCamera"));
			script.mainCamera =(Transform) EditorGUILayout.ObjectField(script.mainCamera, typeof(Transform),true, GUILayout.Width(100));
			GUILayout.EndHorizontal();
			
			GUILayout.Space(5);	
			
			GUILayout.BeginHorizontal();
			GUILayout.Label(new GUIContent("Distance from camera","Distance of the menu from the camera"));
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			script.distance = EditorGUILayout.Vector3Field("",script.distance, GUILayout.Width(150));
			GUILayout.EndHorizontal();
			
			GUILayout.Space(5);	
			
			GUILayout.BeginHorizontal();
			GUILayout.Label(new GUIContent("Camera Field of View","This would be the Field of View value when paused"));
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			script.cameraFieldOfView = EditorGUILayout.FloatField(script.cameraFieldOfView, GUILayout.Width(150));
			GUILayout.EndHorizontal();
		}
		
		GUILayout.Space(10);
		
		GUILayout.BeginHorizontal();
		GUILayout.TextArea("Choose the paramters you want to apply globally to all menu Titles\n Set these parameters by pressing global parameters button",GUILayout.Width(200));
		GUILayout.EndHorizontal();
		
		GUILayout.Space(15);
		
		GUI.changed = false;
		GUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Single Position Space between Titles?","Select this option, if you want all titles have the same space between them"));
		script.singlePositionSpace = EditorGUILayout.Toggle(script.singlePositionSpace, GUILayout.Width(15));
		GUILayout.EndHorizontal();
		if (GUI.changed && script.menuComponentsIndex > 0)
			applyGlobalParameters(script.components, Activity.redraw);
		
		
		GUILayout.Space(5);
		GUI.changed = false;
		GUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Single Font type?", "Select this option, if you want all titles with the same font type"));
		script.singleFontType = EditorGUILayout.Toggle(script.singleFontType, GUILayout.Width(15));
		GUILayout.EndHorizontal();
		
		
		
		GUILayout.Space(5);
		
		GUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Single Font style?", "Select this option, if you want all titles with the same font style"));
		script.singleFontStyle = EditorGUILayout.Toggle(script.singleFontStyle, GUILayout.Width(15));
		GUILayout.EndHorizontal();
		
		GUILayout.Space(5);
		
		GUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Single Font size?", "Select this option, if you want all titles with the same font size"));
		script.singleFontSize = EditorGUILayout.Toggle(script.singleFontSize, GUILayout.Width(15));
		GUILayout.EndHorizontal();
		if (GUI.changed && script.menuComponentsIndex > 0)
			applyGlobalParameters(script.components, Activity.ChangeFontAndProcess);
		
		GUILayout.Space(5);
		
		
//		GUILayout.BeginHorizontal();
//		GUILayout.Label(new GUIContent("Single Font letter space?","Select this option, if you want all titles with same space between title letters"));
//		script.singleLetterSpace = EditorGUILayout.Toggle(script.singleLetterSpace, GUILayout.Width(15));
//		GUILayout.EndHorizontal();
//		if (GUI.changed) {applyGlobalParameters(script.components); redrawTitles(script.components);GUI.changed = false;}
		
		GUILayout.Space(5);
		GUI.changed = false;
		GUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Single Font Color?","Select this option, if you want the same font color for all the titles"));
		script.singleColor = EditorGUILayout.Toggle(script.singleColor, GUILayout.Width(15));
		GUILayout.EndHorizontal();
		if (GUI.changed && script.menuComponentsIndex > 0)
			applyGlobalParameters(script.components, Activity.changeColor);
		
		
		GUILayout.Space(5);
		GUI.changed = false;
		GUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Single Font Shader?","Select this option, if you want all Titles use the same font shader"));
		script.singleShader = EditorGUILayout.Toggle(script.singleShader, GUILayout.Width(15));
		GUILayout.EndHorizontal();
		if (GUI.changed && script.menuComponentsIndex > 0)
			applyGlobalParameters(script.components, Activity.ChangeShader);
		GUILayout.Space(5);
		
		
		GUI.changed = false;
		GUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Single Font event Shader?", "Select this option, if you want the same shader for all titles on each event"));
		script.singleEventsShader = EditorGUILayout.Toggle(script.singleEventsShader, GUILayout.Width(15));
		GUILayout.EndHorizontal();
		
		GUILayout.Space(5);
		
		
		GUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Single Audio Clip per event?", "Select this option, if you want all titles use the same audo clip on each event"));
		script.singleEventsAudio = EditorGUILayout.Toggle(script.singleEventsAudio, GUILayout.Width(15));
		GUILayout.EndHorizontal();
		
		
		GUILayout.Space(5);
		
		GUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Same function target \nfor all menu titles?", "Select this option, if you want allt titles use the same function target for each event"));
		script.singleEventsTarget = EditorGUILayout.Toggle(script.singleEventsTarget, GUILayout.Width(15));
		GUILayout.EndHorizontal();
		
		GUILayout.Space(5);
		
		GUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Same function for \nall menu titles?", "Select this option, if you want all titlesuse the same functions for each event.\n Note: if you use this option, i.e. all titles when clicked (OnMouseDown event) would call exactly the same function."));
		script.singleEventsFunctions = EditorGUILayout.Toggle(script.singleEventsFunctions, GUILayout.Width(15));
		GUILayout.EndHorizontal();
		
		GUILayout.Space(5);
		
		GUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Same parameters function \nfor all menu titles?", "Select this option, if you want all titles use the same function parameters for each event"));
		script.singleEventsFunctionsParam = EditorGUILayout.Toggle(script.singleEventsFunctionsParam, GUILayout.Width(15));
		GUILayout.EndHorizontal();
		
		GUILayout.Space(5);
		
		GUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Use iTween for all events?", "Select this option, if you want all titles to have the same iTween effects per event"));
		script.singleEventsUseItween = EditorGUILayout.Toggle(script.singleEventsUseItween, GUILayout.Width(15));
		GUILayout.EndHorizontal();
		
		if (GUI.changed && script.menuComponentsIndex > 0)
			applyGlobalParameters(script.components, Activity.none);
		
		
		GUILayout.Space(15);
		
		GUILayout.BeginHorizontal();
		GUILayout.TextArea("       Global Parameters", GUILayout.Width(200));
		GUILayout.EndHorizontal();
		
				
		GUILayout.Space(10);
		GUILayout.BeginHorizontal();
		if(GUILayout.Button(new GUIContent("Global parameters","Press this button to open the global parameter window and set the parameters selected on the previous section"),GUILayout.Width(200)))
		{
			if (Application.isPlaying && !script.cleanOnceGlobal) script.cleanOnceGlobal= true;
			else if (!Application.isPlaying && script.cleanOnceGlobal) script.cleanOnceGlobal= false;
			
			windowGlobal = (TextWriterGlobalWindow) EditorWindow.GetWindow(typeof(TextWriterGlobalWindow));
			windowGlobal.autoRepaintOnSceneChange = true;
       		windowGlobal.Init(script, this);
		}
		GUILayout.EndHorizontal();
		
		
		GUILayout.Space(25);
		
		GUILayout.BeginHorizontal();
		GUILayout.TextArea("Menu Titles", GUILayout.Width(200));
		GUILayout.EndHorizontal();
		
		
		
		GUILayout.Space(15);
		GUILayout.BeginHorizontal();
		GUILayout.Space(50);
		if(GUILayout.Button(new GUIContent("+", "Press to add Titles to this menu"),EditorStyles.miniButtonLeft,GUILayout.Width(50)))
		{
			addTitle();
			GUI.changed = false;
			
		}
		GUILayout.Space(10);
		if(GUILayout.Button(new GUIContent("-", "Press to remove last title on this menu"),EditorStyles.miniButtonRight,GUILayout.Width(50)))
		{
			if (script.menuComponentsIndex !=0)
			{
				DestroyImmediate(script.components[script.menuComponentsIndex-1]);
				script.menuComponentsIndex--;
				script.menuComponentsIndex = Mathf.Clamp(script.menuComponentsIndex,0, int.MaxValue);
				MenuTitlegotFocused = new bool[script.menuComponentsIndex];
				for (int i = 0; i < MenuTitlegotFocused.Length;i++)
					MenuTitlegotFocused[i] = false;
				initializeVariable();
			}
		}
		GUILayout.EndHorizontal();	
			
		
			
		for(int index = 0; index < script.menuComponentsIndex; index++)
		{
			GUILayout.Space(10);
			GUILayout.BeginHorizontal();
			GUILayout.TextArea("Menu component # " + index,GUILayout.Width(200));
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			GUILayout.Label("Title");
			GUI.changed = false;
//			int lastLength = script.menuComponentsTitles[index].Length;
			GUI.SetNextControlName("Titles");
			script.menuComponentsTitles[index] = EditorGUILayout.TextField(script.menuComponentsTitles[index] != null?script.menuComponentsTitles[index]:"Menu Title" + script.menuComponentsIndex, GUILayout.Width(150));
			GUILayout.EndHorizontal();
			
			TextWriter textWriter = script.components[index].GetComponent<TextWriter>();
			if (GUI.changed)
			try{
				Real3DFont textReal3DFont = script.components[index].GetComponentInChildren<Real3DFont>();
				textReal3DFont.textToPrint = script.menuComponentsTitles[index];
				textReal3DFont.meshType = Real3DFont.MeshType.Line;
				Real3DFontEditor.process(textReal3DFont);
				redrawTitles(script.components);
			}
			catch{}
			
//			updateTitle(index, lastLength, textWriter);
			

			
			GUILayout.BeginHorizontal();
			if(GUILayout.Button(new GUIContent("Parameters", "Press to acces the parameters window for this title"),GUILayout.Width(200)))
			{
				if (Application.isPlaying && !script.cleanOnceComponent) script.cleanOnceComponent= true;
				else if (!Application.isPlaying && script.cleanOnceComponent) script.cleanOnceComponent= false;
				if (windowComponent)
				if (windowComponent.windowTween != null) windowComponent.windowTween.Close();
				if (!windowComponent)windowComponent = (TextWriterComponentWindow) EditorWindow.GetWindow(typeof(TextWriterComponentWindow));
       			windowComponent.Init(textWriter, this, index);
				
			}
			GUILayout.EndHorizontal();
		}
		
		if (windowGlobal)
			windowGlobal.Repaint();
		if (windowComponent)
			windowComponent.Repaint();
		if (GUI.changed)
		{
			EditorUtility.SetDirty(target);
		}
	}
	 
	void updateTitle(int index, int lastLength, TextWriter textWriter)
	{
		int currentLength = script.menuComponentsTitles[index].Length;
		string tempControlID = GUI.GetNameOfFocusedControl();
		
			
		if (lastLength > currentLength)
		{
			for (int Char = currentLength; Char < lastLength; Char++)
			TextWriter.write(script.controlName +" Title " + (index), TextWriter.Hash("letter","\b"[0],"position",script.menuObject.transform.position + script.positionSpace * (index),"rotation",script.menuObject.transform.rotation, "letterspace",textWriter.letterSpace, "fontsize", textWriter.fontSize, "displaycursor", textWriter.displayCursor, "fontshader", script.fontShader, "material", textWriter.material,"fontcolor",textWriter.colorDefault) );
		}
		else if (lastLength < currentLength)
		{
			for (int Char = lastLength; Char < currentLength; Char++)
			TextWriter.write(script.controlName +" Title " + (index), TextWriter.Hash("letter",script.menuComponentsTitles[index][Char],"position",script.menuObject.transform.position + script.positionSpace * (index),"rotation",script.menuObject.transform.rotation, "letterspace",textWriter.letterSpace, "fontsize", textWriter.fontSize, "displaycursor", textWriter.displayCursor, "fontshader", script.fontShader, "material", textWriter.material,"fontcolor",textWriter.colorDefault) );
		}
		if (tempControlID == "Titles")
		{
			MenuTitlegotFocused[index] = true;
		}
		else if (tempControlID != "Titles" && MenuTitlegotFocused[index])
		{
			BoxCollider tempCollider;
			tempCollider = script.components[index].GetComponentInChildren<BoxCollider>();
			tempCollider.size = TextWriter.calculateSize(script.components[index]);
			tempCollider.center = new Vector3(-tempCollider.size.x /2,tempCollider.size.y/2,0);
		}
	}
	
	void addTitle()
	{
		addTitle("");
	}
	
	void addTitle(string text)
	{
			script.menuComponentsIndex++;
		
			
			MenuTitlegotFocused = new bool[script.menuComponentsIndex];
			for (int i = 0; i < MenuTitlegotFocused.Length;i++)
				MenuTitlegotFocused[i] = false;
			
			initializeVariable();
			
			Material newMaterial = new Material(Shader.Find("Diffuse"));
			newMaterial.name = script.controlName +" Title " + (script.menuComponentsIndex-1);
			
			script.menuComponentsTitles[script.menuComponentsIndex-1] = "Title " + script.menuComponentsIndex;
			if (text.Length ==0)text = script.menuComponentsTitles[script.menuComponentsIndex-1] = "Title " + script.menuComponentsIndex;
			script.components[script.menuComponentsIndex-1] = new GameObject();
			script.components[script.menuComponentsIndex-1].name = script.controlName +" Title " + (script.menuComponentsIndex-1);
			script.components[script.menuComponentsIndex-1].AddComponent<TextWriter>();
//			script.components[script.menuComponentsIndex-1] = TextWriter.displayString(script.menuComponentsTitles[script.menuComponentsIndex-1],script.menuObject.transform.position + script.positionSpace * (script.menuComponentsIndex-1),script.menuObject.transform.rotation ,new Vector3(0.1f,0,0),script.controlName +" Title " + (script.menuComponentsIndex-1) , 1, newMaterial, script.fontShader);
			script.components[script.menuComponentsIndex-1].transform.parent = script.menuObject.transform;
		TextWriter tempTextWriter =script.components[script.menuComponentsIndex-1].GetComponent<TextWriter>();
		tempTextWriter.initializeOnAwake = true;
//			script.components[script.menuComponentsIndex-1].GetComponent<TextWriter>().initializeOnAwake = true;
			GameObject new3DText = new GameObject();
			new3DText.AddComponent<Real3DFont>();
		
		Real3DFont tempReal3DFont = new3DText.GetComponent<Real3DFont>();
		
		new3DText.AddComponent<MeshRenderer>();
		new3DText.AddComponent<MeshFilter>();
		tempReal3DFont.fontSize = 25;
		tempReal3DFont.textToPrint = text;
		new3DText.AddComponent<MenuOptionsSelection>();
		
		new3DText.transform.parent = script.components[script.menuComponentsIndex-1].transform;
		
		script.components[script.menuComponentsIndex-1].transform.Translate(tempTextWriter.positionSpace * (script.menuComponentsIndex -1));
		
		
		
		tempReal3DFont.fontResolution = 3;
		tempReal3DFont.fontStyleIndex = 0;
		tempReal3DFont.text =  new Real3DText.Fonts.ExtrudedText();
		Real3DFontEditor.updateFontList(tempReal3DFont);
		for (int index = 0; index < tempReal3DFont.fontsName.Length; index++){
			if (tempReal3DFont.fontsName[index] =="Arial") {tempReal3DFont.fontNameIndex =index;break;}
			else tempReal3DFont.fontNameIndex =0;
		}
		tempReal3DFont.vectorFont = new Real3DText.Fonts.VectorFont();
		Real3DFontEditor.changeFont(tempReal3DFont);
		Real3DFontEditor.process(tempReal3DFont);
		tempReal3DFont.vectorFont.toList();
		
		new3DText.AddComponent<BoxCollider>();	
		MeshRenderer renderer = new3DText.GetComponent<MeshRenderer>();
		renderer.sharedMaterial = newMaterial;
		renderer.sharedMaterial.shader = script.fontShader;
			
		tempTextWriter.material = newMaterial;
		SerializedObject tempSerializedObject = new SerializedObject(tempTextWriter);
		SerializedProperty tempEvents = tempSerializedObject.FindProperty("events1");
			
		tempEvents.serializedObject.Update();
		tempEvents.arraySize = 3;
		tempEvents.serializedObject.ApplyModifiedProperties();
		tempTextWriter.events1[0].type = TextWriterVisual.OnEventType.OnMouseEnter;
		tempTextWriter.eventIndex[0] = 0;
		tempTextWriter.events1[1].type = TextWriterVisual.OnEventType.OnMouseExit;
		tempTextWriter.eventIndex[1] = 1;
		tempTextWriter.events1[2].type = TextWriterVisual.OnEventType.OnMouseDown;
		tempTextWriter.eventIndex[2] = 2;
		tempTextWriter.letterShader = Shader.Find("Diffuse");
		applyGlobalParameters(script.components, Activity.All);
		redrawTitles(script.components);
		EditorUtility.SetDirty(tempReal3DFont);
	}
	
	void initializeVariable()
	{
		int Index1 = script.menuComponentsIndex !=0?script.menuComponentsIndex:0;
		System.Array.Resize<GameObject>(ref script.components, Index1);
		System.Array.Resize<string>(ref script.menuComponentsTitles, Index1);
	}
	
	public void OnEnable()
	{
		script = (TextWriterVisual) target;
		if (Real3DText.Fonts.FontList.fontlist == null) Real3DText.Fonts.FontList.fontlist = new Dictionary<string, List<Real3DText.Fonts.fontDesc>>();
		if (script.logo == null) script.logo =(Texture2D) Resources.Load("3DMenuCreatorLogo");
		if (script.logoGP == null) script.logoGP =(Texture2D) Resources.Load("3DMenuCreatorLogoGP");
		if (script.logoCP == null) script.logoCP =(Texture2D) Resources.Load("3DMenuCreatorLogoCP");
		if (script.logoiTween == null) script.logoiTween =(Texture2D) Resources.Load("iTweenLogo");
		
		
		script.menuObject = script.gameObject;
		script.menuObject.name = script.controlName;
		
		MenuTitlegotFocused = new bool[script.menuComponentsIndex];
			for (int i = 0; i < MenuTitlegotFocused.Length;i++)
				MenuTitlegotFocused[i] = false;
		textWriterVisual = new SerializedObject(target);
		
		events = textWriterVisual.FindProperty("events1");
		if (script.components.Length !=0){
			for (int i = 0; i < script.components.Length; i++)
			{
				Real3DFont test = script.components[i].GetComponentInChildren<Real3DFont>();
				if (test.vectorFont.kerningTable.Count == 0)
					test.vectorFont.toDictionary();
			}
		}
	} 
	
	public void OnDisable()
	{
		if (windowGlobal && windowGlobal.windowTween)
			windowGlobal.windowTween.Close();
		if (windowGlobal)
			windowGlobal.Close();
		if (windowComponent && windowComponent.windowTween)
			windowComponent.windowTween.Close();
		if (windowComponent)
			windowComponent.Close();
		
	}
	
	public bool typeSearch(TextWriterVisual.OnEventType typeToSearch, events[] eventsSearchOn, int self )
	{
		for (int i = 0; i < eventsSearchOn.Length; i++)
		{
			if (i != self)
			{
				if (eventsSearchOn[i].type == typeToSearch)
				{
					return true;
				}
			}
		}
		return false;
		
		
	}
	
	public int typeSearch(TextWriterVisual.OnEventType typeToSearch, events[] eventsSearchOn )
	{
		for (int i = 0; i < eventsSearchOn.Length; i++)
		{
				if (eventsSearchOn[i].type == typeToSearch)
				{
					return i;
				}
		}
		return -1;
		
		
	}
	
	
	public void applyGlobalParameters (GameObject[] components, Activity activity)
	{
		if (components.Length !=0)
		{
			foreach (GameObject component in components)
			{
				TextWriter textWriter = component.GetComponent<TextWriter>();
				Real3DFont tempReal3DFont = component.GetComponentInChildren<Real3DFont>();
			
				
				if (script.singlePositionSpace)
					textWriter.positionSpace = script.positionSpace;
				
				if (script.singleFontType)
					tempReal3DFont.fontNameIndex = script.fontNameIndex;
				
				if (script.singleFontStyle)
					tempReal3DFont.fontStyleIndex = script.fontStyleIndex;
				
				if (script.singleFontSize){
					tempReal3DFont.fontSize = script.fontsize;
					tempReal3DFont.depth = script.depth;
					tempReal3DFont.fontResolution = script.fontResolution;
				}
				
				if (script.singleShader)
					textWriter.letterShader = script.fontShader;
				
				if (script.singleColor)
					textWriter.colorDefault = script.fontcolor;
				
				if (script.singleLetterSpace)
					textWriter.letterSpace = script.letterSpace;
				
				if (script.singleEventsAudio)
					textWriter.eventAudio = script.eventsAudio;
				
				if (script.singleEventsFunctions)
					textWriter.eventFunction = script.eventsFunction;
				
				if (script.singleEventsFunctionsParam)
					textWriter.eventFunctionParam = script.eventsParam;
				
				if (script.singleEventsShader)
					textWriter.eventShader = script.eventsShader;
				
				if (script.singleEventsTarget)
					textWriter.eventTarget = script.eventsTarget;
				
					
				
				if (script.singleEventsUseItween)
				{
					textWriter.useiTween = script.useiTween;
					for(int i = 0; i < textWriter.events1.Length; i++)
					{
						int result = typeSearch(textWriter.events1[i].type, script.events1);
						if (result != -1)
						{
							textWriter.events1[textWriter.eventIndex[i]].tweens = new iTweens[0];
							textWriter.events1[textWriter.eventIndex[i]].tweens = script.events1[result].tweens;
							if (script.pauseMenu)
							{
								for (int x = 0; x < textWriter.events1[textWriter.eventIndex[i]].tweens.Length; x++)
								{
									bool found = false;
									for (int w = 0; w < textWriter.events1[textWriter.eventIndex[i]].tweens[x].keys.Length; w++)
									{
										if(textWriter.events1[textWriter.eventIndex[i]].tweens[x].keys[w] == "ignoretimescale")
										{
											found = true;
										}
										if (w ==textWriter.events1[textWriter.eventIndex[i]].tweens[x].keys.Length -1 && !found)
										{
											System.Array.Resize<string>(ref textWriter.events1[textWriter.eventIndex[i]].tweens[x].keys, textWriter.events1[textWriter.eventIndex[i]].tweens[x].keys.Length + 1);
											System.Array.Resize<int>(ref textWriter.events1[textWriter.eventIndex[i]].tweens[x].indexes, textWriter.events1[textWriter.eventIndex[i]].tweens[x].indexes.Length + 1);
											System.Array.Resize<bool>(ref textWriter.events1[textWriter.eventIndex[i]].tweens[x].bools, textWriter.events1[textWriter.eventIndex[i]].tweens[x].bools.Length + 1);
											textWriter.events1[textWriter.eventIndex[i]].tweens[x].keys[textWriter.events1[textWriter.eventIndex[i]].tweens[x].keys.Length-1] = "ignoretimescale";
											textWriter.events1[textWriter.eventIndex[i]].tweens[x].indexes[textWriter.events1[textWriter.eventIndex[i]].tweens[x].indexes.Length-1] = textWriter.events1[textWriter.eventIndex[i]].tweens[x].bools.Length-1;
											textWriter.events1[textWriter.eventIndex[i]].tweens[x].bools[textWriter.events1[textWriter.eventIndex[i]].tweens[x].bools.Length-1] = true;
										}
									}
								}			
							}
						}
					}
				}
				Real3DFontEditor.updateFontList(tempReal3DFont);
				if (activity == Activity.All){
					TextWriter.fontColor(textWriter.colorDefault,textWriter.gameObject);
					TextWriter.fontShader(textWriter.letterShader,textWriter.gameObject);
					Real3DFontEditor.updateFontList(ref Real3DText.Fonts.FontList.fontlist,ref script.fontsName,ref script.fontStyle);
					Real3DFontEditor.changeFont(tempReal3DFont, Real3DText.Fonts.FontList.fontlist[tempReal3DFont.fontsName[tempReal3DFont.fontNameIndex]][tempReal3DFont.fontStyleIndex]);
					Real3DFontEditor.process(tempReal3DFont);
					}
				if (activity == Activity.changeColor)
					TextWriter.fontColor(textWriter.colorDefault,textWriter.gameObject);	
				
				if (activity == Activity.ChangeFont)
					Real3DFontEditor.changeFont(tempReal3DFont, Real3DText.Fonts.FontList.fontlist[tempReal3DFont.fontsName[tempReal3DFont.fontNameIndex]][tempReal3DFont.fontStyleIndex]);
				if (activity == Activity.ChangeFontAndProcess){
					Real3DFontEditor.changeFont(tempReal3DFont, Real3DText.Fonts.FontList.fontlist[tempReal3DFont.fontsName[tempReal3DFont.fontNameIndex]][tempReal3DFont.fontStyleIndex]);
					Real3DFontEditor.process(tempReal3DFont);
				}
					
				if (activity == Activity.ChangeShader)
					TextWriter.fontShader(textWriter.letterShader,textWriter.gameObject);
				if (activity == Activity.Process)
					Real3DFontEditor.process(tempReal3DFont);
			}
		}
		switch (activity)
		{
		case Activity.All:
		case Activity.redraw:
		case Activity.ChangeFontAndProcess:
			redrawTitles(script.components);
			break;
		case Activity.none:
			break;
		}
	}
	
	public void redrawTitles(GameObject[] components)
	{
		if (components.Length != 0)
		{
			BoxCollider tempCollider0 = components[0].GetComponentInChildren<BoxCollider>();
			int index = 0;
			foreach (GameObject component in components)
			{
				
				
				TextWriter textWriter = component.GetComponent<TextWriter>();
				Real3DFont tempReal3DFont = textWriter.GetComponentInChildren<Real3DFont>();
				BoxCollider tempBox = tempReal3DFont.GetComponent<BoxCollider>();
				DestroyImmediate(tempBox);
				tempReal3DFont.gameObject.AddComponent<BoxCollider>();
				if (tempCollider0 == null)tempCollider0 = components[0].GetComponentInChildren<BoxCollider>();
				GameObject tempWordParent = textWriter.gameObject.GetComponentInChildren<MenuOptionsSelection>().gameObject;
				BoxCollider tempBoxC = tempWordParent.GetComponentInChildren<BoxCollider>();
				Vector3 size = tempBoxC.bounds.extents;
				tempWordParent.transform.localPosition = new Vector3(0,0,0);
				tempWordParent.transform.localPosition = textWriter.positionSpace * index;
				if(script.justify == TextWriterVisual.justifyType.Center)
				{
					tempWordParent.transform.Translate(new Vector3(size.x,0,0));
				}else if (script.justify == TextWriterVisual.justifyType.Rigth && index > 0)
				{
					tempWordParent.transform.Translate(new Vector3((size.x * 2) - (tempCollider0.size.x),0,0));
				}
				index++;
			}
		}
	}
	
	
	tempVector3 transformVector3(Vector3 tempV3)
	{
		tempVector3 w = new tempVector3();
		w.x= tempV3.x;
		w.y= tempV3.y;
		w.z= tempV3.z;
		return w;
	}
	Vector3 transformVector3(tempVector3 tempV3)
	{
		Vector3 w = new Vector3();
		w.x= tempV3.x;
		w.y= tempV3.y;
		w.z= tempV3.z;
		return w;
	}
	
	tempVector3[] transformVector3s(Vector3[] tempV3)
	{
		tempVector3[] w = new tempVector3[tempV3.Length];
		
		for (int i = 0; i < tempV3.Length; i++)
		{
			w[i] = new tempVector3();
			w[i].x= tempV3[i].x;
			w[i].y= tempV3[i].y;
			w[i].z= tempV3[i].z;
		}
		return w;
	}
	
	Vector3[] transformVector3s(tempVector3[] tempV3)
	{
		Vector3[] w = new Vector3[tempV3.Length] ;
		for (int i = 0; i < tempV3.Length; i++)
		{
			w[i] = new Vector3();
			w[i].x= tempV3[i].x;
			w[i].y= tempV3[i].y;
			w[i].z= tempV3[i].z;
		}
		return w;
	}
	
	tempVector4 transformVector4(Vector4 tempV4)
	{
		
		tempVector4 w = new tempVector4();
		w.x= tempV4.x;
		w.y= tempV4.y;
		w.z= tempV4.z;
		w.w= tempV4.w;
		return w;
	}
	
	Vector4 transformVector4(tempVector4 tempV4)
	{
		Vector4 w = new Vector4();
		w.x= tempV4.x;
		w.y= tempV4.y;
		w.z= tempV4.z;
		w.w= tempV4.w;
		return w;
	}
	
	tempVector4 transformColor(Color tempV4)
	{
		tempVector4 w = new tempVector4();
		w.x= tempV4.r;
		w.y= tempV4.g;
		w.z= tempV4.b;
		w.w= tempV4.a;
		return w;
	}
	
	Color transformColor(tempVector4 tempV4)
	{
		Color w = new Color();
		w.r= tempV4.x;
		w.g= tempV4.y;
		w.b= tempV4.z;
		w.a= tempV4.w;
		return w;
	}
	
	tempVector4[] transformColors(Color[] tempV4)
	{
		tempVector4[] w = new tempVector4[tempV4.Length];
		for (int i = 0; i < tempV4.Length; i++)
		{
			w[i] = new tempVector4();
			w[i].x= tempV4[i].r;
			w[i].y= tempV4[i].g;
			w[i].z= tempV4[i].b;
			w[i].w= tempV4[i].a;
		}
		return w;
	}
	
	Color[] transformColors(tempVector4[] tempV4)
	{
		Color[] w = new Color[tempV4.Length];
		for (int i = 0; i < tempV4.Length; i++)
		{
			w[i] = new Color();
			w[i].r= tempV4[i].x;
			w[i].g= tempV4[i].y;
			w[i].b= tempV4[i].z;
			w[i].a= tempV4[i].w;
		}
		return w;
	}
	
	string[] objectoToStringArray(object[] convert)
	{
		string[] temp = new string[convert.Length];
		for (int i = 0; i< convert.Length; i++)
		{
			if (convert.GetType() == typeof(GameObject))
				temp[i] = ((GameObject[])convert)[i].name;
		
			if (convert.GetType() == typeof(Transform))
				temp[i]  = ((Transform[])convert)[i].name;
		
			if (convert.GetType() == typeof(Shader))
				temp[i]  = ((Shader[])convert)[i].name;
		}
		return temp;
	}
	
	object[] objectoToStringArray(string[] convert, object type)
	{
		object[] temp = new object[convert.Length];
		for (int i = 0; i< convert.Length; i++)
		{
			if (type.GetType() == typeof(GameObject))
				temp[i] =(GameObject)GameObject.Find(convert[i]);
		
			if (type.GetType() == typeof(Transform))
				temp[i]  = (Transform)GameObject.Find(convert[i]).transform;
		
			if (type.GetType() == typeof(Shader))
				temp[i]  = (Shader)Shader.Find(convert[i]);
		}
		return temp;
	}
	
	Shader[] StringArrayToShaderArray(string[] convert)
	{
		Shader[] temp = new Shader[convert.Length];
		for (int i = 0; i< convert.Length; i++)
		{
				temp[i]  = (Shader)Shader.Find(convert[i]);
		}
		return temp;
	}
	
	void saveSettings()
	{
		LoadNSave tempSave = new LoadNSave();
		tempSave.events = new Hashtable();
	
		if(!Directory.Exists(Application.dataPath + "/3D Menu Creator/Templates")) 
				Directory.CreateDirectory(Application.dataPath + "/3D Menu Creator/Templates");
			
		SaveLoad.currentFilePath = EditorUtility.SaveFilePanel("Template save",Application.dataPath + "/3D Menu Creator/Templates","untitled","template");
		
		tempSave.events.Add("Global eventIndex",script.eventIndex);
		
		tempSave.events.Add("Global events Length",script.events1.Length);
		
		for (int x = 0; x < script.events1.Length; x++)
		{
			tempSave.events.Add("Global events1 type"+x,script.events1[x].type);
			tempSave.events.Add("Global events1"+x+"tween",script.events1[x].tweens.Length);
			for (int y = 0; y < script.events1[x].tweens.Length; y++)
			{
				tempSave.events.Add("Global events1"+x+"tweens"+y+"bools",script.events1[x].tweens[y].bools);
				tempSave.events.Add("Global events1"+x+"tweens"+y+"colors",transformColors(script.events1[x].tweens[y].colors));
				tempSave.events.Add("Global events1"+x+"tweens"+y+"delay",script.events1[x].tweens[y].delay);
				tempSave.events.Add("Global events1"+x+"tweens"+y+"easeTypes",script.events1[x].tweens[y].easeTypes);
				tempSave.events.Add("Global events1"+x+"tweens"+y+"floats",script.events1[x].tweens[y].floats);
				tempSave.events.Add("Global events1"+x+"tweens"+y+"indexes",script.events1[x].tweens[y].indexes);
				tempSave.events.Add("Global events1"+x+"tweens"+y+"ints",script.events1[x].tweens[y].ints);
				tempSave.events.Add("Global events1"+x+"tweens"+y+"keys",script.events1[x].tweens[y].keys);
				tempSave.events.Add("Global events1"+x+"tweens"+y+"loopTypes",script.events1[x].tweens[y].loopTypes);
				tempSave.events.Add("Global events1"+x+"tweens"+y+"metadatas",script.events1[x].tweens[y].metadatas);
				tempSave.events.Add("Global events1"+x+"tweens"+y+"spaces",script.events1[x].tweens[y].spaces);
				tempSave.events.Add("Global events1"+x+"tweens"+y+"strings",script.events1[x].tweens[y].strings);
				tempSave.events.Add("Global events1"+x+"tweens"+y+"type",script.events1[x].tweens[y].type);
				tempSave.events.Add("Global events1"+x+"tweens"+y+"vector3Arrays",script.events1[x].tweens[y].vector3Arrays);
				tempSave.events.Add("Global events1"+x+"tweens"+y+"vector3s",transformVector3s(script.events1[x].tweens[y].vector3s));
			}
		}
		
		
		tempSave.events.Add("Global menuComponentsTitles",script.menuComponentsTitles);
		tempSave.events.Add("Global positionSpace",transformVector3(script.positionSpace));
		tempSave.events.Add("Global letterSpace",transformVector3(script.letterSpace));
		tempSave.events.Add("Global fontsize",script.fontsize);
		tempSave.events.Add("Global fontShader",script.fontShader.name);
		tempSave.events.Add("Global fontcolor",transformColor( script.fontcolor));
		tempSave.events.Add("Global selectedValue",script.selectedValue);
		tempSave.events.Add("Global eventEnabled",script.eventEnabled);
		tempSave.events.Add("Global singlePositionSpace",script.singlePositionSpace);
		tempSave.events.Add("Global singleFontSize",script.singleFontSize);
		tempSave.events.Add("Global singleLetterSpace",script.singleLetterSpace);
		tempSave.events.Add("Global singleColor",script.singleColor);
		tempSave.events.Add("Global singleShader",script.singleShader);
		tempSave.events.Add("Global singleEventsShader",script.singleEventsShader);
		tempSave.events.Add("Global singleEventsAudio",script.singleEventsAudio);
		tempSave.events.Add("Global singleEventsTarget",script.singleEventsTarget);
		tempSave.events.Add("Global singleEventsFunctions",script.singleEventsFunctions);
		tempSave.events.Add("Global singleEventsFunctionsParam",script.singleEventsFunctionsParam);
		tempSave.events.Add("Global singleEventsUseItween",script.singleEventsUseItween);
		tempSave.events.Add("Global justify",script.justify);
		tempSave.events.Add("Global eventsShader",objectoToStringArray(script.eventsShader));
		tempSave.events.Add("Global useiTween",script.useiTween);
		tempSave.events.Add("Global pauseMenu",script.pauseMenu);
		tempSave.events.Add("Global distance",transformVector3(script.distance));
		
		
		for (int i = 0; i < script.components.Length; i++)
		{
			TextWriter componentSettings = script.components[i].GetComponent<TextWriter>();
			
			tempSave.events.Add("Component"+i+"_eventIndex",componentSettings.eventIndex);
			tempSave.events.Add("Component"+i+"_events1",componentSettings.events1.Length);
			
			for (int x = 0; x < script.events1.Length; x++)
			{
				tempSave.events.Add("Component"+i+"_events1 type"+x,componentSettings.events1[x].type);
				
				tempSave.events.Add("Component"+i+"_events1"+x+"tween", componentSettings.events1[x].tweens.Length);
				
				for (int y = 0; y < componentSettings.events1[x].tweens.Length; y++)
				{
					tempSave.events.Add("Component"+i+"_events1"+x+"tweens"+y+"bools",componentSettings.events1[x].tweens[y].bools);
					tempSave.events.Add("Component"+i+"_events1"+x+"tweens"+y+"colors",transformColors(componentSettings.events1[x].tweens[y].colors));
					tempSave.events.Add("Component"+i+"_events1"+x+"tweens"+y+"delay",componentSettings.events1[x].tweens[y].delay);
					tempSave.events.Add("Component"+i+"_events1"+x+"tweens"+y+"easeTypes",componentSettings.events1[x].tweens[y].easeTypes);
					tempSave.events.Add("Component"+i+"_events1"+x+"tweens"+y+"floats",componentSettings.events1[x].tweens[y].floats);
					tempSave.events.Add("Component"+i+"_events1"+x+"tweens"+y+"indexes",componentSettings.events1[x].tweens[y].indexes);
					tempSave.events.Add("Component"+i+"_events1"+x+"tweens"+y+"ints",componentSettings.events1[x].tweens[y].ints);
					tempSave.events.Add("Component"+i+"_events1"+x+"tweens"+y+"keys",componentSettings.events1[x].tweens[y].keys);
					tempSave.events.Add("Component"+i+"_events1"+x+"tweens"+y+"loopTypes",componentSettings.events1[x].tweens[y].loopTypes);
					tempSave.events.Add("Component"+i+"_events1"+x+"tweens"+y+"metadatas",componentSettings.events1[x].tweens[y].metadatas);
					tempSave.events.Add("Component"+i+"_events1"+x+"tweens"+y+"spaces",componentSettings.events1[x].tweens[y].spaces);
					tempSave.events.Add("Component"+i+"_events1"+x+"tweens"+y+"strings",componentSettings.events1[x].tweens[y].strings);
					tempSave.events.Add("Component"+i+"_events1"+x+"tweens"+y+"type",componentSettings.events1[x].tweens[y].type);
					tempSave.events.Add("Component"+i+"_events1"+x+"tweens"+y+"vector3Arrays",componentSettings.events1[x].tweens[y].vector3Arrays);
					tempSave.events.Add("Component"+i+"_events1"+x+"tweens"+y+"vector3s",transformVector3s(componentSettings.events1[x].tweens[y].vector3s));
				}
			}
			
			tempSave.events.Add("Component"+i+"_positionSpace",transformVector3(componentSettings.positionSpace));
			tempSave.events.Add("Component"+i+"_letterSpace",transformVector3(componentSettings.letterSpace));
			tempSave.events.Add("Component"+i+"_fontsize",componentSettings.fontSize);
			tempSave.events.Add("Component"+i+"_selectedValue",componentSettings.selectedValue);
			tempSave.events.Add("Component"+i+"_eventEnabled",componentSettings.eventEnabled);
			tempSave.events.Add("Component"+i+"_eventShader",objectoToStringArray( componentSettings.eventShader));
			tempSave.events.Add("Component"+i+"_useiTween",componentSettings.useiTween);
			tempSave.events.Add("Component"+i+"_positionOffset",transformVector3(componentSettings.positionOffset));
			tempSave.events.Add("Component"+i+"_wordCharactersCount",componentSettings.wordCharactersCount);
			tempSave.events.Add("Component"+i+"_words",componentSettings.words);
			
			tempVector3[] r = new tempVector3[componentSettings.size.Length];
			
			for (int o =0; o < r.Length;o++)
			{
				r[o] = transformVector3(componentSettings.size[o]);
			}
			
			tempSave.events.Add("Component"+i+"_size",r);
			
			
			tempSave.events.Add("Component"+i+"_initialOffsetDone",componentSettings.initialOffsetDone);
			tempSave.events.Add("Component"+i+"_textDisplayed",componentSettings.textDisplayed);
			tempSave.events.Add("Component"+i+"_colorDefault",transformColor( componentSettings.colorDefault));
			tempSave.events.Add("Component"+i+"_colorDefaultHighLight",transformColor( componentSettings.colorDefaultHighLight));
			tempSave.events.Add("Component"+i+"_displayCursor",componentSettings.displayCursor);
			tempSave.events.Add("Component"+i+"_inputControl",componentSettings.inputControl);
			tempSave.events.Add("Component"+i+"_letterShader",componentSettings.letterShader.ToString());
			tempSave.events.Add("Component"+i+"_initializeOnAwake",componentSettings.initializeOnAwake);
		}
		
		SaveLoad.Save(tempSave);
		tempSave.events.Clear();
		
	}
	
	void loadSettings()
	{
		LoadNSave tempLoad = new LoadNSave();
		tempLoad.events = new Hashtable();
	
		if(!Directory.Exists(Application.dataPath + "/3D Menu Creator/Templates")) 
				Directory.CreateDirectory(Application.dataPath + "/3D Menu Creator/Templates");
			
		SaveLoad.currentFilePath = EditorUtility.OpenFilePanel("Template load",Application.dataPath + "/3D Menu Creator/Templates","template");
		
		if (System.IO.File.Exists(SaveLoad.currentFilePath))
			tempLoad = SaveLoad.Load(SaveLoad.currentFilePath);
		else {
			EditorUtility.DisplayDialog("3D Menu Creator Error!","The file does not exits!", "Ok");
			tempLoad.events.Clear();
			return;
		}
		
		if (tempLoad.events.ContainsKey("Global eventIndex"))
		{
			TextWriter[] textWriters = script.GetComponentsInChildren<TextWriter>();
			foreach (TextWriter t in textWriters)
				DestroyImmediate(t.gameObject);
			
			script.components = new GameObject[0];
			script.menuComponentsIndex = 0;
			script.menuComponentsTitles = new string[0];
			
			script.menuComponentsTitles = (string[])tempLoad.events["Global menuComponentsTitles"];
			
			string[] tempTitles = script.menuComponentsTitles;
			for (int q = 0; q < tempTitles.Length; q++)
			{
				
				addTitle(tempTitles[q]);
				redrawTitles(script.components);
//				TextWriter textWriter = script.components[q].GetComponent<TextWriter>();
//				int lastLength = script.menuComponentsTitles[q].Length;
//				script.menuComponentsTitles[q] = "";	
//				updateTitle(q, lastLength,textWriter);
//				script.menuComponentsTitles[q] = tempTitles[q];
//				updateTitle(q, 0,textWriter);
			}
//			applyGlobalParameters(script.components);
			
			
			script.eventIndex = (int[])tempLoad.events["Global eventIndex"];
			SerializedObject tempTextWriterVisual = new SerializedObject(script);
			SerializedProperty tempEvents = tempTextWriterVisual.FindProperty("events1");
		
			tempEvents.serializedObject.Update();
			tempEvents.arraySize = (int)tempLoad.events["Global events Length"];
			tempEvents.serializedObject.ApplyModifiedProperties();
			
			for (int x = 0; x < (int)tempLoad.events["Global events Length"]; x++)
			{
				script.events1[x].type =  (TextWriterVisual.OnEventType)tempLoad.events["Global events1 type"+x];
				SerializedProperty tempEvents1 = tempEvents.GetArrayElementAtIndex(x);
				SerializedProperty tempTweens =  tempEvents1.FindPropertyRelative("tweens");
				tempTweens.arraySize = (int)tempLoad.events["Global events1"+x+"tween"];
				tempEvents.serializedObject.ApplyModifiedProperties();
				for (int y = 0; y < (int)tempLoad.events["Global events1"+x+"tween"]; y++)
				{
					script.events1[x].tweens[y].bools = (bool[])tempLoad.events["Global events1"+x+"tweens"+y+"bools"];
					script.events1[x].tweens[y].colors = (Color[])transformColors((tempVector4[])tempLoad.events["Global events1"+x+"tweens"+y+"colors"]);
					script.events1[x].tweens[y].delay = (float)tempLoad.events["Global events1"+x+"tweens"+y+"delay"];
					script.events1[x].tweens[y].easeTypes = (iTween.EaseType[])tempLoad.events["Global events1"+x+"tweens"+y+"easeTypes"];
					script.events1[x].tweens[y].floats = (float[])tempLoad.events["Global events1"+x+"tweens"+y+"floats"];
					script.events1[x].tweens[y].indexes = (int[])tempLoad.events["Global events1"+x+"tweens"+y+"indexes"];
					script.events1[x].tweens[y].ints = (int[])tempLoad.events["Global events1"+x+"tweens"+y+"ints"];
					script.events1[x].tweens[y].keys = (string[])tempLoad.events["Global events1"+x+"tweens"+y+"keys"];
					script.events1[x].tweens[y].loopTypes = (iTween.LoopType[])tempLoad.events["Global events1"+x+"tweens"+y+"loopTypes"];
					script.events1[x].tweens[y].metadatas = (string[])tempLoad.events["Global events1"+x+"tweens"+y+"metadatas"];
					script.events1[x].tweens[y].spaces = (Space[])tempLoad.events["Global events1"+x+"tweens"+y+"spaces"];
					script.events1[x].tweens[y].strings = (string[])tempLoad.events["Global events1"+x+"tweens"+y+"strings"];
					script.events1[x].tweens[y].type = (TextWriterVisual.TweenType)tempLoad.events["Global events1"+x+"tweens"+y+"type"];
					script.events1[x].tweens[y].vector3Arrays = (ArraysIndex[])tempLoad.events["Global events1"+x+"tweens"+y+"vector3Arrays"];
					script.events1[x].tweens[y].vector3s = transformVector3s((tempVector3[])tempLoad.events["Global events1"+x+"tweens"+y+"vector3s"]);
				}
			}
			
			
			script.positionSpace = transformVector3((tempVector3)tempLoad.events["Global positionSpace"]);
			script.letterSpace = transformVector3((tempVector3)tempLoad.events["Global letterSpace"]);
			script.fontsize = (int)tempLoad.events["Global fontsize"];
			script.fontShader = Shader.Find((string)tempLoad.events["Global fontShader"]);
			script.fontcolor = transformColor((tempVector4)tempLoad.events["Global fontcolor"]);
			script.selectedValue = (int[])tempLoad.events["Global selectedValue"];
			script.eventEnabled = (bool[])tempLoad.events["Global eventEnabled"];
			script.singlePositionSpace = (bool)tempLoad.events["Global singlePositionSpace"];
			script.singleFontSize = (bool)tempLoad.events["Global singleFontSize"];
			script.singleLetterSpace = (bool)tempLoad.events["Global singleLetterSpace"];
			script.singleColor = (bool)tempLoad.events["Global singleColor"];
			script.singleShader = (bool)tempLoad.events["Global singleShader"];
			script.singleEventsShader = (bool)tempLoad.events["Global singleEventsShader"];
			script.singleEventsAudio = (bool)tempLoad.events["Global singleEventsAudio"];
			script.singleEventsTarget = (bool)tempLoad.events["Global singleEventsTarget"];
			script.singleEventsFunctions = (bool)tempLoad.events["Global singleEventsFunctions"];
			script.singleEventsFunctionsParam = (bool)tempLoad.events["Global singleEventsFunctionsParam"];
			script.singleEventsUseItween = (bool)tempLoad.events["Global singleEventsUseItween"];
			script.justify = (TextWriterVisual.justifyType)tempLoad.events["Global justify"];
			script.eventsShader= (Shader[])StringArrayToShaderArray((string[])tempLoad.events["Global eventsShader"]);
			script.useiTween = (bool[])tempLoad.events["Global useiTween"];
			script.pauseMenu = (bool)tempLoad.events["Global pauseMenu"];
			script.distance = transformVector3((tempVector3)tempLoad.events["Global distance"]);
			
			
			for (int i = 0; i < script.components.Length; i++)
			{
				TextWriter componentSettings = script.components[i].GetComponent<TextWriter>();
				
				componentSettings.eventIndex = (int[])tempLoad.events["Component"+i+"_eventIndex"];
				SerializedObject tempTextWriter = new SerializedObject(componentSettings);
				SerializedProperty writerEvents = tempTextWriter.FindProperty("events1");
				writerEvents.serializedObject.Update();
				writerEvents.arraySize = (int)tempLoad.events["Component"+i+"_events1"];
				writerEvents.serializedObject.ApplyModifiedProperties();
				
				
				
				for (int x = 0; x < (int)tempLoad.events["Component"+i+"_events1"]; x++)
				{
					componentSettings.events1[x].type = (TextWriterVisual.OnEventType)tempLoad.events["Component"+i+"_events1 type"+x];
					
					SerializedProperty tempEvents1 = writerEvents.GetArrayElementAtIndex(x);
					SerializedProperty tempTweens =  tempEvents1.FindPropertyRelative("tweens");
					tempTweens.arraySize = (int)tempLoad.events["Component"+i+"_events1"+x+"tween"];
					writerEvents.serializedObject.ApplyModifiedProperties();
					
					for (int y = 0; y < (int)tempLoad.events["Component"+i+"_events1"+x+"tween"]; y++)
					{
						componentSettings.events1[x].tweens[y].bools = (bool[])tempLoad.events["Component"+i+"_events1"+x+"tweens"+y+"bools"];
						componentSettings.events1[x].tweens[y].colors = transformColors((tempVector4[])tempLoad.events["Component"+i+"_events1"+x+"tweens"+y+"colors"]);
						componentSettings.events1[x].tweens[y].delay = (float)tempLoad.events["Component"+i+"_events1"+x+"tweens"+y+"delay"];
						componentSettings.events1[x].tweens[y].easeTypes = (iTween.EaseType[])tempLoad.events["Component"+i+"_events1"+x+"tweens"+y+"easeTypes"];
						componentSettings.events1[x].tweens[y].floats = (float[])tempLoad.events["Component"+i+"_events1"+x+"tweens"+y+"floats"];
						componentSettings.events1[x].tweens[y].indexes = (int[])tempLoad.events["Component"+i+"_events1"+x+"tweens"+y+"indexes"];
						componentSettings.events1[x].tweens[y].ints = (int[])tempLoad.events["Component"+i+"_events1"+x+"tweens"+y+"ints"];
						componentSettings.events1[x].tweens[y].keys = (string[])tempLoad.events["Component"+i+"_events1"+x+"tweens"+y+"keys"];
						componentSettings.events1[x].tweens[y].loopTypes = (iTween.LoopType[])tempLoad.events["Component"+i+"_events1"+x+"tweens"+y+"loopTypes"];
						componentSettings.events1[x].tweens[y].metadatas = (string[])tempLoad.events["Component"+i+"_events1"+x+"tweens"+y+"metadatas"];
						componentSettings.events1[x].tweens[y].spaces = (Space[])tempLoad.events["Component"+i+"_events1"+x+"tweens"+y+"spaces"];
						componentSettings.events1[x].tweens[y].strings = (string[])tempLoad.events["Component"+i+"_events1"+x+"tweens"+y+"strings"];
						componentSettings.events1[x].tweens[y].type = (TextWriterVisual.TweenType)tempLoad.events["Component"+i+"_events1"+x+"tweens"+y+"type"];
						componentSettings.events1[x].tweens[y].vector3Arrays = (ArraysIndex[])tempLoad.events["Component"+i+"_events1"+x+"tweens"+y+"vector3Arrays"];
						componentSettings.events1[x].tweens[y].vector3s = transformVector3s((tempVector3[])tempLoad.events["Component"+i+"_events1"+x+"tweens"+y+"vector3s"]);
					}
				}
				
				
				
				componentSettings.positionSpace = transformVector3((tempVector3)tempLoad.events["Component"+i+"_positionSpace"]);
				componentSettings.letterSpace = (Vector3)transformVector3((tempVector3)tempLoad.events["Component"+i+"_letterSpace"]);
				componentSettings.fontSize = (float)tempLoad.events["Component"+i+"_fontsize"];
				componentSettings.selectedValue = (int[])tempLoad.events["Component"+i+"_selectedValue"];
				componentSettings.eventEnabled = (bool[])tempLoad.events["Component"+i+"_eventEnabled"];
				componentSettings.eventShader = (Shader[])StringArrayToShaderArray((string[])tempLoad.events["Component"+i+"_eventShader"]);
				componentSettings.useiTween = (bool[])tempLoad.events["Component"+i+"_useiTween"];
				componentSettings.positionOffset = transformVector3((tempVector3)tempLoad.events["Component"+i+"_positionOffset"]);
				componentSettings.colorDefault = transformColor((tempVector4)tempLoad.events["Component"+i+"_colorDefault"]);
				componentSettings.colorDefaultHighLight = transformColor((tempVector4)tempLoad.events["Component"+i+"_colorDefaultHighLight"]);
				componentSettings.displayCursor = (bool)tempLoad.events["Component"+i+"_displayCursor"];
				componentSettings.inputControl = (bool)tempLoad.events["Component"+i+"_inputControl"];
				componentSettings.letterShader = Shader.Find((string)tempLoad.events["Component"+i+"_letterShader"]);
				componentSettings.initializeOnAwake = (bool)tempLoad.events["Component"+i+"_initializeOnAwake"];
			
				applyGlobalParameters(script.components, Activity.All);	
				
			}
			
		} else EditorUtility.DisplayDialog("3D Menu Creator Error!","The file you are trying to load it's not a 3D Menu Creator Template", "Ok");
		tempLoad.events.Clear();	
	}
	
}



