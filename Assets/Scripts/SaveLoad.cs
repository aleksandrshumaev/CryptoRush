using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SaveLoad : MonoBehaviour
{
    public void Save(int farmToolPoolCount, int coinPoolCount, Player player, Coin coin, List<DateTime> boxesTimers)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.cum";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData( coinPoolCount, farmToolPoolCount, player,coin,boxesTimers);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public PlayerData Load()
    {
        string path = Application.persistentDataPath + "/save.cum";
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
}
