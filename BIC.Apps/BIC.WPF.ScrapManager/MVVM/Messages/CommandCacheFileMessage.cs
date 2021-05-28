using GalaSoft.MvvmLight.Messaging;

namespace BIC.WPF.ScrapManager.MVVM.Messages
{
    class CommandCacheFileMessage : MessageBase
    {
        public CommandCacheFileMessage(string command, string path)
        {
            Command = command;
            Path = path;
        }
        public string Command { get; set; }
        public string Path { get; set; }
    }
}
