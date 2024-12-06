using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunBehaviour : MonoBehaviour, IDamage
{
    [Header("Animator")]
    [SerializeField] Animator animator;
    [SerializeField] private string OnShootName;

    [Header("Gun")]
    [SerializeField] public Transform posGun;
    [SerializeField] public Transform cam;
    [SerializeField] public LayerMask player;
    RaycastHit hit;

    public void DoDamage(int vld)
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        Debug.DrawRay(cam.position, cam.forward * 10f, Color.green);
        Debug.DrawRay(posGun.position, posGun.forward * 10f, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 direction = cam.TransformDirection(new Vector3(0, 0, 1));

            GameObject bulletObj = ObjectPoolingManager.Instance.GetBullet();

            animator.SetTrigger(OnShootName);

            bulletObj.transform.position = posGun.position;
            if (Physics.Raycast(cam.position, cam.forward, out hit, Mathf.Infinity, ~player))
            {
                bulletObj.transform.LookAt(hit.point);
            }
            else
            {
                Vector3 dir = cam.position + cam.forward * 10f;

            }

        }
    }


}