using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Entities;
using Unity.Collections;
using UnityEditor.PackageManager;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance { get; private set; }
    public int ID;
    public void Awake()
    {
        Instance = this;
    }
    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        float moveVal = callbackContext.ReadValue<float>();
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityQuery entityQuery = new EntityQueryBuilder(Allocator.Temp).WithAll<PieceComponent, CurrentTetrominoComponent>().Build(entityManager);

        NativeArray<Entity> entities = entityQuery.ToEntityArray(Allocator.Temp);
        NativeArray<PieceComponent> pieceComponents = entityQuery.ToComponentDataArray<PieceComponent>(Allocator.Temp);
        for (int i = 0; i < pieceComponents.Length; i++)
        {
            if (pieceComponents[i].TetrisManagerID == ID)
            {
                PieceComponent pieceComponent =  pieceComponents[i];
                pieceComponent.XSpeed = moveVal;
                entityManager.SetComponentData(entities[i], pieceComponent);
            }
        }
    }
    public void OnSpeedUp(InputAction.CallbackContext callbackContext)
    {
        float speedUpVal = callbackContext.ReadValue<float>();
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityQuery entityQuery = new EntityQueryBuilder(Allocator.Temp).WithAll<PieceComponent, CurrentTetrominoComponent>().Build(entityManager);

        NativeArray<Entity> entities = entityQuery.ToEntityArray(Allocator.Temp);
        NativeArray<PieceComponent> pieceComponents = entityQuery.ToComponentDataArray<PieceComponent>(Allocator.Temp);
        for (int i = 0; i < pieceComponents.Length; i++)
        {
            if (pieceComponents[i].TetrisManagerID == ID)
            {
                PieceComponent pieceComponent = pieceComponents[i];
                pieceComponent.YSpeed = speedUpVal;
                entityManager.SetComponentData(entities[i], pieceComponent);
            }
        }
    }
    public int GetRandomNum()
    {
        int ran = Random.Range(0, 4);
        print(ran);
        return ran;
    }
}
