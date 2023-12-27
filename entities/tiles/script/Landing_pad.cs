using Godot;
using System;
using System.Collections.Generic;

public partial class Landing_pad : TileMap
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		string metaData = "obstacle";

		string[] metadataList = new string[] { "block", "landingPad" };

		this.SetMeta(metaData, metadataList);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void IsPad(string a)
	{
		GD.Print(a);
	}

}
