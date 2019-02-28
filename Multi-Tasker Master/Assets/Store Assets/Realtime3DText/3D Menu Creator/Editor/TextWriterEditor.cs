using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Reflection;




[InitializeOnLoad]
[CustomEditor(typeof(TextWriter))]
public class TextWriterEditor : Editor {
	
	
	public TextWriter script;
	public Real3DFont real3DFont;
	
	[MenuItem("3D Menu Creator/Add Keyboard Capture Control")]
    static void AddInputControl () {
		GameObject inputObject;
		
		
		if(Selection.activeGameObject != null) {
			inputObject = Selection.activeGameObject;
			if (inputObject.GetComponent<TextWriterVisual>()||inputObject.GetComponent<TextWriter>()|| inputObject.name != "GameObject"|| inputObject.name != "New Game Object")
			{
				int decision;
				if (inputObject.GetComponent<TextWriterVisual>()||inputObject.GetComponent<TextWriter>())
					decision = EditorUtility.DisplayDialogComplex("Warning!", "This GameObject is a 3DMenu component already, Do you want to replace it? \n If you replace it, all current settings and titles would be cleared!","Yes", "No", "Create New Menu");	
					else decision = EditorUtility.DisplayDialogComplex("Warning!", "Did you want to add Keyboard Capture component to the GameObject "+inputObject.name+" ?","Yes", "No", "Create New Menu");	
						
				switch (decision)
				{
				case 0:
					if (inputObject.GetComponent<TextWriterVisual>())
						DestroyImmediate(inputObject.GetComponent<TextWriterVisual>());
					if (inputObject.GetComponent<TextWriter>())
						DestroyImmediate(inputObject.GetComponent<TextWriter>());
					if (inputObject.GetComponent<Real3DFont>())
						DestroyImmediate(inputObject.GetComponent<Real3DFont>());
					break;
				case 1:
					return;
				case 2:
					inputObject = new GameObject();
					break;
				}
			}
		}else{
		
		inputObject = new GameObject();
		}
		
		
		
		inputObject.AddComponent(typeof(TextWriter));
		
		TextWriter tempScript = inputObject.GetComponent<TextWriter>();
		tempScript.inputControl = true;
		tempScript.letterShader = Shader.Find("Diffuse");
		
		LoadNSave tempSave = new LoadNSave();
		tempSave.events = new Hashtable();
		SaveLoad.currentFilePath = "TextWriterInputsControl.DAT";
		int index = 0;
		if (System.IO.File.Exists(SaveLoad.currentFilePath))
		{
			tempSave = SaveLoad.Load(SaveLoad.currentFilePath);
			index = (int)tempSave.events["InputControlIndex"]+1;
		}else{
			tempSave.events.Add("InputControlIndex" ,index);
		}
		tempScript.name = "Input Control "+index;
		tempSave.events.Add("Input Control "+index, tempScript.name);
		tempSave.events["InputControlIndex"]=index;
		
		SaveLoad.Save(tempSave);
		tempSave.events.Clear();
		inputObject.name = tempScript.name;
		
		
		inputObject.AddComponent(typeof(Real3DFont));
		Real3DFont tempScript1 = inputObject.GetComponent<Real3DFont>();
		if (!tempScript1.gameObject.GetComponent<MeshRenderer>()){
		tempScript1.gameObject.AddComponent<MeshRenderer>();
		tempScript1.gameObject.AddComponent<MeshFilter>();}
		tempScript1.fontSize = 100;
		tempScript1.textToPrint = "Hello";
		tempScript1.fontNameIndex = 0;
		tempScript1.fontStyleIndex = 0;
		tempScript1.text = new Real3DText.Fonts.ExtrudedText();
		Real3DFontEditor.updateFontList(tempScript1);
		tempScript1.vectorFont = new Real3DText.Fonts.VectorFont();
		Real3DFontEditor.changeFont(tempScript1);
		Real3DFontEditor.process(tempScript1);
		tempScript1.vectorFont.toList();
		tempScript1.isSubUsed = true;
    }
	
	
	public void OnEnable()
	{
		script = (TextWriter) target;
		real3DFont = script.gameObject.GetComponentInChildren<Real3DFont>();
		if (script.logo == null) script.logo =(Texture2D) Resources.Load("3DMenuCreatorLogo");
		Real3DFontEditor.updateFontList(real3DFont);
		if (real3DFont.vectorFont.kerningTable.Count == 0)real3DFont.vectorFont.toDictionary();
		
	} 
	
	public override void OnInspectorGUI ()
	{
		
			GUI.changed = false;
			GUILayout.Space(5);
			GUILayout.BeginHorizontal();
			GUILayout.Space(25);
			GUILayout.Label(script.logo,GUILayout.Width(245));
			GUILayout.EndHorizontal();
			
			
		if (script.inputControl)
		{
//			GUILayout.Space(25);
//			GUILayout.BeginHorizontal();
//			script.positionOffset = EditorGUILayout.Vector3Field("Position Offset",script.positionOffset,GUILayout.Width(245));
//			GUILayout.EndHorizontal();
			
			GUI.changed = false;
			GUILayout.Space(15);
			GUILayout.BeginHorizontal();
			script.defaultWord = EditorGUILayout.TextField(new GUIContent("Default word displayed","This word would be displayed and the end user could replace it, i.e This could be Player 1, so the end user enter his name"),script.defaultWord,GUILayout.Width(245));
			GUILayout.EndHorizontal();
			
			if (GUI.changed) {
				if (script.displayCursor)
					real3DFont.textToPrint = script.defaultWord + "|";
				else real3DFont.textToPrint = script.defaultWord;
				Real3DFontEditor.process(real3DFont);
//				if (script.displayCursor){
//					script.UpdateCursor(real3DFont);
//				}
				EditorUtility.SetDirty(script);
				EditorUtility.SetDirty(real3DFont);
			}
			
			GUI.changed = false;
			GUILayout.Space(15);
			GUILayout.BeginHorizontal();
			real3DFont.fontNameIndex = EditorGUILayout.Popup("Font",real3DFont.fontNameIndex,real3DFont.fontsName,GUILayout.Width(245));
			GUILayout.EndHorizontal();
			
			GUILayout.Space(15);
			GUILayout.BeginHorizontal();
			string[] array = Real3DFontEditor.styleArray(real3DFont.fontStyle,real3DFont.fontNameIndex);	
			real3DFont.fontStyleIndex =EditorGUILayout.Popup("Style",real3DFont.fontStyleIndex >= real3DFont.fontStyle[real3DFont.fontNameIndex].Length?0:real3DFont.fontStyleIndex, array,GUILayout.Width(245));
			GUILayout.EndHorizontal();
			
			GUILayout.Space(15);
			GUILayout.BeginHorizontal();
			real3DFont.fontSize = EditorGUILayout.IntField(new GUIContent("Font Size","The size of the font to be displayed"),real3DFont.fontSize,GUILayout.Width(245));
			GUILayout.EndHorizontal();
			
			
			GUILayout.BeginHorizontal();
			real3DFont.fontResolution =EditorGUILayout.IntField("Resolution",real3DFont.fontResolution<3?3:real3DFont.fontResolution,GUILayout.Width(245));
			real3DFont.fontResolution =real3DFont.fontResolution < 3?3:real3DFont.fontResolution;
			GUILayout.EndHorizontal();
			
			GUILayout.Space(5);
			
			GUILayout.BeginHorizontal();
			real3DFont.depth =EditorGUILayout.FloatField("Depth",real3DFont.depth,GUILayout.Width(245));
			GUILayout.EndHorizontal();
			
			if (GUI.changed) {
				Real3DFontEditor.changeFont(real3DFont);
				Real3DFontEditor.process(real3DFont);
				if (script.displayCursor){
//					script.UpdateCursor(real3DFont);
				}
				EditorUtility.SetDirty(script);
				EditorUtility.SetDirty(real3DFont);
			}
			
//			GUILayout.Space(15);
//			GUILayout.BeginHorizontal();
//			script.letterSpace = EditorGUILayout.Vector3Field("Letter Spacing",script.letterSpace,GUILayout.Width(245));
//			GUILayout.EndHorizontal();
			
			GUI.changed = false;
			GUILayout.Space(15);
			GUILayout.BeginHorizontal();
			script.colorDefault = EditorGUILayout.ColorField(new GUIContent("Font color","The color of the font to be displayed"),script.colorDefault,GUILayout.Width(245));
			GUILayout.EndHorizontal();
			if (GUI.changed){
				TextWriter.fontColor(script.colorDefault, script.gameObject);
				EditorUtility.SetDirty(script);
				EditorUtility.SetDirty(real3DFont);
			}
			
			GUI.changed = false;
			GUILayout.Space(15);
			GUILayout.BeginHorizontal();
			script.displayCursor = EditorGUILayout.Toggle(new GUIContent("Display the cursor","If selected, the cursor would be displayed at the end of the last letter of the word"),script.displayCursor,GUILayout.Width(245));
			GUILayout.EndHorizontal();
			
//				script.createCursor(real3DFont);
			if (GUI.changed){
				if (script.displayCursor)
					real3DFont.textToPrint = script.defaultWord + "|";
				else real3DFont.textToPrint = script.defaultWord;
				Real3DFontEditor.process(real3DFont);
				EditorUtility.SetDirty(script);
				EditorUtility.SetDirty(real3DFont);
			}
			
			GUILayout.Space(15);
			GUILayout.BeginHorizontal();
			
			GUI.changed = false;
			if (GUILayout.Button(new GUIContent("New material","Creates new material for the font to be displayed"),EditorStyles.miniButton,GUILayout.Width(100)))
			{	
				script.material = new Material(Shader.Find("Diffuse"));
				script.material.name = script.name + "Material";
				GUI.changed = true;
			}
			if (GUI.changed && script.material)
				script.material.color = script.colorDefault;
			
			GUILayout.EndHorizontal();
			
			GUILayout.Space(15);
			GUILayout.BeginHorizontal();
			script.material = (Material)EditorGUILayout.ObjectField(new GUIContent("Font material","The material of the font to be displayed, double clik the material to set it's parameters"),script.material,typeof (Material),false,GUILayout.Width(245));
			GUILayout.EndHorizontal();
			if (GUI.changed) {
				real3DFont.gameObject.GetComponent<MeshRenderer>().sharedMaterial = script.material;
				EditorUtility.SetDirty(script);
				EditorUtility.SetDirty(real3DFont);
			}
		}
		else
			EditorGUILayout.TextArea("Please use the main GameObject\n to acces the configurable parameters\n for this Title");
	}
}



