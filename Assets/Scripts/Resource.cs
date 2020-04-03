using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = System.Random;


public class Resource : MonoBehaviour
{
    private GameObject _player;
    private PlayerResources _playerResources;
    public GameObject resource;
    public GameObject depletedResource;
    private Animator _animator;
    public float respawnTime = 1f;
    public int resourceID;
    private int resourceQuantity;
    public int maxQuantity = 5;
    private PlayerEquipment _playerEq;
    private CustomAudioManager audioManager;
    public GameObject resourcePopup;
    public Texture icon;
    public int ExpID;
    private PlayerExperience _playerExperience;
    public Collider meshCollider;
    private bool isDepleted = false;
    
    void Start()
    {
        _player = GameObject.Find("Player");
        _playerResources = _player.GetComponent<PlayerResources>();
        _playerExperience = _player.GetComponent<PlayerExperience>();
        resourceQuantity = maxQuantity + Convert.ToInt32(UnityEngine.Random.Range(-maxQuantity*0.3f, maxQuantity*0.3f));
        _animator = _player.GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<CustomAudioManager>();
        _playerEq = _player.GetComponent<PlayerEquipment>();
    }

    void Update()
    {
    }

    public void OnMouseDown()
    {
        Vector3 playerPosition = _player.transform.position;
        Vector3 closestPoint = meshCollider.ClosestPoint(playerPosition);
        float distance = Vector3.Distance(playerPosition, closestPoint);
        if (distance < 3)
        {
            Gather();
        }
    }

    private void Gather()
    {
        if (!isDepleted)
        {
            int toolEff = _playerEq.toolEfficiency;
            _animator.SetTrigger("chop");
            int gatherValue;
            int tmp = resourceQuantity - toolEff;
            if (tmp >= 0)
            {
                gatherValue = toolEff;
            }
            else
            {
                gatherValue = toolEff + tmp;
            }
            _playerResources.AddResource(resourceID, gatherValue);
            resourceQuantity -= gatherValue;
            _playerExperience.AddExp(ExpID, Convert.ToUInt64(gatherValue));
            var popup = Instantiate(resourcePopup);
            popup.GetComponent<ResourcePopup>().SetText(gatherValue);
            popup.GetComponent<ResourcePopup>().SetIcon(icon);
            if (resourceQuantity <= 0)
            {
                audioManager.Play(GenerateSoundName(true, 1));
                resource.SetActive(false);
                depletedResource.SetActive(true);
                meshCollider.isTrigger = true;
                StartRespawn();
                isDepleted = true;
            }
            else
            {
                audioManager.Play(GenerateSoundName(false, 3));
            }
        }
    }

    IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        resource.SetActive(true);
        depletedResource.SetActive(false);
        resourceQuantity = maxQuantity + Convert.ToInt32(UnityEngine.Random.Range(-maxQuantity*0.3f, maxQuantity*0.3f));
        meshCollider.isTrigger = false;
        isDepleted = false;
    }
    
    public void StartRespawn()
    {
        var coroutine = WaitForRespawn();
        StartCoroutine(coroutine);
    }

    private string GenerateSoundName(bool isBreaking, int numberOfSounds)
    {
        string soundname = "";
        if (resourceID < 16)
        {
            soundname += "Wood";
        }
        if (resourceID > 15 & resourceID < 32)
        {
            soundname += "Stone";
        }

        if (isBreaking)
        {
            soundname += "Break";
        }
        else
        {
            soundname += "Hit";
        }
        soundname += UnityEngine.Random.Range(1, numberOfSounds+1).ToString();
    
        return soundname;
    }
}
