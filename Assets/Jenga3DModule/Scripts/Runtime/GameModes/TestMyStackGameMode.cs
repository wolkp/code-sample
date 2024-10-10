using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "TestMyStackGameMode", menuName = "GameModes/TestMyStackGameMode")]
public class TestMyStackGameMode : GameMode
{
    [SerializeField] private int _masteryLevelBlocksToDestroy;

    [Inject] private readonly StackFocusController _stackFocusController;
    [Inject] private readonly BlocksRegistry _blocksRegistry;

    public override void Activate()
    {
        var stackInFocus = _stackFocusController.CurrentFocusedStack;
        var blocksFromStack = _blocksRegistry.GetBlocksFromStack(stackInFocus);

        foreach (var block in blocksFromStack)
        {
            if (ShouldDestroyBlock(block))
            {
                DestroyBlock(block);
            }
            else
            {
                EnableBlockPhysics(block);
            }
        }
    }

    private bool ShouldDestroyBlock(Block block)
    {
        var gradeData = block.Data.GradeData;
        return gradeData.mastery == _masteryLevelBlocksToDestroy;
    }

    private void DestroyBlock(Block block)
    {
        GameObject.Destroy(block.gameObject);
    }

    private void EnableBlockPhysics(Block block)
    {
        block.Physics.EnablePhysics();
    }
}