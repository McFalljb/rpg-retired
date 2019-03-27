using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA.Scriptable;
using SA.MonoActions;

namespace SA
{
    public class StatesManager : MonoBehaviour, IParryable, ILockable, IHittable
    {
        public GameObject activeModel;

        public StateActions initAction;
        public Inventory inventory;
        public PlayerProfile profile;
        public CharacterViz character;

        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public AnimatorHook a_hook;
        [HideInInspector]
        public new Rigidbody rigidbody;
        [HideInInspector]
        public Collider controllerCollider;

        [HideInInspector]
        public LayerMask ignoreLayers;
        [HideInInspector]
        public LayerMask ignoreForGroundCheck;

        public bool isPlayer;
        public float delta;
        public Transform mTransform;

        public State currentState;

        [Header("Inputs")]
        public float vertical;
        public float horizontal;
        public float moveAmount;
        public Vector3 rotateDirection;
        public Vector3 rollDirection;
        public float generalT;
        public bool rb;
        public bool rt;
        public bool lb;
        public bool lt;

        public bool isTwoHanded;
        public bool isLockingOn;
        public bool isGrounded;
        public bool isBackstep;
        public bool isParrying;
        public bool isStunned;
        public bool damageCollidersAreOpen;
        public bool forceEndActions;
        public bool canRotate;
        public bool dontInterrupt;

        public EnemyStatManager enStatManager;
        public PlayerStatsManager playerStatsManager;

        public Spell currentSpell;
        public GameObject currentProjectile;
        public State throwProjectileState;

        public Transform currentTarget;

        public DamageCollider unarmedCollider;

        public enum InputButton
        {
            rb, rt, lb, lt
        }

        void Start()
        {
            mTransform = this.transform;

            Init();

            if (initAction != null)

                initAction.Execute(this);


        }

        public void Init()
        {
            mTransform = this.transform;
            SetupAnimator();
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.angularDrag = 999;
            rigidbody.drag = 4;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            gameObject.layer = 8;
            ignoreLayers = ~(1 << 9);
            ignoreForGroundCheck = ~(1 << 9 | 1 << 10);

            a_hook = activeModel.AddComponent<AnimatorHook>();
            if(a_hook == null)
            {
                a_hook = activeModel.AddComponent<AnimatorHook>();
            }
            a_hook.Init(this);

        }

        void SetupAnimator()
        {
            if (activeModel == null)
            {
                anim = GetComponentInChildren<Animator>();
                activeModel = anim.gameObject;
            }

            if (anim == null)
                anim = GetComponentInChildren<Animator>();

            anim.applyRootMotion = false;
            //anim.GetBoneTransform(HumanBodyBones.LeftHand).localScale = Vector3.one;
            //anim.GetBoneTransform(HumanBodyBones.RightHand).localScale = Vector3.one;
        }

        private void FixedUpdate()
        {
            delta = Time.deltaTime;

            if (currentState != null)
            {
                currentState.FixedTick(this);
            }
        }

        void Update()
        {

            delta = Time.deltaTime;

            if (currentState != null)
            {
                currentState.Tick(this);
            }

            //  if(forceInit)
            //{
            //  Tick(Time.deltaTime);
            //}
        }

        public void PlayAnimation(string targetAnim, bool isMirror)
        {
            canRotate = true;
            anim.SetBool("mirror", isMirror);
            anim.CrossFade(targetAnim, 0.2f);
            anim.SetBool("isInteracting", true);
        }

        public void PlayAnimation(string targetAnim)
        {
            canRotate = true;
            Debug.Log(targetAnim);
            anim.CrossFade(targetAnim, 0.2f);
            anim.SetBool("isInteracting", true);
        }

        #region Manager Functions
        public void ThrowProjectile()
        {
            if (currentProjectile == null)
                return;

            GameObject go = Instantiate(currentProjectile) as GameObject;
            go.transform.position = mTransform.position + (Vector3.up * 1.5f) + mTransform.forward;
            Rigidbody pr = go.GetComponent<Rigidbody>();
            pr.velocity = mTransform.forward * 5;

        }

        public void CreateSpellParticle()
        {
            //if (currentSpell.spellParticle != null)
            //{
            //	GameObject go = Instantiate(currentSpell.spellParticle) as GameObject;
            //	go.SetActive(false);
            //	go.transform.position = mTransform.position;
            //	go.transform.rotation = mTransform.rotation;
            //	go.SetActive(true);
            //}
        }

        public void HandleDamageCollision(StatesManager targetStates)
        {
            if (targetStates == this)
                return;

            targetStates.GetHit();
        }

        public void GetHit()
        {

        }

        public void SetDamageCollidersStatus(bool status)
        {
            bool mirror = anim.GetBool("mirror");
            if (!mirror)
            {
                inventory.rightHandWeapon.runtime.weaponHook.SetColliderStatus(status);
            }
            else

            {
                inventory.leftHandWeapon.runtime.weaponHook.SetColliderStatus(status);
            }
        }

        public Condition canBeParriedLogic;
        public State waitForAnimState;

        public void OnGettingParried()
        {
            if (canBeParriedLogic)
            {
                bool success = canBeParriedLogic.CheckCondition(this);
                if (success)
                {
                    currentState = waitForAnimState;
                }
            }
        }

        public void isGettingParried(StatesManager s)
        {
            if (isStunned)
            {
                PlayAnimation("parry_recieved");
                s.mTransform.position = mTransform.position + (mTransform.forward * 1.4f);
                s.mTransform.rotation = Quaternion.Inverse(mTransform.rotation);
            }
        }

        public Transform lockOnTransform;

        public Transform LockOn()
        {
            if (!isPlayer)
            {
                if (lockOnTransform == null)
                {
                    lockOnTransform = anim.GetBoneTransform(HumanBodyBones.Hips);
                    
                }

                if (lockOnTransform == null)
                {
                    lockOnTransform = mTransform;
                }
                return lockOnTransform;
            }
            else
            {
                return null;
            }
        }

        public void ParentWeaponUnderHand(StatesManager states, Weapon w, HumanBodyBones targetBone, bool isLeft = false)
        {
            Transform b = states.anim.GetBoneTransform(targetBone);

            w.runtime.modelInstance.transform.parent = b;
            if(isLeft)
            {
                w.runtime.modelInstance.transform.localPosition = states.inventory.leftHandPosition.value;
                w.runtime.modelInstance.transform.localEulerAngles = states.inventory.leftHandRotation.value;
            }
            else
            {
                w.runtime.modelInstance.transform.localPosition = Vector3.zero;
                w.runtime.modelInstance.transform.localEulerAngles = Vector3.zero;
            }

            w.runtime.modelInstance.transform.localScale = Vector3.one;
            w.runtime.modelInstance.SetActive(true);
        }

        public void SetWearedCloth(string id, ResourcesManager rm, StatesManager states)
        {
            Item item = rm.GetItemInstance(id);
            if(item !=null)
            {
                ClothItem c = (ClothItem)item;
                states.character.WearCloth(c);
            }
        }

        public void OnHit(StatesManager hitter)
        {
            ItemActions a = hitter.inventory.currentItemAction;
            if (a == null)
            {
                Debug.Log("Current item action is null");
                return;
            }

            a.OnHit(hitter, this);
            //TODO: damage calc
        }

        #endregion
    }

}
