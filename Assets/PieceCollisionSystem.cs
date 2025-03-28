using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics.Systems;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using Unity.Physics.Stateful;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateBefore(typeof(PhysicsSimulationGroup))] // We are updating before `PhysicsSimulationGroup` - this means that we will get the events of the previous frame
public partial struct CollisionEventsSystem : ISystem
{
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
    public void OnUpdate(ref SystemState state)
    {

        

        //foreach (CollisionEvent collisionEvent in collisionEvents)
        //{
        //    Debug.Log(collisionEvent);
        //    //foreach ((
        //    //RefRW<LocalTransform> localTransform,
        //    //RefRW<PieceComponent> pieceComponent,
        //    //RefRW<PhysicsVelocity> physicsVelocity,
        //    //RefRW<PhysicsMass> physicsMass,
        //    //RefRO<CurrentTetrominoComponent> currentTetrominoComponent)
        //    //in SystemAPI.Query<
        //    //    RefRW<LocalTransform>,
        //    //    RefRW<PieceComponent>,
        //    //    RefRW<PhysicsVelocity>,
        //    //    RefRW<PhysicsMass>,
        //    //    RefRO<CurrentTetrominoComponent>>())
        //    //{
        //    //    Debug.Log("xd");
        //    //    //if (collisionEvent.EntityA == piece)
        //    //}
        //}

        // ...
    }
}

