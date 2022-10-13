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
            Blocks = new List<Block>();

            var genesisBlock = new Block();
            Blocks.Add(genesisBlock);
            Last = genesisBlock;
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
        }
    }
}
