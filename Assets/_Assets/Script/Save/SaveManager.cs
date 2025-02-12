using UnityEditor;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Save(string keyname, int value)
    {
        PlayerPrefs.SetInt(keyname, value);
    }

    public void SaveString(string keyname, string objname)
    {
        PlayerPrefs.SetString(keyname, objname);
    }

    public void SaveFloat(string keyname, float value)
    {
        PlayerPrefs.SetFloat(keyname, value);
    }

    public int GetIntData(string keyname, int value)
    {
        return PlayerPrefs.GetInt(keyname, value);
    }

    public string GetStringData(string keyname, string value)
    {
        return PlayerPrefs.GetString(keyname, value);
    }

    public float GetFloatData(string keyname, float value)
    {
        return PlayerPrefs.GetFloat(keyname, value);
    }
}
