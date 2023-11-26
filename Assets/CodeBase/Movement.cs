using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            Move(Vector3.forward);

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            Move(Vector3.back);
    }

    private void Move(Vector3 direction) => 
        transform.Translate(direction * _speed * Time.deltaTime);
}