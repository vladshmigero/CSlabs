using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSlabs.Labs
{
    public class Lab2
    {
        public enum PlayerState
        {
            NotInGame,
            Playing,
            Winner,
            Loser
        }
        public class Player
        {
            private string name;
            private int position;
            private PlayerState state;
            private int totalDistance;

            public Player(string playerName)
            {
                name = playerName;
                position = 0;
                state = PlayerState.NotInGame;
                totalDistance = 0;
            }

            public string Name => name;
            public int Position => position;
            public PlayerState State => state;
            public int TotalDistance => totalDistance;

            public void SetPosition(int newPosition)
            {
                position = newPosition;
                if (state == PlayerState.NotInGame)
                {
                    state = PlayerState.Playing;
                }
            }

            public void Move(int distance, int fieldSize)
            {
                if (state == PlayerState.NotInGame)
                    return;

                totalDistance += Math.Abs(distance);
                position += distance;

                // поле
                while (position < 1)
                    position += fieldSize;
                while (position > fieldSize)
                    position -= fieldSize;
            }

            public void SetState(PlayerState State)
            {
                state = State;
            }

            public string GetPositionString()
            {
                if (state == PlayerState.NotInGame) {return "??";}
                return position.ToString();
            }
        }

        public class Game
        {
            private Player cat;
            private Player mouse;
            private int fieldSize;
            private List<string> printOutputs;
            private bool gameEnded;
            private bool mouseCaught;
            private int caughtPosition;

            public Game()
            {
                cat = new Player("Cat");
                mouse = new Player("Mouse");
                printOutputs = new List<string>();
                gameEnded = false;
                mouseCaught = false;
            }
            
            public void RunFromFile(string inputFile,string outputFile)
            {
                string[] lines = File.ReadAllLines(inputFile);
                int firstLineIndex = 0;


            }
            private void CheckGameEnd()
            {
                if (cat.State == PlayerState.Playing && mouse.State == PlayerState.Playing)
                {
                    if (cat.Position == mouse.Position)
                    {
                        mouseCaught = true;
                        caughtPosition = cat.Position;
                        cat.SetState(PlayerState.Winner);
                        mouse.SetState(PlayerState.Loser);
                        gameEnded = true;
                    }
                }
            }
        }
    }
}
