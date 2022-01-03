using System;
using System.Collections.Generic;

public class GameScene : IScene
{
    private InputManager _inputs = new InputManager();
    private Level _level;
    private int _steps = 0;

    public Actions GetInput()
    {
        return _inputs.ReadKey();
    }

    public void Compute(Actions action)
    {
        switch (action) {
            case Actions.MENU: Menu();
                break;
            case Actions.DIRECTION:
                if (!_level.isComplete)
                    NewDirection();
                break;
            case Actions.LEVEL_SELECTION:
                if (_level.isComplete) {
                    LevelSelection();
                }
                break;
            case Actions.REPLAY:
                if (_level.isComplete) {
                    _level.Reset();
                    _steps = 0;
                }
                break;
            default:
                break;
        }
    }

    public void Draw()
    {
        Console.WriteLine("-------------------------------------------");
        if (_level != null) {
            if (!_level.isComplete) {
                Console.WriteLine($"Target position: {_level.end}");
                Console.WriteLine($"Current position: {_level.current}");
                Console.WriteLine("- Select a direction (d)");
                Console.WriteLine("- Menu (m)");
                Console.WriteLine("- Help (h)");
                Console.WriteLine("- Quit (q)");
            } else {
                Console.WriteLine("- Replay (r)");
                Console.WriteLine("- Level selection (s)");
                Console.WriteLine("- Menu (m)");
                Console.WriteLine("- Quit (q)");
            }
        }
    }

    public void Help()
    {
        Console.WriteLine("- Direction Y (x)");
        Console.WriteLine("- Direction Z (x+1)");
    }

    public void SetLevel(Level level)
    {
        _level = level;
    }

    private void Menu()
    {
        _level.Reset();
        Console.WriteLine("Entering menu scene");
        Labyrinth.currentScene = Scenes.MENU;
    }

    private void NewDirection()
    {
        List<Vertex> neighbors = _level.GetCurrentNeighbors();
        if (neighbors.Count > 0) {
            Console.WriteLine($"Please choose your direction:");
            for (int i = 0; i < neighbors.Count; i ++) {
                Console.WriteLine($"- {neighbors[i].value} ({i + 1})");
            }
            Console.WriteLine("Cancel (all keys)");
        } else {
            Console.WriteLine("It looks like you're stuck... You have no neighbors around");
        }
        int? number = _inputs.ReadIntKey();
        if (number != null) {
            if (_level.SetNewPosition((int)number)) {
                _steps++;
            }
        }
        if (_level.isComplete) {
            Console.WriteLine("Congratulation you reach the finish line!");
            Console.WriteLine($"You made it in {_steps} steps");
            Console.WriteLine($"The shortest path was in only {_level.GetMinimalSteps()} steps");
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
            _steps = 0;
            _level.Reset();
            _level = Labyrinth.levels[(int)number - 1];
        }
    }
};