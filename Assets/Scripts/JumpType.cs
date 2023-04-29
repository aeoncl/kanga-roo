using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Jump", menuName = "Movement/Jump")]
public class JumpType : ScriptableObject
{
        public float initalJumpForce;
        public AnimationCurve gravityRise;
        public AnimationCurve gravityFall;
        public float gravityOnRelease;

}
