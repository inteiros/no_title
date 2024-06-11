using UnityEngine;

public class Espelho : MonoBehaviour
{
    public Transform cameraPlayer;
    private Transform cameraEspelho;

    void Start()
    {
        cameraEspelho = GetComponentInChildren<Camera>().transform;

        if (cameraPlayer == null)
        {
            Debug.LogError("camera do player nao definida");
        }
    }

    void Update()
    {
        if (cameraPlayer != null && cameraEspelho != null)
        {
            Vector3 newPos = cameraEspelho.position;
            newPos.y = cameraPlayer.position.y;
            cameraEspelho.position = newPos;
        }
    }
}
