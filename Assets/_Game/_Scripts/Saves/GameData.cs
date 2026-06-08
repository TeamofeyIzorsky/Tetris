using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public struct GameData
{
    //StandartMode
    public float BestStandartTime;
    public int BestStandartScore;

    //40 Lines
    public float Best40LinesTime;

    //Blitz
    public int BestBlirzScore;
    public int BestBlitzLinesCount;

    //Global
    public float allTime;
    public int gamesPlayed;

    public GameSesion gameSesion;

    public void GamePlayed()
    {
        gamesPlayed += 1;
    }

    public bool BestLinesCount(int lines, GameMode gameMode)
    {
        if (gameMode == GameMode.Blitz)
        {
            if (BestBlitzLinesCount < lines)
            {
                BestBlitzLinesCount = lines;
                return true;
            }
            return false;
        }

        return false;
    }

    public bool BestTime(float time, GameMode gameMode)
    {
        if (gameMode == GameMode.Standart)
        {
            if (BestStandartTime < time)
            {
                BestStandartTime = time;
                return true;
            }
            return false;
        }
        else if (gameMode == GameMode.Lines40)
        {
            if (Best40LinesTime < time)
            {
                Best40LinesTime = time;
                return true;
            }
            return false;
        }

        return false;
    }

    public bool BestScore(int score, GameMode gameMode)
    {
        if (gameMode == GameMode.Standart)
        {
            if (BestStandartScore < score)
            {
                BestStandartScore = score;
                return true;
            }
            return false;
        }
        else if (gameMode == GameMode.Blitz)
        {
            if (BestBlirzScore < score)
            {
                BestBlirzScore = score;
                return true;
            }
            return false;
        }

        return false;
    }
}

public struct GameSesion
{
    public int linesCount;
    public int score;
    public int comboCount;

    public ComboType comboType;

    public float time;

    public Piece CurrentPiece;
    public List<Piece> Bag;

    public List<Vector2Int> piecePositions;
    public bool[,] grid;
    public bool isCanHold;
}