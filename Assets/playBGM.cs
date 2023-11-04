using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playBGM : MonoBehaviour
{
    public AudioSource BGM;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playMusic());
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    IEnumerator playMusic()
    {
        yield return new WaitForSeconds(0.3f);    
        BGM.Play();
    }
}
