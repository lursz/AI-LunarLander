using Godot;
using System;
using System.Collections.Generic;

public partial class Landing_pad : TileMap
{
	public override void _Ready()
	{
		string metaData = "obstacle";

		string[] metadataList = new string[] { "block", "landingPad" };

		this.SetMeta(metaData, metadataList);
	}

	public override void _Process(double delta)
	{
	}

	public void IsPad(string a)
	{
		GD.Print(a);
	}

}
