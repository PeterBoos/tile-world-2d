﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float Speed = 5;
    public float AcceptableDistanceToWork = 1.6f;

    Character character;
    Vector3 moveTarget;
    bool isMoving;
    bool isIdle;

    // Worker
    Work currentWork;
    bool isAtWorkPosition;
    float workTimeLeft;

    // Use this for initialization
    void Start () {
        character = new Character("Bulbo", 20f);
        moveTarget = transform.position;
        isMoving = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (character.IsDead)
        {
            // Do die stuffs
        }

        if (isMoving)
        {
            MovePlayer();
        }

        if (isIdle)
        {
            LookForWork();
        }

        if (currentWork != null)
        {
            DoWork();
        }
	}

    public void MoveTo(Vector3 target)
    {
        SetMoveTarget(target);
    }

    void MovePlayer()
    {
        transform.LookAt(moveTarget);
        transform.position = Vector3.MoveTowards(transform.position, moveTarget, Speed * Time.deltaTime);

        if (transform.position == moveTarget)
        {
            isMoving = false;
        }

        if (currentWork != null)
        {
            var distanceToWork = Vector3.Distance(currentWork.Position, transform.position);
            if (distanceToWork <= AcceptableDistanceToWork)
            {
                isMoving = false;
                isAtWorkPosition = true;
            }
        }

        Debug.DrawLine(transform.position, moveTarget, Color.red);
    }

    void SetMoveTarget(Vector3 moveTo)
    {
        var planePoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        var plane = new Plane(Vector3.up, planePoint);
        var ray = Camera.main.ScreenPointToRay(moveTo);
        float point = 0f;

        if (plane.Raycast(ray, out point))
        {
            moveTarget = ray.GetPoint(point);
        }

        isMoving = true;
    }

    void LookForWork()
    {
        var work = WorkController.GetOldestWork();
        if (work == null) return;
        SetNewWork(work);
    }

    void SetNewWork(Work work)
    {
        currentWork = work;
        isAtWorkPosition = false;
        SetMoveTarget(work.Position);
        workTimeLeft = 3.0f;
    }

    void DoWork()
    {        
        if (currentWork != null && isAtWorkPosition)
        {
            workTimeLeft -= Time.deltaTime;
            if (workTimeLeft <= 0)
            {
                WorkFinished();
            }
        }
    }

    void WorkFinished()
    {
        Debug.Log("Work of type " + currentWork + " is done!");
        WorkController.RemoveWork(currentWork);
        currentWork = null;
        isIdle = true;
    }
}
