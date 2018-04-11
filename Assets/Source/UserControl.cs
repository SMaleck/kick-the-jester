using Assets.Source.Entities.Jester.Components;
using UnityEngine;

public class UserControl : MonoBehaviour
{
    private Rigidbody body;
    private KickForceManager kickForceManager;    

    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
        kickForceManager = gameObject.GetComponent<KickForceManager>();
    }

	void Update ()
    {        
        if (IsKickAction() && CanKick())
        {
            ApplyKick();
        }
	}


    private bool IsKickAction()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }


    private bool CanKick()
    {
        return true;
    }

    
    // Applies the actual Kick to the Jester
    private void ApplyKick()
    {        
        Vector3 AppliedForce = kickForceManager.GetAppliedKickForce();
        body.AddForce(AppliedForce);        
    }
}
