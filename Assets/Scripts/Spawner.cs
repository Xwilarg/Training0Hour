using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    List<GameObject> spawned = new();

    private void Start()
    {
        Spawn();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        if (spawned.Count > 0)
        {
            spawned.Last().GetComponent<PlayerController>().IsCurrent = false;
        }

        var go = Instantiate(_prefab, transform);
        go.transform.position = transform.position;
        spawned.Add(go);
    }
}