using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody2D _rb;
    
    [SerializeField] private float _movement;

    private bool _jumping;
    public bool _grounded = true;

    [SerializeField] private float groundDetectDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private EnemyStatsSo _stats;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _speedRight;
    [SerializeField] private float _speedLeft;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _moveSpeed = _stats.Speed;
        //_speedLeft = _stats.Speed;
        //_speedRight = _stats.Speed;
    }
    

    public void MoveRight()
    {
        _movement = _speedRight;
        _rb.velocity = new Vector2(Mathf.Lerp(_rb.velocity.x, _movement * _moveSpeed, _stats.Friction * Time.fixedDeltaTime), _rb.velocity.y);
    }

    public void MoveLeft()
    {
        _movement = _speedLeft;
        _rb.velocity = new Vector2(Mathf.Lerp(-_rb.velocity.x, -_movement * _moveSpeed, _stats.Friction * Time.fixedDeltaTime),_rb.velocity.y);
    }
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, groundDetectDistance);
    }
}
