using System;
using System.Collections.Generic;
using System.Linq;
using Substation_Builder.Model;

namespace Substation_Builder.Helpers
{
    public static class NodesDataSource
    {
        public static Random random = new Random();

        public static Node GetRandomNode()
        {
            return new Node
                {
                    Name = "Node" + random.Next(0,100),
                    X = random.Next(0,500),
                    Y = random.Next(0,500)
                };
            
        }

        public static IEnumerable<Node> GetRandomNodes()
        {
            return Enumerable.Range(5, random.Next(6, 10)).Select(x => GetRandomNode());
        }

        public static Connector GetRandomConnector(IEnumerable<Node> nodes)
        {
            return new Connector
                {
                      
                };
        }

        public static IEnumerable<Connector> GetRandomConnectors(List<Node> nodes)
        {
            var result = new List<Connector>();
            for (int i = 0; i < nodes.Count() - 1; i++)
            {
                result.Add(new Connector() 
                {
                    Start = nodes[i], 
                    End = nodes[i + 1],
                    Name = "Connector" + random.Next(1, 100).ToString()
                });
            }
            return result;
        }
    }
}