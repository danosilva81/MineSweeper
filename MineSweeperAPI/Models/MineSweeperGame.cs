using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MineSweeperAPI.Models
{
    public class MineSweeperGame
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int XDimension { get; set; }

        public int YDimension { get; set; }

        public MineCell[] MineCellCollection { get; set; }

        public int NumberOfBombs { get; set; }

        public bool GameIsOver { get; set; }

        public bool GameIsWon { get; set; }
    }
}
