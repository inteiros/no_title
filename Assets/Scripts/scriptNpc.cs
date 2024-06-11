using UnityEngine;
using UnityEngine.AI;

public class scriptNpc : MonoBehaviour
{
    public GameObject pc;
    private NavMeshAgent agnt;
    public GameObject[] waypoints = new GameObject[4];
    public float distMin = 5;
    public float grabDist = 3f;
    private Animator anim;
    private int i = 0;
    private GameObject destino;
    private bool isFollowing = false;
    private bool isDead = false;
    private bool isInvulnerable = false;
    public bool playerDead = false;

    public AudioClip seePlayerClip;
    public AudioClip takeDamageClip;
    public AudioClip grabPlayerClip;

    private AudioSource audioSource;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        agnt = GetComponent<NavMeshAgent>();
        agnt.speed = 4.5f;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        prox();
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, pc.transform.position);

        if (distanceToPlayer < grabDist && !gameManager.endgame)
        {
            agnt.isStopped = true;
            anim.SetBool("isGrabbing", true);
            if (!playerDead)
            {
                PlayAudio(grabPlayerClip);
                playerDead = true;
                gameManager.OnPlayerAttacked();
            }
        }
        else if (isFollowing || (distanceToPlayer < 20 && !gameManager.endgame))
        {
            if (!isFollowing)
            {
                PlayAudio(seePlayerClip);
                gameManager.OnFirstSeePlayer();
            }
            isFollowing = true;
            agnt.SetDestination(pc.transform.position);
        }
        else if (Vector3.Distance(transform.position, destino.transform.position) < distMin)
        {
            prox();
        }

        float speed = agnt.velocity.magnitude;
        anim.SetFloat("Speed", speed);
    }

    private void prox()
    {
        if (isDead) return;

        destino = waypoints[i++];
        if (i == waypoints.Length)
            i = 0;
        agnt.SetDestination(destino.transform.position);
        agnt.isStopped = false;
    }

    public void TakeDamage()
    {
        if (isDead || isInvulnerable) return;

        isDead = true;
        isInvulnerable = true;
        anim.SetBool("isDead", true);
        agnt.isStopped = true;
        PlayAudio(takeDamageClip);
        Invoke("Revive", 2f);
    }

    private void Revive()
    {
        isDead = false;
        anim.SetBool("isDead", false);
        prox();
        Invoke("RemoveInvulnerability", 0.5f);
    }

    private void RemoveInvulnerability()
    {
        isInvulnerable = false;
    }

    private void PlayAudio(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.volume = 0.3f;
            audioSource.PlayOneShot(clip);
        }
    }
}
