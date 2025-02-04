using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSText : MonoBehaviour
{
    private TextMeshProUGUI fpsText;
    // Start is called before the first frame update
    private void Start()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(SetFPS());
    }

    private IEnumerator SetFPS()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            float fps = 1 / Time.unscaledDeltaTime;
            fpsText.text = $"FPS : {fps:N1}";
        }
    }
}
