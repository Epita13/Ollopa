using Godot;
using System;

public class PlayerInputs : Node2D
{


    [Signal] delegate void BlockPlaced();

    private Vector2 mousePos;
    private PlayerState.State LastState;
    private Usable.Type LastSelectedUsable;

    public override void _Ready()
    {
        Player.inventoryUsables.Add(Usable.Type.Dirt, 10);
        Player.inventoryUsables.Add(Usable.Type.Grass, 10);
        Player.inventoryUsables.Add(Usable.Type.Stone, 10);
        ConnectSignals();
    }

    private void ConnectSignals()
    {
        Connect("BlockPlaced", (Node)GetTree().GetNodesInGroup("ToolBar")[0], "SendRefresh"); // Pour Actualisation de la ToolBar
    }

  
    public override void _Process(float delta)
    {
        
        if (LastState!=PlayerState.GetState() || LastSelectedUsable!=Player.UsableSelected)
        {
            World.UIBlockTilemap.Clear();
            LastSelectedUsable = Player.UsableSelected;
            LastState = PlayerState.GetState();
        }

        /*Affichage*/
        if (PlayerState.GetState() == PlayerState.State.Normal)
        {
            mousePos = Convertion.Location2WorldFloor(GetGlobalMousePosition());
            NormalState();
        }


        /*Inputs*/

        if (Input.IsActionJustPressed("mouse1"))
        {
            if (PlayerState.GetState() == PlayerState.State.Normal)
            {
                ClickNormalState();
            }
        }
        else if (Input.IsActionJustPressed("mouse2"))
        {

        }
    }


    private void NormalState()
    {
        Usable.Type type = Player.UsableSelected;
        Usable.Category cat = Usable.category[(int)type];
        if(cat==Usable.Category.Block)
        {
            World.UIBlockTilemap.Clear();
            int amount = Player.inventoryUsables.GetItemCount(type);
            bool canPlace = PlaceBlock.CanPlace((int)mousePos.x, (int)mousePos.y, out canPlace);
            if (amount>0 && canPlace && MouseInRange())
            {
                World.UIBlockTilemap.SetCell((int)mousePos.x, -(int)mousePos.y+Chunk.height, 1);
            }
            else
            {
                World.UIBlockTilemap.SetCell((int)mousePos.x, -(int)mousePos.y+Chunk.height, 0);
            }
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
                    bool succeed = PlaceBlock.Place((int)mousePos.x, (int)mousePos.y, Usable.blocks[type]);
                    if (succeed)
                    {
                        Player.inventoryUsables.Remove(type, 1);
                        EmitSignal("BlockPlaced");
                    }
                }
            }else
            {
                World.GetChunk((int)mousePos.x).RemoveBlock(Chunk.GetLocaleX((int)mousePos.x), (int)mousePos.y);
            }
        }
    }

    private bool MouseInRange()
    {
        Vector2 playerPos = Convertion.Location2World(PlayerMouvements.instance.Position);
        float xmin = Mathf.Floor(playerPos.x-PlayerMouvements.size.x/2);
        float xmax = Mathf.Floor(playerPos.x+PlayerMouvements.size.x/2);
        float ymin = Mathf.Floor(playerPos.y-PlayerMouvements.size.y/2);
        float ymax = Mathf.Floor(playerPos.y+PlayerMouvements.size.y/2);
        GD.Print(mousePos);
        GD.Print(xmin, " ", xmax, "  -  ", ymin, " ", ymax);
        if (Mathf.Floor(mousePos.x)<=xmax && Mathf.Floor(mousePos.x)>=xmin && Mathf.Floor(mousePos.y)<=ymax && Mathf.Floor(mousePos.y)>=ymin)
            return false;
        int x = Mathf.FloorToInt(mousePos.x);
        int y = Mathf.FloorToInt(mousePos.y);
        float distance = Mathf.Sqrt( Mathf.Pow((x-playerPos.x),2) + Mathf.Pow((y-playerPos.y),2));
        return (distance<8); 
    }





}
