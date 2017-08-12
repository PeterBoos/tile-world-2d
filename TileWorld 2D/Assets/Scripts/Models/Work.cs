using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Work {

	public enum WorkType { Chop_tree, Mine_stone, Construction }

    WorkType type;
    Vector3 position;

    #region Properties

    public WorkType Type
    {
        get
        {
            return type;
        }

        private set
        {
            type = value;
        }
    }

    public Vector3 Position
    {
        get
        {
            return position;
        }

        set
        {
            position = value;
        }
    }

    #endregion

    public Work(WorkType type, Vector3 position)
    {
        this.Type = type;
        this.Position = position;
    }
}
