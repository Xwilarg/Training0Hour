using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _jump;

    public bool IsCurrent { set; get; } = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        if (!IsCurrent) return;
        if (!_jump)
        {
            _jump = Input.GetKeyDown(KeyCode.Space);
        }

        if (transform.position.y < -10f)
        {
            Spawner.S.Kill(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (!IsCurrent) return;
        _rb.velocity = new Vector3(Input.GetAxis("Horizontal") * 5f, _rb.velocity.y, 0f);

        if (_rb.velocity.x < 0f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 90f, transform.rotation.z));
        }
        else if (_rb.velocity.x > 0f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, -90f, transform.rotation.z));
        }

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
