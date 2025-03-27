using UnityEngine;
using UnityEngine.Rendering; // Add this namespace

public class TransparencyBasedOnCollision : MonoBehaviour
{
    [Range(0, 1)] public float initialTransparency = 0.2f;
    public string objectTag = "Common Stud";

    private void Start()
    {
        SetInitialTransparency();
    }

    void SetInitialTransparency()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(objectTag);
        foreach (GameObject obj in objects)
        {
            SetTransparency(obj, initialTransparency);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(objectTag))
        {
            SetTransparency(gameObject, 1f);
        }
    }

    void SetTransparency(GameObject obj, float alpha)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material mat = renderer.material;
            mat.SetFloat("_Mode", alpha < 1 ? 3 : 0);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha); // Fixed
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha); // Fixed
            mat.EnableKeyword(alpha < 1 ? "_ALPHABLEND_ON" : "_ALPHABLEND_OFF");

            Color color = mat.color;
            color.a = alpha;
            mat.color = color;
        }
    }
}