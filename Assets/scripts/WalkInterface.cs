using UnityEngine;

public interface IMovementStrategy
{
    Vector3 Move(Vector3 input, Transform player);
}