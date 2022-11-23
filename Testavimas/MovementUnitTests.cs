using ClassLib;
using ClassLib.Units.Ship;
using Client;
using NUnit.Framework;
using System.Windows.Forms;

namespace Testavimas
{
    public class Tests
    {
        public Map map;

        [SetUp]
        public void Setup()
        {
            Game game = new Game();
            map = new Map(game._hubConnection,"testName", 0, 10, game);
        }

        void ImitateTick()
        {
            map.moveTimerEvent(null, null);
        }

        void ImitateKeyPress(Keys key)
        {
            map.keyisdown(null, new KeyEventArgs(key));
        }

        [Test]
        public void Player_Moving_Left()
        {
            int xStartingLocation = map.player.Image.Location.X;
            ImitateKeyPress(Keys.Left);
            ImitateTick();

            Assert.Less(map.player.Image.Location.X, xStartingLocation);
        }

        [Test]
        public void Player_Moving_Right()
        {
            int xStartingLocation = map.player.Image.Location.X;
            ImitateKeyPress(Keys.Right);
            ImitateTick();

            Assert.Greater(map.player.Image.Location.X, xStartingLocation);
        }

        [Test]
        public void Player_Moving_Up()
        {
            int yStartingLocation = map.player.Image.Location.Y;
            ImitateKeyPress(Keys.Up);
            ImitateTick();

            Assert.Less(map.player.Image.Location.Y, yStartingLocation);
        }

        [Test]
        public void Player_Moving_Down()
        {
            int yStartingLocation = map.player.Image.Location.Y;
            ImitateKeyPress(Keys.Down);
            ImitateTick();

            Assert.Greater(map.player.Image.Location.Y, yStartingLocation);
        }

        [Test]
        public void Player_Cant_Cross_Left_Border()
        {
            ImitateKeyPress(Keys.Left);
            for (int i = 0; i < 500; i++)
            {
                ImitateTick();
            }

            Assert.That(0, Is.EqualTo(map.player.Image.Location.X));
        }

        [Test]
        public void Player_Cant_Cross_Right_Border()
        {
            ImitateKeyPress(Keys.Right);
            for (int i = 0; i < 500; i++)
            {
                ImitateTick();
            }
            Assert.That(map.ClientSize.Width, Is.EqualTo(map.player.Image.Location.X));
        }

        [Test]
        public void Player_Cant_Cross_Top_Border()
        {
            ImitateKeyPress(Keys.Up);
            for (int i = 0; i < 500; i++)
            {
                ImitateTick();
            }
            Assert.That(0, Is.EqualTo(map.player.Image.Location.Y));
        }

        [Test]
        public void Player_Cant_Cross_Bottom_Border()
        {
            ImitateKeyPress(Keys.Down);
            for (int i = 0; i < 500; i++)
            {
                ImitateTick();
            }
            Assert.That(map.ClientSize.Height, Is.EqualTo(map.player.Image.Location.Y));
        }
    }
}