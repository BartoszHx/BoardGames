using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using System.Collections.Generic;

namespace BoardGames.Interfaces
{
    public interface IRulesChess
    {
	    IEnumerable<IField> PawnWherCanMove(IField field);
	    bool PawnMove(IField fieldOld, IField fieldNew);
	    ILastMove LastMove { get; set; }

	    void SetStartPositionPaws();
	    bool IsPawnUpgrade(IField field);
	    PawColors? IsCheckOnColor(IEnumerable<PawColors> colorList);
	    PawColors? IsCheckmateOnColor(IEnumerable<PawColors> colorList);
    }
}
