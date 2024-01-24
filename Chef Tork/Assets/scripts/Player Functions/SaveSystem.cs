using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveSystem{

    public static void SavePlayer(PlayerController playercontroller)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.lol";
        FileStream stream = new FileStream(path, FileMode.Create);

        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerData data = new PlayerData(playercontroller, currentLevelIndex);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.lol";
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
            Debug.Log("No save" + path);
            return null;
        }
    }
}
