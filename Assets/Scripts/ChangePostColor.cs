using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class ChangePostColor : MonoBehaviour
{
    PostProcessingBehaviour PostEffect;
    // Start is called before the first frame update
    void Start()
    {
        PostEffect = GetComponent<PostProcessingBehaviour>();
        var settings = PostEffect.profile.colorGrading.settings;
        //settings.colorWheels.log.power.b = 0;
        PostEffect.profile.colorGrading.settings = settings;
    }

    // Update is called once per frame
    void Update()
    {


    }
}
