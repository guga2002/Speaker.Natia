using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speaker.leison.Business_layer.Services
{
    public interface Isound
    {
        void SpeakNow(string text, int speed = 1);
    }
}
