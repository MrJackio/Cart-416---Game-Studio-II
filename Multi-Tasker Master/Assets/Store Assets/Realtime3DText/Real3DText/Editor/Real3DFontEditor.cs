using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Reflection;
using Real3DText.Fonts.VectorizedFont;
using Real3DText.Fonts;


[InitializeOnLoad]
[CustomEditor(typeof(Real3DFont))]
public class Real3DFontEditor : Editor {
	
	
	public Real3DFont script;
	
	
	[MenuItem("3D Menu Creator/Add 3DText")]
    static GameObject AddInputControl () {
		GameObject inputObject;
		FreeTypeManager.verifyDLLInstallation();
		
		if(Selection.activeGameObject != null) {
			inputObject = Selection.activeGameObject;
			if (inputObject.GetComponent<Real3DFont>()|| inputObject.name != "GameObject"|| inputObject.name != "New Game Object")
			{
				int decision;
				if (inputObject.GetComponent<Real3DFont>())
					decision = EditorUtility.DisplayDialogComplex("Warning!", "This GameObject is a 3DText component already, Do you want to replace it? \n If you replace it, all current settings and text would be cleared!","Yes", "No", "Create New Menu");	
					else decision = EditorUtility.DisplayDialogComplex("Warning!", "Do you want to add 3DText component to the GameObject "+inputObject.name+" ?","Yes", "No", "Create New 3DText");	
						
				switch (decision)
				{
				case 0:
					if (inputObject.GetComponent<Real3DFont>())
						DestroyImmediate(inputObject.GetComponent<Real3DFont>());
					break;
				case 1:
					return null;
				case 2:
					inputObject = new GameObject();
					break;
				}
			}
		}else{
		
		inputObject = new GameObject();
		}
		inputObject.AddComponent(typeof(Real3DFont));
		Real3DFont tempScript = inputObject.GetComponent<Real3DFont>();
		if (!tempScript.gameObject.GetComponent<MeshRenderer>()){
		tempScript.gameObject.AddComponent<MeshRenderer>();
		tempScript.gameObject.AddComponent<MeshFilter>();}
		tempScript.fontSize = 100;
		tempScript.textToPrint = "Hello";
		
		inputObject.name = tempScript.name;
		tempScript.fontNameIndex = 0;
		tempScript.fontStyleIndex = 0;
		tempScript.text = new ExtrudedText();
		updateFontList(tempScript);
		tempScript.vectorFont = new VectorFont();
		changeFont(tempScript);
		process(tempScript);
		tempScript.vectorFont.toList();
		tempScript.isSubUsed = false;
		
		return inputObject;
    }
	[MenuItem("3D Menu Creator/Add 3DText Runtime Editable")]
	static void AddInputControlRuntime () {
		GameObject obj = AddInputControl();
		if (obj !=null)
			obj.AddComponent<RuntimeLabel>();
	}
	
	RuntimeLabel label;
	
	public void OnEnable()
	{
		
		script = (Real3DFont) target;
		label = script.GetComponent<RuntimeLabel>();
		updateFontList(script);
		script.vectorFont.toDictionary();
//		changeFont(script);
//		process(script);
	} 
	
	public override void OnInspectorGUI ()
	{
		if (!script.isSubUsed){
			GUI.changed = false;
			GUILayout.Space(5);
			
			if (label == null)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Space(25);
				script.textToPrint = EditorGUILayout.TextField("Text",(script.textToPrint!=null?script.textToPrint:"\b"),GUILayout.Width(245));
				GUILayout.EndHorizontal();
			}else{
				EditorGUILayout.HelpBox("Use the Runtime Label words field to change the text!",MessageType.Info);
			}
			if (GUI.changed)
			{
				process(script);
				EditorUtility.SetDirty(script);
			}
			GUI.changed = false;
			GUILayout.Space(5);
			GUILayout.BeginHorizontal();
			GUILayout.Space(25);
			script.fontSize =EditorGUILayout.IntField("Font Size",script.fontSize,GUILayout.Width(245));
			GUILayout.EndHorizontal();
			if (GUI.changed)
			{
				changeFont(script);
				process(script);
				EditorUtility.SetDirty(script);
			}
			GUI.changed = false;
			GUILayout.Space(5);
			GUILayout.BeginHorizontal();
			GUILayout.Space(25);
			script.fontResolution =EditorGUILayout.IntField("Resolution",script.fontResolution<1?1:script.fontResolution,GUILayout.Width(245));
			script.fontResolution =script.fontResolution < 1?1:script.fontResolution;
			GUILayout.EndHorizontal();


			GUILayout.Space(5);
			GUILayout.BeginHorizontal();
			GUILayout.Space(25);
			script.smoothingAngle =EditorGUILayout.Slider(new GUIContent( "Smoothing Angle","This is the angle that would be used for calculating the normals"),script.smoothingAngle,0f,180f,GUILayout.Width(245));
			GUILayout.EndHorizontal();

			if (GUI.changed)
			{
				changeFont(script);
				process(script);
				EditorUtility.SetDirty(script);
			}
			GUI.changed = false;
				GUILayout.Space(5);
				GUILayout.BeginHorizontal();
				GUILayout.Space(25);
				script.depth =EditorGUILayout.FloatField("Depth",script.depth,GUILayout.Width(245));
				GUILayout.EndHorizontal();
			
			if (GUI.changed)
			{
				script.vectorFont.Changed = true;
				process(script);
				EditorUtility.SetDirty(script);
			}
		
			GUI.changed = false;
			GUILayout.Space(5);
			GUILayout.BeginHorizontal();
			GUILayout.Space(25);
			script.fontNameIndex = EditorGUILayout.Popup("Font",script.fontNameIndex,script.fontsName,GUILayout.Width(245));
			GUILayout.EndHorizontal();
			GUILayout.Space(5);
			GUILayout.BeginHorizontal();
			GUILayout.Space(25);
			string[] array = styleArray(script.fontStyle,script.fontNameIndex);	
			script.fontStyleIndex =EditorGUILayout.Popup("Style",script.fontStyleIndex >= script.fontStyle[script.fontNameIndex].Length?0:script.fontStyleIndex, array,GUILayout.Width(245));
			GUILayout.EndHorizontal();
			
			
			GUILayout.Space(5);
			GUILayout.BeginHorizontal();
			GUILayout.Space(25);
			script.meshType = (Real3DFont.MeshType)EditorGUILayout.EnumPopup("Mesh Type",script.meshType,GUILayout.Width(245)); 
			GUILayout.EndHorizontal();

			if (GUI.changed)
			{
				changeFont(script);
				process(script);
				Repaint();
				EditorUtility.SetDirty(script);
			}
			GUI.changed = false;
			GUILayout.Space(5);
			GUILayout.BeginHorizontal();
			GUILayout.Space(25);
			script.individualFaceMaterials = EditorGUILayout.Toggle(new GUIContent("Individual face Materials","Make the mesh with individual sub meshes for each face?"),script.individualFaceMaterials,GUILayout.Width(245)); 
			GUILayout.EndHorizontal();
			if (GUI.changed)
			{
				changeFont(script);
				process(script);
				Repaint();
				EditorUtility.SetDirty(script);
			}
			GUI.changed = false;

			GUILayout.Space(5);
			GUILayout.BeginHorizontal();
			GUILayout.Space(25);
			script.useMeshMaterials = EditorGUILayout.Toggle(new GUIContent("Use Mesh Materials","If enabled the materials from the mesh would be used, else the materials from Real3DFont script would be used"),script.useMeshMaterials,GUILayout.Width(245)); 
			GUILayout.EndHorizontal();
			if (GUI.changed)
			{
				if (!script.useMeshMaterials)
					setMaterials(script);
				Repaint();
				EditorUtility.SetDirty(script);
			}
			GUI.changed = false;

			GUILayout.Space(5);
			GUILayout.BeginHorizontal();
			GUILayout.Space(25);
			script.individualMeshMaterials = EditorGUILayout.Toggle(new GUIContent("Individual meshes materials","If enabled the materials would be independent for each generated mesh, otherwise it would be the same materials for all the generated meshes"),script.individualMeshMaterials,GUILayout.Width(245)); 
			GUILayout.EndHorizontal();
			if (GUI.changed)
			{
				if (!script.useMeshMaterials)
					setMaterials(script);
				Repaint();
				EditorUtility.SetDirty(script);
			}

			GUI.changed = false;
			if (!script.useMeshMaterials){

				if (script.text.TMaterial != null){
					int amount = script.individualMeshMaterials?script.text.TMaterial.Length:1;
					bool applyButton = (amount>1);


					for (int i = 0; i <amount;i++)
					{
						GUILayout.BeginVertical("Mesh# " + i.ToString(),EditorStyles.textField);
						GUILayout.Space(15);
						if (script.individualFaceMaterials){
							int counter = 0;
							for (int i1 = 0;i1 < script.text.TMaterial[i].material.Length;i1++){
								string name = "";
								if (counter ==2)
								{
									name = "Depth";
								}else if (counter ==1)
								{
									name = "front";
								}
								else if (counter ==0)
								{
									name = "Back";
								}

								GUILayout.BeginHorizontal();
								script.text.TMaterial[i].material[i1] = (Material) EditorGUILayout.ObjectField(
									new GUIContent("Material_"+name+"_"),script.text.TMaterial[i].material[i1],typeof(Material),false);
								if (applyButton)
								{
									if (GUILayout.Button("Apply",EditorStyles.miniButton,GUILayout.Width(45)))
									{
										ApplyMaterialToFaceDepthBack(script,counter,script.text.TMaterial[i].material[i1]);
									}
								}
								if (!script.individualMeshMaterials && GUI.changed)
								{
									ApplyMaterialToFaceDepthBack(script,counter,script.text.TMaterial[i].material[i1]);
								}
								GUILayout.EndHorizontal();
								counter++;
								if (counter > 2)counter =0;
							}
						}else{


							if(script.text.TMaterial[i] != null && script.text.TMaterial[i].material != null && script.text.TMaterial[i].material.Length >0){
							
								GUILayout.BeginHorizontal();
								script.text.TMaterial[i].material[0] = (Material) EditorGUILayout.ObjectField(
									new GUIContent("Material_"+ i.ToString()),script.text.TMaterial[i].material[0],typeof(Material),false);

								if (applyButton)
								{
									if (GUILayout.Button("Apply",EditorStyles.miniButton,GUILayout.Width(45)))
									{
										ApplyMaterialToFaceDepthBack(script,0,script.text.TMaterial[i].material[0]);
									}
								}
								if (!script.individualMeshMaterials && GUI.changed)
								{
									ApplyMaterialToFaceDepthBack(script,0,script.text.TMaterial[i].material[0]);
								}
								GUILayout.EndHorizontal();

							}
						}

						GUILayout.EndVertical();

					}
				}
			}
			if (GUI.changed)
			{
				if (!script.useMeshMaterials)
					setMaterials(script);
				Repaint();
				EditorUtility.SetDirty(script);
			}

		}	
		else
			EditorGUILayout.TextArea("Please use the main GameObject\n to acces the configurable parameters\n for this Title");
	}

	static void ApplyMaterialToFaceDepthBack(Real3DFont script, int index, Material material)
	{
		int counter = 0;
		for (int i = 0; i < script.text.TMaterial.Length;i++)
		{
			for (int y = 0; y < script.text.TMaterial[i].material.Length;y++)
			{
				if (counter == index)
					script.text.TMaterial[i].material[y] = material;

				counter++;
				if (counter > 2)counter =0;
			}
		}
		setMaterials(script);

	}

	public static string[] styleArray(List<FontStyle[]> fontStyle, int fontNameIndex)
	{
		string[] array = new string[fontStyle[fontNameIndex].Length];	
		for (int i = 0; i < fontStyle[fontNameIndex].Length; i++)
			array[i] = fontStyle[fontNameIndex][i].ToString();
		return array;
	}
	
	public static void updateFontList(Real3DFont script)
	{
		
		updateFontList(ref Real3DText.Fonts.FontList.fontlist , ref script.fontsName,ref script.fontStyle);
	}
	
	public static void updateFontList(ref string[] fontsName,ref List<FontStyle[]> fontStyle)
	{
		updateFontList(ref Real3DText.Fonts.FontList.fontlist, ref fontsName, ref fontStyle);
	}
	
	public static void updateFontList(ref Dictionary<string,List<fontDesc>> fontlist,ref string[] fontsName,ref List<FontStyle[]> fontStyle)
	{
		if (fontlist.Count == 0)
		{
			FreeTypeManager manager = new FreeTypeManager();
			fontlist = manager.getSystemFonts();
			manager.shutdownFreeType();
		}
		
		fontsName = new string[fontlist.Keys.Count];
		fontlist.Keys.CopyTo(fontsName,0);
		fontStyle = new List<FontStyle[]>();
		 for (int i = 0; i < fontlist.Keys.Count;i++)
		{
			List<fontDesc> list = fontlist[fontsName[i]];
			FontStyle[] style = new FontStyle[list.Count];
			int ix = 0;
			foreach(fontDesc font in list){
				style[ix] = font.fontStyle;	
				ix++;
			}
			fontStyle.Add(style);	
		}
		
	}
	
	public static void process(Real3DFont script)
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
	
	public static void getMaterials(Real3DFont script)
	{
		MeshRenderer[] tempRenderer = script.gameObject.GetComponentsInChildren<MeshRenderer>();
		if (script.text.TMaterial == null) 
			script.text.TMaterial = new Text.Materials[tempRenderer.Length];
		System.Array.Resize<Text.Materials>(ref script.text.material,tempRenderer.Length==0?1:tempRenderer.Length);
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

	static Material[] GetCopy(Material[] mat)
	{
		Material[] newArray = (Material[]) mat.Clone();
		return newArray;
	}

	public static void setMaterials(Real3DFont script)
	{
		int r = 0;
		MeshRenderer[] tempRenderer = script.gameObject.GetComponentsInChildren<MeshRenderer>();
		if (script.text.TMaterial.Length != tempRenderer.Length)
		{
			System.Array.Resize<Text.Materials>(ref script.text.material,tempRenderer.Length==0?1:tempRenderer.Length);
		}

		foreach (MeshRenderer renderer in tempRenderer){
			MeshFilter meshF = renderer.GetComponent<MeshFilter>();

			if (meshF.sharedMesh.vertexCount != 0 && meshF.sharedMesh.uv2 == null)
				Unwrapping.GenerateSecondaryUVSet(meshF.sharedMesh);

			if (script.text.SubMeshCount == 3)
			{
				if (script.text.TMaterial[r] == null || script.text.TMaterial[r].material.Length !=3 && r>0)
				{
					script.text.TMaterial[r] = new Text.Materials();
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

	static void CheckIfMaterialNeedsToBeCreated(ref Text.Materials mat)
	{
		if (mat == null || mat.material == null || mat.material.Length < 3)
		{
			mat = new Text.Materials();
			mat.material = new Material[3];
		}
	}

	public static void changeFont(Real3DFont script)
	{
		fontDesc tempFont = Real3DText.Fonts.FontList.fontlist[script.fontsName[script.fontNameIndex]][script.fontStyleIndex];
		changeFont(script, tempFont);
	}
	
	public static void changeFont(Real3DFont script, fontDesc tempFont)
	{
		tempFont.fontSize = script.fontSize;
		Real3DText.Fonts.Vectorizer.FreeTypeFontVectorizer.VectorFont(script.textToPrint, tempFont, ref script.vectorFont, script.fontResolution);
		script.vectorFont.Changed = true;
	}
	
	public static void clearMeshes(Real3DFont script, bool destroyMesh)
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

//	void OnSceneGUI()
//	{
//		int counter = 0;
//		Color defaultC = Handles.color;
//		MeshFilter[] tempChilds = script.gameObject.GetComponentsInChildren<MeshFilter>();
//		Vector3 normal1 = Vector3.one;
//		Vector3 normal2= Vector3.one;
//		foreach(MeshFilter mFilter in tempChilds)
//		{
//			if (script.text.SubMeshCount == 3)
//			{
//				int[] pointsIndex = mFilter.sharedMesh.GetTriangles(2);
//				int[] pointsIndex1 = mFilter.sharedMesh.GetTriangles(0);
//				int[] pointsIndex2 = mFilter.sharedMesh.GetTriangles(1);
//				Vector3[] points = new Vector3[3];
//				int triIndex = 0;
//				for (int i = 0; i < pointsIndex.Length;i++)
//				{
//
//
//
//
////					for (int y = 0; y < pointsIndex1.Length;y++)
////					{
////						if (pointsIndex[i] == pointsIndex1[y])
////							Handles.color = Color.red;
////						else Handles.color = defaultC;
////					}
//					if (i < 5 || (i >pointsIndex.Length-4 ))
//					{
//					points[triIndex] = mFilter.sharedMesh.vertices[pointsIndex[i]];
//					Handles.Label(points[triIndex],i.ToString());
//					if (triIndex>=2){
//						if (counter <2){
//							Handles.color = Color.red;
//							normal1 = Vector3.Cross(points[1]-points[0],points[2]-points[0]);
//						}
//						else{ 
//							Handles.color = defaultC;
//							normal2 = Vector3.Cross(points[1]-points[0],points[2]-points[0]);
////							Debug.Log (Vector3.Angle(normal1,normal2));
//						}
//						counter++;
//						if (counter > 3)
//							counter = 0;
//
//						Handles.DrawAAConvexPolygon(points);
//					}
//					triIndex++;
//					if (triIndex >2)triIndex = 0;
//					}
//				}
//
//			}
//		}
//
////		int fontCharacterIndex = 0;
////		if(script.vectorFont.CharacterMap.TryGetValue('d', out fontCharacterIndex)) {
//////			Debug.Log (script.vectorFont.characters[fontCharacterIndex].Vertices.Count);
////			for (int i = 0;i< script.vectorFont.characters[fontCharacterIndex].Vertices.Count;i++)
////			{
////				Handles.Label(script.vectorFont.characters[fontCharacterIndex].Vertices[i],i.ToString());
////			}
////		}
//	}


}



