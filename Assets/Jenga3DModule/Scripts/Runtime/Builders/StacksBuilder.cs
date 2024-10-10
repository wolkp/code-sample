using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Zenject;

public class StacksBuilder : IInitializable
{
    private readonly SignalBus _signalBus;
    private readonly StackFactory _stackFactory;
    private readonly StacksBuilderInstaller.Settings _builderSettings;
    private readonly IDataProvider _dataProvider;
    private readonly StacksParent _stacksParent;

    public StacksBuilder(
        SignalBus signalBus,
        StackFactory stackFactory,
        StacksBuilderInstaller.Settings builderSettings,
        IDataProvider dataProvider,
        StacksParent stacksParent)
    {
        _signalBus = signalBus;
        _stackFactory = stackFactory;
        _builderSettings = builderSettings;
        _dataProvider = dataProvider;
        _stacksParent = stacksParent;
    }

    public void Initialize()
    {
        BuildStacksAsync().Forget();
    }

    private async UniTaskVoid BuildStacksAsync()
    {
        var cancellationToken = _stacksParent.GetCancellationTokenOnDestroy();
        var gradeData = await _dataProvider.FetchGradeDataAsync(cancellationToken);

        if (gradeData != null)
        {
            CreateStacksBasedOnData(gradeData);
        }
        else
        {
            Debug.LogError("Failed to fetch grade data.");
        }

        _signalBus.Fire(new AllStacksCreatedSignal());
    }

    private void CreateStacksBasedOnData(GradeData[] gradeData)
    {
        var gradeGroups = GroupByGrade(gradeData);
        var index = 0;

        foreach (var gradeGroup in gradeGroups)
        {
            GradeData[] sortedGradeData = SortGradeData(gradeGroup.Value);
            var stackPosition = CalculateStackPosition(index);

            Stack stack = CreateStack(gradeGroup.Key, stackPosition, _stacksParent.transform);
            _signalBus.Fire(new StackCreatedSignal(stack, sortedGradeData));

            index++;
        }
    }

    private Dictionary<string, GradeData[]> GroupByGrade(GradeData[] data)
    {
        return data
            .GroupBy(gradeData => gradeData.grade)
            .ToDictionary(group => group.Key, group => group.ToArray());
    }

    private GradeData[] SortGradeData(GradeData[] data)
    {
        return data
            .OrderBy(gradeData => gradeData.domain)
            .ThenBy(gradeData => gradeData.cluster)
            .ThenBy(gradeData => gradeData.standardid)
            .ToArray();
    }

    private Stack CreateStack(string gradeLevel, Vector3 position, Transform parentTransform)
    {
        StackCreationConfig stackCreationConfig = new()
        {
            GradeLevel = gradeLevel,
            Position = position,
            ParentTransform = parentTransform
        };

        return _stackFactory.Create(stackCreationConfig);
    }

    private Vector3 CalculateStackPosition(int index)
    {
        var spaceBetweenStacks = _builderSettings.SpaceBetweenStacks;
        var xOffset = index * spaceBetweenStacks;

        return new Vector3(xOffset, 0, 0);
    }
}