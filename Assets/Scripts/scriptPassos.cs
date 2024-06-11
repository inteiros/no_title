using UnityEngine;

public class scriptPassos : MonoBehaviour
{
    public AudioSource footstepsSound;
    private scriptPc playerScript;
    private bool isMoving = false;

    void Start()
    {
        playerScript = GetComponent<scriptPc>();
    }

    void Update()
    {
        bool isCurrentlyMoving = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && playerScript.onGround;
        if (isCurrentlyMoving && !isMoving)
        {
            footstepsSound.loop = true;
            footstepsSound.Play();
        }
        else if (!isCurrentlyMoving && isMoving)
        {
            footstepsSound.loop = false;
        }

        isMoving = isCurrentlyMoving;
    }
}
