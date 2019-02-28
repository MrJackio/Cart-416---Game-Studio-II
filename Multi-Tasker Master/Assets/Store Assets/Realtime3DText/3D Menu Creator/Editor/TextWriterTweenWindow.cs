using System;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TextWriterTweenWindow : EditorWindow {
	
	events[] events1;
	TextWriterMenuEditor tempEditor;
	TextWriterVisual.TweenType type;
//	int[] selectedValue= new int[8];
//	Dictionary<string, bool> propertiesEnabled = new Dictionary<string, bool>();
//	string[] keys;
	int Index1;
	int Index2;
	Dictionary<string, object> values;
	Dictionary<string, bool> propertiesEnabled = new Dictionary<string, bool>();
	string wTitle;
	Vector2 scrollPos;
	
//	TextWriterVisual.TweenType previousType = TextWriterVisual.TweenType.ColorFrom;
	
	public Dictionary<string, object> Values {
		get { 
			if(null == values1) {
				DeserializeValues();
			}
			return values1;
		}
		set {
			values1 = value;
			SerializeValues();
		}
	}
	
	Dictionary<string, object> values1;
	
	public void Init(TextWriterVisual.TweenType Tweentype,  events[] key1, int ii, int indexI, TextWriterMenuEditor tE, string windowTitle)
    {
		tempEditor = tE;
		Index1 = ii;
		Index2 = indexI;
		events1 = key1;
		type = Tweentype;
		wTitle = windowTitle;
		DeserializeValues();
		
		this.title = windowTitle;
		
		foreach(var key in TextWriterParamMapping.mappings[Tweentype].Keys) {
			propertiesEnabled[key] = false;
		}
    }
	
	
	public void Init2(TextWriterVisual.TweenType Tweentype, events[] key1, int ii, int indexI, TextWriterMenuEditor tE, string windowTitle)
    {
		tempEditor = tE;
		Index1 = ii;
		Index2 = indexI;
		events1 = key1;
		type = Tweentype;
		wTitle = windowTitle;
//		previousType = type;
		this.title = windowTitle;
			foreach(var key in TextWriterParamMapping.mappings[type].Keys) {
				propertiesEnabled[key] = false;
			}
		Values = new Dictionary<string, object>();
//		DeserializeValues();
		
		
    }
	
		
	void OnGUI() {
		
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width), GUILayout.Height(position.height));
		
		GUI.changed = false;
		if (Application.isPlaying && !tempEditor.script.cleanOnceTween)
		{
			GUILayout.Space(position.height/2);
			GUILayout.BeginHorizontal();
			GUILayout.Box("3D Menu Creator\nTween Paramters\nPlease re-open this window\nto refresh values",GUILayout.Width(255));
			GUILayout.EndHorizontal();
			return;
		}else if (!Application.isPlaying && tempEditor.script.cleanOnceTween)
		{
			GUILayout.Space(position.height/2);
			GUILayout.BeginHorizontal();
			GUILayout.Box("3D Menu Creator\nTween Paramters\nPlease re-open this window\nto refresh values",GUILayout.Width(255));
			GUILayout.EndHorizontal();
			return;
		}
		
		GUILayout.BeginHorizontal();
		GUILayout.Space((position.width/2 - 55));
		GUILayout.Label(tempEditor.script.logoiTween,GUILayout.Width(255));
		GUILayout.EndHorizontal();
		
		
		values = Values;
		var keys = values.Keys.ToArray();
		
		type = events1[Index1].tweens[Index2].type;
		
		foreach(var key in keys) {
			propertiesEnabled[key] = true;
			if(typeof(Vector3OrTransform) == TextWriterParamMapping.mappings[type][key]) {
				var val = new Vector3OrTransform();
				
				if(null == values[key] || typeof(Transform) == values[key].GetType()) {
					if(null == values[key]) {
						val.transform = null;
					}
					else {
						val.transform = (Transform)values[key];
					}
					val.selected = Vector3OrTransform.transformSelected;
				}
				else if(typeof(Vector3) == values[key].GetType()) {
					val.vector = (Vector3)values[key];
					val.selected = Vector3OrTransform.vector3Selected;
				}
				
				values[key] = val;
			}
			if(typeof(Vector3OrTransformArray) == TextWriterParamMapping.mappings[type][key]) {
				var val = new Vector3OrTransformArray();
				
				if(null == values[key] || typeof(Transform[]) == values[key].GetType()) {
					if(null == values[key]) {
						val.transformArray = null;
					}
					else {
						val.transformArray = (Transform[])values[key];
					}
					val.selected = Vector3OrTransformArray.transformSelected;
				}
				else if(typeof(Vector3[]) == values[key].GetType()) {
					val.vectorArray = (Vector3[])values[key];
					val.selected = Vector3OrTransformArray.vector3Selected;
				}
				else if(typeof(string) == values[key].GetType()) {
					val.pathName = (string)values[key];
					val.selected = Vector3OrTransformArray.iTweenPathSelected;
				}
				
				values[key] = val;
			}
		}

		GUILayout.Space(5);
		
		GUILayout.BeginHorizontal();
		GUILayout.TextArea("Now On: " + wTitle);
		GUILayout.EndHorizontal();
		
		GUILayout.Space(15);
		GUILayout.BeginHorizontal();
		GUILayout.Box("Event type: " +tempEditor.script.events[Index1] + "\niTween Type: " + events1[Index1].tweens[Index2].type, GUILayout.Width(255));
		GUILayout.EndHorizontal();
		

		
		GUILayout.Space(25);
		
		GUILayout.BeginHorizontal();
		GUILayout.Label("Initial Start Delay:");
		events1[Index1].tweens[Index2].delay = EditorGUILayout.FloatField(events1[Index1].tweens[Index2].delay);
		GUILayout.EndHorizontal();
		
		EditorGUILayout.Separator();
		
//		if (previousType != type)
//		{
//			foreach(var key in TextWriterParamMapping.mappings[type].Keys) {
//				propertiesEnabled[key] = false;
//			}
//			Values = new Dictionary<string, object>();
//			previousType = type;
//			return;			
//		}
		
		var properties = TextWriterParamMapping.mappings[type];
		foreach(var pair in properties) {
			var key = pair.Key;
			
			GUILayout.BeginHorizontal();
			
			if(EditorGUILayout.BeginToggleGroup(key, propertiesEnabled[key])) {
				propertiesEnabled[key] = true;
				
				GUILayout.BeginVertical();
			
				if(typeof(string) == pair.Value) {
					values[key] = EditorGUILayout.TextField(values.ContainsKey(key) ? (string)values[key] : "");
				}
				else if(typeof(float) == pair.Value) {
					values[key] = EditorGUILayout.FloatField(values.ContainsKey(key) ? (float)values[key] : 0);
				}
				else if(typeof(int) == pair.Value) {
					values[key] = EditorGUILayout.IntField(values.ContainsKey(key) ? (int)values[key] : 0);
				}
				else if(typeof(bool) == pair.Value) {
					values[key] = propertiesEnabled[key];
				}
				else if(typeof(GameObject) == pair.Value) {
					values[key] = EditorGUILayout.ObjectField(values.ContainsKey(key) ? (GameObject)values[key] : null, typeof(GameObject), true);
				}
				else if(typeof(Vector3) == pair.Value) {
					values[key] = EditorGUILayout.Vector3Field("", values.ContainsKey(key) ? (Vector3)values[key] : Vector3.zero);
				}
				else if(typeof(Vector3OrTransform) == pair.Value) {
					if(!values.ContainsKey(key)) {
						values[key] = new Vector3OrTransform();
					}
					var val = (Vector3OrTransform)values[key];
					
					val.selected = GUILayout.SelectionGrid(val.selected, Vector3OrTransform.choices, 2);
	
					if(Vector3OrTransform.vector3Selected == val.selected) {
						val.vector = EditorGUILayout.Vector3Field("", val.vector);
					}
					else {
						val.transform = (Transform)EditorGUILayout.ObjectField(val.transform, typeof(Transform), true);
					}
					values[key] = val;
				}
				else if(typeof(Vector3OrTransformArray) == pair.Value) {
					if(!values.ContainsKey(key)) {
						values[key] = new Vector3OrTransformArray();
					}
					var val = (Vector3OrTransformArray)values[key];
					val.selected = GUILayout.SelectionGrid(val.selected, Vector3OrTransformArray.choices, Vector3OrTransformArray.choices.Length);

					if(Vector3OrTransformArray.vector3Selected == val.selected) {
						if(null == val.vectorArray) {
							val.vectorArray = new Vector3[0];
						}
						var elements = val.vectorArray.Length;
						GUILayout.BeginHorizontal();
							GUILayout.Label("Number of points");
							elements = EditorGUILayout.IntField(elements);
						GUILayout.EndHorizontal();
						if(elements != val.vectorArray.Length) {
							var resizedArray = new Vector3[elements];
							val.vectorArray.CopyTo(resizedArray, 0);
							val.vectorArray = resizedArray;
						}
						for(var i = 0; i < val.vectorArray.Length; ++i) {
							val.vectorArray[i] = EditorGUILayout.Vector3Field("", val.vectorArray[i]);
						}
					}
					else if(Vector3OrTransformArray.transformSelected == val.selected) {
						if(null == val.transformArray) {
							val.transformArray = new Transform[0];
						}
						var elements = val.transformArray.Length;
						GUILayout.BeginHorizontal();
							GUILayout.Label("Number of points");
							elements = EditorGUILayout.IntField(elements);
						GUILayout.EndHorizontal();
						if(elements != val.transformArray.Length) {
							var resizedArray = new Transform[elements];
							val.transformArray.CopyTo(resizedArray, 0);
							val.transformArray = resizedArray;
						}
						for(var i = 0; i < val.transformArray.Length; ++i) {
							val.transformArray[i] = (Transform)EditorGUILayout.ObjectField(val.transformArray[i], typeof(Transform), true);
						}
					}
					else if(Vector3OrTransformArray.iTweenPathSelected == val.selected) {
						var index = 0;
						var paths = (GameObject.FindObjectsOfType(typeof(iTweenPath)) as iTweenPath[]);
						if(0 == paths.Length) {
							val.pathName = "";
							GUILayout.Label("No paths are defined");
						}
						else {
							for(var i = 0; i < paths.Length; ++i) {
								if(paths[i].pathName == val.pathName) {
									index = i;
								}
							}
							index = EditorGUILayout.Popup(index, (GameObject.FindObjectsOfType(typeof(iTweenPath)) as iTweenPath[]).Select(path => path.pathName).ToArray());	
							
							val.pathName = paths[index].pathName;
						}
					}
					values[key] = val;
				}
				else if(typeof(iTween.LoopType) == pair.Value) {
					values[key] = EditorGUILayout.EnumPopup(values.ContainsKey(key) ? (iTween.LoopType)values[key] : iTween.LoopType.none);
				}
				else if(typeof(iTween.EaseType) == pair.Value) {
					values[key] = EditorGUILayout.EnumPopup(values.ContainsKey(key) ? (iTween.EaseType)values[key] : iTween.EaseType.linear);
				}
				else if(typeof(AudioSource) == pair.Value) {
					values[key] = (AudioSource)EditorGUILayout.ObjectField(values.ContainsKey(key) ? (AudioSource)values[key] : null, typeof(AudioSource), true);
				}
				else if(typeof(AudioClip) == pair.Value) {
					values[key] = (AudioClip)EditorGUILayout.ObjectField(values.ContainsKey(key) ? (AudioClip)values[key] : null, typeof(AudioClip), true);
				}
				else if(typeof(Color) == pair.Value) {
					values[key] = EditorGUILayout.ColorField(values.ContainsKey(key) ? (Color)values[key] : Color.white);
				}
				else if(typeof(Space) == pair.Value) {
					values[key] = EditorGUILayout.EnumPopup(values.ContainsKey(key) ? (Space)values[key] : Space.Self);
				}
				
				GUILayout.EndVertical();
			}
			else {
				propertiesEnabled[key] = false;
				values.Remove(key);
			}
			
			EditorGUILayout.EndToggleGroup();
			GUILayout.EndHorizontal();
			EditorGUILayout.Separator();
		}
		
		keys = values.Keys.ToArray();
		
		foreach(var key in keys) {
			if(values[key] != null && values[key].GetType() == typeof(Vector3OrTransform)) {
				var val = (Vector3OrTransform)values[key];
				if(Vector3OrTransform.vector3Selected == val.selected) {
					values[key] = val.vector;
				}
				else {
					values[key] = val.transform;
				}
			}
			else if(values[key] != null && values[key].GetType() == typeof(Vector3OrTransformArray)) {
				var val = (Vector3OrTransformArray)values[key];
				if(Vector3OrTransformArray.vector3Selected == val.selected) {
					values[key] = val.vectorArray;
				}
				else if(Vector3OrTransformArray.transformSelected == val.selected) {
					values[key] = val.transformArray;
				}
				else if(Vector3OrTransformArray.iTweenPathSelected == val.selected) {
					values[key] = val.pathName;
				}
			}
		}
		Values = values;
//		previousType = type;
		EditorGUILayout.EndScrollView();
		
		if (GUI.changed &&tempEditor.script.menuComponentsIndex > 0)
			tempEditor.applyGlobalParameters(tempEditor.script.components, TextWriterMenuEditor.Activity.none);
		
	}
	
	
	

	
	
	
	
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
		
		if(null == events1[Index1].tweens[Index2].keys) {
			return;
		}
		
		for(var i = 0; i < events1[Index1].tweens[Index2].keys.Length; ++i) {
			var mappings = TextWriterParamMapping.mappings[type];
			var valueType = mappings[events1[Index1].tweens[Index2].keys[i]];
			int[] indexes = events1[Index1].tweens[Index2].indexes;
			if(typeof(int) == valueType) {
				values1.Add(events1[Index1].tweens[Index2].keys[i], events1[Index1].tweens[Index2].ints[indexes[i]]);
			}
			else if(typeof(float) == valueType) {
				values1.Add(events1[Index1].tweens[Index2].keys[i], events1[Index1].tweens[Index2].floats[indexes[i]]);
			}
			else if(typeof(bool) == valueType) {
				values1.Add(events1[Index1].tweens[Index2].keys[i], events1[Index1].tweens[Index2].bools[indexes[i]]);
			}
			else if(typeof(string) == valueType) {
				values1.Add(events1[Index1].tweens[Index2].keys[i], events1[Index1].tweens[Index2].strings[indexes[i]]);
			}
			else if(typeof(Vector3) == valueType) {
				values1.Add(events1[Index1].tweens[Index2].keys[i], events1[Index1].tweens[Index2].vector3s[indexes[i]]);
			}
			else if(typeof(Color) == valueType) {
				values1.Add(events1[Index1].tweens[Index2].keys[i], events1[Index1].tweens[Index2].colors[indexes[i]]);
			}
			else if(typeof(Space) == valueType) {
				values1.Add(events1[Index1].tweens[Index2].keys[i], events1[Index1].tweens[Index2].spaces[indexes[i]]);
			}
			else if(typeof(iTween.EaseType) == valueType) {
				values1.Add(events1[Index1].tweens[Index2].keys[i], events1[Index1].tweens[Index2].easeTypes[indexes[i]]);
			}
			else if(typeof(iTween.LoopType) == valueType) {
				values1.Add(events1[Index1].tweens[Index2].keys[i], events1[Index1].tweens[Index2].loopTypes[indexes[i]]) ;
			}
			else if(typeof(GameObject) == valueType) {
				values1.Add(events1[Index1].tweens[Index2].keys[i], events1[Index1].tweens[Index2].gameObjects[indexes[i]]) ;
			}
			else if(typeof(Transform) == valueType) {
				values1.Add(events1[Index1].tweens[Index2].keys[i], events1[Index1].tweens[Index2].transforms[indexes[i]]) ;
			}
			else if(typeof(Vector3OrTransform) == valueType) {
				if("v" == events1[Index1].tweens[Index2].metadatas[i]) {
					values1.Add(events1[Index1].tweens[Index2].keys[i], events1[Index1].tweens[Index2].vector3s[indexes[i]]) ;
				}
				else if("t" == events1[Index1].tweens[Index2].metadatas[i]) {
					values1.Add(events1[Index1].tweens[Index2].keys[i], events1[Index1].tweens[Index2].transforms[indexes[i]]) ;
				}
			}
			else if(typeof(Vector3OrTransformArray) == valueType) {
				if("v" == events1[Index1].tweens[Index2].metadatas[i]) {
					var arrayIndexes = events1[Index1].tweens[Index2].vector3Arrays[indexes[i]];
					var vectorArray = new Vector3[arrayIndexes.indexes.Length];
					for(var idx = 0; idx < arrayIndexes.indexes.Length; ++idx) {
						vectorArray[idx] = events1[Index1].tweens[Index2].vector3s[arrayIndexes.indexes[idx]];
					}
					
					values1.Add(events1[Index1].tweens[Index2].keys[i], vectorArray);
				}
				else if("t" == events1[Index1].tweens[Index2].metadatas[i]) {
					var arrayIndexes = events1[Index1].tweens[Index2].transformArrays[indexes[i]];
					var transformArray = new Transform[arrayIndexes.indexes.Length];
					for(var idx = 0; idx < arrayIndexes.indexes.Length; ++idx) {
						transformArray[idx] = events1[Index1].tweens[Index2].transforms[arrayIndexes.indexes[idx]];
					}
					
					values1.Add(events1[Index1].tweens[Index2].keys[i], transformArray);
				}
				else if("p" == events1[Index1].tweens[Index2].metadatas[i]) {
					values1.Add(events1[Index1].tweens[Index2].keys[i], events1[Index1].tweens[Index2].strings[indexes[i]]);
				}
			}
		}
	}


	
	
	
}
