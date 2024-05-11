using Speaker.leison.Business_layer.Interface;
using Speaker.leison.Sistem.layer.Interfaces;
using Speaker.leison.Sistem.layer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speaker.leison.Business_layer.Services
{
    public class UdpServices : IUdp
    {
        private readonly IUdpComunicationRepository ser;
        public UdpServices()
        {
            ser = new UdpComunicationRepository();
        }
        public string Receive()
        {
            return ser.Receive();
        }
    }
}
