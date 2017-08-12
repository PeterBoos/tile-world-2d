using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorkController : Singleton<WorkController> {
    
    public static List<Work> workList;

	// Use this for initialization
	void Start () {
        workList = new List<Work>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void AddNewWork(Work.WorkType typeOfWork, Vector3 workPosition)
    {
        workList.Add(new Work(typeOfWork, workPosition));
    }

    public static void RemoveWork(Work.WorkType typeOfWork, Vector3 workPosition)
    {
        var workToRemove = workList.SingleOrDefault(w => w.Type == typeOfWork && w.Position == workPosition);
        workList.Remove(workToRemove);
    }

    public static void RemoveWork(Work workItemToRemove)
    {
        workList.Remove(workItemToRemove);
    }

    public static Work GetOldestWork()
    {
        if (!workList.Any()) return null;
        return workList[0];
    }

    public static Work GetOldestWorkOfType(Work.WorkType typeOfWork)
    {
        if (!workList.Where(w => w.Type == typeOfWork).Any()) return null;

        return workList.First(w => w.Type == typeOfWork);
    }
}
