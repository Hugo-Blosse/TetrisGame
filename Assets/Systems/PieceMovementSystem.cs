using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Stateful;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
partial struct PieceMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        PhysicsWorldSingleton physicsWorldSingleton = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
        CollisionWorld collisionWorld = physicsWorldSingleton.CollisionWorld;
        foreach ((
            RefRW<LocalTransform> localTransform,
            RefRW<PieceComponent> pieceComponent,
            RefRW<PhysicsVelocity> physicsVelocity,
            RefRW<PhysicsMass> physicsMass)
            in SystemAPI.Query<
                RefRW<LocalTransform>,
                RefRW<PieceComponent>,
                RefRW<PhysicsVelocity>,
                RefRW<PhysicsMass>>()) {

            if (!pieceComponent.ValueRO.IsStopped)
            {
                physicsVelocity.ValueRW.Linear = new float3(5f * pieceComponent.ValueRO.XSpeed, -2f + 5 * -pieceComponent.ValueRO.YSpeed, 0f);
            }
            physicsVelocity.ValueRW.Angular = new float3(0f, 0f, 0f);
            physicsMass.ValueRW.InverseInertia = new float3(0f, 0f, 0f);
        }
    }
    public partial struct DetectCollision : IJobEntity
    {
        public void Execute(Entity entity, ref DynamicBuffer<StatefulCollisionEvent> collisionEvents)
        {
            foreach (StatefulCollisionEvent col in collisionEvents)
            {
                Entity a = col.EntityA;
                Entity b = col.EntityB;
                Debug.Log(a);
                Debug.Log(b);
            }
        }
    }
}