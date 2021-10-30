using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow S;

    private void Awake()
    {
        S = this;
    }
    private Vector3? _offset = null;

    private GameObject _toFollow;

    public void FollowMe(GameObject go)
    {
        if (_offset == null)
        {
            _offset = transform.position - go.transform.position;
        }
        _toFollow = go;
    }

    private void Update()
    {
        if (_offset == null)
        {
            return;
        }
        transform.position = _toFollow.transform.position + _offset.Value;
    }
}