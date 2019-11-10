namespace BoardGamesShared.Enums
{
    public enum PawType
    {
        PawnChess,
        BishopChess,
        KingChess,
        KnightChess,
        QueenChess,
        RockChess,
        PawnCheckers,
        QueenCheckers
    }

    public enum PawChess
    {
        Pawn = PawType.PawnChess,
        Bishop = PawType.BishopChess,
        King = PawType.KingChess,
        Knight = PawType.KnightChess,
        Queen = PawType.QueenChess,
        Rock = PawType.RockChess
    }

    public enum PawCheckers
    {
        PawnCheckers = PawType.PawnCheckers,
        QueenCheckers = PawType.QueenCheckers
    }
}
