using System.IO;
using System.Text;
using UnityEngine;

public static class SaveManager
{
    public static void SaveData(SaveData data)
    {
        var savePath = Application.persistentDataPath + Constants.SavePath;
        using (FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate))
        {
            using (BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8, false))
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
            using (BinaryReader br = new BinaryReader(fs, Encoding.UTF8, false))
            {
                while (br.BaseStream.Length != br.BaseStream.Position)
                {
                    try
                    {
                        var jsonData = br.ReadString();
                        result = JsonUtility.FromJson<SaveData>(jsonData);
                    }
                    catch
                    {
                        Debug.LogError("Load data failed!");
                        br.Close();
                    }
                }
            }
        }
        return result;
    }
}
