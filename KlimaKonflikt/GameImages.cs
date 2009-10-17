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
//            return new GameImage(content.Load <Texture2D>(@"Blomst\Blomst0020"));
            return new GameImage(
                new SequencedIterator<GameDevTexture>(new ForwardingSequencer(0, 19),
                    new List<GameDevTexture>()
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
                        content.Load<Texture2D>(@"Blomst\Blomst0020")
                      }), 20);
        }
    }
}
