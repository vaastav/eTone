using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{


    /*Enums for the slingshot, the state of the game, and the agent.*/


    public enum SlingshotState
    {
        Idle,
        VoiceInput,
        AgentFlying
    }

    public enum GameState
    {
        Start,
        Playing
    }

    public enum AgentState
    {
        BeforeThrown,
        Thrown,
        Landed
    }
}

