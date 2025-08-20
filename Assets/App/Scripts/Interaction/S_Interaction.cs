using UnityEngine;

public class S_Interaction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject UI;

    [Header("Input")]
    [SerializeField] private RSE_Interaction rse_Interaction;

    private void OnTriggerEnter2D(Collider2D other)
    {
        rse_Interaction.action += Interaction;

        SetUI(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        rse_Interaction.action -= Interaction;

        SetUI(false);
    }

    private void SetUI(bool value)
    {
        if (UI != null)
        {
            UI.SetActive(value);
        }
    }

    private void Interaction()
    {
        Debug.Log("1");
    }
}