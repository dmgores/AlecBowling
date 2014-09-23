using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class BowlingLane
    {
        private int CurrentPlayer;
        public List<PlayersGame> playersGames = new List<PlayersGame>();

        public void addPlayer(string playerName)
        {
            var playersGame = new PlayersGame(playerName);
            playersGames.Add(playersGame);
        }
        public bool BallThrown(int pinsKnockedDown)
        {
            bool gameOver = false;
            var currentPlayerGame = playersGames[CurrentPlayer];
            string frameName;
            var framefinished = currentPlayerGame.ballthrown(pinsKnockedDown, out frameName);

            if (framefinished)
                CurrentPlayer++;
            if (CurrentPlayer >= playersGames.Count)
            {
                CurrentPlayer = 0;
                if (frameName == "10")
                    gameOver = true;
            }
          
            
            return gameOver;
        }

        public override string ToString()
        {
            var s = "";
            foreach (var playerGame in playersGames)
            {
                s += playerGame.ToString() + "\n";
            }
            return s;
        }
        
    }

    class PlayersGame
    {
        public String playerName;
        Frame[] frames = new Frame[10];

        public PlayersGame(String playerName)
        {
            this.playerName = playerName;
            for (int i = 0; i < 10; i++)
            {
                var framename = (i + 1).ToString();
                frames[i] = new Frame(framename);
            }
        }
        public bool ballthrown(int pinsKnockedDown, out string frameName)
        {
            frameName = null;
            bool framefinished = false;

            for (int i = 0; i < 10; i++)
            {
                var frame = frames[i];
                var istenthframe = frame.framename == "10";
                frameName = frame.framename;


                if (frame.numberOfThrown() == 0)
                {
                    frame.throws.Add(pinsKnockedDown);
                    if (pinsKnockedDown == 10 && !istenthframe)
                        framefinished = true;
                    break;
                }
                else if (frame.numberOfThrown() == 1)
                {
                    if (frame.throws[0] == 10 && !istenthframe)
                        continue;
                    frame.throws.Add(pinsKnockedDown);

                    if (!istenthframe)
                        framefinished = true;
                    else
                    {
                        if (frame.throws[0] == 10)
                            framefinished = false;//strike
                        else if (frame.throws[0] + pinsKnockedDown == 10)
                            framefinished = false;//spare
                        else
                            framefinished = true;
                    }

                    break;
                }
                else if (istenthframe)
                {
                    if (frame.throws.Count >= 3)
                        throw new Exception("Game is over sucka");

                    frame.throws.Add(pinsKnockedDown);
                    framefinished = true;
                    break;
                }

            }

            return framefinished;
        }

        public override string ToString()
        {
            var s = playerName + ": ";

            for (int i = 0; i < 10; i++)
            {
                var frame = frames[i];
                s += frame;
            }
            return s;
        }
    }


    class Frame
    {
       public List<int> throws;// = new List<int>();
       public String framename;

        public Frame(String name) 
        { 
            throws = new List<int>();
            this.framename = name;

        }

        public int numberOfThrown()
        {
            return throws.Count;
           
        }
        public int CalculateScore(Frame prev , Frame prevPrev)
        {
            int framescore = 0;

            if (throws.Count == 0)
            {
            }
            else if (throws.Count == 1)
            {
                int pinsknockedDown = throws[0];
                if (pinsknockedDown < 10)
                    framescore = pinsknockedDown;
                else
                {
                    // Strike
                    if (prev == null)
                        framescore = 10;
                    else 
                    {
                        if (prev.throws.Count == 1)
                        {
                            // Prev frame was a strike
                            if (prevPrev == null)
                                framescore = prev.throws[0] + 10;
                            else
                                framescore = prev.throws[0] + prevPrev.throws.Last() + 10;
                        }
                        else
                            framescore = prev.throws[1] + prev.throws[0] + 10;

                    }
                }
            }
            else if (throws.Count == 2)
            {
                int pins1 = throws[0];
                int pins2 = throws[1];
                framescore = pins1 + pins2;

                if (framescore == 10)
                {
                    //spare
                    if (prev != null)
                        framescore = pins1 + pins2 + prev.throws.Last();
                }
            }
            else if (throws.Count == 3)
            {

            }
            
            return framescore;
        }
        public override string ToString()
        {
            string s = null;

            if (throws.Count == 0)
                s = "|    " ;
            else if (throws.Count == 1)
                s = "|" + throws[0] + "   ";
            else if (throws.Count == 2)
                s = "|" + throws[0] + " " + throws[1] + " ";
            else if (throws.Count == 3)
                s = "|" + throws[0] + " " + throws[1] + " " + throws[2] + " ";

            return s;
        }

        

    }
}
