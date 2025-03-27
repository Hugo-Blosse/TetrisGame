using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

[BurstCompile]
public partial struct MoveTetrominoSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (transform, piece, physicsVelocity) in
                 SystemAPI.Query<RefRW<LocalTransform>, RefRW<PieceComponent>, RefRW<PhysicsVelocity>>())
        {
            if (!piece.ValueRO.IsStopped)
            {
                physicsVelocity.ValueRW.Linear = new float3(piece.ValueRO.Velocity.x, piece.ValueRO.Velocity.y, 0);
            }
        }
    }
}
