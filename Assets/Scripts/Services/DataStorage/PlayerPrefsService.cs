using UnityEngine;

public class PlayerPrefsService : NativeSingleton<PlayerPrefsService>, IPrimitiveDataManager
{
    public float LoadFloatPrimitive(string name)
    {
        return PlayerPrefs.GetFloat(name, 0f);
    }

    public int LoadIntPrimitive(string name)
    {
        return PlayerPrefs.GetInt(name, 0);
    }

    public string LoadStringPrimitive(string name)
    {
        return PlayerPrefs.GetString(name, "");
    }

    public void SavePrimitive(string name, string data)
    {
        PlayerPrefs.SetString(name, data);
    }

    public void SavePrimitive(string name, int data)
    {
        PlayerPrefs.SetInt(name, data);
    }

    public void SavePrimitive(string name, float data)
    {
        PlayerPrefs.SetFloat(name, data);
    }
}