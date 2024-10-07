using Code.Gameplay;

namespace Code.Providers
{
    public interface IBlocksProvider
    {
        Block[,] Blocks { get; }
    }
}