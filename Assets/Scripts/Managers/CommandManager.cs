using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public class CommandManager : Singleton<CommandManager>
    {
        public List<CommandButton> inputCommandList = new List<CommandButton>();

        public bool isInputButton = false;

        public float calcInputTime = 0f;
        public float checkInputTime = 10f;
    }
}