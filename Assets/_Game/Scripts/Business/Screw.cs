using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum ScrewType
{
    Red,
    Blue,
    Yellow,
    None
}
public class Screw : MonoBehaviour
{
    public ScrewType type;

    [SerializeField] GameObject top, bot, mask;
    private void Start()
    {
        var rbParent = transform.parent.GetComponent<Rigidbody2D>();
        GetComponent<HingeJoint2D>().connectedBody = rbParent;
    }
    public bool CheckOverride(Collider2D[] cols)
    {
        foreach (var col in cols)
        {
            if (col.gameObject.layer > transform.parent.gameObject.layer) return true;
        }
        return false;
    }

    public void UnscrewEffect(Transform target)
    {
        top.transform.DOKill();
        bot.transform.DOKill();
        mask.transform.DOKill();
        top.SetActive(false);
        bot.SetActive(true);
        mask.SetActive(true);
        bot.transform.DOLocalMoveY(1f, .25f).SetEase(Ease.Linear).OnComplete(() =>
      {
          transform.DOMove(target.position, .25f).SetEase(Ease.Linear).OnComplete(() =>
          {
              bot.transform.DOLocalMoveY(0, .25f).SetEase(Ease.Linear).OnComplete(() =>
              {
                  top.SetActive(true);
                  bot.SetActive(false);
                  mask.SetActive(false);
              });
          }).SetDelay(.1f);
      });
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, .25f);
    }
}
