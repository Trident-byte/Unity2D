using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/saves/";
    public static readonly string FILE_EXT = ".json";

    public static void Initalize()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string fileName, string data)
    {
        File.WriteAllText(SAVE_FOLDER + fileName + FILE_EXT, data);
    }

    public static string Load(string fileName)
    {
        string fileLocation = SAVE_FOLDER + fileName + FILE_EXT;
        if (File.Exists(fileLocation))
        {
            string loadedData = File.ReadAllText(fileLocation);
            return loadedData;
        }
        else
        {
            return null;
        }
    }
}
