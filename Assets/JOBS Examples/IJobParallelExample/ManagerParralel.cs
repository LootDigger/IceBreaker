using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
public class ManagerParralel : MonoBehaviour
{
    private NativeArray<Vector3> vectors;
    private NativeArray<float> resultMagnitudes;
    
    private void Start()
    {
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            vectors = new NativeArray<Vector3>(1000, Allocator.TempJob);
            resultMagnitudes = new NativeArray<float>(1000, Allocator.TempJob);
        
            ParallelMathJob parallelMathJob = new ParallelMathJob()
            {
                input = vectors,
                output = resultMagnitudes
            };
        
            var handle = parallelMathJob.Schedule(vectors.Length,4);
            handle.Complete();
            Debug.Log("Complete");
            vectors.Dispose();
            resultMagnitudes.Dispose();
        }


        if (Input.GetKeyDown(KeyCode.F2))
        {
            List<Vector3> target = new List<Vector3>();
            List<float> results = new List<float>();
            
            for (int i = 0; i < 1000; i++)
            {
                target.Add(Vector3.zero);
                results.Add(target[i].magnitude);
            }
            
            Debug.Log("Complete");
        }
    }
    
}

public struct ParallelMathJob : IJobParallelFor
{
    [ReadOnly]
    public NativeArray<Vector3> input;
    [WriteOnly]
    public NativeArray<float> output;
    
    public void Execute(int index)
    {
        output[index] = input[index].magnitude;
    }
}
