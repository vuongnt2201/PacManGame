using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PacMan : Singleton<PacMan>
{
    public float _speed = 0.1f;
    private Rigidbody _rigidBody;   
    private Vector3 _dest = Vector3.zero;
    private Vector3 _dir = Vector3.zero;
    private Vector3 _nextDir = Vector3.zero;
    private GameObject _mainCamera;
    private bool _canMove;

    public void UpdatePacManMoving(bool CanMove)
    {
        _canMove = CanMove;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        _rigidBody = GetComponent<Rigidbody>();
        _dest = transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {  
        if(_canMove)
            ReadInputAndMove();
    }

    private bool Valid(Vector3 direction)
    {
        Vector3 pos = transform.position;
        Vector3 targetPos = pos + direction;

        // Thực hiện Linecast để kiểm tra va chạm với tường (Wall)
        if (Physics.Linecast(pos, targetPos, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Wall"))
                return false;
        }

        return true;
    }

    private void ReadInputAndMove()
    {
        Vector3 p = Vector3.MoveTowards(transform.position, _dest, _speed * Time.deltaTime);
        GetComponent<Rigidbody>().MovePosition(p);

        // Lấy hướng di chuyển tiếp theo từ bàn phím
        if (Input.GetAxis("Horizontal") > 0) _nextDir = Vector3.right;
        if (Input.GetAxis("Horizontal") < 0) _nextDir = Vector3.left;
        if (Input.GetAxis("Vertical") > 0) _nextDir = Vector3.forward;
        if (Input.GetAxis("Vertical") < 0) _nextDir = Vector3.back;

        // Nếu đối tượng đang ở trung tâm của một ô
        if (Vector3.Distance(_dest, transform.position) < 0.00001f)
        {
            if (Valid(_nextDir))
            {
                _dest = transform.position + _nextDir;
                _dir = _nextDir;
            }
            else if (Valid(_dir))
            {
                _dest = transform.position + _dir;
            }
        }
    }

    public void ResetMoving()
    {
        _dest = transform.position;
    }
}
