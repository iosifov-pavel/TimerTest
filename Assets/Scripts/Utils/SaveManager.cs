using System.IO;
using System.Text;
using UnityEngine;

public static class SaveManager
{
    public static void SaveData(SaveData data)
    {
        var savePath = Application.persistentDataPath + Constants.SavePath;
        using (FileStream fs = new FileStream(savePath, FileMode.Create))
        {
            using (BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8, false))
            {
                var jsonData = JsonUtility.ToJson(data);
                fs.SetLength(0);
                bw.Write(jsonData);
            }
        }
    }

    public static SaveData LoadData()
    {
        var failedToLoad = false;
        SaveData result = new SaveData();
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
                        Debug.LogError("Save data corrupted!");
                        failedToLoad = true;
                        br.Close();
                        break;
                    }
                }
            }
        }
        if (failedToLoad)
        {
            SaveData(result);
        }
        return result;
    }
}