using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class BehaviourBoss : EnemyBehaviour
{
   [SerializeField]
   private GameObject flower;

   [SerializeField] private Animator _anim;

   readonly Random ran = new Random();
   [SerializeField] private Projectile _p;

   [SerializeField] private float attackDelay;
   protected override IEnumerator Condition()
   {
      _anim = transform.parent.GetChild(1).GetComponent<Animator>();
      u.StopPath();
      tracking = false;
      while (true)
      {
         var choice = ran.Next(0, 3);
         Debug.Log("Chose " + choice);

         switch (choice)
         {
            case 0:
               yield return StartCoroutine(AttackSpread());
               break;
            case 1:
               yield return StartCoroutine(AttackAccurate());
               break;
            case 2:
               yield return StartCoroutine(AttackRandom());
               break;
         }

         yield return new WaitForSeconds(attackDelay);
      }
   }

   protected override IEnumerator OnDeath()
   {
      Instantiate(flower);
      Destroy(transform.parent.gameObject);
      yield return null;
   }
   
   private IEnumerator AttackSpread()
   {
      if (_a == null) yield break;
      
      float angle = 0;
      _anim.Play("BossShoot");
      for (int i = 0; i < 4; i++)
      {
         Debug.Log(angle);
         _a.RequestProjectile(_p, 1, angle);
         angle += 90;
      }
      _anim.Play("BossIdle");

      yield return new WaitForSeconds(0.8f);
      angle = 45;

      _anim.Play("BossShoot");
      for (int i = 0; i < 4; i++)
      {
         _a.RequestProjectile(_p, 1, angle);
         angle += 90;
      }
      _anim.Play("BossIdle");

      yield return new WaitForSeconds(0.8f);
      angle = 0;

      _anim.Play("BossShoot");
      for (int i = 0; i < 8; i++)
      {
         _a.RequestProjectile(_p, 1, angle);
         angle += 45;
      }
      _anim.Play("BossIdle");
      yield return null;
   }

   private IEnumerator AttackAccurate()
   {
      if (_a == null) yield break;

      float angle = getAngleFromVectors(new Vector2(u.target.position.x, u.target.position.y));
      angle -= 90;
      for(int i = 0; i <= 8; i++)
      {
         _anim.Play("BossShoot");
         _a.RequestProjectile(_p, 1, angle + 15);
         _a.RequestProjectile(_p, 1, angle);
         _a.RequestProjectile(_p, 1, angle - 15);
         _anim.Play("BossIdle");
         yield return new WaitForSeconds(0.3f);
      }
      yield return null;
   }

   private IEnumerator AttackRandom()
   {
      if (_a == null) yield break;
      float angle = 0;
      for (int i = 0; i < 5; i++)
      {
         _anim.Play("BossShoot");
         for (int j = 0; j < 8; j++)
         {
            angle = ran.Next(0, 359);
            _a.RequestProjectile(_p, 1, angle);
            yield return new WaitForSeconds(0.02f);
         }
         _anim.Play("BossIdle");
         yield return new WaitForSeconds(0.4f);
      }
      yield return null;
   }
   
   private float getAngleFromVectors(Vector2 lookPoint)
   {
      Vector2 rotation = (lookPoint - new Vector2(transform.position.x, transform.position.y) ).normalized;
      Vector2 current = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad),
         Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad));
      float dot = (rotation.x * current.x) + (rotation.y * current.y);
      float mag = Mathf.Sqrt((rotation.x * rotation.x) + (rotation.y * rotation.y)) * Mathf.Sqrt((current.x * current.x) + (current.y * current.y));
      float theta = math.degrees(Mathf.Acos(dot / mag));
      return theta;
   }
}
