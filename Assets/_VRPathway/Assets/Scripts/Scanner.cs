using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class Scanner : XRGrabInteractable
{
    
    [Header("Scanner Data")]
    public Animator animator;
    public LineRenderer laserRenderer;
    
    public TextMeshProUGUI targetName;
    public TextMeshProUGUI targetPosition;

    protected override void Awake()
    {
        base.Awake();
        laserRenderer.gameObject.SetActive(false);
        targetName.gameObject.SetActive(false);
        targetPosition.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        animator.SetBool("Opened", true);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        animator.SetBool("Opened", false);
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);
        ScannerActivated(true);
    }
    
    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        base.OnDeactivated(args);
        ScannerActivated(false);
    }
    
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (laserRenderer.gameObject.activeSelf)
            ScanForObjects();
    }
    
    private void ScannerActivated(bool isActivated)
    {
        laserRenderer.gameObject.SetActive(isActivated);
        targetName.gameObject.SetActive(isActivated);
        targetPosition.gameObject.SetActive(isActivated);
    }
    
    private void ScanForObjects()
    {
        RaycastHit hit;
        
        Vector3 worldHit = laserRenderer.transform.position + laserRenderer.transform.forward * 1000.0f;
        
        if (Physics.Raycast(laserRenderer.transform.position, laserRenderer.transform.forward, out hit))
        {
            worldHit = hit.point;
            
            targetName.SetText(hit.collider.name);
            targetPosition.SetText(hit.collider.transform.position.ToString());
        }
        
        laserRenderer.SetPosition(1, laserRenderer.transform.InverseTransformPoint(worldHit));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
