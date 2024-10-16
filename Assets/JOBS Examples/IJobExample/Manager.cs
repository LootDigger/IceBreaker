using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class Manager : MonoBehaviour
{
    void Start()
    {
      //  RunSimpleJob();
      //  RunSimpleJobInput();
        RunJobChain();
    }

    void RunSimpleJob()
    {
        MathOperationJob job = new MathOperationJob();
        var handle = job.Schedule();
        handle.Complete();
    }
    
    void RunSimpleJobInput()
    {
        NativeArray<int> array = new NativeArray<int>(3, Allocator.TempJob);
        array[0] = 11;
        array[1] = 13;
        MathOperationJobInput job = new MathOperationJobInput()
        {
            bridge = array
        };
        
        var handle = job.Schedule();
        handle.Complete();
        
        Debug.Log("Job result: " + array[2]);
        array.Dispose();
    }

    void RunJobChain()
    {
        MathOperationJob job = new MathOperationJob();
        var handle = job.Schedule();
        
        NativeArray<int> array = new NativeArray<int>(3, Allocator.TempJob);
        array[0] = 11;
        array[1] = 13;
        MathOperationJobInput job2 = new MathOperationJobInput()
        {
            bridge = array
        };

        var handle2 = job2.Schedule(handle);
        handle.Complete();
        handle2.Complete();
        Debug.Log("Job result: " + array[2]);
        array.Dispose();
    }
}

public struct MathOperationJob : IJob
{
    public void Execute()
    {
        Debug.Log(SummOperation(10,5));
    }
    public int SummOperation(int x, int y) => x + y;
}

public struct MathOperationJobInput : IJob
{
    public NativeArray<int> bridge;
    
    public void Execute()
    {
        bridge[2] = SummOperation(bridge[0],bridge[1]);
    }
    public int SummOperation(int x, int y) => x + y;
}
