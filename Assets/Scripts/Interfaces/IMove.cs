using UnityEngine;

public interface IMove
{
    void MoveVelocity(Vector3 moveVelocity);
    void MoveVelocity(Vector3 moveVelocity, int offScreenOffSet);
    void SetVelocitySpeed(float speed);
}

