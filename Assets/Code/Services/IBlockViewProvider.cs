using Code.Logic;

namespace Code.Services
{
    public interface IBlockViewProvider
    {
        BlockView[,] Blocks { get; }
    }
}