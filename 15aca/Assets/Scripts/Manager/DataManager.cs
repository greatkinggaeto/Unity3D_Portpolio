using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public struct CharacterTableData
    {
        public int Key;
        public string Name;
        public float Speed;
        public float MaxHp;
        public float Power;
        public float JumpPower;
        public string Type;
    }
    // Start is called before the first frame update

    static private DataManager _instance;

    static public DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("Manager");
                _instance = obj.AddComponent<DataManager>();
            }

            return _instance;
        }
    }

    private void LoadCharacterTable()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("TextData/CharacterData");
        string temp = textAsset.text.Replace("\r\n","\n");

        // \n은 줄바꿈
        string[] str = textAsset.text.Split('\n');

        for(int i =1; i< str.Length; i++)
        {
            string[] data = str[i].Split(',');

            if (data.Length < 2) return;

            CharacterTableData characterData;
            characterData.Key = int.Parse(data[0]);
            characterData.Name = data[1];
            characterData.Speed = float.Parse(data[2]);
            characterData.MaxHp = float.Parse(data[3]);
            characterData.Power = float.Parse(data[4]);
            characterData.JumpPower = float.Parse(data[5]);
            characterData.Type = data[6];
            characterDatas.Add(characterData.Key, characterData);
        }
    }

    public CharacterTableData GetCharacterData(int key)
    {
        return characterDatas[key];
    }

    // 해쉬테이블에어울리는  dictionary
     private Dictionary<int, CharacterTableData> characterDatas = new Dictionary<int, CharacterTableData>();
    public void LoadData()
    {
        LoadCharacterTable();

    }


}
