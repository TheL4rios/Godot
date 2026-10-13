using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public partial class MainScene : Node2D
{
	[Export] private PackedScene[] levels = [];

	private int _currentLevelIndex = 1;
	private Node instanceLevel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LoadLevel(_currentLevelIndex);
	}

	private void LoadLevel(int levelIndex)
	{
		if (instanceLevel != null)
		{
			instanceLevel.QueueFree();
		}

		instanceLevel = levels[levelIndex - 1].Instantiate();
		AddChild(instanceLevel);

		List<Node> children = instanceLevel.GetChildren().ToList();
		foreach (Node child in children)
		{
			if (child is Player player)
			{
				player.PlayerDied += () => CallDeferred(MethodName.resetLevel);
				break;
			}
		}
	}

	private void deleteLevel()
	{
		if (instanceLevel != null)
		{
			instanceLevel.QueueFree();
		}
	}

	private void resetLevel()
	{
		deleteLevel();
		LoadLevel(_currentLevelIndex);
	}
}
