using Code.Providers;
using Zenject;

namespace Code.Gameplay.Providers
{
    public class BlocksProvider : IBlocksProvider, IInitializable
    {
        public Block[,] Blocks { get; private set; }

        private readonly ISelectedLevelProvider _selectedLevelProvider;

        public BlocksProvider(ISelectedLevelProvider selectedLevelProvider)
        {
            _selectedLevelProvider = selectedLevelProvider;
        }

        public void Initialize()
        {
            Blocks = new Block[_selectedLevelProvider.Level.Value.x, _selectedLevelProvider.Level.Value.y];
        }

        public void AddBlock(Block block)
        {
            Blocks[block.Model.Position.x, block.Model.Position.y] = block;
        }

        public void Clear()
        {
            for (var x = 0; x < Blocks.GetLength(0); x++)
            {
                for (var y = 0; y < Blocks.GetLength(1); y++)
                {
                    Blocks[x, y]?.Delete();
                    Blocks[x, y] = null;
                }
            }
        }
    }
}