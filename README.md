# cs-path-finding

Path Finding library with Dijstra, A* 

# Install

Run the following command to install nuget package:

```bash 
Install-Package cs-path-finding
```

# Usage

The sample code below shows how to use the Dijstra Path finding:

```cs 
using System;
using System.Collections.Generic;

namespace PathFinding
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            QuadTree space = new QuadTree(new Rectangle(0, 0, 2000, 2000));
            GridWorld pathFinder = new GridWorld(space);

            FVec2 target = new FVec2(1000, 1000);
            List<IAgent> population = new List<IAgent>();
            for (int i = 0; i < 10; ++i)
            {
                IAgent agent = new SimpleAgent(space);
                float y = 0; // vertical position
                float x = random.Next(2000);
                float z = random.Next(2000);
                agent.Position = new FVec3(x, y, z);
                agent.AgentID = i + 1;
                population.Add(agent);
            }

            for (int steps = 0; steps < 20; ++steps)
            {
                Dijstra paths = pathFinder.dijstra(target);

                foreach (IAgent agent in population)
                {
                    float y = 0; // vertical position
                    float x = random.Next(2000);
                    float z = random.Next(2000);
                    agent.Position = new FVec3(x, y, z);
                    List<FVec3> path = paths.GetPath(agent);
                    Console.WriteLine("Path for agent #{0} in step {1}: {2} navigation points", 
                        agent.AgentID, steps+1, path.Count);
                }

                target = new FVec2(random.Next(2000), random.Next(2000));
            }

        }
    }
}
```
