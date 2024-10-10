using UnityEngine;

public class BlockPhysics : MonoBehaviour, IBlockPhysics
{
    [SerializeField] private Rigidbody _rigidbody;

    public void EnablePhysics()
    {
        TogglePhysics(true);
    }

    public void DisablePhysics()
    {
        TogglePhysics(false);
    }

    private void TogglePhysics(bool enable)
    {
        bool setIsKinematic = !enable;
        _rigidbody.isKinematic = setIsKinematic;
    }
}