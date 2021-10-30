﻿using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private GameObject _panel;

    List<GameObject> spawned = new();

    private int _index = 0;

    private bool _waitEnter = true;

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
            }
        }
    }

    private void Spawn()
    {
        if (spawned.Count > 0)
        {
            spawned[_index].GetComponent<PlayerController>().IsCurrent = false;
        }
        _index = spawned.Count;

        var go = Instantiate(_prefab, transform);
        go.transform.position = transform.position;
        spawned.Add(go);
    }
}