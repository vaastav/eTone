using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{


    /*Enums for the slingshot, the state of the game, and the agent.*/

    public enum GameState
    {
        Start,
        Playing,
	GameOver,
	Won
    }

    public enum AgentState
    {
        BeforeThrown,
        Thrown,
        Landed
    }
}

