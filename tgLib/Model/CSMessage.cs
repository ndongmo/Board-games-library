using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGL.Model
{
    /// <summary>
    /// Message model used for communication between clients and server.
    /// Each message has an overall type and a set of Pair<MessageType, Object>.
    /// </summary>
    [Serializable]
    public class CSMessage
    {
        private MessageType type;
        private Dictionary<MessageType, Object> attributes;

        public CSMessage(MessageType type)
        {
            this.type = type;
            this.attributes = new Dictionary<MessageType, object>();
        }

        public MessageType Type { get { return type; } set { type = value; } }

        public CSMessage add(MessageType key, Object value)
        {
            attributes[key] = value;
            return this;
        }

        public Object get(MessageType key)
        {
            Object value;
            attributes.TryGetValue(key, out value);
            return value;
        }

        public int size()
        {
            return attributes.Count;
        }

        public List<Object> getList()
        {
            return attributes.Values.ToList();
        }

        public void remove(MessageType key)
        {
            attributes.Remove(key);
        }

        public void clear()
        {
            attributes.Clear();
        }
    }
}
