using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    private Rigidbody rb = null;
    private float moveSpeed = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        inputReader.MoveEvent += HandleMove;
    }

    private void OnDestroy()
    {
        inputReader.MoveEvent -= HandleMove;
    }

    private void HandleMove(Vector2 movement)
    {
        Debug.Log(movement.x);
        Debug.Log(movement.y);
        Vector3 moveVector = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.y * moveSpeed);
        transform.Translate(moveVector * Time.fixedDeltaTime);
        //rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.y * moveSpeed);
    }
}
