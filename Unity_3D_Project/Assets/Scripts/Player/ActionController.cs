using System;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float _range;

    private bool _pickupActivated = false;

    private RaycastHit _hitinfo;

    [SerializeField]
    private LayerMask _layerMask;
    
    [SerializeField]
    private Text _actionText;

    private PlayerInput _input;

    private void Awake()
    {
        _input = GetComponentInParent<PlayerInput>();
        //_actionText = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (_input.Interaction)
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CheckItem()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out _hitinfo,10f, _layerMask))
        {
            if (_hitinfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else
            ItemInfoDisappear();
    }

    private void ItemInfoAppear()
    {
        _pickupActivated = true;
        _actionText.gameObject.SetActive(true);
        _actionText.text = _hitinfo.transform.GetComponent<ItemPickUp>().item.name + "È¹µæ" + "<color=yellow>" + "¿ÞÅ¬¸¯" + "</color>";
    }

    private void ItemInfoDisappear()
    {
        _pickupActivated = false;
        _actionText.gameObject.SetActive(false);
    }

    private void CanPickUp()
    {
        if(_pickupActivated)
        {
            if(_hitinfo.transform !=null)
            {
                Debug.Log(_hitinfo.transform.GetComponent<ItemPickUp>().item.itemName + "È¹µæ Çß½À´Ï´Ù.");
                Destroy(_hitinfo.transform.gameObject);
                ItemInfoDisappear();
            }
        }
    }

}
