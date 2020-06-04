using UnityEngine;

public class DestroyAnimation : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void End()
    {
        Destroy(gameObject);
    }
}
