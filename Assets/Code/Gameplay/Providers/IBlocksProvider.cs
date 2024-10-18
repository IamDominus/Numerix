namespace Code.Gameplay.Providers
{
    public interface IBlocksProvider
    {
        Block[,] Blocks { get; }
        void AddBlock(Block block);
        void Clear();
    }
}