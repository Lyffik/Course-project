using System;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame6.gamePlay
{
    [Serializable]
    public class BuildingCard : Card
    {
        private readonly int cost;

        public BuildingCard()
        {
        }

        public BuildingCard(Texture2D front, Texture2D shirt, string name, int cardCost) : base(front, shirt, name)
        {
            cost = cardCost;
        }

        public int Cost
        {
            get { return cost; }
        }
    }
}