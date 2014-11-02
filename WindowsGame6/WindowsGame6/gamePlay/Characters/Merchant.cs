using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame6.gamePlay.Characters
{
    public class Merchant : Character
    {
        public Merchant(Game game, Texture2D front, Texture2D shirt, string cardName, int cardRank, GameClass gameClass)
            : base(game,front, shirt, cardName, cardRank, gameClass)
        {
        }

        public override void Ability()
        {
        }
    }
}