using UnityEngine;

[DisallowMultipleComponent]
public class Movement : MonoBehaviour, IMove
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] float maxAcceleration = 100f;
    Vector3 velocity;

    public void MoveVelocity(Vector3 moveVelocity)
    {
        Vector3 inputVelocity = new Vector3(moveVelocity.x, 0f, moveVelocity.y) * moveSpeed;

        float maxSpeedChange = maxAcceleration * Time.deltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x, inputVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, inputVelocity.z, maxSpeedChange);

        Vector3 acceleration = new Vector3(moveVelocity.x, 0f, moveVelocity.y) * moveSpeed;
        velocity += acceleration * Time.deltaTime;

        Vector3 differnce = velocity * Time.deltaTime;

        Vector3 newPosition = transform.localPosition + differnce;

        //TODO:: SHIP TILTING

        if (newPosition.x > GameManager.Instance.bounds.xMax)
        {
            // REACHED RIGHT OF SCREEN
            newPosition.x = GameManager.Instance.bounds.xMin;
        }
        else if (newPosition.x < GameManager.Instance.bounds.xMin)
        {
            // REACHED LEFT OF SCREEN
            newPosition.x = GameManager.Instance.bounds.xMax;
        }

        if (newPosition.z > GameManager.Instance.bounds.zMax)
        {
            // REACHED TOP OF SCREEN
            newPosition.z = GameManager.Instance.bounds.zMin;
        }
        else if (newPosition.z < GameManager.Instance.bounds.zMin)
        {
            // REACHED BOTTOM OF SCREEN
            newPosition.z = GameManager.Instance.bounds.zMax;
        }

        transform.localPosition = newPosition;
    }

    public void MoveVelocity(Vector3 moveVelocity, int offScreenOffSet)
    {
        Vector3 inputVelocity = new Vector3(moveVelocity.x, 0f, moveVelocity.y) * moveSpeed;

        float maxSpeedChange = maxAcceleration * Time.deltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x, inputVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, inputVelocity.z, maxSpeedChange);

        Vector3 acceleration = new Vector3(moveVelocity.x, 0f, moveVelocity.y) * moveSpeed;
        velocity += acceleration * Time.deltaTime;

        Vector3 difference = velocity * Time.deltaTime;

        Vector3 newPosition = transform.localPosition + difference;


        if (newPosition.x > GameManager.Instance.bounds.xMax + offScreenOffSet)
        {
            // REACHED RIGHT OF SCREEN
            newPosition.x = GameManager.Instance.bounds.xMin;
        }
        else if (newPosition.x < GameManager.Instance.bounds.xMin - offScreenOffSet)
        {
            // REACHED LEFT OF SCREEN
            newPosition.x = GameManager.Instance.bounds.xMax;
        }


        if (newPosition.z > GameManager.Instance.bounds.zMax + offScreenOffSet)
        {
            // REACHED TOP OF SCREEN
            newPosition.z = GameManager.Instance.bounds.zMin;
        }
        else if (newPosition.z < GameManager.Instance.bounds.zMin - offScreenOffSet)
        {
            // REACHED BOTTOM OF SCREEN
            newPosition.z = GameManager.Instance.bounds.zMax;
        }

        transform.localPosition = newPosition;
    }

    public void SetVelocitySpeed(float speed) => moveSpeed = speed;
}