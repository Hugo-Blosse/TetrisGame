using Unity.Entities;
using UnityEngine;

public class EntityPrefabAuthoring : MonoBehaviour
{
    public GameObject clevelandZ;
    public GameObject rhodeIslandZ;
    public GameObject teewee;
    public GameObject smashboy;
    public class EntityPrefabBaker : Baker<EntityPrefabAuthoring>
    {
        public override void Bake(EntityPrefabAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EntityPrefabComponent
            {
                clevelandZ = GetEntity(authoring.clevelandZ, TransformUsageFlags.Dynamic),
                rhodeIslandZ = GetEntity(authoring.rhodeIslandZ, TransformUsageFlags.Dynamic),
                teewee = GetEntity(authoring.teewee, TransformUsageFlags.Dynamic),
                smashboy = GetEntity(authoring.smashboy, TransformUsageFlags.Dynamic),
            });
        }
    }

}
public struct EntityPrefabComponent : IComponentData
{
    public Entity clevelandZ;
    public Entity rhodeIslandZ;
    public Entity teewee;
    public Entity smashboy;
}