using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameDev.Core.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameDev.Core.Sequencing;
using GameDev.Core;

namespace KlimaKonflikt
{
    public static class GameImages
    {
        private static ContentManager _Content;
        private static ContentManager Content {
            get { 
            if (_Content == null)
	{
                _Content = GameDevGame.Current.Content;
	}
                return _Content;
            }
        }

        public static GameImage GetBlomstImage()
        {
            
            return new GameImage(
                new SequencedIterator<Texture2D>(new ForwardingSequencer(0, 29),
                    new List<Texture2D>()
                    {
                        Content.Load<Texture2D>(@"Blomst\Blomst0001"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0002"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0003"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0004"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0005"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0006"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0007"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0008"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0009"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0010"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0011"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0012"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0013"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0014"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0015"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0016"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0017"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0018"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0019"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0020"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0021"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0022"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0023"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0024"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0025"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0026"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0027"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0028"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0029"),
                        Content.Load<Texture2D>(@"Blomst\Blomst0030")
                      }), 20);
        }

        public static GameImage GetOlieImage()
        {
            return new GameImage(
                new SequencedIterator<Texture2D>(new ForwardingSequencer(0, 29),
                    new List<Texture2D>()
                    {
                        Content.Load<Texture2D>(@"Olie\ThePatch0001"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0002"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0003"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0004"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0005"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0006"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0007"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0008"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0009"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0010"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0011"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0012"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0013"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0014"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0015"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0016"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0017"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0018"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0019"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0020"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0021"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0022"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0023"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0024"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0025"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0026"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0027"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0028"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0029"),
                        Content.Load<Texture2D>(@"Olie\ThePatch0030")
                      }), 20);
        }

        public static GameImage GetOlieTaarnImage(ContentManager Content)
        {
            return new GameImage(
                new SequencedIterator<Texture2D>(new RepeatingSequencer(0, 39),
                    new List<Texture2D>()
                    {
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0001"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0002"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0003"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0004"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0005"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0006"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0007"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0008"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0009"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0010"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0011"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0012"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0013"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0014"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0015"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0016"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0017"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0018"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0019"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0020"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0021"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0022"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0023"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0024"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0025"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0026"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0027"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0028"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0029"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0030"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0031"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0032"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0033"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0034"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0035"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0036"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0037"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0038"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0039"),
                        Content.Load<Texture2D>(@"OlieTaarnet\OilTower0040")
                      }), 45);
        }


        public static GameImage GetIldImage(ContentManager Content)
        {

             return new GameImage(
                new SequencedIterator<Texture2D>(new RepeatingSequencer(0, 9),
                    new List<Texture2D>()
                    {
                        Content.Load<Texture2D>(@"Ild\Ild0001"),
                        Content.Load<Texture2D>(@"Ild\Ild0002"),
                        Content.Load<Texture2D>(@"Ild\Ild0003"),
                        Content.Load<Texture2D>(@"Ild\Ild0004"),
                        Content.Load<Texture2D>(@"Ild\Ild0005"),
                        Content.Load<Texture2D>(@"Ild\Ild0006"),
                        Content.Load<Texture2D>(@"Ild\Ild0007"),
                        Content.Load<Texture2D>(@"Ild\Ild0008"),
                        Content.Load<Texture2D>(@"Ild\Ild0009"),
                        Content.Load<Texture2D>(@"Ild\Ild0010"),
                      }), new MinMaxIterator(new RandomSequencer(0,40), 80, 120));
        }



        public static GameImage GetOilBarrelImage(ContentManager Content)
        {
            return new GameImage(
                new SequencedIterator<Texture2D>(new AlternatingSequencer(0, 6),
                    new List<Texture2D>()
                    {
                        Content.Load<Texture2D>(@"OilBarrel\OilBarrel001"),
                        Content.Load<Texture2D>(@"OilBarrel\OilBarrel002"),
                        Content.Load<Texture2D>(@"OilBarrel\OilBarrel003"),
                        Content.Load<Texture2D>(@"OilBarrel\OilBarrel004"),
                        Content.Load<Texture2D>(@"OilBarrel\OilBarrel005"),
                        Content.Load<Texture2D>(@"OilBarrel\OilBarrel006"),
                        Content.Load<Texture2D>(@"OilBarrel\OilBarrel007")
                      }), 45);
        }

        public static GameImage GetFlowersackImage(ContentManager Content)
        {
            return new GameImage(
                new SequencedIterator<Texture2D>(new RepeatingSequencer(0, 6),
                    new List<Texture2D>()
                    {
                        Content.Load<Texture2D>(@"Flowersack\FlowerSack1"),
                        Content.Load<Texture2D>(@"Flowersack\FlowerSack2"),
                        Content.Load<Texture2D>(@"Flowersack\FlowerSack3"),
                        Content.Load<Texture2D>(@"Flowersack\FlowerSack4"),
                        Content.Load<Texture2D>(@"Flowersack\FlowerSack5"),
                        Content.Load<Texture2D>(@"Flowersack\FlowerSack6"),
                        Content.Load<Texture2D>(@"Flowersack\FlowerSack7")
                      }), 10);
        }

        public static GameImage GetPulsatingCircleImage(ContentManager Content)
        {
            return new GameImage(
                new SequencedIterator<Texture2D>(new AlternatingSequencer(1),
                    new List<Texture2D>()
                    {
                        Content.Load<Texture2D>(@"AnimatedCircle\Circle_large"),
                        Content.Load<Texture2D>(@"AnimatedCircle\Circle_small"),
                      }), 250);
        }
    }
}
