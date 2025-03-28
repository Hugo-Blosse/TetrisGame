using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class TetrisManAuthoring : MonoBehaviour
{
    public GameObject prefab;
    public float2 velocity;
    public bool isStopped;
    public bool isCurrentTetromino;
    class PieceSpawnerBaker : Baker<TetrisManAuthoring>
    {
        public override void Bake(TetrisManAuthoring authoring)
        {
            //Entity prefab = authoring.prefabs[Unity.Mathematics.Random.CreateFromIndex((uint)Time.captureDeltaTime).NextInt(0, authoring.prefabs.Length)];
            Entity entityPrefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic);

            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            //AddComponent(entity, new PieceComponent()
            //{
            //    canSpawn = true,
            //    entity = entityPrefab,
            //});
            ////AddComponent(entity, new PieceComponent
            ////{
            ////    //Prefab = GetEntity(prefab, TransformUsageFlags.Dynamic),
            ////    //IsStopped = false,
            ////    //IsCurrentTetromino = true,
            ////    //Velocity = new Vector2(0, -2)
            ////});
        }
    }
}
