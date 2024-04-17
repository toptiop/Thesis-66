using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemInGameManager : MonoBehaviour
{
    public static ItemInGameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public List<SetItemTransform> items = new List<SetItemTransform>();
    public List<ItemTransform> itemObject = new List<ItemTransform>();
    [SerializeField]private List<ItemTransform> itemTransformsData = new List<ItemTransform>();
    void Start()
    {
        
    }


    void Update()
    {

    }

    public void SaveData()
    {
        itemTransformsData = itemObject;

        string dataPath = Application.persistentDataPath + "/ItemInGame.json";

        List<string> jsonDataList = new List<string>();

        foreach(var DataPos in itemTransformsData)
        {
            string jsonData = JsonUtility.ToJson(DataPos);
            jsonDataList.Add(jsonData);
        }

        File.WriteAllLines(dataPath, jsonDataList);
    }

    public void LoadData()
    {
        string dataPath = Application.persistentDataPath + "/ItemInGame.json";

        if(File.Exists(dataPath))
        {
            string[] jsonDataArray = File.ReadAllLines(dataPath);

            itemTransformsData.Clear();

            foreach(string jsonData in jsonDataArray)
            {
                ItemTransform itemTransform = JsonUtility.FromJson<ItemTransform>(jsonData);
                itemTransformsData.Add(itemTransform);
            }

            for(int i = 0; i < itemTransformsData.Count; i++)
            {
                 InstantiateItemObject(itemTransformsData[i].prefab, new Vector3(itemTransformsData[i].Position.x, itemTransformsData[i].Position.y, itemTransformsData[i].Position.z), itemTransformsData[i].Rotation);
                
            }

            itemObject = itemTransformsData;
        }
    }

    public void InstantiateItemObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject ins = Instantiate(prefab, position, rotation);

        SetItemTransform setItemTransform = ins.GetComponent<SetItemTransform>();
        items.Add(setItemTransform);
    }
}
