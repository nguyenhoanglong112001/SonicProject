using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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

    public void SaveListint(List<int> list,string key)
    {
        string json = string.Join(",", list);
        PlayerPrefs.SetString(key,json);
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

    public List<int> LoadListInt(string keyname)
    {
        if(!PlayerPrefs.HasKey(keyname)) return new List<int>();
        string json = PlayerPrefs.GetString(keyname);
        List<int> list = json.Split(",").Select(int.Parse).ToList();
        return list;
    }
}
