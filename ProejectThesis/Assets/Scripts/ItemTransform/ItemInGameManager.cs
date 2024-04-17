using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemInGameManager : MonoBehaviour
{
    public string dataPaths;
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
        GameObject[] itemObjects = GameObject.FindGameObjectsWithTag("Interact");

        foreach (GameObject item in itemObjects)
        {
            SetItemTransform setItemTransform = item.GetComponent<SetItemTransform>();
            if (setItemTransform != null)
            {
                items.Add(setItemTransform);
                itemObject.Add(setItemTransform.itemTransform);
            }
        }
    }


    void Update()
    {

    }

    public void SaveData()
    {
        itemTransformsData = itemObject;

        string dataPath = Application.persistentDataPath + "/" + dataPaths +".json";

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
        string dataPath = Application.persistentDataPath + "/" + dataPaths+".json";
        Debug.Log(dataPath);
        if(File.Exists(dataPath))
        {
            string[] jsonDataArray = File.ReadAllLines(dataPath);

            itemTransformsData.Clear();

            foreach(string jsonData in jsonDataArray)
            {
                ItemTransform itemTransform = JsonUtility.FromJson<ItemTransform>(jsonData);
                itemTransformsData.Add(itemTransform);
            }

         // ตรวจสอบขนาดของรายการเพื่อป้องกันการเกินของดัชนี
            int itemCount = Mathf.Min(items.Count, itemTransformsData.Count);

            for (int i = 0; i < itemCount; i++)
            {
                if (itemTransformsData[i].uniqueId == items[i].uniqueId)
                {
                    items[i].transform.position = itemTransformsData[i].Position;
                    items[i].transform.rotation = itemTransformsData[i].Rotation;
                }
                else // ถ้า uniqueId ไม่ตรงกัน
                {
                    // ให้ทำการ Instantiate ไอเท็มใหม่แล้วเพิ่มเข้าไปในรายการ items
                    InstantiateItemObject(itemTransformsData[i].prefab, itemTransformsData[i].Position, itemTransformsData[i].Rotation);
                }
            }

            itemObject.Clear();

            foreach (SetItemTransform item in items)
            {
                SetItemTransform setItemTransform = item.GetComponent<SetItemTransform>();
                if (setItemTransform != null)
                {
                    //items.Add(setItemTransform);
                    itemObject.Add(setItemTransform.itemTransform);
                }
            }
        }
    }

    public void InstantiateItemObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject ins = Instantiate(prefab, position, rotation);

        SetItemTransform setItemTransform = ins.GetComponent<SetItemTransform>();
        items.Add(setItemTransform);
    }
}
