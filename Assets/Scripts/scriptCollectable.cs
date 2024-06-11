using UnityEngine;

public class scriptCollectable : MonoBehaviour
{
    public int tiros = 5;
    public float collectionRadius = 2f;
    private scriptAudio audioManager;
    private GameManager gameManager;

    private void Start()
    {
        SphereCollider collider = GetComponent<SphereCollider>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<scriptAudio>();
        gameManager = FindObjectOfType<GameManager>();

        if (collider == null)
        {
            collider = gameObject.AddComponent<SphereCollider>();
        }
        collider.isTrigger = true;
        collider.radius = collectionRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Tiros playerTiros = other.GetComponent<Tiros>();
            if (playerTiros != null)
            {
                playerTiros.AddTiros(tiros);
                audioManager.PlaySFX(audioManager.item);
                gameManager.OnAmmoCollected();
                Destroy(gameObject);
            }
        }
    }
}
