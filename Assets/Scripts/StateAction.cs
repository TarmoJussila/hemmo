using UnityEngine;

public class StateAction : MonoBehaviour
{
    [SerializeField] private StateType stateType;

    public void ChangeState()
    {
        FindObjectOfType<StateManager>().ChangeState(stateType);
    }
}