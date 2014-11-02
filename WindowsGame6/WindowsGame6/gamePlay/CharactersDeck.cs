using System.Collections.Generic;
using WindowsGame6.core;
using Microsoft.Xna.Framework;

namespace WindowsGame6.gamePlay
{
    public class CharactersDeck : Deck
    {
   
        public CharactersDeck(Game game, int cardHeight)
            : base(game)
        {
           
            cardsViewer.SetParams(Game.Window.ClientBounds.Width/2, 220,
                new Vector2(Game.Window.ClientBounds.Width/4,
                    (Game.Window.ClientBounds.Height - cardHeight)/2),
                new Vector2(40, Game.Window.ClientBounds.Height - 240), 1.75f);
        }
    }
}