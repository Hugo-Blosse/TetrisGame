//using Unity.Burst;
//using Unity.Entities;
//using Unity.Physics;
//using Unity.Physics.Systems;

//[BurstCompile]
//public partial struct CollisionEventJob : ICollisionEventsJob
//{
//    public void Execute(CollisionEvent collisionEvent)
//    {
//        Entity entityA = collisionEvent.EntityA;
//        Entity entityB = collisionEvent.EntityB;

//        // Handle the collision logic here
//        UnityEngine.Debug.Log($"Collision detected between {entityA} and {entityB}");
//    }

//    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
//    [UpdateAfter(typeof(PhysicsSimulationGroup))]
//    public partial class CollisionDetectionSystem : SystemBase
//    {
//        //private BuildPhysicsWorld _buildPhysicsWorld;
//        //private StepPhysicsWorld _stepPhysicsWorld;

//        //protected override void OnCreate()
//        //{
//        //    _buildPhysicsWorld = World.GetOrCreateSystemManaged<BuildPhysicsWorld>();
//        //    _stepPhysicsWorld = World.GetOrCreateSystemManaged<StepPhysicsWorld>();
//        //}

//        protected override void OnUpdate()
//        {
//            //Dependency = new CollisionEventJob()
//            //{
//                // You can pass additional parameters here if needed
//            //}.Schedule(_stepPhysicsWorld.Simulation, Dependency);
//        }
//    }
//}