//using Unity.Burst;
//using Unity.Collections;
//using Unity.Entities;
//using Unity.Jobs;
//using Unity.Mathematics;
//using Unity.Physics;
//using Unity.Transforms;
//using UnityEngine;

//[BurstCompile]
//public partial struct MoveTetrominoSystem : ISystem
//{
//    [BurstCompile]
//    public void OnUpdate(ref SystemState state)
//    {
//        var ecb = new EntityCommandBuffer(Allocator.Temp);

//        // Get all Entities that have the component with the Entity reference
//        foreach (var prefab in
//                 SystemAPI.Query<RefRO<PieceComponent>>())
//        {
//            // Instantiate the prefab Entity
//            var instance = ecb.Instantiate(prefab.ValueRO.entity);
//            // Note: the returned instance is only relevant when used in the ECB
//            // as the entity is not created in the EntityManager until ECB.Playback
//            ecb.AddComponent<PieceComponent>(instance);
//        }

//        ecb.Playback(state.EntityManager);
//        ecb.Dispose();
//        //foreach (var (transform, piece, physicsVelocity) in
//        //         SystemAPI.Query<RefRW<LocalTransform>, RefRW<PieceComponent>, RefRW<PhysicsVelocity>>())
//        //{
//        //    if (!piece.ValueRO.IsStopped)
//        //    {
//        //        physicsVelocity.ValueRW.Linear = new float3(piece.ValueRO.Velocity.x, piece.ValueRO.Velocity.y, 0);
//        //    }
//        //}
//        ////RefRW<PieceComponent> pieceComponent = SystemAPI.GetComponentRW<PieceComponent>()

//    }
//}
