using UnityEngine;

namespace Assets.Scripts.Character
{
    /// <summary>
    /// Stato di uscita dall'auto
    /// </summary>
    public class ExitCarCharacterState : State
    {
        readonly PlayerController _owner;
        readonly Animator _animator;
        readonly float _clipLength;
        float _timePassed;
        public ExitCarCharacterState(PlayerController owner, Animator animator)
        {
            _owner = owner;
            _animator = animator;
            _clipLength = _owner.ExitCarSettings.Clip.length;
        }

        public override void OnStart()
        {
            _animator.SetTrigger(_owner.ExitCarSettings.AnimationTrigger);
            _timePassed = 0;

        }

        public override void OnUpdate()
        {
            _timePassed += Time.deltaTime;

            if (_owner.transform.rotation != _owner.CurrentCar.ExitPivot.rotation)
            {
                _owner.transform.rotation =
                    Quaternion.RotateTowards(
                        _owner.transform.rotation,
                        _owner.CurrentCar.ExitPivot.rotation,
                        _owner.RotationSpeed * Time.deltaTime);

                if (Quaternion.Angle(_owner.transform.rotation, _owner.CurrentCar.ExitPivot.rotation)
                    > _owner.MinimumStoppingRotationAngle)
                {
                    return;
                }

                _owner.transform.rotation = _owner.CurrentCar.ExitPivot.rotation;
            }

            if (_timePassed >= _clipLength)
            {
                _owner.SetIdle();
                return;
            }
        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnEnd()
        {
            _owner.ForgetCar();

            // Mostra le pizze se il giocatore ha ancora da consegnare
            if (_owner.HasPizzas)
            {
                _owner.SetPizzaVisibility(true);
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