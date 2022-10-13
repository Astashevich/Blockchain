using System.Security.Cryptography;
using System.Text;

namespace Blockchain
{
    /// <summary>
    /// A block from a chain of blocks.
    /// </summary>
    public class Block
    {
        /// <summary>
        /// Identificator.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Block data.
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// The moment the block was created.
        /// </summary>
        public DateTime Created { get; private set; }

        /// <summary>
        /// Block hash.
        /// </summary>
        public string Hash { get; private set; }

        /// <summary>
        /// Previous block hash.
        /// </summary>
        public string PreviousHash { get; private set; }

        /// <summary>
        /// ID of the user who created the block.
        /// </summary>
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
            if(string.IsNullOrWhiteSpace(data)) throw new ArgumentNullException("Empty data argument", nameof(data));

            if (string.IsNullOrWhiteSpace(user)) throw new ArgumentNullException("Empty user argument", nameof(user));

            if (block == null) throw new ArgumentNullException("Empty block argument", nameof(block));

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
    }
}
