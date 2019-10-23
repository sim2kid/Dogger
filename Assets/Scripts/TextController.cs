using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    
    private Text overheadText;
    private float fade = 0;
    [SerializeField] float FadeOutTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        overheadText = GetComponent<Text>();
        overheadText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (fade > 0)
        {
            fade -= Time.deltaTime;
        }
        else {
            fade = 0;
            overheadText.text = "";
        }
    }

    public void set(string text) {
        fade = FadeOutTime;
        overheadText.text = text;
    }

    public void hold(string text)
    {
        fade = FadeOutTime*20;
        overheadText.text = text;
    }
}
