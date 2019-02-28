
using System;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class TextWriterComponentWindow : EditorWindow {
	
	public SerializedObject textWriter;
	public SerializedProperty events, type;

	Vector2 scrollPos;
	TextWriter script;
	Real3DFont script1;
	TextWriterMenuEditor tempEditor;
	TextWriterVisual.TweenType[,] previousType = new TextWriterVisual.TweenType[8,30];
	public TextWriterTweenWindow windowTween;
	
	TextWriterVisual.OnEventType onEventsTypes;
	
	int index;
	
	
	public void Init(TextWriter thisObject, TextWriterMenuEditor values, int i)
    {
		index = i;
        script = thisObject;
		tempEditor = values;
		
		textWriter = new SerializedObject(thisObject);
		events = textWriter.FindProperty("events1");
		type = events.FindPropertyRelative("type");
		previousType = new TextWriterVisual.TweenType[8,30];
		this.title = "Menu Title # "+ index + " Paramters";
		script1 = thisObject.gameObject.GetComponentInChildren<Real3DFont>();
		Real3DFontEditor.updateFontList(script1);
    }
	
	void OnGUI()
	{
//		textWriter.Update();
		if (Application.isPlaying && !tempEditor.script.cleanOnceComponent)
		{
			GUILayout.Space(100);
			GUILayout.BeginHorizontal();
			GUILayout.Box("3D Menu Creator\nComponent Parameters\nPlease re-open this window\nto refresh values",GUILayout.Width(255));
			GUILayout.EndHorizontal();
			return;
		} else if (!Application.isPlaying && tempEditor.script.cleanOnceComponent)
		{
			GUILayout.Space(100);
			GUILayout.BeginHorizontal();
			GUILayout.Box("3D Menu Creator\nComponent Parameters\nPlease re-open this window\nto refresh values",GUILayout.Width(255));
			GUILayout.EndHorizontal();
			return;
		}
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width), GUILayout.Height(position.height));
		
		 GUI.changed = false;
		
		
		GUILayout.BeginHorizontal();
		GUILayout.Space((position.width/2 - 130));
		GUILayout.Label(tempEditor.script.logoCP,GUILayout.Width(255));
		GUILayout.EndHorizontal();
		
		GUILayout.Space(10);
		GUILayout.BeginHorizontal();
		GUILayout.Label("3D Menu Creator\nMenu Title # "+ index + " Paramters",GUILayout.Width(255));
		GUILayout.EndHorizontal();
		
		GUI.changed = false;
		if (!tempEditor.script.singlePositionSpace)
		{
			GUILayout.Space(10);
		
			GUILayout.BeginHorizontal();
			GUILayout.TextArea("                 Position Space");
			GUILayout.EndHorizontal();
			
			GUILayout.Space(5);
			
			GUILayout.BeginHorizontal();
			script.positionSpace = EditorGUILayout.Vector3Field("Space between Menu Titles",script.positionSpace, GUILayout.Width(255));
			GUILayout.EndHorizontal();
			if (GUI.changed && tempEditor.script.menuComponentsIndex > 0)
					tempEditor.applyGlobalParameters(tempEditor.script.components, TextWriterMenuEditor.Activity.redraw);
		}
		GUI.changed = false;
		if (!tempEditor.script.singleFontType)
		{
		
			GUILayout.Space(15);
			GUILayout.BeginHorizontal(); 
			
			script1.fontNameIndex = EditorGUILayout.Popup("Font",script1.fontNameIndex,script1.fontsName,GUILayout.Width(245));
			GUILayout.EndHorizontal();
		}
		if (!tempEditor.script.singleFontStyle)
		{
			GUILayout.Space(15);
			GUILayout.BeginHorizontal();
			string[] array = Real3DFontEditor.styleArray(script1.fontStyle,script1.fontNameIndex);	
			script1.fontStyleIndex =EditorGUILayout.Popup("Style",script1.fontStyleIndex >= script1.fontStyle[script1.fontNameIndex].Length?0:script1.fontStyleIndex, array,GUILayout.Width(245));
			GUILayout.EndHorizontal();
		}
		
		if (!tempEditor.script.singleFontSize)
		{
			GUILayout.Space(10);
			
			GUILayout.BeginHorizontal();
			GUILayout.TextArea("                 Font size");
			GUILayout.EndHorizontal();
			
			GUILayout.Space(5);
			
			GUILayout.BeginHorizontal();
			script1.fontSize = EditorGUILayout.IntField("Size",script1.fontSize, GUILayout.Width(200));
			GUILayout.EndHorizontal();
			
			GUILayout.Space(5);
			
			GUILayout.BeginHorizontal();
			script1.fontResolution =EditorGUILayout.IntField("Resolution",script1.fontResolution<3?3:script1.fontResolution,GUILayout.Width(245));
			script1.fontResolution =script1.fontResolution < 3?3:script1.fontResolution;
			GUILayout.EndHorizontal();
			
			GUILayout.Space(5);
			
			GUILayout.BeginHorizontal();
			script1.depth =EditorGUILayout.FloatField("Depth",script1.depth,GUILayout.Width(245));
			GUILayout.EndHorizontal();
		}
		if (GUI.changed && tempEditor.script.menuComponentsIndex > 0)
					tempEditor.applyGlobalParameters(tempEditor.script.components, TextWriterMenuEditor.Activity.ChangeFontAndProcess);
		
		
//		if (!tempEditor.script.singleLetterSpace)
//		{
//			GUILayout.Space(10);
//			
//			GUILayout.BeginHorizontal();
//			GUILayout.TextArea("                 Font letter space");
//			GUILayout.EndHorizontal();
//			
//			GUILayout.Space(5);
//			
//			GUILayout.BeginHorizontal();
//			script.letterSpace = EditorGUILayout.Vector3Field("Space between letters",script.letterSpace, GUILayout.Width(255));
//			GUILayout.EndHorizontal();
//		}
		
//		if (GUI.changed) {tempEditor.applyGlobalParameters(tempEditor.script.components); tempEditor.redrawTitles(tempEditor.script.components);  GUI.changed = false;}
		GUI.changed = false;
		if (!tempEditor.script.singleColor)
		{
			GUILayout.Space(10);
			
			GUILayout.BeginHorizontal();
			GUILayout.TextArea("                 Font Color");
			GUILayout.EndHorizontal();
			
			GUILayout.Space(5);
			
			GUILayout.BeginHorizontal();
			GUILayout.Label("Font Color");
			script.colorDefault = EditorGUILayout.ColorField(script.colorDefault, GUILayout.Width(100));
			GUILayout.EndHorizontal();
			if (GUI.changed && tempEditor.script.menuComponentsIndex > 0)
				tempEditor.applyGlobalParameters(tempEditor.script.components, TextWriterMenuEditor.Activity.changeColor);			
		}
		GUI.changed = false;		
		
		if (!tempEditor.script.singleShader)
		{
			GUILayout.Space(10);
			
			GUILayout.BeginHorizontal();
			GUILayout.TextArea("                 Font Shader");
			GUILayout.EndHorizontal();
			
			GUILayout.Space(5);
			
			GUILayout.BeginHorizontal();
			GUILayout.Label("Font Shader (By selection)");
			script.letterShader =(Shader) EditorGUILayout.ObjectField(script.letterShader, typeof (Shader),false, GUILayout.Width(100));
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Label("Font Shader (By name)");
			script.letterShader = Shader.Find(EditorGUILayout.TextField((script.letterShader!=null?script.letterShader.name:""), GUILayout.Width(100)));
			GUILayout.EndHorizontal();
				if (GUI.changed && tempEditor.script.menuComponentsIndex > 0)
					tempEditor.applyGlobalParameters(tempEditor.script.components, TextWriterMenuEditor.Activity.ChangeShader);
		}
		GUI.changed = false;
		GUILayout.Space(20);
		SerializedProperty tempEvents = events;
		tempEvents.serializedObject.Update();
		
		EditorGUILayout.BeginHorizontal();
		
		onEventsTypes = (TextWriterVisual.OnEventType)EditorGUILayout.EnumPopup( onEventsTypes,GUILayout.Width(150));
		
		EditorGUILayout.LabelField("Events (+/-)");
		
		if (GUILayout.Button("+",EditorStyles.miniButtonLeft,GUILayout.Width(25)))
		{
			if (tempEditor.typeSearch(onEventsTypes, script.events1, 10))
			{
			 	EditorUtility.DisplayDialog("Warning!","The event " + onEventsTypes +" Already exits!, please choose another event", "Ok");
			}else
			{
				tempEvents.arraySize++;
				tempEvents.serializedObject.ApplyModifiedProperties();
				script.events1[tempEvents.arraySize-1].type = onEventsTypes;
				
				switch(script.events1[tempEvents.arraySize-1].type.ToString())
				{
				case "OnMouseEnter":
					script.eventIndex[0] = tempEvents.arraySize-1;
					break;
				
				case "OnMouseExit":
					script.eventIndex[1] = tempEvents.arraySize-1;
					break;
				
				case "OnMouseDown":
					script.eventIndex[2] = tempEvents.arraySize-1;
					break;
				
				case "OnMouseOver":
					script.eventIndex[3] = tempEvents.arraySize-1;
					break;
				
				case "CollisionEnter":
					script.eventIndex[4] = tempEvents.arraySize-1;
					break;
				
				case "CollisionExit":
					script.eventIndex[5] = tempEvents.arraySize-1;
					break;
				
				case "BecameVisible":
					script.eventIndex[6] = tempEvents.arraySize-1;
					break;
				
				case "BecameInvisible":
					script.eventIndex[7] = tempEvents.arraySize-1;
					break;
				}
			}
		}
		
		if (GUILayout.Button("-",EditorStyles.miniButtonRight,GUILayout.Width(25)))
		{
			if (tempEvents.arraySize == 1)
			{
				tempEvents.ClearArray();
				tempEvents.serializedObject.ApplyModifiedProperties();
			}
			else {
				tempEvents.DeleteCommand();
				tempEvents.arraySize--;
				tempEvents.serializedObject.ApplyModifiedProperties();
			}
		}
		EditorGUILayout.EndHorizontal();
		
		tempEvents.arraySize = Mathf.Clamp(tempEvents.arraySize,0, tempEditor.script.events.Length);
		
		
		for (int i = 0; i < script.events1.Length; i++)
		{
			
			
			GUILayout.Space(20);
			GUILayout.BeginHorizontal();
			
			EditorGUILayout.TextArea("Parameters "+script.events1[i].type +" Event");
			GUILayout.EndHorizontal();
			
			GUILayout.Space(8);
			
			
			
			if (!tempEditor.script.singleEventsShader)
			{
				GUILayout.Space(5);
			
				GUILayout.BeginHorizontal();
				GUILayout.Label("Font Shader (By selection)");
				script.eventShader[i] = EditorGUILayout.ObjectField(script.eventShader[i],typeof (Shader),false, GUILayout.Width(100)) as Shader;
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				GUILayout.Label("Font Shader (By name)");
				script.eventShader[i] = Shader.Find(EditorGUILayout.TextField((script.eventShader[i]!=null?script.eventShader[i].name:""), GUILayout.Width(100)));
				GUILayout.EndHorizontal();
			
				GUILayout.Space(5);
			}
			
			
			
			
			if (!tempEditor.script.singleEventsAudio)
			{
				GUILayout.Space(5);
				
				GUILayout.BeginHorizontal();
				GUILayout.Label("Audio");
				script.eventAudio[i] = EditorGUILayout.ObjectField(script.eventAudio[i],typeof (AudioClip), false , GUILayout.Width(100)) as AudioClip;
				GUILayout.EndHorizontal();
				
				GUILayout.Space(5);
			}
			
			if (!tempEditor.script.singleEventsTarget)
			{
				GUILayout.Space(5);
				
				GUILayout.BeginHorizontal();
				GUILayout.Label("Target");
				script.eventTarget[i] = EditorGUILayout.ObjectField(script.eventTarget[i],typeof (GameObject),true, GUILayout.Width(100)) as GameObject;
				GUILayout.EndHorizontal();
				
				GUILayout.Space(5);
			}
			
			if (!tempEditor.script.singleEventsFunctions)
			{
				GUILayout.Space(5);
				
				GUILayout.BeginHorizontal();
				GUILayout.Label("Target Function");
				script.eventFunction[i] = EditorGUILayout.TextField(script.eventFunction[i], GUILayout.Width(150));
				GUILayout.EndHorizontal();
				
				GUILayout.Space(5);
			}
			
			if (!tempEditor.script.singleEventsFunctionsParam)
			{
				GUILayout.Space(5);
				
				GUILayout.BeginHorizontal();
				GUILayout.Label("Function Argument");
					script.eventFunctionParam[i] = EditorGUILayout.TextField((script.eventFunctionParam[i] as string)!=null?(script.eventFunctionParam[i] as string):"", GUILayout.Width(100));
					GUILayout.EndHorizontal();
				GUILayout.Space(5);
			}
			
			if (!tempEditor.script.singleEventsUseItween)
			{
					textWriter.Update();
				SerializedProperty tempIndex = events.GetArrayElementAtIndex(i);
				SerializedProperty tempTweens = tempIndex.FindPropertyRelative("tweens");
				GUILayout.Space(5);
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("iTween Effect");
				
				if (GUILayout.Button("+",EditorStyles.miniButtonLeft,GUILayout.Width(25)))
				{
					tempTweens.arraySize++;
					GUI.changed = true;
				}
				
				if (GUILayout.Button("-",EditorStyles.miniButtonRight,GUILayout.Width(25)))
				{
					if (tempTweens.arraySize == 1)
					{
						tempTweens.ClearArray();
					}
					else {
							tempTweens.DeleteCommand();
						tempTweens.arraySize--;
					}
					GUI.changed = true;
				}
				
				tempTweens.arraySize = Mathf.Clamp(tempTweens.arraySize,0,30);
				
				EditorGUILayout.EndHorizontal();
				
				if (GUI.changed && tempEditor.script.menuComponentsIndex > 0)
					tempEditor.applyGlobalParameters(tempEditor.script.components, TextWriterMenuEditor.Activity.none);
				
				for (int index1 = 0; index1 <script.events1[i].tweens.Length;index1++)
				{
					textWriter.ApplyModifiedProperties();
					if (index1 < script.events1[i].tweens.Length)
					{
						GUILayout.BeginHorizontal();
						
						GUI.changed = false;
						script.events1[i].tweens[index1].type = (TextWriterVisual.TweenType)EditorGUILayout.EnumPopup(script.events1[i].tweens[index1].type);
						
						if (GUI.changed)
							script.events1[i].tweens[index1].keys = new string[0];
						
						TextWriterVisual.TweenType type = script.events1[i].tweens[index1].type;
						
						GUILayout.EndHorizontal();
						if (GUILayout.Button("Config",EditorStyles.miniButton,GUILayout.Width(50)))
						{
							previousType[i,index1] = type;
							if (Application.isPlaying && !tempEditor.script.cleanOnceTween)tempEditor.script.cleanOnceTween= true;
							else if (!Application.isPlaying && tempEditor.script.cleanOnceTween)tempEditor.script.cleanOnceTween= false;
							
							if (windowTween) windowTween.Init(type,  script.events1, i, index1, tempEditor, "Menu Title # " + index + "Parameters");
							else
							{
								windowTween = (TextWriterTweenWindow) EditorWindow.GetWindow(typeof(TextWriterTweenWindow));
	       						windowTween.autoRepaintOnSceneChange=true;
	       						windowTween.Init(type,  script.events1, i, index1, tempEditor, "Menu Title # " + index + "Parameters");
							}
							GUI.changed = true;
						}	
						tempEditor.textWriterVisual.ApplyModifiedProperties();
						if (previousType[i,index1] != type)
						{
							previousType[i,index1] = type;
							if (windowTween)
							{
								windowTween.Init2(type,  script.events1, i, index1, tempEditor, "Menu Title # " + index + "Parameters");
							}
						}
					}
				}
				if (GUI.changed)
            		EditorUtility.SetDirty(script.gameObject);
				if (windowTween)
					windowTween.Repaint();
				textWriter.ApplyModifiedProperties();
			}
			  
		}
		if (GUI.changed && tempEditor.script.menuComponentsIndex > 0)
			tempEditor.applyGlobalParameters(tempEditor.script.components, TextWriterMenuEditor.Activity.none);
		EditorGUILayout.EndScrollView();
	}
}