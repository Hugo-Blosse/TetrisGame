using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

public struct PieceComponent : IComponentData
{
    public float2 Velocity;
    public bool IsStopped;
    public bool IsCurrentTetromino;
}
