using WindowsGame6.core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame6.gamePlay
{
    public class Player : ComputerPlayer
    {
        private readonly CardsViewer cardsOnHandsViewer;


        public Player(Game game, string name, PanelButton panel) : base(game, name, panel)
        {
            cardsOnHandsViewer = new CardsViewer(game);
           Components.Add(cardsOnHandsViewer);
        }


    
        public void ShowCardsOnHands()
        {
            cardsOnHandsViewer.Show();
        }

   

        public void HideCardsOnHands()
        {
            cardsOnHandsViewer.Hide();
        }
    }
}