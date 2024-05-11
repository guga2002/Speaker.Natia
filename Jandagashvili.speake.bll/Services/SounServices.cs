using Sistem.layer.Repositories;
using Speaker.leison.Sistem.layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speaker.leison.Business_layer.Services
{
    public class SounServices : Isound
    {
        private readonly ISoundRepository soundRepository;

        public SounServices()
        {
            soundRepository = new SoundRepository();
        }
        public void SpeakNow(string text, int second = 1)
        {
           soundRepository.SpeakNow(text,second);
        }
    }
}
