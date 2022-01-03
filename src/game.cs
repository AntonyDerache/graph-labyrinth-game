using System;
using System.Collections.Generic;
using System.IO;

public class Labyrinth
{
    public static List<Level> levels = new List<Level>();
    public static Level selectedLevel;
    public static Scenes currentScene = Scenes.MENU;
    public static GameScene game = new GameScene();

    private static MenuScene menu = new MenuScene();
    private static bool _isAlive = true;
    private static Dictionary<Scenes, IScene> _scenes = new Dictionary<Scenes, IScene>()
    {
        {Scenes.MENU, menu},
        {Scenes.GAME, game}
    };

    public static void CheckApplicationEvents(Actions input)
    {
        if (input == Actions.QUIT) {
            _isAlive = false;
        }
        if (input == Actions.HELP) {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Welcome to my labyrinth game!");
            Console.WriteLine("Commands:");
            _scenes[currentScene].Help();
        }
    }

    public static void Game()
    {
        Console.WriteLine("Game starting...");
        _scenes[currentScene].Draw();
        while (_isAlive) {
            Actions input = _scenes[currentScene].GetInput();
            CheckApplicationEvents(input);
            if (!_isAlive) {
                break;
            }
            _scenes[currentScene].Compute(input);
            _scenes[currentScene].Draw();
        };
        Console.WriteLine("Game quitting");
    }

    public static void SearchForLevels()
    {
        string path = "./levels";
        string format = "*.txt";
        if (Directory.Exists(path)) {
            string[] files = Directory.GetFiles(path, format);
            foreach (var file in files) {
                int pathLength = path.Length;
                int formatLength = format.Length;
                try {
                    List<string> fileContent = new List<string>(File.ReadAllLines(file));
                    string levelName = file.Substring(path.Length + 1, (file.Length - pathLength - formatLength));
                    Level level = new Level(levelName, fileContent);
                    if (level.GenerateGraph()) {
                        levels.Add(level);
                    } else {
                        throw new Exception("File format is incorrect");
                    }
                } catch (Exception e) {
                    Console.Write("Something happened: ");
                    Console.WriteLine(e.Message);
                }
            }
        }
    }

    public static void Main(string[] args)
    {
        SearchForLevels();
        if (levels.Count <= 0) {
            Console.WriteLine("There is no available levels. Please relaunch the game with level correct files");
            return;
        }
        Game();
    }
}