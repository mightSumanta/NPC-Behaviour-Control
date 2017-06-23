using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter2;
using Chapter3;
using Chapter4;


namespace Chapter7
{
	public class BigItemPickUpThrow : MonoBehaviour 
	{
        public Collider[] colliders;
        public PhysicMaterial myMaterial;
        public Rigidbody[] rigidBodies;
        public Vector3 itemLocalPosition;
        public Quaternion itemLocalRotation;
        private Vector3 throwDirection;
        private BigItemManager_Master bigItemManagerMasterScript;
        public float throwForce;
        public string pickupLayer;
        public string throwLayer;
        public float myVolume;
        public AudioClip throwSound;
        public Material tranparentMat;
        private Material defaultMat;

        void OnEnable()
        {
            bigItemManagerMasterScript = GetComponent<BigItemManager_Master>();
            bigItemManagerMasterScript.PickupActionEvent += pickupBigItem;
            bigItemManagerMasterScript.putDownItemEvent += putDownAction;
            bigItemManagerMasterScript.ThrowBigItemEvent += throwBigItem;
        }

        void OnDisable()
        {
            bigItemManagerMasterScript.PickupActionEvent -= pickupBigItem;
            bigItemManagerMasterScript.putDownItemEvent -= putDownAction;
            bigItemManagerMasterScript.ThrowBigItemEvent -= throwBigItem;
        }

        void pickupBigItem(Transform parent)
        {
            if (!transform.CompareTag(GameManager_References._itemTag)) 
            {
                transform.SetParent(parent);
                //Debug.Log(parent.name);
                setIsKinematicToTrue();
                turnCollidersOff();
                transform.localPosition = itemLocalPosition;
                transform.localRotation = itemLocalRotation;
                transform.gameObject.layer = LayerMask.NameToLayer(pickupLayer);
                defaultMat = GetComponent<Renderer>().material;
                GetComponent<Renderer>().material = tranparentMat;
                GameManager_References._player.GetComponent<PlayerManager_Master>().callBigItemPickupEvent();
            }
         }

        void throwBigItem()
        {
            //Debug.Log(transform.root.name);
            throwDirection = transform.root.forward;
            transform.gameObject.layer = LayerMask.NameToLayer(throwLayer);
            transform.parent = null;
            setIsKinematicToFalse();
            turnCollidersOn();
            AudioSource.PlayClipAtPoint(throwSound, transform.position, myVolume);
            GetComponent<Renderer>().material = defaultMat;
            transform.GetComponent<Rigidbody>().AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }

        void putDownAction()
        {
            float temp = throwForce;
            throwForce = 1;
            throwBigItem();
            throwForce = temp;
        }

        void setIsKinematicToTrue()
        {
            if (rigidBodies.Length > 0)
            {
                foreach (Rigidbody rBody in rigidBodies)
                    rBody.isKinematic = true;
            }
        }

        void setIsKinematicToFalse()
        {
            if (rigidBodies.Length > 0)
            {
                foreach (Rigidbody rBody in rigidBodies)
                    rBody.isKinematic = false;
            }
        }

        void turnCollidersOn()
        {
            if (colliders.Length > 0)
            {
                foreach (Collider col in colliders)
                {
                    col.enabled = true;

                    //if (myMaterial != null)
                    //{
                    //    col.material = myMaterial;
                    //}
                }
            }
        }

        void turnCollidersOff()
        {
            if (colliders.Length > 0)
            {
                foreach (Collider col in colliders)
                {
                    col.enabled = false;
                }
            }
        }
    }

}
