using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner S;

    private void Awake()
    {
        S = this;
    }

    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private GameObject _panel;

    List<GameObject> spawned = new();

    private int _index = 0;

    private bool _waitEnter = true;

    public void Kill(GameObject go)
    {
        spawned.FirstOrDefault(x => x == go);
        if (go.GetComponent<PlayerController>().IsCurrent)
        {
            SetNextCurrent();
        }
        spawned.Remove(go);
        Destroy(go);

        if (spawned.Count == 0)
        {
            Spawn();
        }
    }

    public void Update()
    {
        if (_waitEnter)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _panel.SetActive(false);
                Spawn();
                _waitEnter = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Spawn();
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SetNextCurrent();
            }
        }
    }

    public void SetNextCurrent()
    {
        spawned[_index].GetComponent<PlayerController>().IsCurrent = false;
        spawned[_index].GetComponent<PlayerController>().IsCurrent = false;
        if (_index + 1 == spawned.Count)
        {
            _index = 0;
        }
        else
        {
            _index++;
        }
        spawned[_index].GetComponent<PlayerController>().IsCurrent = true;
        spawned[_index].GetComponent<PlayerController>().Select();
    }

    private void Spawn()
    {
        if (spawned.Count > 0)
        {
            spawned[_index].GetComponent<PlayerController>().IsCurrent = false;
            spawned[_index].GetComponent<PlayerController>().Unselect();
        }
        _index = spawned.Count;

        var go = Instantiate(_prefab, transform);
        go.transform.position = transform.position;
        spawned.Add(go);
        spawned[_index].GetComponent<PlayerController>().Select();
    }
}