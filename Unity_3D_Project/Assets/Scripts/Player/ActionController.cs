using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float _range = 10f;

    private bool _pickupActivated = false;

    private RaycastHit _hitinfo;

    private int _layerMask;

    private Text _actionText;
    private PlayerInput _input;
    private Camera _camera;

    private Inventory _inventory;
    [SerializeField]
    private int selectItemNum;

    public event UnityAction<GameObject> PlayerSound;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _camera = GetComponentInChildren<Camera>();
        _inventory = GetComponentInChildren<Inventory>();
        _actionText = GetComponentInChildren<Text>();
        _layerMask = 1 << (LayerMask.NameToLayer("Item"));
    }

    private void Update()
    {
        CheckItem();
        TryAction();
        SelectSlot();
        TryUseItem();
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
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hitinfo, _range, _layerMask))
            ItemInfoAppear();
        else
            ItemInfoDisappear();
    }

    private void ItemInfoAppear()
    {
        _pickupActivated = true;
        _actionText.gameObject.SetActive(true);
        _actionText.text = _hitinfo.transform.GetComponent<Item>().itemData.name + "È¹µæ" + "<color=yellow>" + "¿ÞÅ¬¸¯" + "</color>";
    }

    private void ItemInfoDisappear()
    {
        _pickupActivated = false;
        _actionText.gameObject.SetActive(false);
    }

    private void CanPickUp()
    {
        if (_pickupActivated)
        {
            if (_hitinfo.transform != null)
            {
                if (_inventory.AcquireItem(_hitinfo.transform.GetComponent<Item>()))
                {
                    Debug.Log(_hitinfo.transform.GetComponent<Item>().itemData.itemName + "È¹µæ Çß½À´Ï´Ù.");
                    _hitinfo.transform.gameObject.SetActive(false);
                    ItemInfoDisappear();
                }
            }
        }
    }

    private void SelectSlot()
    {
        if (_input.selectItem != 0)
        {
            _inventory.SelectSlot(selectItemNum);
            if (_input.selectItem > 0)
            {
                ++selectItemNum;
                if (selectItemNum > 2)
                    selectItemNum = 0;
            }
            else
            {
                --selectItemNum;
                if (selectItemNum < 0)
                    selectItemNum = 2;
            }
            _inventory.SelectSlot(selectItemNum);
        }
    }
    private void TryUseItem()
    {
        if (_input.useItem)
        {
            if (_inventory.Useitem())
                PlayerShoutOut();
        }
    }

    public void PlayerShoutOut()
    {
        PlayerSound.Invoke(gameObject);
    }

  
}
