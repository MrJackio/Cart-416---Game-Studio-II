
using UnityEngine;    // For Debug.Log, etc.
using System.Collections;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System;
using System.Runtime.Serialization;
using System.Reflection;

// === This is the info container class ===
[Serializable ()]
public class LoadNSave : ISerializable {

  // === Values ===
  // Edit these during gameplay
  public Hashtable events;
  // === /Values ===

  // The default constructor. Included for when we call it during Save() and Load()
  public LoadNSave () {}

  // This constructor is called automatically by the parent class, ISerializable
  // We get to custom-implement the serialization process here
  public LoadNSave (SerializationInfo info, StreamingContext ctxt)
  {
    // Get the values from info and assign them to the appropriate properties. Make sure to cast each variable.
    // Do this for each var defined in the Values section above
    events = (Hashtable)info.GetValue("events", typeof(Hashtable));
  }

  // Required by the ISerializable class to be properly serialized. This is called automatically
  public void GetObjectData (SerializationInfo info, StreamingContext ctxt)
  {
    // Repeat this for each var defined in the Values section
    info.AddValue("events", (events));
  }
}

// === This is the class that will be accessed from scripts ===
public class SaveLoad {

  public static string currentFilePath = "SaveData.cjc";    // Edit this for different save files

  // Call this to write data
  public static void Save (LoadNSave data)  // Overloaded
  {
    Save (currentFilePath, data);
  }
  public static void Save (string filePath, LoadNSave data)
  {
//    LoadNSave data = new LoadNSave ();

    Stream stream = File.Open(filePath, FileMode.Create);
    BinaryFormatter bformatter = new BinaryFormatter();
    bformatter.Binder = new VersionDeserializationBinder1(); 
    bformatter.Serialize(stream, data);
    stream.Close();
  }

  // Call this to load from a file into "data"
  public static void Load ()  { Load(currentFilePath);  }   // Overloaded
  public static LoadNSave Load (string filePath) 
  {
    LoadNSave data = new LoadNSave ();
    Stream stream = File.Open(filePath, FileMode.Open);
    BinaryFormatter bformatter = new BinaryFormatter();
    bformatter.Binder = new VersionDeserializationBinder1(); 
    data = (LoadNSave)bformatter.Deserialize(stream);
    stream.Close();
	return data;

    // Now use "data" to access your Values
  }

}

// === This is required to guarantee a fixed serialization assembly name, which Unity likes to randomize on each compile
// Do not change this
public sealed class VersionDeserializationBinder1 : SerializationBinder 
{ 
    public override Type BindToType( string assemblyName, string typeName )
    { 
        if ( !string.IsNullOrEmpty( assemblyName ) && !string.IsNullOrEmpty( typeName ) ) 
        { 
            Type typeToDeserialize = null; 

            assemblyName = Assembly.GetExecutingAssembly().FullName; 

            // The following line of code returns the type. 
            typeToDeserialize = Type.GetType( String.Format( "{0}, {1}", typeName, assemblyName ) ); 

            return typeToDeserialize; 
        } 

        return null; 
    } 
}