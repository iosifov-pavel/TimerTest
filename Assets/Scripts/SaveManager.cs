using System.IO;
using UnityEngine;

public static class SaveManager
{
    public static void SaveData(SaveData data)
    {
        var savePath = Application.persistentDataPath + Constants.SavePath;
        using (FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate))
        {
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                var jsonData = JsonUtility.ToJson(data);
                bw.Write(jsonData);
            }
        }
    }

    public static SaveData LoadData()
    {
        SaveData result = null;
        var savePath = Application.persistentDataPath + Constants.SavePath;
        using (FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate))
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                while (br.BaseStream.Length != br.BaseStream.Position)
                {
                    var jsonData = br.ReadString();
                    result = JsonUtility.FromJson<SaveData>(jsonData);
                }
            }
        }
        return result;
    }
}
