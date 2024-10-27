using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ToolBox : MonoBehaviour
{
    public ScrewType screwType;
    public int amountHole;
    public Slot hole;

    public List<Slot> slots = new List<Slot>();

    [SerializeField] Transform cover;

    private void Start()
    {
        AddHole(amountHole);
    }

    private void SpawnHole()
    {
        var holeIns = Instantiate(hole, transform);
        slots.Add(holeIns);
    }

    public void AddHole(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnHole();
        }
    }

    public bool IsFull()
    {
        foreach (var slot in slots)
        {
            if (slot.screw == null) return false;
        }
        return true;
    }

    public void AddToSlot(Screw screw)
    {
        if (!IsFull())
        {
            foreach (var slot in slots)
            {
                if (slot.screw == null)
                {
                    screw.UnscrewEffect(slot.transform);
                    screw.transform.SetParent(slot.transform);
                    slot.screw = screw;

                    break;
                }
            }
        }
    }

    public void MoveEffect()
    {
        transform.DOLocalMoveX(transform.localPosition.x + 500f, .15f).SetEase(Ease.OutQuad);
    }

    public void FullBoxEffect()
    {
        var coverStartPos = cover.localPosition;
        cover.localPosition = coverStartPos + new Vector3(0, 1000, 0);
        cover.gameObject.SetActive(true);
        cover.DOLocalMove(coverStartPos, .2f).SetEase(Ease.OutQuad);
        transform.DOLocalMoveX(transform.localPosition.x + 500f, .15f).SetEase(Ease.OutQuad).SetDelay(.35f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
