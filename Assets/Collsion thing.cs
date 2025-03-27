using UnityEngine;

public class SolidifyOnSameTagCollision : MonoBehaviour
{
    [Tooltip("Objects with this tag will trigger solidification")]
    public string objectTag = "Common_Stud"; // Set your tag in Inspector

    private void OnCollisionEnter(Collision collision)
    {
        // Check if collided object has the same tag
        if (collision.gameObject.CompareTag(objectTag))
        {
            MakeSolid(gameObject); // Make this object solid
            // Optional: Make the other object solid too
            // MakeSolid(collision.gameObject);
        }
    }

    void MakeSolid(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material mat = renderer.material;

            // Standard Shader settings for opaque objects
            mat.SetFloat("_Mode", 0); // Opaque mode
            mat.SetInt("_ZWrite", 1); // Enable depth writing
            mat.DisableKeyword("_ALPHABLEND_ON"); // Disable blending

            // Remove any transparency
            Color color = mat.color;
            color.a = 1f; // Full opacity
            mat.color = color;
        }
    }
}