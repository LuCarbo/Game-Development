using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Impostazioni Dialogo")]
    public DialogueData dialogo;

    [Header("Chi può far partire il dialogo?")]
    public GameObject ilTuoPersonaggio;

    private DialogueManager manager;
    private PlayerInputHandler inputPersonaggio;

    private bool playerVicino = false;

    void Start()
    {
        manager = FindFirstObjectByType<DialogueManager>();
        inputPersonaggio = FindFirstObjectByType<PlayerInputHandler>();
    }

    void Update()
    {
        if (playerVicino && inputPersonaggio != null && inputPersonaggio.InteractPressed)
        {
            // L'NPC fa partire il testo SOLO se il Manager non sta già parlando
            if (manager != null && !manager.staParlando)
            {
                manager.AvviaDialogo(dialogo);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ilTuoPersonaggio)
        {
            playerVicino = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == ilTuoPersonaggio)
        {
            playerVicino = false;

            if (manager != null && manager.staParlando)
            {
                manager.TerminaDialogo();
            }
        }
    }
}