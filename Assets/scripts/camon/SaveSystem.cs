using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem 
{
    public static bool isSaved = false;
    public static void SavePlayer()
    {
        SaveSystem.isSaved = true;
        SaveIsSaved();
    }



    public static PlayerData LoadPlayer()
    {

        string path = Application.persistentDataPath + "/hero.save";
        //Debug.Log(path);
        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();


            return data;
        }
        else
        {

            return null;
        }
    }

    public static void DeletePlayer()
    {
        SaveSystem.isSaved = false;
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/hero.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();

        formatter.Serialize(stream, data);
        stream.Close();
        SaveIsSaved();
    }
    public static void SaveIsSaved()
    {
        if (isSaved) PlayerPrefs.SetInt("isSaved", 1);
        else PlayerPrefs.DeleteKey("isSaved");

        PlayerPrefs.Save();
    }
}
