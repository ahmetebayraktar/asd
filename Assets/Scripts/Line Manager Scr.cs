using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManagerScr : MonoBehaviour
{
    [SerializeField] List<GameObject> customerList = new List<GameObject>();
    [SerializeField] GameObject customerPrefab;

    [Header("First Move Vars")]
    List<Vector3> lineInitialPoses = new List<Vector3>();
    List<Vector3> firstMoveTargetPoses = new List<Vector3>();
    float firstLineDistance = 11.5f;
    bool firstLineMove = false;
    float startTime;
    [SerializeField] float firstLineMoveDuration = 5f;

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
    }

    void MoveTheLine(float t)
    {
        if(firstLineMove)
        {
            float moveAm = (Time.time-startTime)/t;

            for(int i = 0 ; i <customerList.Count; i++)
            {
                customerList[i].transform.position = Vector3.Lerp(lineInitialPoses[i],firstMoveTargetPoses[i],moveAm);
            }
        }
    }

    void FirstMoveStart()
    {
        foreach (GameObject x in customerList)
        {
            lineInitialPoses.Add(x.transform.position);
            firstMoveTargetPoses.Add(x.transform.position + x.transform.right*firstLineDistance);
        }
        
        startTime = Time.time;
        firstLineMove = true;

        Invoke("firstLineStop",firstLineMoveDuration);     
    }

    void LeaveTheLine()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            customerList[0].GetComponentInChildren<Canvas>().enabled = false;
            lineHeadPos = customerList[0].transform.position;
            lineHeadDes = lineHeadPos + (customerList[0].transform.right * lineLeaveDistance);
            lineLeaveStartTime = Time.time;
            goLeaveLine = true;
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
        firstLineMove = false;
    }
}
