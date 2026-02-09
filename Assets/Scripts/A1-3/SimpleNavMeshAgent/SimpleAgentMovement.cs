using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class SimpleAgentMovement : MonoBehaviour
{
    private InputSystem_Actions inputActions;

    NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        inputActions = new();
        inputActions.Enable();

        inputActions.Player.Attack.performed += OnAttack;
    }

    void OnAttack(InputAction.CallbackContext context)
    {
        var mouse = Mouse.current;
        Vector2 mousePos = new(mouse.position.x.ReadValue(), mouse.position.y.ReadValue());
        if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out var hit))
        {
            agent.SetDestination(hit.point);
        }
    }
}
