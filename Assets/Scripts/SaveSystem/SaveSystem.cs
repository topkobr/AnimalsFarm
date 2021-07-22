using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static readonly string path = Application.persistentDataPath + "/save.prk";

    public static bool _isChanged;
    private static SaveData _save;
    public static SaveData GetSave()
    {
        Debug.Log("Get save");
        if (_save != null)
            return _save;
        else
            return _save = LoadProgress();
    }

    public static void CommitSave()
    {
        Debug.Log("Commit save");
        _isChanged = true;
    }

    public static void SaveProgress(SaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Save game to file: " + path);
        _isChanged = false;
    }

    public static SaveData LoadProgress()
    {
        if (!File.Exists(path))
        {
            SaveDefaultProgress();
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        SaveData data = formatter.Deserialize(stream) as SaveData;
        stream.Close();

        return data;
    }

    private static void SaveDefaultProgress()
    {
        Debug.Log("Save file not found, create..");
        SaveProgress(new SaveData());
    }

    public static void ResetProgress()
    {
        if (!File.Exists(path))
        {
            SaveDefaultProgress();
            return;
        }
        File.Delete(path);
        SaveDefaultProgress();
    }
}
