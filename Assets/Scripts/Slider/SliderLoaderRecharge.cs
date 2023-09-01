using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SliderLoaderRecharge : MonoBehaviour
{
    // Start is called before the first frame update

    public event EventHandler OnDepletion, OnFullRecharge;

    public event EventHandler OnActivation, OnDeactivation;

    protected bool _isRecharging = false;
    public bool IsRecharging { get { return _isRecharging; } }



    protected bool _isActive = false;
    public bool IsActive { get { return _isActive; } }



    protected void OnDepletionInvoke(object sender)
    {
        OnDepletion?.Invoke(sender, EventArgs.Empty);
        _isRecharging = true;
    
    
    }
    protected void OnFullRechargeInvoke(object sender)
    {
        OnFullRecharge?.Invoke(sender, EventArgs.Empty);
        _isRecharging = false;


    }




    public void OnActivationInvoke(object sender)
    {
        OnActivation?.Invoke(sender, EventArgs.Empty);
        _isActive = true;


    }
    public void OnDeactivationInvoke(object sender)
    {
        OnDeactivation?.Invoke(sender, EventArgs.Empty);
        _isActive = false;


    }




}
