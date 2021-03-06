using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _jump;

    [SerializeField]
    private Material _normal, _selected;

    public void Select()
    {
        GetComponent<MeshRenderer>().material = _selected;
    }

    public void Unselect()
    {
        GetComponent<MeshRenderer>().material = _normal;
    }

    public bool IsCurrent { set; get; } = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        if (Spawner.S.GameEnded)
        {
            return;
        }
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
        if (Spawner.S.GameEnded)
        {
            return;
        }
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
            if (info.distance < 1.01f)
            {
                _rb.AddForce(Vector3.up * 5.5f, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Goal"))
        {
            _rb.velocity = Vector3.zero;
            Spawner.S.ShowPanelWin();
        }
    }
}
