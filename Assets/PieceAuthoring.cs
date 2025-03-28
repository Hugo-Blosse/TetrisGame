using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PieceAuthoring : MonoBehaviour
{
    public bool IsStopped;
    public float YSpeed;
    public float XSpeed;
    public int TetrisManagerID;
    public class PieceBaker : Baker<PieceAuthoring>
    {
        public override void Bake(PieceAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PieceComponent
            {
                IsStopped = false,
                YSpeed = authoring.YSpeed,
                XSpeed = authoring.XSpeed,
                TetrisManagerID = authoring.TetrisManagerID,
            });
        }
    }
}


public struct PieceComponent : IComponentData
{
    public Entity Entity;
    public bool IsStopped;
    public float YSpeed;
    public float XSpeed;
    public int TetrisManagerID;
}

