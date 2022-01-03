using System;
using System.Collections.Generic;

public class Level
{
    public String name;
    public char current;
    public char end;
    public bool isComplete;

    private char _start;
    private Graph _graph = new Graph();
    private List<string> _content;

    public Level(String name, List<string> content)
    {
        _content = content;
        this.name = name;
    }

    public bool GenerateGraph()
    {
        if (_content.Count == 0) {
            return false;
        }
        foreach (string line in _content) {
            string[] splitedLine = line.Split(':');
            if (splitedLine.Length != 2) {
                return false;
            }
            if (splitedLine[0][0] != '#') {
                string vertex = splitedLine[0];
                string[] neighbors = splitedLine[1].Split(',');
                foreach (string neighbor in neighbors) {
                    _graph.AddNeighbor(vertex[0], neighbor[0]);
                }
            } else if (splitedLine[0] == "#start") {
                current = splitedLine[1][0];
                _start = current;
            } else if (splitedLine[0] == "#end") {
                end = splitedLine[1][0];
            }
        }
        return true;
    }

    public List<Vertex> GetCurrentNeighbors()
    {
        return _graph.GetNeighborsOf(current);
    }

    public bool SetNewPosition(int number)
    {
        char? neighborValue = _graph.GetValueOfNeighbor(current, number);

        if (neighborValue != null) {
            current = (char)neighborValue;
            if (current == end) {
                isComplete = true;
            }
            return true;
        }
        return false;
    }

    public void Reset()
    {
        current = _start;
        isComplete = false;
    }

    public int GetMinimalSteps()
    {
        return _graph.BFS(_start, end);
    }
};
