using Code.Logic;

namespace Code.Services
{
    public interface IBlocksProvider
    {
        Block[,] Blocks { get; }
    }
}