using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame6.gamePlay
{
    internal class CharacterCard : Card
    {
        private readonly int rank;

        public CharacterCard(Texture2D front, Texture2D shirt, string cardName, int cardRank)
            : base(front, shirt, cardName)
        {
            rank = cardRank;
        }

        public int Rank
        {
            get { return rank; }
        }
    }
}