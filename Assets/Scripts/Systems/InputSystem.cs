using Components;
using Leopotam.EcsLite;
using UnityEngine;

public class InputSystem :  IEcsRunSystem
{
    private EcsPool<TargetPositionComponent> _targetPositionPool;
    private readonly EcsFilter _filterInput;
    public InputSystem(EcsFilter filterInput)
    {
        _filterInput = filterInput;
    }

    public void Run(IEcsSystems systems)
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (var entity in _filterInput)
            {
                if (Camera.main != null)
                {  
                    _targetPositionPool = systems.GetWorld().GetPool<TargetPositionComponent>();
                    ref var targetPositionComponent = ref _targetPositionPool.Get(entity);
                        
                    var rayFromMousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
                        
                    if (Physics.Raycast(rayFromMousePosition, out var hit))
                        targetPositionComponent.TargetPosition = hit.point;
                }
            }
        }
    }
}
