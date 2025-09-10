using UnityEngine;

public class oscillator2 : MonoBehaviour
{
    [SerializeField] float waitTime = 2f;
    [SerializeField] float downSpeed = 1f;
    [SerializeField] float upSpeed = 5f;
    [SerializeField] float distance = 3f;

    Vector3 topPosition;
    Vector3 bottomPosition;
    bool goingDown = true;
    float timer = 0f;

    void Start()
    {
        topPosition = transform.position;
        bottomPosition = topPosition - new Vector3(0, distance, 0);
    }

    void Update()
    {
        if (goingDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, bottomPosition, downSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, bottomPosition) < 0.01f)
            {
                timer += Time.deltaTime;
                if (timer >= waitTime)
                {
                    goingDown = false;
                    timer = 0f;
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, topPosition, upSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, topPosition) < 0.01f)
            {
                goingDown = true;
            }
        }
    }
}
