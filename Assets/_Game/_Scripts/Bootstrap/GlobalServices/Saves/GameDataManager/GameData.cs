public struct GameData
{
    //Структура, хранящая данные рекордов и другую статистику, которую надо сохранить между сессиями

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