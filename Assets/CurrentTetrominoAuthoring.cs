using System.ComponentModel;
using Unity.Entities;
using UnityEngine;

public class CurrentTetrominoAuthoring : MonoBehaviour
{
    public class CurrentTetrominoBaker : Baker<CurrentTetrominoAuthoring>
    {
        public override void Bake(CurrentTetrominoAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new CurrentTetrominoComponent());
        }
    }
}
public struct CurrentTetrominoComponent : IComponentData, IEnableableComponent {
}