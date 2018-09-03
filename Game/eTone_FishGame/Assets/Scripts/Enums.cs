using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    //enums for state of game, and state of agent


    public enum GameState
    {
        Preload,
        Play,
        MainMenu,
        Intermediary,
        Playing,
        Finished
    }

    public enum LauncherState
    {
        Idle,
        End,
        Launch
    }

    public enum PayloadState
    {
        Uncaptured, 
        Captured
    }

    public enum AgentState
    {
        Idle,
        Jumping,
        Landed
    }

    public enum MenuState
    {
        MainMenu,
        Pause,
        Settings
    }

}