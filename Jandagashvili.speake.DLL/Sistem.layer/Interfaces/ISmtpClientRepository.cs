namespace Speaker.leison.Sistem.layer.Interfaces
{
    public interface ISmtpClientRepository
    {
        void SendMessage(string to, string body, string Context);
    }
}
