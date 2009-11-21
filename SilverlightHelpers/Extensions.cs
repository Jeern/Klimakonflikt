using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SilverArcade.SilverSprite.Audio;

namespace SilverlightHelpers
{
    public static class Extensions
    {
        public static SoundEffectInstance CreateInstance(this SoundEffect soundEffect)
        {
            //A hack since CreateInstance is missing in SilverSprite don't know if it will work.
            SoundEffectInstance instance = soundEffect.Play();
            instance.Stop();
            return instance;
        }
    }
}
