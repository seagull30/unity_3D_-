using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    Queue<GameObject> queue = new Queue<GameObject>();
    public GameObject ProjecttilePrefab;
    private void Awake()
    {
        for (int i = 0; i < 3; ++i)
        {
            GameObject Projectile = Instantiate(ProjecttilePrefab);
            queue.Enqueue(Projectile);
            Projectile.SetActive(false);
        }
    }

    public void Fire()
    {
        GameObject Projectile = queue.Dequeue();
        Projectile.transform.position = transform.position + new Vector3(0f, 1f, 0f);
        Projectile.SetActive(true);
        queue.Enqueue(Projectile);

    }
}
