using UnityEngine;

public class WalkStrategy : IMovementStrategy
{
    public float speed = 8f;

    public Vector3 Move(Vector3 input, Transform player)
    {
        return input * speed;
    }
}
public class RunStrategy : IMovementStrategy
{
    public Vector3 Move(Vector3 input, Transform player)
    {
        return input * 14f;
    }
}