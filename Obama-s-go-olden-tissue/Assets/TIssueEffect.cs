using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TIssueEffect : MonoBehaviour {

    public RectTransform MyTransform;

    public RectTransform StartTile; //시작 타일
    public RectTransform EndTile; //끝 타일

    public Vector3 posTissue; //티슈 위치 변수

    public float Angle; //회전 각도 변수
    public float AngleSpeed; //회전 각속도

    public float Scale; //크기 변수

    public float MoveStartTime; //이동 시작시간 변수
    public float MoveTime; //이동 소요시간 변수

    public float MinSize; //최소 사이즈
    public float MaxSize; //최대 사이즈
    public float NowSize; //현재 사이즈
    public float ResizeStartTime; //사이즈변환 시작시간 변수
    public float ResizeTime; //사이즈변환 소요시간 변수



    // Use this for initialization
    void Start () {

        MyTransform = this.GetComponent<RectTransform>();
        ResizeStartTime = Time.time;
        ResizeTime = 0.7f;
        MoveStartTime = Time.time;
        MoveTime = 0.7f;
        AngleSpeed = 12;

    }
	
	// Update is called once per frame
	void Update () {

        RotateTissue();
        MoveTissue();
        Resize();

    }

    void MoveTissue()
    {
        if (Time.time - MoveStartTime <= MoveTime)
        {

            MyTransform.position = Vector3.Lerp(StartTile.position, EndTile.position, (Time.time - MoveStartTime) / MoveTime);

        }
    }

    void RotateTissue()
    {

        Angle += AngleSpeed;
        MyTransform.rotation = Quaternion.Euler(0, 0, Angle);
    }

    void Resize()
    {
        
        if (Time.time - ResizeStartTime <= ResizeTime)
        {
            if ( (Time.time - ResizeStartTime) / ResizeTime <= 0.5f)
            {
                NowSize = Mathf.Lerp(MinSize, MaxSize, (Time.time - ResizeStartTime) / ResizeTime);
            }
            else
            {
                NowSize = Mathf.Lerp(MaxSize, MinSize, (Time.time - ResizeStartTime) / ResizeTime);
            }

            MyTransform.localScale = new Vector3(NowSize, NowSize, 1);

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
