using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera cameraPrefab;
    [HideInInspector]
    const string weaponLayerName = "Weapon";
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Camera weaponCamera;
    
    public void Setup()
    {
        mainCamera = Instantiate(cameraPrefab, transform.position, transform.rotation);
        mainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer(weaponLayerName));

        weaponCamera = Instantiate(cameraPrefab, transform);
        weaponCamera.transform.localPosition = Vector3.zero;
        weaponCamera.transform.localRotation = Quaternion.identity;
        weaponCamera.name = "WeaponCamera";
        weaponCamera.clearFlags = CameraClearFlags.Depth;
        weaponCamera.cullingMask = (1 << LayerMask.NameToLayer(weaponLayerName));
        weaponCamera.depth = 1;
        weaponCamera.fieldOfView = 40;
    }
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(mainCamera != null)
        {
            CameraFollowing();
        }
    }

    private void CameraFollowing()
    {
        mainCamera.transform.position = transform.position;
        mainCamera.transform.rotation = transform.rotation;
    }

    public Camera GetCamera()
    {
        return mainCamera;
    }
}
