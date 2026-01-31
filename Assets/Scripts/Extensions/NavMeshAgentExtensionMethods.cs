using UnityEngine.AI;

namespace Assets.Scripts.Extensions
{
    public static class NavMeshAgentExtensionMethods
    {
        /// <summary>
        /// Extension Method to understand when the Agent with a Path has completely stopped
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="minimumSqrMagnitude"></param>
        /// <returns></returns>
        public static bool IsAgentStopped(this NavMeshAgent agent, float minimumSqrMagnitude = 0f)
        {
            return agent.pathPending &&
                    agent.remainingDistance <= agent.stoppingDistance &&
                    !agent.hasPath ||
                    agent.velocity.sqrMagnitude <= minimumSqrMagnitude;
        }
    }
}
