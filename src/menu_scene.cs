using System;
using System.Collections.Generic;

public class MenuScene : IScene
{
    private InputManager _inputs = new InputManager();

    public Actions GetInput()
    {
        return _inputs.ReadKey();
    }

    public void Compute(Actions action)
    {
        switch (action) {
            case Actions.GAME: Game();
                break;
            case Actions.LEVEL_SELECTION: LevelSelection();
                break;
            default:
                break;
        }
    }

    public void Draw()
    {
        Console.WriteLine("-------------------------------------------");
        if (Labyrinth.selectedLevel == null) {
            Console.WriteLine($"Level selected: None");
        } else {
            Console.WriteLine($"Level selected: {Labyrinth.selectedLevel.name}");
        }
        Console.WriteLine("- Level selection (s)");
        Console.WriteLine("- Game (g)");
        Console.WriteLine("- Help (h)");
        Console.WriteLine("- Quit (q)");
    }

    public void Help()
    {
        Console.WriteLine("- Level x (x)");
        Console.WriteLine("- Level x+1 (x+1)");
        Console.WriteLine("- Game (g)");
    }

    private void Game() {
        if (Labyrinth.selectedLevel != null) {
            Console.WriteLine($"Starting {Labyrinth.selectedLevel.name} level");
            Labyrinth.currentScene = Scenes.GAME;
            Labyrinth.game.SetLevel(Labyrinth.selectedLevel);
        } else {
            Console.WriteLine("Please choose a level before entering in game");
        }
    }

    private void LevelSelection()
    {
        Console.WriteLine("Please choose a level: ");
        for (int i = 0; i < Labyrinth.levels.Count; i++) {
            Console.WriteLine($" {i + 1} - {Labyrinth.levels[i].name}");
        }
        Console.WriteLine(" Cancel (all keys)");
        int? number = _inputs.ReadIntKey();
        if (number != null && number > 0 && number <= Labyrinth.levels.Count) {
            Labyrinth.selectedLevel = Labyrinth.levels[(int)number - 1];
        }
    }
};