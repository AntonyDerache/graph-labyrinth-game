using System;
using System.Collections.Generic;

public class InputManager {
    Dictionary<char, Actions> _inputs;

    public InputManager()
    {
        _inputs = new Dictionary<char, Actions>()
            {
                { 'm', Actions.MENU },
                { 'g', Actions.GAME },
                { 'c', Actions.CANCEL },
                { 'h', Actions.HELP },
                { 'q', Actions.QUIT },
                { 's', Actions.LEVEL_SELECTION },
                { 'd', Actions.DIRECTION },
                { 'r', Actions.REPLAY },
            };
    }

    public Actions ReadKey()
    {
        string str;
        Actions value;

        Console.Write("> ");
        if ((str = Console.ReadLine()) != null && str.Length == 1) {
            if (_inputs.TryGetValue(str[0], out value)) {
                return value;
            } else {
                return Actions.NONE;
            }
        } else {
            return Actions.NONE;
        }
    }

    public int? ReadIntKey()
    {
        string str;
        int number;

        Console.Write("> ");
        str = Console.ReadLine();
        if (int.TryParse(str, out number)) {
            return number;
        }
        return null;
    }
}
