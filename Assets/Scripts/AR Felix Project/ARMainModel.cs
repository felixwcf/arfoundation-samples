using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class ARMainModel : MonoBehaviour
{
    ARInteraction arInteraction;

    [SerializeField] GameObject dieselGenerator;
    [SerializeField] GameObject voltageCasingObj;
    [SerializeField] GameObject boardBGObj;

    bool canLookAtCam;

    // Start is called before the first frame update
    void Start()
    {
        arInteraction = GameObject.FindGameObjectWithTag("ARInteraction").GetComponent<ARInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canLookAtCam)
        {
            Vector3 relativePos = Camera.main.transform.position - transform.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(-relativePos, Vector3.up); // Vector3.up
            Quaternion q = rotation;
            q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
            transform.rotation = q;
        }
    }

    public void SetLookAtCamera(bool _look)
    {
        canLookAtCam = _look;
    }

    // Temporary fade away
    public void dimissObject()
    {
        arInteraction.SetShowSelectedIndicator(false);

        //transform.DOLocalMove(new Vector3(1,2,3),1,false);
        //transform.DOLocalRotate(new Vector3(0, -90, 20), 1, RotateMode.Fast);

        //Material _generator_mat = dieselGenerator.GetComponent<MeshRenderer>().material;
        //ToFadeMode(_generator_mat);

        //dieselGenerator.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.1f);

        //dieselGenerator.GetComponent<MeshRenderer>().material.DOFade(0, 2);

        //dieselGenerator.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.1f);



        //dieselGenerator.colo
    }

    void ToFadeMode(Material material)
    {
        material.SetOverrideTag("RenderType", "Transparent");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }

    void ToOpaqueMode(Material material)
    {
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
    }
}
