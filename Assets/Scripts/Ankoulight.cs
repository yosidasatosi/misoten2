using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ankoulight : MonoBehaviour
{
    public float GrowingTime = 2.0f;
    public float ShrinkTime = 1.0f;
    public GameObject Ankou;

    private float StartScale;
    private float timer;
    private bool IsGrowing;
    

    private void OnEnable()
    {
        StartScale = transform.localScale.x;
        timer = 0.0f;
        IsGrowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float progress = Mathf.Min(timer / (IsGrowing ? GrowingTime : ShrinkTime), 1.0f);
        float startScale = IsGrowing ? StartScale : 1.0f;
        float endScale = IsGrowing ? 1.0f : 0.0f;

        transform.localScale = Vector3.one * Mathf.Lerp(startScale, endScale, progress);
        
        if(progress >= 1.0f)
        {
            if(IsGrowing)
            {
                IsGrowing = false;
                timer = 0.0f;
            }
            else
            {
                Destroy(gameObject);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            Instantiate(Ankou, transform.position, transform.rotation, transform.parent);
        }
    }
}
