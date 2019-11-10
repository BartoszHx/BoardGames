using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;

namespace BoardGames.Models
{
    internal class PawModel : IPawn
    {
	    public PawColors Color { get; set; }
	    public PawType Type { get; set; }

	    public PawModel()
	    {
		    
	    }

	    public PawModel(PawType pawType, PawColors pawColors)
	    {
		    this.Type = pawType;
		    this.Color = pawColors;
	    }
    }
}
