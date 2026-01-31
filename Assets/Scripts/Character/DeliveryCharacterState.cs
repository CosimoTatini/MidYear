using UnityEngine;

/// <summary>
/// Stato di pizza delivery
/// </summary>
internal class DeliveryCharacterState : State
{
    private PlayerController owner;
    private Animator animator;
    private Rigidbody rigidbody;
    private Vector3 moveDirection;

    public DeliveryCharacterState(PlayerController owner, Animator animator)
    {
        this.owner = owner;
        this.animator = animator;
        rigidbody = owner.GetComponent<Rigidbody>();

    }
    public override void OnStart()
    {
        rigidbody.isKinematic = false;
        animator.SetTrigger(owner.DeliveryStateSettings.AnimationTrigger);
        owner.NavigationArrow.enabled = false;
        owner.SetPizzaVisibility(true);
    }
    public override void OnUpdate()
    {
        if (owner.InteractionRequest)
        {
            owner.SetNearestCar();

            if (owner.CurrentCar != null)
            {
                owner.SetWalkAgent();
                return;
            }
        }

        if (owner.Direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            owner.transform.rotation = Quaternion.RotateTowards(owner.transform.rotation, targetRotation, owner.RotationSpeed * Time.deltaTime);
        }
    }
    public override void OnFixedUpdate()
    {
        moveDirection = owner.CameraTransform.TransformDirection(owner.Direction).normalized;
        moveDirection.y = 0;
        Vector3 velocity = moveDirection * owner.MaxSpeed * Time.fixedDeltaTime;
        velocity.y = rigidbody.linearVelocity.y;
        rigidbody.linearVelocity = velocity;
    }

    public override void OnEnd()
    {
        owner.SetPizzaVisibility(false);
    }

    public override void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DeliveryZone"))
        {
            owner.DeliverPizza();
            owner.enabled=false;
        }
    }


    public override void OnTriggerExit()
    {

    }


}