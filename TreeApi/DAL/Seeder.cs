using Microsoft.EntityFrameworkCore;
using TreeApp.Models;

namespace TreeApp.DAL;

internal sealed class Seeder(TreeDbContext dbContext)
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Nodes.Any())
            {
                var nodes = GetNodes();
                dbContext.Nodes.AddRange(nodes);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Node> GetNodes()
    {
        List<Node> nodes =
        [
            new()
            {
                Id = new Guid("87834ba6-8766-44af-a1b4-7200234c1ec8"),
                Name = "Root",
                Childrens = new List<Node>
                {
                    new()
                    {
                        Id = new Guid("a288c115-93a0-4c80-9c50-ff9e78f98154"),
                        ParentNodeId = new Guid("87834ba6-8766-44af-a1b4-7200234c1ec8"),
                        Name = "Documents"
                    },
                    new()
                    {
                        Id = new Guid("65e0b2bd-c74d-4167-9220-75659b9edb24"),
                        Name = "Photos",
                        ParentNodeId = new Guid("87834ba6-8766-44af-a1b4-7200234c1ec8"),
                        Childrens = new List<Node>
                        {
                            new()
                            {
                                Id = new Guid("f7e27fca-55b3-4e5d-a69f-6565bae392b3"),
                                ParentNodeId = new Guid("65e0b2bd-c74d-4167-9220-75659b9edb24"),
                                Name = "Holidays"
                            }
                        }
                    },
                    new()
                    {
                        Id = new Guid("9f6d1646-e3ec-4729-b581-c67555b1d932"),
                        Name = "Music",
                        ParentNodeId = new Guid("87834ba6-8766-44af-a1b4-7200234c1ec8"),
                        Childrens = new List<Node>
                        {
                            new()
                            {
                                Id = new Guid("494bc9c5-ce21-40a8-b70d-0b6c70776174"),
                                ParentNodeId = new Guid("9f6d1646-e3ec-4729-b581-c67555b1d932"),
                                Name = "Rock"
                            },
                        }
                    },
                }
            }
        ];

        return nodes;
    }
}