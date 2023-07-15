using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManagerScr : MonoBehaviour
{
    [SerializeField] List<GameObject> customerList = new List<GameObject>();
    [SerializeField] GameObject customerPrefab;

    [Header("First Move Vars")]
    List<Vector3> lineInitialPoses = new List<Vector3>();
    List<Vector3> lineTargetPoses = new List<Vector3>();
    float firstLineDistance = 11.5f;
    bool lineMove = false;
    float startTime;
    [SerializeField] float firstLineMoveDuration = 5f;
    [SerializeField] float lineContinueDuration = 3f;
    [SerializeField] float lineContinueDistance = 2f;
    bool lineContinue = false;

    [Header("Leave the Line vars")]
    Vector3 lineHeadPos;
    Vector3 lineHeadDes;
    [SerializeField] float lineLeaveDistance;
    [SerializeField] float lineLeaveDuration;
    float lineLeaveStartTime;
    bool goLeaveLine = false;

    void Start()
    {
        FirstMoveStart();
    }

    void Update()
    {
        MoveTheLine(firstLineMoveDuration);
        LeaveTheLine();
        ContinueTheLine();
    }

    void MoveTheLine(float t)
    {
        if(lineMove)
        {
            float moveAm = (Time.time-startTime)/t;

            for(int i = 0 ; i <customerList.Count; i++)
            {
                customerList[i].transform.position = Vector3.Lerp(lineInitialPoses[i],lineTargetPoses[i],moveAm);
            }
        }
    }

    void FirstMoveStart()
    {
        foreach (GameObject x in customerList)
        {
            lineInitialPoses.Add(x.transform.position);
            lineTargetPoses.Add(x.transform.position + x.transform.right*firstLineDistance);
        }
        
        startTime = Time.time;
        lineMove = true;

        Invoke("firstLineStop",firstLineMoveDuration);     
    }

    void LeaveTheLine()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //This simulates the order being compeleted for now
        {
            customerList[0].GetComponentInChildren<Canvas>().enabled = false;
            lineHeadPos = customerList[0].transform.position;
            lineHeadDes = lineHeadPos + (customerList[0].transform.right * lineLeaveDistance);
            lineLeaveStartTime = Time.time;
            goLeaveLine = true;

            lineInitialPoses.Clear();
            lineTargetPoses.Clear();

            for (int i = 1; i < customerList.Count ; i ++)
            {
                lineInitialPoses.Add(customerList[i].transform.position);
                lineTargetPoses.Add(customerList[i].transform.position + customerList[i].transform.right*lineContinueDistance);
            }

            startTime = Time.time;
            lineContinue = true;

            Invoke("firstLineStop",firstLineMoveDuration);
            Invoke("lineContinueStop",lineContinueDuration);
        }

        if (goLeaveLine)
        {
            float leaveAm = (Time.time - lineLeaveStartTime)/lineLeaveDuration;
            customerList[0].transform.position = Vector3.Lerp(lineHeadPos,lineHeadDes,leaveAm);            
        }
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Customer")
        {
            goLeaveLine = false;
            customerList.RemoveAt(0);
            Destroy(other.gameObject);

            GameObject newCustomer = Instantiate(customerPrefab, new Vector3(-14,-7,0), Quaternion.identity);
            customerList.Add(newCustomer);
        }
    }

    void firstLineStop()
    {
        lineMove = false;
    }

    void ContinueTheLine()
    {
        if(lineContinue)
        {
            float moveAm = (Time.time-startTime)/lineContinueDuration;

            for(int i = 0 ; i <customerList.Count-1; i++)
            {
                customerList[i+1].transform.position = Vector3.Lerp(lineInitialPoses[i],lineTargetPoses[i],moveAm);
            }
        }        
    }

    void lineContinueStop()
    {
        lineContinue = false;
    }
}
