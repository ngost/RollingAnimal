using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using UnityEngine.UI;

[Serializable]
public class GameDataLoader
{
    // Start is called before the first frame update
    public string jsonData;
    
    public GameDataLoader()
    {
		//        Debug.Log(Application.persistentDataPath);
		

		if (LoadData() == null)
        {
            InventoryClass jtc = new InventoryClass(true);
            jsonData = ObjectToJson(jtc);
			Debug.Log("json: "+jsonData);

            WriteData(JsonToOject<InventoryClass>(jsonData));
        }
    }

    string ObjectToJson(object obj)
	{
		return JsonConvert.SerializeObject(obj);
	}

	T JsonToOject<T>(string jsonData)
	{
		return JsonConvert.DeserializeObject<T>(jsonData);
	}


   
    void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
		createPath = Path.Combine(createPath, "gameData");

		if (!Directory.Exists(createPath))
		{
//			Directory.CreateDirectory(createPath);
			DirectoryInfo dir_info = new DirectoryInfo(createPath);
			dir_info.Create();

            

            if (!dir_info.Attributes.HasFlag(FileAttributes.Hidden)){
				dir_info.Attributes = dir_info.Attributes | FileAttributes.Hidden;

                
            }
		}
		FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();

        FileStream fileStreamForHidden = new FileStream(string.Format("{0}/{1}.nomedia", createPath, ""), FileMode.Create);
        byte[] dataForHidden = Encoding.UTF8.GetBytes("prevent");
        fileStreamForHidden.Write(dataForHidden, 0, dataForHidden.Length);
        fileStreamForHidden.Close();


    }

    //게임 데이터 변경사항 적용 함수
    public void WriteData(InventoryClass obj)
    {
		Debug.Log(Application.persistentDataPath);
        string json = ObjectToJson(obj);
        CreateJsonFile(Application.persistentDataPath,"inventory",json);
    }

    //저장된 게임 데이터 로드 함수
    public InventoryClass LoadData()
    {
        
        try
        {
			string dir = Path.Combine(Application.persistentDataPath, "gameData");
			if (!Directory.Exists(dir))
			{
				DirectoryInfo dir_info = new DirectoryInfo(dir);
				dir_info.Create();
				if (!dir_info.Attributes.HasFlag(FileAttributes.Hidden))
				{
					dir_info.Attributes = dir_info.Attributes | FileAttributes.Hidden;
				}
			}



			FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", dir, "inventory"), FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();

			string jsonData = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject<InventoryClass>(jsonData);
        }
        catch (FileNotFoundException e)
        {
			Debug.Log("null");
            return null;
        }
    }
}
