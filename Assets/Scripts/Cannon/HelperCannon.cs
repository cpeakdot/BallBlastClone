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
        if (helperCannonActive) { return; }
        
        Vector3 spawnPosition = transform.position;
        spawnPosition.x *= -1f;

        cannonInstance = Instantiate(this.gameObject, spawnPosition, this.transform.rotation);

        CannonMovement helperCannonMovement = cannonInstance.GetComponent<CannonMovement>();
        Cannon helperCannon = cannonInstance.GetComponent<Cannon>();
        Health helperCannonHealth = helperCannon.GetHealth;

        helperCannonMovement.SetSwerveSpeed(helperCannonMovement.GetSwerveSpeed * -1f);
        helperCannon.SetWheelRotationSpeed(helperCannon.GetWheelRotationSpeed * -1f);
        helperCannonHealth.IsHelperCannon = true;

        MeshRenderer helperCannonMR = helperCannon.GetBodyOBJ.GetComponent<MeshRenderer>();
        helperCannonMR.sharedMaterial = helperCannonBodyMat;

        timer = 0f;
        helperCannonActive = true;

        ActivatePowerupVisual();
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

    public override void ActivatePowerupVisual()
    {
        GameManager.Instance.ActivatePowerupVisual(powerUpType, lifeTime);
    }
}
