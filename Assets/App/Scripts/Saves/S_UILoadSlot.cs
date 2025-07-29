using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_UILoadSlot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI textNameValue;
    [SerializeField] private TextMeshProUGUI textPlayerValue;
    [SerializeField] private TextMeshProUGUI textDateValue;
    [SerializeField] private Button buttonLoad;
    [SerializeField] private Button buttonDelete;

    private string saveName;

    public void Setup(string name)
    {
        saveName = name;

        textNameValue.text = $"{name}";
        textPlayerValue.text = "";
        textDateValue.text = "";
        buttonLoad.interactable = false;
        buttonDelete.interactable = false;
    }
}