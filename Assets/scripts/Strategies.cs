using UnityEngine;

public class WalkStrategy : IMovementStrategy
{
    public Vector3 Move(Vector3 input, Transform player)
    {
        return input * 8f;
    }
}
public class RunStrategy : IMovementStrategy
{
    public Vector3 Move(Vector3 input, Transform player)
    {
        return input * 14f;
    }
}