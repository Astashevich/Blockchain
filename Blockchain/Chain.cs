using Blockchain.DbContexts;

namespace Blockchain
{
    /// <summary>
    /// Blocks chain.
    /// </summary>
    public class Chain
    {
        /// <summary>
        /// All blocks.
        /// </summary>
        public List<Block> Blocks { get; private set; }

        /// <summary>
        /// Last added block.
        /// </summary>
        public Block Last { get; private set; }

        /// <summary>
        /// Creating a new block chain.
        /// </summary>
        public Chain()
        {
            Blocks = LoadChainFromDb();

            if (Blocks.Count == 0)
            {
                var genesisBlock = new Block();
                Blocks.Add(genesisBlock);
                Last = genesisBlock;
                Save(Last);
            }
            else
            {
                if(Check()) Last = Blocks.Last();
                else throw new Exception("Getting blocks from database error. The chain did not pass the integrity check.");
            }
        }

        /// <summary>
        /// Add block.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        public void Add(string data, string user)
        {
            var block = new Block(data, user, Last);
            Blocks.Add(block);
            Last = block;
            Save(Last);
        }

        /// <summary>
        /// Chain Validation Method
        /// </summary>
        /// <returns> true - chain is correct. false - chain isn't correct. </returns>
        public bool Check()
        {
            var genesisBlock = new Block();
            var previousHash = genesisBlock.Hash;

            foreach(var block in Blocks.Skip(1))
            {
                var hash = block.PreviousHash;

                if(previousHash != hash) return false;

                previousHash = block.Hash;
            }

            return true;
        }

        /// <summary>
        /// Block write method to database
        /// </summary>
        /// <param name="block"> Saved block. </param>
        private void Save(Block block)
        {
            using (BlockchainDbContext db = new())
            {
                db.Blocks.Add(block);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Get data by chain from DB 
        /// </summary>
        /// <returns> List of blocks data. </returns>
        private List<Block> LoadChainFromDb()
        {
            List<Block> result;

            using (BlockchainDbContext db = new())
            {
                var count = db.Blocks.Count();

                result = new List<Block>(count * 2);

                result.AddRange(db.Blocks);
            }

            return result;
        }

        private void Sync()
        {

        }
    }
}
