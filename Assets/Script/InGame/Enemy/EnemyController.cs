using UnityEngine;
using UnityEngine.AI;

namespace InGame.Enemy
{
    /// <summary>
    /// ìGÇÃêßå‰
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private EnemyStateBase[] _states;
        [SerializeField] private EnemyAnimationPlayer _animation;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Transform _target;

        private EnemyStateBase _currentState;
        private EnemyStateContext _context;

        private void Start()
        {
            _context = new(_animation, _agent, _target, this.transform);

            foreach (var state in _states)
            {
                if (state.IsStart)
                {
                    ChangeState(state);
                    break;
                }
            }
        }

        private void ChangeState(EnemyStateBase state)
        {
            _currentState?.ExitState(_context);
            _currentState = state;
            _currentState?.EnterState(_context);
        }

        private void Update()
        {
            _currentState?.UpdateState(_context);

            if (!_currentState.IsExitCondition(_context)) return;

            foreach (var state in _states)
            {
                if (state == _currentState) continue;

                if (state.IsStartCondition(_context))
                {
                    ChangeState(state);
                    return;
                }
            }
        }

    }
}
