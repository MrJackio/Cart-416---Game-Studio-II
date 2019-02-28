using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
//using Real3DText.Fonts.VectorizedFont;
using Real3DText.Fonts;


public class Real3DFont : MonoBehaviour {
	 	
	public bool isSubUsed = true;
	public string textToPrint;
	public int fontSize;
//	public Dictionary<string,List<fontDesc>> fontlist = new Dictionary<string, List<fontDesc>>();
	public string[] fontsName;
	[SerializeField]
	public List<FontStyle[]> fontStyle;
	public int fontNameIndex;
	public int fontStyleIndex;
    public int fontResolution = 3;
	[SerializeField]
	public ExtrudedText text;
	[SerializeField]
	public VectorFont vectorFont;
	public float depth = 0.2f;
	public float smoothingAngle = 60f;
	public MeshType meshType;
	public bool individualFaceMaterials = false;
	public enum MeshType{
		Line,
		Word,
		Letter
	}

	public bool useMeshMaterials = true;
	public bool individualMeshMaterials = true;

}
