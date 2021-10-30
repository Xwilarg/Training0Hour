using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _jump;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_jump)
        {
            _jump = Input.GetKeyDown(KeyCode.Space);
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(Input.GetAxis("Horizontal") * 5f, _rb.velocity.y, 0f);

        if (_jump)
        {
            Physics.Raycast(new Ray(transform.position, Vector3.down), out RaycastHit info);
            _jump = false;
            if (info.collider.name == "Floor" && info.distance < 1.01f)
            {
                _rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            }
        }
    }
}
