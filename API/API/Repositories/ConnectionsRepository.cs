using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Repositories
{
    public class ConnectionsRepository
    {
        private readonly Dictionary<string, string> connections = new Dictionary<string, string>();

        public void Add(string slmId, string uniqueID)
        {
            connections[slmId] = uniqueID;
        }

        public string GetConnectionIdSlm(string id)
        {
            return (from con in connections
                    where con.Key == id
                    select con.Value).FirstOrDefault();
        }
    }
}
