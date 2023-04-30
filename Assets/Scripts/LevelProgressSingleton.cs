using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressSingleton : PersistentSingleton<LevelProgressSingleton>
{

   public string currentLevelName;
   public string NextLevelName;

   public float timer;

   private const string FinalLevelName = "FINISH";

   public bool IsLastLevel() {
        return this.NextLevelName.Equals(FinalLevelName);
   }
}
