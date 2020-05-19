using Godot;
using System;

public class EndMenu : Control
{
    public override void _EnterTree()
    {
        GetNode<Label>("Time").Text = "Time : " + Game.GetTimePlayedString();
    }

    private void _on_Button_button_down()
    {
        GetTree().Quit();
    }
}
