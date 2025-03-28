using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

partial struct TetrominoSpawnerSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        EntityPrefabComponent tetromino = SystemAPI.GetSingleton<EntityPrefabComponent>();

        foreach ((
            RefRO<LocalTransform> localTransform,
            RefRW<TetrominoSpawnerComponent> tetrominoComponent)
             in SystemAPI.Query<
                 RefRO<LocalTransform>,
                 RefRW<TetrominoSpawnerComponent>>())
        {
            tetrominoComponent.ValueRW.timer -= SystemAPI.Time.DeltaTime;
            if (tetrominoComponent.ValueRO.timer > 0)
            {
                continue;
            }
            tetrominoComponent.ValueRW.timer = tetrominoComponent.ValueRO.timerMax;
            int randomNum = GameObject.FindFirstObjectByType<PlayerInputManager>().GetRandomNum();
            Entity tetrominoPrefab;
            if (randomNum == 1)
            {
                tetrominoPrefab = tetromino.clevelandZ;
            }
            else if (randomNum == 2)
            {
                tetrominoPrefab = tetromino.smashboy;
            }
            else if (randomNum == 3)
            {
                tetrominoPrefab = tetromino.rhodeIslandZ;
            }
            else
            {
                tetrominoPrefab = tetromino.teewee;
            }
            Entity tetrominoEntity = state.EntityManager.Instantiate(tetrominoPrefab);
            SystemAPI.SetComponent(tetrominoEntity, LocalTransform.FromPosition(localTransform.ValueRO.Position));
        }
    }
}
