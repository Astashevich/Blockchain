using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Blockchain
{
    /// <summary>
    /// A block from a chain of blocks.
    /// </summary>
    [DataContract]
    public class Block
    {
        /// <summary>
        /// Identificator.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; private set; }

        /// <summary>
        /// Block data.
        /// </summary>
        [DataMember]
        public string Data { get; private set; }

        /// <summary>
        /// The moment the block was created.
        /// </summary>
        [DataMember]
        public DateTime Created { get; private set; }

        /// <summary>
        /// Block hash.
        /// </summary>
        [DataMember]
        public string Hash { get; private set; }

        /// <summary>
        /// Previous block hash.
        /// </summary>
        [DataMember]
        public string PreviousHash { get; private set; }

        /// <summary>
        /// ID of the user who created the block.
        /// </summary>
        [DataMember]
        public string User { get; private set; }

        /// <summary>
        /// Create a new instance of the start (genesis) block.
        /// </summary>
        public Block() 
        {
            Id = 1;
            Data = "Hello, World";
            Created = DateTime.Parse("2022-01-01T00:00:00.000+00:00").ToUniversalTime();
            PreviousHash = "111111";
            User = "Admin";

            Hash = GetHash(GetData());
        }

        /// <summary>
        /// Create a new instance of the block.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        /// <param name="block"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Block(string data, string user, Block block)
        {
            if(string.IsNullOrWhiteSpace(data)) throw new ArgumentNullException(nameof(data), "Empty data argument");

            if (string.IsNullOrWhiteSpace(user)) throw new ArgumentNullException(nameof(user), "Empty user argument");

            if (block == null) throw new ArgumentNullException(nameof(block), "Empty block argument");

            Data = data;
            User = user;
            PreviousHash = block.Hash;
            Created = DateTime.UtcNow;
            Id = block.Id + 1;

            Hash = GetHash(GetData());
        }

        /// <summary>
        /// Get data from the object, based on which the hash will be built.
        /// </summary>
        /// <returns> Data for hashing. </returns>
        private string GetData()
        {
            string result = "";

            result += Id.ToString();
            result += Data;
            result += Created.ToString("dd.MM.yyyy HH:mm:ss.fff");
            result += PreviousHash;
            result += User;

            return result;
        }

        /// <summary>
        /// Data hashing.
        /// </summary>
        /// <param name="data"></param>
        /// <returns> Hash. </returns>
        private string GetHash(string data)
        {
            var message = Encoding.ASCII.GetBytes(data);
            var hex = "";

            var hashValue = new SHA256Managed().ComputeHash(message);
            foreach(byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        public override string ToString()
        {
            return Data;
        }

        /// <summary>
        /// Serialize object to JSON.
        /// </summary>
        /// <returns> JSON of Block. </returns>
        private string Serialize()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(Block));

            using(var ms = new MemoryStream())
            {
                jsonSerializer.WriteObject(ms, this);
                var result = Encoding.UTF8.GetString(ms.ToArray());
                return result;
            }
        }

        /// <summary>
        /// Deserialize JSON to Block object.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private Block Deserialize(string json)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(Block));

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var result = (Block)jsonSerializer.ReadObject(ms);
                return result;
            }
        }
    }
}
