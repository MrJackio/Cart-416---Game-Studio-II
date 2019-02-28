using System;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class TextWriterGlobalWindow : EditorWindow {
	
	TextWriterVisual script;
	TextWriterMenuEditor tempEditor;
	TextWriterVisual.TweenType[,] previousType = new TextWriterVisual.TweenType[8,25];
	public TextWriterTweenWindow windowTween;
	
	TextWriterVisual.OnEventType onEventsTypes;
	
	Vector2 scrollPos;
	
	
	public void Init(TextWriterVisual thisObject, TextWriterMenuEditor values)
    {
        script = thisObject;
		tempEditor = values;
		this.title = "Global Config";
		Real3DFontEditor.updateFontList(ref Real3DText.Fonts.FontList.fontlist, ref script.fontsName, ref script.fontStyle);
    }
	
	void OnGUI()
	{
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width), GUILayout.Height(position.height));
		
		if (Application.isPlaying && !script.cleanOnceGlobal)
		{
			GUILayout.Space(100);
			GUILayout.BeginHorizontal();
			GUILayout.Box("3D Menu Creator\nGlobal Paramters\nPlease re-open this window\nto refresh values",GUILayout.Width(255));
			GUILayout.EndHorizontal();
			return;
		} else if (!Application.isPlaying && script.cleanOnceGlobal)
		{
			GUILayout.Space(100);
			GUILayout.BeginHorizontal();
			GUILayout.Box("3D Menu Creator\nGlobal Paramters\nPlease re-open this window\nto refresh values",GUILayout.Width(255));
			GUILayout.EndHorizontal();
			return;
		}
		
		 GUI.changed = false;
		
		GUILayout.Space(10);
		GUILayout.BeginHorizontal();
		GUILayout.Space((position.width/2 - 115));
		GUILayout.Label(script.logoGP,GUILayout.Width(250));
		GUILayout.EndHorizontal();
		
		GUILayout.Space(5);
		
		
		GUILayout.Space(10);
		
		GUILayout.BeginHorizontal();
		GUILayout.TextArea("    Global Justification (Left/Center/Right)");
		GUILayout.EndHorizontal();
			
		GUILayout.Space(5);
	
		GUI.changed = false;
		GUILayout.BeginHorizontal();
		script.justify = (TextWriterVisual.justifyType)EditorGUILayout.EnumPopup(script.justify, GUILayout.Width(150));
		GUILayout.EndHorizontal();
		if (GUI.changed && tempEditor.script.menuComponentsIndex > 0)
			tempEditor.applyGlobalParameters(tempEditor.script.components, TextWriterMenuEditor.Activity.redraw);
		GUI.changed = false;
		if (script.singlePositionSpace)
		{
			GUILayout.Space(10);
		
			GUILayout.BeginHorizontal();
			GUILayout.TextArea("                 Global Position Space");
			GUILayout.EndHorizontal();
			
			GUILayout.Space(5);
			
			GUILayout.BeginHorizontal();
			script.positionSpace = EditorGUILayout.Vector3Field("Space between Menu Titles",script.positionSpace, GUILayout.Width(255));
			GUILayout.EndHorizontal();
			if (GUI.changed && tempEditor.script.menuComponentsIndex > 0)
				tempEditor.applyGlobalParameters(tempEditor.script.components, TextWriterMenuEditor.Activity.redraw);
		}
		GUI.changed = false;
		if (script.singleFontType)
		{
		
			GUILayout.Space(15);
			GUILayout.BeginHorizontal();
			script.fontNameIndex = EditorGUILayout.Popup("Font",script.fontNameIndex,script.fontsName,GUILayout.Width(245));
			GUILayout.EndHorizontal();
		}
		if (script.singleFontStyle)
		{
			GUILayout.Space(15);
			GUILayout.BeginHorizontal();
			string[] array = Real3DFontEditor.styleArray(script.fontStyle,script.fontNameIndex);	
			script.fontStyleIndex =EditorGUILayout.Popup("Style",script.fontStyleIndex >= script.fontStyle[script.fontNameIndex].Length?0:script.fontStyleIndex, array,GUILayout.Width(245));
			GUILayout.EndHorizontal();
		}
		
		
		if (script.singleFontSize)
		{
			GUILayout.Space(10);
			
			GUILayout.BeginHorizontal();
			GUILayout.TextArea("                 Global Font size");
			GUILayout.EndHorizontal();
			
			GUILayout.Space(5);
			
			GUILayout.BeginHorizontal();
			script.fontsize = EditorGUILayout.IntField("Size",script.fontsize, GUILayout.Width(200));
			GUILayout.EndHorizontal();
			
			GUILayout.Space(5);
			
			GUILayout.BeginHorizontal();
			script.fontResolution =EditorGUILayout.IntField("Resolution",script.fontResolution<3?3:script.fontResolution,GUILayout.Width(245));
			script.fontResolution =script.fontResolution < 3?3:script.fontResolution;
			GUILayout.EndHorizontal();
			
			GUILayout.Space(5);
			
			GUILayout.BeginHorizontal();
			script.depth =EditorGUILayout.FloatField("Depth",script.depth,GUILayout.Width(245));
			GUILayout.EndHorizontal();
		}
		if (GUI.changed && tempEditor.script.menuComponentsIndex > 0)
			tempEditor.applyGlobalParameters(tempEditor.script.components, TextWriterMenuEditor.Activity.ChangeFontAndProcess);
		
//		if (script.singleLetterSpace)
//		{
//			GUILayout.Space(10);
//			
//			GUILayout.BeginHorizontal();
//			GUILayout.TextArea("                 Global Font letter space");
//			GUILayout.EndHorizontal();
//			
//			GUILayout.Space(5);
//			
//			GUILayout.BeginHorizontal();
//			script.letterSpace = EditorGUILayout.Vector3Field("Space between letters",script.letterSpace, GUILayout.Width(255));
//			GUILayout.EndHorizontal();
//		}
//		if (GUI.changed) {tempEditor.applyGlobalParameters(script.components); tempEditor.redrawTitles(script.components); GUI.changed = false;}

		GUI.changed = false;
		if (script.singleColor)
		{
			GUILayout.Space(10);
			
			GUILayout.BeginHorizontal();
			GUILayout.TextArea("                 Global Font Color");
			GUILayout.EndHorizontal();
			
			GUILayout.Space(5);
			
			GUILayout.BeginHorizontal();
			GUILayout.Label("Font Color");
			script.fontcolor = EditorGUILayout.ColorField(script.fontcolor, GUILayout.Width(100));
			GUILayout.EndHorizontal();
			if (GUI.changed && tempEditor.script.menuComponentsIndex > 0)
				tempEditor.applyGlobalParameters(tempEditor.script.components, TextWriterMenuEditor.Activity.changeColor);
		}
		
		GUI.changed = false;		
		if (script.singleShader)
		{
			GUILayout.Space(10);
			
			GUILayout.BeginHorizontal();
			GUILayout.TextArea("                 Global Font Shader");
			GUILayout.EndHorizontal();
			
			GUILayout.Space(5);
			GUILayout.BeginHorizontal();
			GUILayout.Label("Font Shader (By selection)");
			
			script.fontShader = EditorGUILayout.ObjectField(script.fontShader,typeof (Shader),false, GUILayout.Width(100)) as Shader;
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Label("Font Shader (By name)");
			script.fontShader = Shader.Find(EditorGUILayout.TextField((script.fontShader!=null?script.fontShader.name:""), GUILayout.Width(100)));
			GUILayout.EndHorizontal();
			
			if (GUI.changed && tempEditor.script.menuComponentsIndex > 0)
				tempEditor.applyGlobalParameters(tempEditor.script.components, TextWriterMenuEditor.Activity.ChangeShader);
		}
		
		
		GUI.changed = false;		
		
		GUILayout.Space(20);
		
		
		
		SerializedObject tempTextWriterVisual = new SerializedObject(script);
		SerializedProperty tempEvents = tempTextWriterVisual.FindProperty("events1");
		
		tempEvents.serializedObject.Update();
		
		EditorGUILayout.BeginHorizontal();
		
		onEventsTypes = (TextWriterVisual.OnEventType)EditorGUILayout.EnumPopup( onEventsTypes,GUILayout.Width(100));
		
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
			
			EditorGUILayout.TextArea("Global Parameters "+script.events1[i].type +" Event");
			GUILayout.EndHorizontal();
		
			if (script.singleEventsShader)
			{
				GUILayout.Space(5);
			
				GUILayout.BeginHorizontal();
				GUILayout.Label(new GUIContent("Font Shader (By selection)", "Set font shader for al titles (select shader from list)"));
				script.eventsShader[i] = EditorGUILayout.ObjectField(script.eventsShader[i],typeof (Shader),false,GUILayout.Width(100)) as Shader;
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				GUILayout.Label(new GUIContent("Font Shader (By name)", "Select shader by typing the shader's name.  This is helpfull to find Unity built-in shaders that are not shown on the shaders list"));
				script.eventsShader[i] = Shader.Find(EditorGUILayout.TextField((script.eventsShader[i]!=null?script.eventsShader[i].name:""), GUILayout.Width(100)));
				GUILayout.EndHorizontal();
			
				GUILayout.Space(5);
			}
			
			if (script.singleEventsAudio)
			{
				GUILayout.Space(5);
				
				GUILayout.BeginHorizontal();
				GUILayout.Label(new GUIContent("Audio", "Audio to be played for all titles when this event is triggered"));
				script.eventsAudio[i] = EditorGUILayout.ObjectField(script.eventsAudio[i],typeof (AudioClip),false, GUILayout.Width(100)) as AudioClip;
				GUILayout.EndHorizontal();
				
				GUILayout.Space(5);
			}
			
			if (script.singleEventsTarget)
			{
				GUILayout.Space(5);
				
				GUILayout.BeginHorizontal();
				GUILayout.Label(new GUIContent("Target", "Target for all titles to call the specified function of the Target Function option"));
				script.eventsTarget[i] = EditorGUILayout.ObjectField(script.eventsTarget[i],typeof (GameObject),true, GUILayout.Width(100)) as GameObject;
				GUILayout.EndHorizontal();
				
				GUILayout.Space(5);
			}
			
			if (script.singleEventsFunctions)
			{
				GUILayout.Space(5);
				
				GUILayout.BeginHorizontal();
				GUILayout.Label(new GUIContent("Target Function", "Function to be called for al titles when this event is triggered"));
				script.eventsFunction[i] = EditorGUILayout.TextField(script.eventsFunction[i], GUILayout.Width(150));
				GUILayout.EndHorizontal();
				
				GUILayout.Space(5);
			}
			
			if (script.singleEventsFunctionsParam)
			{
				GUILayout.Space(5);
				
				GUILayout.BeginHorizontal();
				GUILayout.Label(new GUIContent("Function Argument", "Function arguments (parameters type of string) to be sent to the function especified on the Target Function Option when this event is triggered (this would be set for al titles)"));
					script.eventsParam[i] = (EditorGUILayout.TextField((script.eventsParam[i])!=null?(script.eventsParam[i]):"", GUILayout.Width(100)));
					GUILayout.EndHorizontal();
				GUILayout.Space(5);
			}
			
			if (script.singleEventsUseItween)
			{
					tempEditor.textWriterVisual.Update();
				
				SerializedProperty tempIndex = tempEditor.events.GetArrayElementAtIndex(i);
				SerializedProperty tempTweens = tempIndex.FindPropertyRelative("tweens");
				GUILayout.Space(5);
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(new GUIContent("iTween Effect", "iTween effects to be played for all titles when this event is triggered"));
				
				if (GUILayout.Button(new GUIContent("+", "Add new iTween effect"),EditorStyles.miniButtonLeft,GUILayout.Width(25)))
				{
					tempTweens.arraySize++;
					tempEditor.textWriterVisual.ApplyModifiedProperties();
					GUI.changed = true;
				}
				
				if (GUILayout.Button(new GUIContent("-", "Remove last iTween effect"),EditorStyles.miniButtonRight,GUILayout.Width(25)))
				{
					if (tempTweens.arraySize == 1)
					{
						tempTweens.ClearArray();
					}
					else {
							tempTweens.DeleteCommand();
						tempTweens.arraySize--;
					}
					tempEditor.textWriterVisual.ApplyModifiedProperties();
					GUI.changed = true;
				}
				EditorGUILayout.EndHorizontal();
				
			
				if (GUI.changed && tempEditor.script.menuComponentsIndex > 0)
					tempEditor.applyGlobalParameters(tempEditor.script.components, TextWriterMenuEditor.Activity.none);
				
				for (int index1 = 0; index1 <script.events1[i].tweens.Length;index1++)
				{
					if (index1 < script.events1[i].tweens.Length)
					{
						GUILayout.BeginHorizontal();
						
						GUI.changed = false;
						script.events1[i].tweens[index1].type = (TextWriterVisual.TweenType)EditorGUILayout.EnumPopup(script.events1[i].tweens[index1].type);
						
						if (GUI.changed)
							script.events1[i].tweens[index1].keys = new string[0];
						
						TextWriterVisual.TweenType type = script.events1[i].tweens[index1].type;
						
						GUILayout.EndHorizontal();
						if (GUILayout.Button(new GUIContent("Config", "Acces the iTween parameters window"),EditorStyles.miniButton,GUILayout.Width(50)))
						{
							previousType[i,index1] = type;
							if (Application.isPlaying && !script.cleanOnceTween)script.cleanOnceTween= true;
							else if (!Application.isPlaying && script.cleanOnceTween)script.cleanOnceTween= false;
							
							if (windowTween){
								windowTween.Init(type,  script.events1, i, index1, tempEditor, "Global Parameters");
							}
							else
							{
								windowTween = (TextWriterTweenWindow) EditorWindow.GetWindow(typeof(TextWriterTweenWindow));
	       						windowTween.autoRepaintOnSceneChange=true;
	       						windowTween.Init(type,  script.events1, i, index1, tempEditor, "Global Parameters");
							}
							GUI.changed = true;
						}	
						tempEditor.textWriterVisual.ApplyModifiedProperties();
						if (previousType[i,index1] != type)
						{
							previousType[i,index1] = type;
							if (windowTween)
							{
								windowTween.Init2(type,  script.events1, i, index1, tempEditor, "Global Parameters");
							}
						}
					}
				}
				
				if (GUI.changed)
            		EditorUtility.SetDirty(script.gameObject);
				
				if (windowTween)
					windowTween.Repaint();
				tempEditor.textWriterVisual.ApplyModifiedProperties();
			}
		}
		
		if (GUI.changed && tempEditor.script.menuComponentsIndex > 0)
			tempEditor.applyGlobalParameters(tempEditor.script.components, TextWriterMenuEditor.Activity.none);
		
	EditorGUILayout.EndScrollView();
	}
	
	
}
