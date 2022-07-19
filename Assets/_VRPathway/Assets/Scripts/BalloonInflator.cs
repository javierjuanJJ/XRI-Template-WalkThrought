using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BalloonInflator : XRGrabInteractable
{
    
    [Header("Balloon Data")]
    public Transform attachPoint;
    public Balloon balloonPrefab;
    
    private Balloon m_BalloonInstance;
    
    private XRBaseController m_Controller;
    
    
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (isSelected && m_Controller != null)
        {
            m_BalloonInstance.transform.localScale = Vector3.one * Mathf.Lerp(1.0f, 4.0f, m_Controller.activateInteractionState.value);
        }

    }
    
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        m_BalloonInstance = Instantiate(balloonPrefab, attachPoint);
        
        var controllerInteractor = args.interactorObject as XRBaseControllerInteractor;
        m_Controller = controllerInteractor.xrController;
        
        m_Controller.SendHapticImpulse(1, 0.5f);
        Debug.Log(m_Controller);
        
    }
    
    
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        Destroy(m_BalloonInstance.gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
