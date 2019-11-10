using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGames.Enums;
using BoardGames.Interfaces;

namespace BoardGames.Games.Chess
{
    internal class PawChess : IPawn
    {
	    public string Name { get; set; }
	    public PawColors Color { get; set; }
	    public PawType Type { get; set; }

	    public PawChess(PawType pawType, PawColors pawColors)
	    {
		    this.Type = pawType;
		    this.Color = pawColors;
		    this.Name = pawType.ToString();
	    }
    }
}
