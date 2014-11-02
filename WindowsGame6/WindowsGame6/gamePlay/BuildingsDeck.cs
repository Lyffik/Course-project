using Microsoft.Xna.Framework;

namespace WindowsGame6.gamePlay
{
    public class BuildingsDeck : Deck
    {
        public BuildingsDeck(Game game, int cardHeight)
            : base(game)
        {
            cardsViewer.SetParams(Game.Window.ClientBounds.Width/2, 220,
                new Vector2((float) Game.Window.ClientBounds.Width/4,
                    (float) (Game.Window.ClientBounds.Height - cardHeight)/2),
                new Vector2(Game.Window.ClientBounds.Width - 190, Game.Window.ClientBounds.Height - 240), 1.75f);
            cardsViewer.Show();
            Show();
        }
    }
}