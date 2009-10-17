using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameDev.Core.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameDev.Core.Sequencing;

namespace KlimaKonflikt
{
    public static class GameImages
    {
        public static GameImage GetBlomstImage(ContentManager content)
        {
            return new GameImage(
                new SequencedIterator<Texture2D>(new ForwardingSequencer(0, 29),
                    new List<Texture2D>()
                    {
                        content.Load<Texture2D>(@"Blomst\Blomst0001"),
                        content.Load<Texture2D>(@"Blomst\Blomst0002"),
                        content.Load<Texture2D>(@"Blomst\Blomst0003"),
                        content.Load<Texture2D>(@"Blomst\Blomst0004"),
                        content.Load<Texture2D>(@"Blomst\Blomst0005"),
                        content.Load<Texture2D>(@"Blomst\Blomst0006"),
                        content.Load<Texture2D>(@"Blomst\Blomst0007"),
                        content.Load<Texture2D>(@"Blomst\Blomst0008"),
                        content.Load<Texture2D>(@"Blomst\Blomst0009"),
                        content.Load<Texture2D>(@"Blomst\Blomst0010"),
                        content.Load<Texture2D>(@"Blomst\Blomst0011"),
                        content.Load<Texture2D>(@"Blomst\Blomst0012"),
                        content.Load<Texture2D>(@"Blomst\Blomst0013"),
                        content.Load<Texture2D>(@"Blomst\Blomst0014"),
                        content.Load<Texture2D>(@"Blomst\Blomst0015"),
                        content.Load<Texture2D>(@"Blomst\Blomst0016"),
                        content.Load<Texture2D>(@"Blomst\Blomst0017"),
                        content.Load<Texture2D>(@"Blomst\Blomst0018"),
                        content.Load<Texture2D>(@"Blomst\Blomst0019"),
                        content.Load<Texture2D>(@"Blomst\Blomst0020"),
                        content.Load<Texture2D>(@"Blomst\Blomst0021"),
                        content.Load<Texture2D>(@"Blomst\Blomst0022"),
                        content.Load<Texture2D>(@"Blomst\Blomst0023"),
                        content.Load<Texture2D>(@"Blomst\Blomst0024"),
                        content.Load<Texture2D>(@"Blomst\Blomst0025"),
                        content.Load<Texture2D>(@"Blomst\Blomst0026"),
                        content.Load<Texture2D>(@"Blomst\Blomst0027"),
                        content.Load<Texture2D>(@"Blomst\Blomst0028"),
                        content.Load<Texture2D>(@"Blomst\Blomst0029"),
                        content.Load<Texture2D>(@"Blomst\Blomst0030")
                      }), 20);
        }

        public static GameImage GetOlieImage(ContentManager content)
        {
            return new GameImage(
                new SequencedIterator<Texture2D>(new ForwardingSequencer(0, 29),
                    new List<Texture2D>()
                    {
                        content.Load<Texture2D>(@"Olie\ThePatch0001"),
                        content.Load<Texture2D>(@"Olie\ThePatch0002"),
                        content.Load<Texture2D>(@"Olie\ThePatch0003"),
                        content.Load<Texture2D>(@"Olie\ThePatch0004"),
                        content.Load<Texture2D>(@"Olie\ThePatch0005"),
                        content.Load<Texture2D>(@"Olie\ThePatch0006"),
                        content.Load<Texture2D>(@"Olie\ThePatch0007"),
                        content.Load<Texture2D>(@"Olie\ThePatch0008"),
                        content.Load<Texture2D>(@"Olie\ThePatch0009"),
                        content.Load<Texture2D>(@"Olie\ThePatch0010"),
                        content.Load<Texture2D>(@"Olie\ThePatch0011"),
                        content.Load<Texture2D>(@"Olie\ThePatch0012"),
                        content.Load<Texture2D>(@"Olie\ThePatch0013"),
                        content.Load<Texture2D>(@"Olie\ThePatch0014"),
                        content.Load<Texture2D>(@"Olie\ThePatch0015"),
                        content.Load<Texture2D>(@"Olie\ThePatch0016"),
                        content.Load<Texture2D>(@"Olie\ThePatch0017"),
                        content.Load<Texture2D>(@"Olie\ThePatch0018"),
                        content.Load<Texture2D>(@"Olie\ThePatch0019"),
                        content.Load<Texture2D>(@"Olie\ThePatch0020"),
                        content.Load<Texture2D>(@"Olie\ThePatch0021"),
                        content.Load<Texture2D>(@"Olie\ThePatch0022"),
                        content.Load<Texture2D>(@"Olie\ThePatch0023"),
                        content.Load<Texture2D>(@"Olie\ThePatch0024"),
                        content.Load<Texture2D>(@"Olie\ThePatch0025"),
                        content.Load<Texture2D>(@"Olie\ThePatch0026"),
                        content.Load<Texture2D>(@"Olie\ThePatch0027"),
                        content.Load<Texture2D>(@"Olie\ThePatch0028"),
                        content.Load<Texture2D>(@"Olie\ThePatch0029"),
                        content.Load<Texture2D>(@"Olie\ThePatch0030")
                      }), 20);
        }

        public static GameImage GetOlieTaarnImage(ContentManager content)
        {
            return new GameImage(
                new SequencedIterator<Texture2D>(new RepeatingSequencer(0, 39),
                    new List<Texture2D>()
                    {
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0001"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0002"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0003"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0004"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0005"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0006"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0007"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0008"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0009"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0010"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0011"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0012"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0013"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0014"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0015"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0016"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0017"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0018"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0019"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0020"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0021"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0022"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0023"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0024"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0025"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0026"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0027"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0028"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0029"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0030"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0031"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0032"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0033"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0034"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0035"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0036"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0037"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0038"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0039"),
                        content.Load<Texture2D>(@"OlieTaarnet\Oilie tårn0040")
                      }), 45);
        }

    }
}
