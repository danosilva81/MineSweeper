using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MineSweeperAPI.Models
{
    public class MineCell
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int ArrayPostion { get; set; }

        public int XPos { get; set; }

        public int YPos { get; set; }

        public bool IsBomb { get; set; }

        public bool MarkedAsBomb { get; set; }

        public int NumberOfAdjacentBombs { get; set; }

        public bool IsRevealed { get; set; }
    }
}
