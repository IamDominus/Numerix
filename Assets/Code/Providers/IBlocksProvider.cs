using Code.Logic;

namespace Code.Providers
{
    public interface IBlocksProvider
    {
        Block[,] Blocks { get; }
    }
}