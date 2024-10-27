using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HG;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] ToolBox[] toolBoxesConfig;

    private Queue<ToolBox> toolBoxes = new Queue<ToolBox>();
    public ToolBox toolBoxAny;
    public bool isUsingHammer;

    private ToolBox currentToolBox;
    private ToolBox doubleToolBox;

    private void Start()
    {
        foreach (var toolBox in toolBoxesConfig)
        {
            toolBoxes.Enqueue(toolBox);
        }

        UpdateCurrentToolbox();
    }


    public void AddToToolbox(Screw screw)
    {
        if (doubleToolBox != null && screw.type == doubleToolBox.screwType)
        {
            doubleToolBox.AddToSlot(screw);
            if (doubleToolBox.IsFull() && currentToolBox.IsFull())
            {
                DOVirtual.DelayedCall(.7f, () =>
               {
                   doubleToolBox.FullBoxEffect();
                   currentToolBox.FullBoxEffect();
                   DOVirtual.DelayedCall(.5f, () =>
                   {
                       foreach (var tool in toolBoxes)
                       {
                           tool.MoveEffect();
                       }
                       if (toolBoxes.Count > 0)
                       {
                           UpdateCurrentToolbox();
                       }
                       else print("========> win!");
                       doubleToolBox = null;
                   });
               });
            }
        }
        else if (screw.type == currentToolBox.screwType)
        {
            currentToolBox.AddToSlot(screw);
            if ((currentToolBox.IsFull() && doubleToolBox == null) || (currentToolBox.IsFull() && doubleToolBox != null && doubleToolBox.IsFull()))
            {
                DOVirtual.DelayedCall(.7f, () =>
                {
                    currentToolBox.FullBoxEffect();
                    if (doubleToolBox != null && doubleToolBox.IsFull()) doubleToolBox.FullBoxEffect();
                    DOVirtual.DelayedCall(.5f, () =>
                    {
                        foreach (var tool in toolBoxes)
                        {
                            tool.MoveEffect();
                        }
                        if (toolBoxes.Count > 0)
                        {
                            UpdateCurrentToolbox();
                        }
                        else print("========> win!");
                    });
                });
            }
        }
        else
        {
            toolBoxAny.AddToSlot(screw);
            if (toolBoxAny.IsFull())
            {
                print("==>>>>>>>> lose!");
            }
        }
    }

    public void UpdateCurrentToolbox()
    {
        currentToolBox = toolBoxes.Dequeue();
        foreach (var slot in toolBoxAny.slots)
        {
            if (slot.screw != null && slot.screw.type == currentToolBox.screwType)
            {
                AddToToolbox(slot.screw);
                slot.screw = null;
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (isUsingHammer)
            {
                RaycastHit2D hit1 = Physics2D.Raycast(mousePos, Vector2.zero);
                if (hit1.collider != null && hit1.collider.CompareTag("Bar"))
                {
                    hit1.collider.GetComponent<Collider2D>().enabled = false;
                    hit1.collider.GetComponent<Renderer>().enabled = false;
                    isUsingHammer = false;
                }
            }
            else
            {
                LayerMask screwMask = LayerMask.GetMask("Screw");
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, screwMask);
                if (hit.collider != null)
                {
                    var cols = Physics2D.OverlapCircleAll(hit.transform.position, 0.25f);
                    if (hit.collider.TryGetComponent<Screw>(out var screw) && !screw.CheckOverride(cols))
                    {
                        AddToToolbox(screw);
                        screw.GetComponent<HingeJoint2D>().enabled = false;
                        screw.enabled = false;
                    }
                }
            }
        }
    }

    public void UsingHammer()
    {
        isUsingHammer = true;
    }

    public void AddHole(int amount)
    {
        toolBoxAny.AddHole(amount);
    }

    public void DoubleToolBox()
    {
        doubleToolBox = currentToolBox;
        doubleToolBox.transform.DOLocalMoveX(doubleToolBox.transform.localPosition.x + 250f, .15f).SetEase(Ease.OutQuad);
        UpdateCurrentToolbox();
        currentToolBox.transform.DOLocalMoveX(currentToolBox.transform.localPosition.x + 250f, .15f).SetEase(Ease.OutQuad);
    }
}
