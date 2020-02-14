using Godot;
using System;

public class PlayerInputs : Node2D
{


    [Signal] delegate void BlockPlaced();

    public override void _Ready()
    {
        Player.inventoryUsables.Add(Usable.Type.Dirt, 25);
        ConnectSignals();
    }

    private void ConnectSignals()
    {
        Connect("BlockPlaced", (Node)GetTree().GetNodesInGroup("ToolBar")[0], "SendRefresh"); // Pour Actualisation de la ToolBar
    }

  
    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("mouse1"))
        {
            if (PlayerState.GetState() == PlayerState.State.Normal)
            {
                ClickNormalState();
            }
        }else if (Input.IsActionJustPressed("mouse2"))
        {

        }
    }

    private void ClickNormalState()
    {
        Usable.Type type = Player.UsableSelected;
        Usable.Category cat = Usable.category[(int)type];
        if (MouseInRange())
        {
            if(cat==Usable.Category.Block)
            {
                int amount = Player.inventoryUsables.GetItemCount(type);
                if (amount>0)
                {
                    Vector2 mouse = Convertion.Location2WorldFloor(GetGlobalMousePosition());
                    bool succeed = PlaceBlock.Place((int)mouse.x, (int)mouse.y, Usable.blocks[type]);
                    if (succeed)
                    {
                        Player.inventoryUsables.Remove(type, 1);
                        EmitSignal("BlockPlaced");
                    }
                }
            }
        }
    }

    private bool MouseInRange()
    {
        Vector2 mouse = GetLocalMousePosition();
        float distance = mouse.Length();
        return (distance<100)&&(distance>10);
        
    }





}
