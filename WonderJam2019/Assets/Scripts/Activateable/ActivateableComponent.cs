using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ActivateableComponent : MonoBehaviour
{
    /// <summary>
    /// The sphere collider that determines the area in which the player controller displays a prompt to the player.
    /// </summary>
    public SphereCollider ActivationZone;

    // Start is called before the first frame update
    void Start()
    {
        this.ActivationZone = this.GetComponent<SphereCollider>();
    }
}