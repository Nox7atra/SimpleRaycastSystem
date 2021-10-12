using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomMove : MonoBehaviour
{
   private const float MaxOffset = 0.1f;
   private IEnumerator Start ()
   {
      var tr = transform;
      var startPos = tr.position;
      while (true)
      {
         yield return null;
         tr.position = startPos + new Vector3(
            Random.Range(0, MaxOffset),
            Random.Range(0, MaxOffset),
            Random.Range(0, MaxOffset)
         );
      }
   }
}
