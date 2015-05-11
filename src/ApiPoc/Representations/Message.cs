using Microsoft.AspNet.WebUtilities;
using System;

namespace ApiPoc.Representations
{
    public class Message : BaseRepresentation
    {
        public string MessageText { get; set; }

        public Message(string messageText)
        {
            MessageText = messageText;
        }
    }
}
