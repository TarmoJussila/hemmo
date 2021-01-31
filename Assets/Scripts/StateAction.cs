using UnityEngine;
using UnityEngine.InputSystem;

public class StateAction : MonoBehaviour
{
    [SerializeField] private StateType stateType;

    private bool isActionTriggered = false;

    public void ChangeState()
    {
        if (isActionTriggered)
        {
            return;
        }

        FindObjectOfType<StateManager>().ChangeState(stateType);
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        if (keyboard.enterKey.wasPressedThisFrame)
        {
            ChangeState();
        }
    }
}