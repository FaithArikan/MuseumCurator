using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ArcadeIdle.Helpers;

namespace ArcadeIdle.SaveSystem
{
    public class SaveManager : PersistentSingleton<SaveManager>, IGameSave
    {
	    public void Save(string filename, object data) 
	    {
		    FileStream file = File.Open(GetFullPath(filename), FileMode.OpenOrCreate);
		    BinaryFormatter bf = new BinaryFormatter();
		    try 
		    {
			    bf.Serialize(file, data);
		    } 
		    catch (SerializationException e) 
		    {
			    Debug.Log("Save failed: " + e.Message);
		    } 
		    finally 
		    {
			    file.Close();
		    }
	    }

	    public T Load<T>(string filename) where T : new() 
	    {
		    T result = new T();

		    if (!FileExists(filename)) 
		    {
			    return result;
		    }
		    FileStream file = File.Open(GetFullPath(filename), FileMode.Open);
		    BinaryFormatter bf = new BinaryFormatter();

		    try 
		    {
			    result = (T)bf.Deserialize(file);
		    } 
		    catch (SerializationException e)
		    {
			    Debug.Log("Load failed: " + e.Message);
		    } 
		    finally 
		    {
			    file.Close();
		    }
		    return result;
	    }
	    public bool FileExists(string filename)
	    {
		    return File.Exists(GetFullPath(filename));
	    }
	    public void Delete(string filename) 
	    {
		    File.Delete(GetFullPath(filename));
	    }
	    public string GetFullPath(string filename)
	    {
		    return Path.Combine(Application.persistentDataPath, filename);
	    }
	    public void WriteToDisk() { }
    }
}