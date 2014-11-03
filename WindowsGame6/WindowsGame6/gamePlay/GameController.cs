using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame6.gamePlay
{
    public class GameController : GameComponent
    {
        private readonly BuildingsDeck buildingsDeck;
        private readonly CharactersDeck charactersDeck;

        private readonly Player mainPlayer;
        private readonly List<ComputerPlayer> players;
        private readonly int playersCount;
        private int currentRank;
        private int currentStage;
        private ComputerPlayer firstPlayer;
        private int indexOfCurrentPlayer;
        private MouseState oldMouseState;

        public GameController(Game game, List<ComputerPlayer> players, CharactersDeck charDeck,
            BuildingsDeck buildingDeck, Player player) : base(game)
        {
            this.players = players;
            charactersDeck = charDeck;
            buildingsDeck = buildingDeck;
            mainPlayer = player;
            playersCount = players.Count;
            NewGame();
        }

        private void NewGame()
        {
            currentStage = 1;
            buildingsDeck.Shuffle();
            foreach (ComputerPlayer player in players)
            {
                player.GetCardsFromDeck(4, buildingsDeck);
                player.Money += 2;
                player.Active = false;
            }
            var rnd = new Random();
            firstPlayer = players[rnd.Next(players.Count)];
            firstPlayer.Active = true;
            indexOfCurrentPlayer = players.IndexOf(firstPlayer);
        }

        private void NewRound()
        {
            currentRank = 1;
            currentStage = 1;
            buildingsDeck.Shuffle();
            charactersDeck.Shuffle();
            switch (playersCount)
            {
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    charactersDeck.DiscardClosed(1);
                    charactersDeck.DiscardOpenly(2);
                    break;
                case 5:
                    charactersDeck.DiscardClosed(1);
                    charactersDeck.DiscardOpenly(1);
                    break;
                case 6:
                    charactersDeck.DiscardClosed(1);
                    break;
                case 7:
                    break;
            }
        }

        private void SelectionCharacters()
        {
            foreach (ComputerPlayer player in players)
            {
                if (player != mainPlayer)
                {
                    player.SelectCharacter(charactersDeck);
                }
            }

            charactersDeck.DiscardClosed(8);
        }

        public override void Update(GameTime gameTime)
        {
            switch (currentStage)
            {
                case 1:
                {
                    NewRound();
                    currentStage++;
                    break;
                }
                case 2:
                {
                    if (players[indexOfCurrentPlayer].Active)
                    {
                        if (players[indexOfCurrentPlayer].CharacterIsSelect == false)
                        {
                            players[indexOfCurrentPlayer].SelectCharacter(charactersDeck);
                            if (players[indexOfCurrentPlayer].CharacterIsSelect)
                            {
                                players[indexOfCurrentPlayer].Active = false;
                                if (indexOfCurrentPlayer < players.Count - 1)
                                {
                                    indexOfCurrentPlayer++;
                                }
                                else
                                {
                                    indexOfCurrentPlayer = 0;
                                }
                                if (indexOfCurrentPlayer != players.IndexOf(firstPlayer))
                                {
                                    players[indexOfCurrentPlayer].Active = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        charactersDeck.DiscardClosed(6);
                        currentStage++;
                    }
                    break;
                }
                case 3:
                    if (currentRank < 9)
                    {
                        foreach (ComputerPlayer player in players)
                        {
                            if (player.Rank == currentRank)
                            {
                                player.Active = true;

                                if (currentRank == 2)
                                {
                                }
                            }
                        }
                    }
                    else
                    {
                        currentStage++;
                    }
                    break;
            }
            base.Update(gameTime);
        }
    }
}