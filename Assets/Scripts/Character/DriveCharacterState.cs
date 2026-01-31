using UnityEngine;

namespace Assets.Scripts.Character
{
    /// <summary>
    /// Character driving state
    /// </summary>
    public class DriveCharacterState : State
    {
        readonly PlayerController _owner;
        readonly Animator _animator;
        float _timePassed;
        public DriveCharacterState(PlayerController owner, Animator animator)
        {
            _owner = owner;
            _animator = animator;
            _timePassed = 0;
        }

        public override void OnStart()
        {
            GameManager.Instance.SwitchToCarCamera();
            GameManager.Instance.SetControllable(_owner.CurrentCar);

            _owner.transform.SetParent(_owner.CurrentCar.transform);

            // Nascondi le pizze quando il giocatore entra in auto
            _owner.SetPizzaVisibility(false);

            _animator.SetTrigger(_owner.DriveSettings.AnimationTrigger);
            _timePassed = 0;

            var navArrow = _owner.GetComponent<DriveNavigationArrow>();
            if (navArrow != null) navArrow.enabled = true;
        }

        public override void OnUpdate()
        {

        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnEnd()
        {
            GameManager.Instance.SwitchToPlayerCamera();
            var navArrow = _owner.GetComponent<DriveNavigationArrow>();
            if (navArrow != null)
            {
                navArrow.enabled = false;
                if (navArrow.arrowUI != null) navArrow.arrowUI.gameObject.SetActive(false);
            }
        }


        public override void OnTriggerEnter(Collider other)
        {

        }

        public override void OnTriggerExit()
        {

        }
    }
}