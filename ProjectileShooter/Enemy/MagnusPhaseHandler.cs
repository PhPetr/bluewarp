using System;
using Nez;

namespace bluewarp
{
    /// <summary>
    /// Handles Boss Magnus phases.
    /// When both his hands are destroyed, activate collider of main body.
    /// </summary>
    public class MagnusPhaseHandler : Component
    {
        private const string LeftHandName = "magnusLeft";
        private const string RightHandName = "magnusRight";
        private bool LeftHandDead;
        private bool RightHandDead;

        public MagnusPhaseHandler()
        {
            LeftHandDead = false;
            RightHandDead = false;
        }

        /// <summary>
        /// Handles when a hand is destroyed.
        /// If both hand are destoryed, activates main body collider.
        /// </summary>
        /// <param name="handName">Name of hand being destroyed</param>
        /// <param name="mainBody">Collider of main body</param>
        public void HandDestroyed (string handName, CircleCollider mainBody)
        {
            Debug.Log("Phase handler called");

            if (handName != LeftHandName && handName != RightHandName) return;

            if (handName == LeftHandName) LeftHandDead = true;
            if (handName == RightHandName) RightHandDead = true;
            Debug.Log("Triggered HAND DESTROYED");
            if (LeftHandDead && RightHandDead) ActivateMainBodyCollider(mainBody);
        }

        private void ActivateMainBodyCollider(CircleCollider mainBody)
        {
            Debug.Log("Enabling main body collider");
            mainBody.SetEnabled(true);
        }
    }
}
