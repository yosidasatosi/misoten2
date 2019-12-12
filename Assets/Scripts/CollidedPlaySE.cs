using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidedPlaySE : MonoBehaviour
{
    public AudioSource source;
    public AudioClip se;
    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        source.clip = se;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        source.PlayOneShot(se);
    }

    private void OnCollisionExit(Collision collision)
    {
        source.PlayOneShot(se);
    }
}
