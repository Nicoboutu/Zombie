using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager Instance;
    public GameObject bulletPrefab;
    public int bulletAmount = 5;

    private List<GameObject> bullets;
    void Awake()
    {
        Instance = this;
        bullets = new List<GameObject>(bulletAmount);
        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject prefabInstance = Instantiate(bulletPrefab);
            
            //prefabInstance.transform.SetParent(transform);
            
            prefabInstance.SetActive(false);
            bullets.Add(prefabInstance);
        }
    }
    public GameObject GetBullet()
    {
        int totalBullet = bullets.Count;
        for (int i = 0; i < totalBullet; i++)
        {
            if (bullets[i].activeInHierarchy)
            {
                bullets[i].SetActive(true);

                return bullets[i];
            }

        }
        GameObject prefabInstance = Instantiate(bulletPrefab);
        //prefabInstance.transform.SetParent(transform);

        prefabInstance.SetActive(true);
        bullets.Add(prefabInstance);
        return prefabInstance;
    }
}