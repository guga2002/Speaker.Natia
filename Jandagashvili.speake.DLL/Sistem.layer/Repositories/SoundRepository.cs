using Speaker.leison.Sistem.layer.Interfaces;
using System;
using System.Speech.Synthesis;
using System.Threading.Tasks;

namespace Sistem.layer.Repositories
{
    public class SoundRepository: ISoundRepository
    {
        private  SpeechSynthesizer synth;

        public  Task SpeakNow(string text,int second=2)
        {
            using (synth= new SpeechSynthesizer())
            {
                foreach (InstalledVoice voice in synth.GetInstalledVoices())
                {
                    VoiceInfo info = voice.VoiceInfo;
                    if (info.Culture.Name.StartsWith("ka-GE") && info.Name.Contains("Nat"))
                    {
                        synth.SelectVoice(info.Name);
                        synth.Rate = second;
                        break;
                    }
                }
                try
                {
                    synth.Speak(text);
                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine("shecdoma saubris dros" + ex.Message);
                }
            }
            return Task.CompletedTask;
        }
    }
}
