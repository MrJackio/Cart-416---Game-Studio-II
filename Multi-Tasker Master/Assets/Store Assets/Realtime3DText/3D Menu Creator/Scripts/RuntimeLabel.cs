using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RuntimeLabel : MonoBehaviour {
	
	public string words = "Text";
	Real3DFont tempReal3DFont;

	// Use this for initialization
	void Start () {
		tempReal3DFont = gameObject.GetComponentInChildren<Real3DFont>();
		tempReal3DFont.vectorFont.toDictionary();
	}
	
	// Update is called once per frame
	int lastLength;
	void Update()
	{
		if (lastLength != words.Length){
			lastLength = words.Length;
			tempReal3DFont.textToPrint = words;
			process(tempReal3DFont);
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

}
