using UnityEngine;

public class GoalDetectModified : MonoBehaviour
{
    [HideInInspector]
    public PushTargetToGoal agent; 

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("target"))
            agent.GoalReached();
    }
}
