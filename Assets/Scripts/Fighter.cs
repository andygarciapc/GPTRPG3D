using UnityEngine;

public class Fighter : MonoBehaviour
{
    private Animator animator;
    private bool hasAnimator, hasCollider;
    private int animIDatk, animIDroll;
    private Collider collider;
    private Rigidbody rb;

    public float cooldownTime = 1.5f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1;

    private void Start()
    {
        hasAnimator = TryGetComponent(out animator);
        hasCollider = TryGetComponent(out collider);
        AssignAnimationIDs();
    }

    private void AssignAnimationIDs()
    {
        animIDatk = Animator.StringToHash("Attack1");
        animIDroll = Animator.StringToHash("Roll");
    }

    private void Update()
    {
        if (!hasAnimator) return;
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1")) { animator.SetBool("Attack1", false); }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2")) { animator.SetBool("Attack1", false); }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            animator.SetBool("Attack3", false);
            noOfClicks = 0;
        }

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        if (Time.time > nextFireTime)
        {
            if (Input.GetMouseButton(0))
            {
                OnClick();
            }
        }
    }

    public void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;
        if(noOfClicks == 1)
        {
            animator.SetBool("Attack1", true);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if (noOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            animator.SetBool("Attack1", false);
            animator.SetBool("Attack2", true);
        }
        if (noOfClicks >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            animator.SetBool("Attack2", false);
            animator.SetBool("Attack3", true);
        }
    }

    public void DoAttack()
    {
        /*
        if(hasAnimator){
            animator.SetTrigger(animIDatk);
        }
         */
    }

    public void DoRoll()
    {
        if (hasAnimator)
        {
            animator.SetTrigger(animIDroll);
            if (hasCollider)
            {
                collider.enabled = false;
                rb.AddForce(transform.forward * 5, ForceMode.Impulse);
            }
            //this.transform.Translate(transform.forward * 1f);
        }
    }

    public void ToggleCollider() 
    {
        if (hasCollider)
        {
            collider.enabled = true;
            Destroy(rb);
        }
    }
}