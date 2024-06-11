using UnityEngine;

public class RemoveAnimationEvents : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        if (animator != null)
        {
            RuntimeAnimatorController ac = animator.runtimeAnimatorController;
            for (int i = 0; i < ac.animationClips.Length; i++)
            {
                AnimationClip clip = ac.animationClips[i];
                clip.events = new AnimationEvent[0];
            }
            Debug.Log("Todos os eventos de anima��o foram removidos.");
        }
        else
        {
            Debug.LogError("Animator n�o atribu�do.");
        }
    }
}
