using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Riferimento al giocatore
    public float distance = 10.0f; // Distanza dalla telecamera al giocatore
    public float height = 5.0f; // Altezza della telecamera
    public float damping = 3.0f; // Velocità di damping
    public float mouseSensitivity = 70.0f; // di default dovrebbe essere a 100.0f
    private float xRotation = 0.0f;
    private float yRotation = 0.0f; // per la rotazione in alto o in basso
    public float zoomSpeed = 2.0f; // Velocità di zoom
    public float minDistance = 5.0f; // Distanza minima della telecamera
    public float maxDistance = 15.0f; // Distanza massima della telecamera

    private bool isRightMouseDown = false; // gestire il blocco rotazione

    void Update()
    {

        // Controlla se il tasto destro è premuto
        if (Input.GetMouseButtonDown(1))
        {
            isRightMouseDown = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isRightMouseDown = false;
        }
        if (!isRightMouseDown)  // questo if SERVE PER BLOCCARE LA VISUALE SE IL TASTO DESTRO è PREMUTO
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -30f, 70f); // qui ho messo solo -10f perchè non voglio che la camera vada troppo in basso

            // Aggiorna la rotazione orizzontale
            yRotation += mouseX;
        }

        // Gestione dello zoom con la rotellina del mouse
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        distance -= scrollInput * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance); // Limita la distanza

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        player.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    void LateUpdate()
    {
        float jumpOffset = Mathf.Clamp(player.GetComponent<Rigidbody>().velocity.y, -1f, 1f); //aiuta a stabilizzare la camera durante il salto.
        // Calcola la posizione desiderata della telecamera
        Vector3 desiredPosition = player.position - transform.forward * distance + Vector3.up * height;

        // Esegui un Raycast dal giocatore alla posizione desiderata della telecamera
        RaycastHit hit;
        if (Physics.Raycast(player.position + Vector3.up * height, (desiredPosition - player.position).normalized, out hit, distance))
        {
            if (Physics.Raycast(player.position + Vector3.up * height, (desiredPosition - player.position).normalized, out hit, distance))
            {
                // Se il Raycast colpisce un ostacolo, calcola la nuova posizione della telecamera
                float hitDistance = Vector3.Distance(player.position, hit.point) - 0.5f; // Offset per non far toccare la camera
                desiredPosition = player.position - transform.forward * hitDistance + Vector3.up * (height + jumpOffset);
            }
        }
        else
        {
            // Nessun ostacolo, posiziona la telecamera normalmente
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
        }

        // Fissa la telecamera verso il giocatore
        transform.LookAt(player);
    }


    /*void LateUpdate()
    {
        // Calcola la posizione della telecamera
        Vector3 desiredPosition = player.position - transform.forward * distance + Vector3.up * height;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);

        // Fissa la telecamera verso il giocatore
        transform.LookAt(player);
    }*/
}