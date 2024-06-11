using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptPc : MonoBehaviour
{
    private Rigidbody rbd;
    public float vel = 8;
    private Quaternion rotIni;
    public float velRotY;
    private float contY = 0;
    public GameObject cabeca;
    public LayerMask mask;
    public float dist = 100f;
    public float jump = 500f;
    public bool onGround;
    private bool canShoot = false;
    public float shootCooldown = 1f;
    private Animator anim;
    private bool isMoving;
    private GameObject gun;
    private Transform modelTransform;
    private Tiros tiroManager;
    private scriptAudio audioManager;
    private bool canPlayEmpty = true;
    public float emptyCooldown = 1f;
    private GameManager gameManager;

    void Start()
    {
        rbd = GetComponent<Rigidbody>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        rotIni = transform.localRotation;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<scriptAudio>();

        modelTransform = transform.Find("model");
        if (modelTransform != null)
        {
            anim = modelTransform.GetComponentInChildren<Animator>();
            gun = modelTransform.Find("Gun").gameObject;
        }

        tiroManager = GetComponent<Tiros>();

        velRotY = 300;
        onGround = true;
        StartCoroutine(EnableShootingAfterDelay(5f));
    }

    void Update()
    {
        if (gameManager.endgame)
        {
            rbd.velocity = Vector3.zero;
            anim.SetFloat("Speed", 0);
            return;
        }

        float frente = Input.GetAxis("Vertical");
        float lado = Input.GetAxis("Horizontal");

        Vector3 movimento = new Vector3(lado, 0, frente);
        rbd.velocity = transform.TransformDirection(new Vector3(lado * vel, rbd.velocity.y, frente * vel));

        contY += Input.GetAxisRaw("Mouse X") * Time.deltaTime * velRotY;

        Quaternion rotY = Quaternion.AngleAxis(contY, Vector3.up);
        transform.localRotation = rotIni * rotY;

        if (anim != null)
        {
            float speed = rbd.velocity.magnitude;
            anim.SetFloat("Speed", speed);
            isMoving = speed > 0.3;

            SetGunVisibility(!isMoving);

            if (!isMoving)
            {
                modelTransform.rotation = Quaternion.Euler(0, cabeca.transform.eulerAngles.y, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rbd.AddForce(Vector3.up * jump, ForceMode.Impulse);
            onGround = false;
        }

        if (!onGround)
        {
            anim.SetFloat("Speed", 2);
            SetGunVisibility(false);
        }

        if (Input.GetMouseButtonDown(0) && onGround && canShoot && !isMoving)
        {
            if (tiroManager != null && tiroManager.CanShoot())
            {
                tiroManager.Shoot();
                StartCoroutine(Shoot());
            }
            else if (tiroManager.currentTiros == 0 && canPlayEmpty)
            {
                StartCoroutine(PlayEmptySound());
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (tiroManager.reserveTiros != 0 && tiroManager.currentTiros < 20)
            {
                audioManager.PlaySFX(audioManager.reload);
                StartCoroutine(ReloadAndDelayShoot());
            }
        }
    }

    private IEnumerator Shoot()
    {
        canShoot = false;

        if (anim != null)
        {
            anim.SetBool("GunFired", true);
        }

        RaycastHit hit;
        audioManager.PlaySFX(audioManager.shot);
        if (Physics.Raycast(cabeca.transform.position, cabeca.transform.forward, out hit, dist, mask))
        {
            Rigidbody hitRbd = hit.collider.GetComponent<Rigidbody>();
            if (hitRbd != null)
            {
                hitRbd.AddForce(cabeca.transform.forward * 500);
            }

            scriptNpc npcScript = hit.collider.GetComponent<scriptNpc>();
            if (npcScript != null)
            {
                npcScript.TakeDamage();
            }
        }

        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
        anim.SetBool("GunFired", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "floor")
        {
            onGround = true;
        }
    }

    private void SetGunVisibility(bool isVisible)
    {
        if (gun != null)
        {
            gun.SetActive(isVisible);
        }
    }

    private IEnumerator PlayEmptySound()
    {
        canPlayEmpty = false;
        audioManager.PlaySFX(audioManager.empty);
        yield return new WaitForSeconds(emptyCooldown);
        canPlayEmpty = true;
    }

    private IEnumerator ReloadAndDelayShoot()
    {
        canShoot = false;
        tiroManager.Reload();
        yield return new WaitForSeconds(1.5f);
        canShoot = true;
    }
    private IEnumerator EnableShootingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }
}
