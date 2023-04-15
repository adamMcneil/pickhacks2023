using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rabbitsText;
    [SerializeField] private TextMeshProUGUI foxesText;
    [SerializeField] private TextMeshProUGUI plantsText;

    private void FixedUpdate()
    {
        rabbitsText.text = Helpers.GetRabbits().Count.ToString();
        foxesText.text = Helpers.GetFoxes().Count.ToString();
        plantsText.text = Helpers.bushes.Count.ToString();
    }

    public void ClearMap()
    {
        Helpers.ClearMap();
    }

}
