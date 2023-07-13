using UnityEngine;

public class HelperCannon : CannonPowerBase
{
    [SerializeField] private float lifeTime = 10f;
    [SerializeField] private Material helperCannonBodyMat;
    private float timer = 0f;
    private bool helperCannonActive = false;
    private GameObject cannonInstance;

    [ContextMenu("Init Helper Cannon")]
    public override void InitPower()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.x *= -1f;

        cannonInstance = Instantiate(this.gameObject, spawnPosition, this.transform.rotation);

        CannonMovement helperCannonMovement = cannonInstance.GetComponent<CannonMovement>();
        Cannon helperCannon = cannonInstance.GetComponent<Cannon>();

        helperCannonMovement.SetSwerveSpeed(helperCannonMovement.GetSwerveSpeed * -1f);
        helperCannon.SetWheelRotationSpeed(helperCannon.GetWheelRotationSpeed * -1f);

        MeshRenderer helperCannonMR = helperCannon.GetBodyOBJ.GetComponent<MeshRenderer>();
        helperCannonMR.sharedMaterial = helperCannonBodyMat;

        timer = 0f;
        helperCannonActive = true;
    }

    private void Update() 
    {
        if (!helperCannonActive) { return; }

        timer += Time.deltaTime;

        if (timer < lifeTime) { return; }
        if (cannonInstance == null) { return; }

        helperCannonActive = false;
        Destroy(cannonInstance);
    }
}
