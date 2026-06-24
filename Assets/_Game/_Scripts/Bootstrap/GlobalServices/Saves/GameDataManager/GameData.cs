public struct GameData
{
    //Структура, хранящая данные рекордов и другую статистику, которую надо сохранить между сессиями
    public GameData(float lines40Time = float.MaxValue)
    {
        BestStandartTime = 0;
        BestStandartScore = 0;

        Best40LinesTime = lines40Time;

        BestBlirzScore = 0;
        BestBlitzLinesCount = 0;

        allTime = 0;
        gamesPlayed = 0;
    }


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
}