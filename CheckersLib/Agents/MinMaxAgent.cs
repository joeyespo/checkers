using System;
using System.Collections;

namespace Checkers.Agents
{
    public abstract class MinMaxBaseAgent : CheckersAgent
    {
        /*
        class MinMaxMove
        {
          //bool isMin;
          CheckersGame curGame;
          int depth;
          int alpha;
          int beta;
        }
    
        Queue minFringe = new Queue(48, 3);
        */

        int searchDepth;
        bool increasingSearchDepth;

        public MinMaxBaseAgent(int searchDepth, bool increasingSearchDepth)
        {
            this.searchDepth = searchDepth;
            this.increasingSearchDepth = increasingSearchDepth;
        }
        public MinMaxBaseAgent(int searchDepth)
            : this(searchDepth, true)
        {
        }
        public MinMaxBaseAgent()
            : this(2)
        {
        }

        public int SearchDepth
        {
            get
            {
                return searchDepth;
            }
        }
        public bool IncreasingSearchDepth
        {
            get
            {
                return increasingSearchDepth;
            }
        }

        public override CheckersMove NextMove(CheckersGame game)
        {
            const int minValue = int.MaxValue;
            int maxValue = int.MinValue;
            CheckersMove bestMove = null;
            // Enumerate all moves
            foreach(CheckersMove move in game.EnumLegalMoves())
            {
                if(!game.IsPlaying)
                    break;
                CheckersGame nextGameState = game.Clone();
                nextGameState.MovePiece(move.Clone(nextGameState));
                int curValue = minMove(game, nextGameState, 1, maxValue, minValue);
                if((curValue > maxValue) || (bestMove == null))
                {
                    maxValue = curValue;
                    bestMove = move;
                }
                OnTick(game);
            }
            return bestMove;
        }

        int maxMove(CheckersGame initGame, CheckersGame curGame, int depth, int alpha, int beta)
        {
            // Check algorithm limits..end prematurely, but with an educated approximation
            if(doCutOff(initGame, curGame, depth))
                return doCalculateStrength(initGame, curGame);

            // Make move with all possibilities
            foreach(CheckersMove move in curGame.EnumLegalMoves())
            {
                // Create next move
                CheckersGame nextGameState = move.Game.Clone();
                CheckersMove nextMoveState = move.Clone(nextGameState);

                // Make next move and search move space
                if(!nextGameState.MovePiece(nextMoveState))
                    continue;
                int value = minMove(initGame, nextGameState, depth + 1, alpha, beta);

                if(value > alpha)
                {
                    // Get new max value
                    alpha = value;
                }

                if(alpha > beta)
                {
                    // Return max value with pruning
                    return beta;
                }
            }
            // Return alpha (max value)
            return alpha;
        }

        int minMove(CheckersGame initGame, CheckersGame curGame, int depth, int alpha, int beta)
        {
            // Check algorithm limits..end prematurely, but with an educated approximation
            if(doCutOff(initGame, curGame, depth))
                return -doCalculateStrength(initGame, curGame);

            // Make move with all possibilities
            foreach(CheckersMove move in curGame.EnumLegalMoves())
            {
                // Create next move
                CheckersGame nextGameState = move.Game.Clone();
                CheckersMove nextMoveState = move.Clone(nextGameState);

                // Make next move and search move space
                if(!nextGameState.MovePiece(nextMoveState))
                    continue;
                int value = maxMove(initGame, nextGameState, depth + 1, alpha, beta);

                if(value < beta)
                {
                    // Get new min value
                    beta = value;
                }

                if(beta < alpha)
                {
                    // Return min value with pruning
                    return alpha;
                }
            }
            // Return alpha (max value)
            return beta;
        }

        protected virtual int doCalculateStrength(CheckersGame initGame, CheckersGame curGame)
        {
            int player = ((curGame.Turn != 1) ? (2) : (1));
            int opponent = ((curGame.Turn != 1) ? (1) : (2));

            // Heuristic: Check for player won state
            if(curGame.Winner == player)
                return int.MaxValue;

            // Heuristic: Check for player lost state
            if(curGame.Winner == opponent)
                return int.MinValue;

            return CalculateStrength(initGame, curGame);
        }

        bool doCutOff(CheckersGame initGame, CheckersGame curGame, int depth)
        {
            OnTick(initGame);
            // Test the game-oriented cut-offs
            if((!curGame.IsPlaying) || (!initGame.IsPlaying))
                return true;
            // Test the depth cut-off
            int curSearchDepth = searchDepth;
            if(increasingSearchDepth)
            {
                int totalPieces = CheckersGame.PiecesPerPlayer * CheckersGame.PlayerCount;
                //int removed = (CheckersGame.PiecesPerPlayer*CheckersGame.PlayerCount) - curGame.GetRemainingCount();
                //int factor = (int)Math.Log(removed, 3);
                int factor = (int)Math.Log(curGame.GetRemainingCount(), 3), mfactor = (int)Math.Log(totalPieces, 3);
                curSearchDepth += (mfactor - factor);
            }
            if((depth >= 0) && (depth > curSearchDepth))
                return true;
            // Test the extended cut-off
            return CutOff(initGame, curGame, depth);
        }


        /// <summary>
        /// Calculates current player's strength at current point in game.
        /// This gives us a reasonable approximation of a particular player's chances of winning.
        /// This heuristic given is used once the min-max algorithm has reached its max search depth.
        /// </summary>
        protected abstract int CalculateStrength(CheckersGame initGame, CheckersGame curGame);

        /// <summary>
        /// Calculates the cut-off point in the game.
        /// </summary>
        protected virtual bool CutOff(CheckersGame initGame, CheckersGame curGame, int depth)
        {
            return false;
        }
    }
}
