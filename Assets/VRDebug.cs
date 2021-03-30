using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VRDebug : MonoBehaviour
{
    [SerializeField] GameObject textPrefab3D;
    // Start is called before the first frame update
    public static VRDebug Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Log(string logtext)
    {
        GameObject textobj=Instantiate(textPrefab3D, transform);
        textobj.GetComponent<TextMeshProUGUI>().SetText(logtext) ;
    }
}
