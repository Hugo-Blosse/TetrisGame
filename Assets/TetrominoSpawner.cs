using Unity.Entities;
using UnityEngine;

public class TetrominoSpawnerAuthoring : MonoBehaviour
{
    public float timer;
    public float timerMax;
    public int index;
    public class TetrominoSpawnerBaker : Baker<TetrominoSpawnerAuthoring>
    {
        public override void Bake(TetrominoSpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new TetrominoSpawnerComponent
            {
                timerMax = authoring.timerMax
            });
        }
    }

}
public struct TetrominoSpawnerComponent : IComponentData
{
    public float timer;
    public float timerMax;
    public int index;
}