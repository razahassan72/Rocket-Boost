using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float Speed;

    Vector3 startPosition;
    Vector3 EndPosition;
    float movementFactor;


    void Start()
    {
        startPosition = transform.position;
        EndPosition = startPosition + movementVector;
    }

    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * Speed, 1f);
        transform.position = Vector3.Lerp(startPosition, EndPosition, movementFactor);
    }
}