using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner Instance;

    public List<ItemObject> objects;
    public float minRadius = 2f;
    public float maxRadius = 10f;

    public Transform itemPickerTf;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void SpawnItem(SO_Item item, int amount) // Drop form Player
    {
        if(item.gamePrefab == null)
        {
            Debug.LogError("No prefab in " + item.name);
            return;
        }

        Vector2 ranPos = Random.insideUnitCircle.normalized * minRadius;
        Vector3 offSet = new Vector3(ranPos.x,0, ranPos.y);
        GameObject spawnItem = Instantiate(item.gamePrefab, itemPickerTf.position + offSet, Quaternion.identity);

        spawnItem.GetComponent<ItemObject>().SetAmout(amount);
    }

    public void SpawnItemByGUI(int amount = 1) // Drop form Player
    {
        for(int i = 0; i < amount; i++)
        {
            int ind = Random.Range(0, objects.Count);
            float distance = Random.Range(minRadius, maxRadius);
            Vector2 ranPoint = Random.insideUnitCircle.normalized * distance;
            Vector3 offSet = new Vector3(ranPoint.x,0, ranPoint.y);
           ItemObject ItemObjectspawn = Instantiate(objects[ind], itemPickerTf.position + offSet, Quaternion.identity);
            ItemObjectspawn.RandomAmout();
        }

    }

    private void OnGUI()
    {
        if(GUILayout.Button("Spawn A Random Item"))
        {
            SpawnItemByGUI();
        }

        if (GUILayout.Button("Spawn 10 Random Item"))
        {
            SpawnItemByGUI(10);
        }

        if (GUILayout.Button("Spawn 20 Random Item"))
        {
            SpawnItemByGUI(20);
        }
    }

}
