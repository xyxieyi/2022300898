using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerController: MonoBehaviour 
{

    public Text Text_Name;//角色名字

    /// <summary>
    /// 角色移动控制 跑道切换 获得足够金币跳转到胜利界面
    /// </summary>
    private int count = 0;           //金币获得数量
    public Text countText;           //金币获得文本

    const float MinLane = -1;//跑道左边位置限制值
    const float MaxLane = 1;//跑道右边位置限制值
	const float LaneWidth = 1.3f;//跑道宽度
	const int DefaultLife = 3;//生命值
	const float StunDuration = 0.5f;//碰到障碍物时停留的0.5秒
	CharacterController controller;  //角色控制器
	public Animator animator;        //动画组件
	
	Vector3 moveDirection = Vector3.zero;//当前主角人物的位置
	public int targetLane;//当前主角人物的跑道位置
	float recoverTime = 0.0f;//恢复停留时的时间间隔
	
	public float gravity;//重力值
	public float speedZ;//Z轴速度
	public float speedX;//X轴速度
	public float speedJump;//跳跃值
	public float accelerationZ;//Z轴的移动值

    public AudioClip coin;          //声音控件
    public AudioClip hitAudio;      //受伤害控件
    public GameObject LoseUI,WinUI,PauseUI;

    public Image hpima;//生命值

    public Text timetext;
    public float timenum;
    public int maxnum;

    public GameObject player1, player2;
    private GameObject player3;

    public GameObject NPCUI;
	
	public bool IsStan ()
	{
		return recoverTime > 0.0f;
	}
	
	void Start ()
	{
        player3 = gameObject.transform.Find("player3").gameObject;
        Destroy(NPCUI, 3);
        if (PlayerPrefs.GetInt("player") == 0)
        {
            player1.SetActive(true);
            player2.SetActive(false);
            player3.SetActive(false);
        }
        else if(PlayerPrefs.GetInt("player") == 1)
        {
            player1.SetActive(false);
            player2.SetActive(true);
            player3.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("player") == 2)
        {
            player1.SetActive(false);
            player2.SetActive(false);
            player3.SetActive(true);
        }
        SetCountText();

        controller = GetComponent<CharacterController>();   //自动获取所需的组件
        animator = GetComponent<Animator>();
        //  animator.SetBool("IsRun", true);                    //调用跑步动画
        InvokeRepeating("Atime", 1, 1);
    }


    //计时
    public void Atime()
    {
        
        timenum--;
        timetext.text = FormatTime(timenum);
        if (timenum <= 0)
        {
            GameObject.Find("Canvas_new").transform.Find("bg").gameObject.SetActive(true);
            LoseUI.SetActive(true);
            Time.timeScale = 0;
            CancelInvoke("Atime");
        }
    }
    public static string FormatTime(float seconds)
    {
        TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(seconds));
        string str = "";

        if (ts.Hours > 0)
        {
            str = ts.Hours.ToString("00") + ":" + ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
        }
        if (ts.Hours == 0 && ts.Minutes > 0)
        {
            str = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
        }
        if (ts.Hours == 0 && ts.Minutes == 0)
        {
            str ="00:" + ts.Seconds.ToString("00");
        }

        return str;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseUI.activeInHierarchy)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
            PauseUI.SetActive(!PauseUI.activeInHierarchy);
        }

        //移动 跳跃的鼠标捕抓
        if (Input.GetKeyDown("a")) MoveToLeft();
        if (Input.GetKeyDown("d")) MoveToRight();
        if (Input.GetKeyDown("space")) Jump();
        if (Input.GetKeyDown("s")) Drop();//下滑

        if (IsStan())
        {

            moveDirection.x = 0.0f;
            moveDirection.z = 0.0f;
            recoverTime -= Time.deltaTime;
        }
        else
        {
            //慢慢加速，按z方向继续前进
            float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
            moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

            //x方向是以和目标位置之间的差别比率来计算速度
            float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
            moveDirection.x = ratioX * speedX;
        }

        moveDirection.y -= gravity * Time.deltaTime;

        //移动实行
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);

        //移动后，若在接地，y方向的速度就会发生变化
        if (controller.isGrounded) moveDirection.y = 0;

        // 速度在0以上时，使奔跑的标志飞行为true
        animator.SetBool("run", moveDirection.z > 0.0f);

    }
    //启动左跑道
	public void MoveToLeft ()
	{
		if (IsStan()) return;
		if (controller.isGrounded && targetLane > MinLane) targetLane--;
	}

    //启动右跑道
	public void MoveToRight ()
	{
		if (IsStan()) return;
		if (controller.isGrounded && targetLane < MaxLane) targetLane++;
	}
	
	public void Jump ()
	{
		if (IsStan()) return;
		if (controller.isGrounded) 
		{
			moveDirection.y = speedJump;

            //设置跳跃动画触发
            animator.SetTrigger("jump");
		}
	}

    public void Drop()
    {
        if (IsStan()) return;
        if (controller.isGrounded)
        {
            // moveDirection.y = speedJump;
            //设置下滑动画触发
            animator.SetTrigger("drop");
        }
    }
	void OnControllerColliderHit (ControllerColliderHit hit) 
	{
		if(IsStan()) return;
		
		if(hit.gameObject.tag == "Robo")
		{
            //减少生命值
			recoverTime = StunDuration;

            hpima.fillAmount -= 0.34f;
            if (hpima.fillAmount <= 0)
            {
                GameObject.Find("Canvas_new").transform.Find("bg").gameObject.SetActive(true);
                LoseUI.SetActive(true);
                Time.timeScale = 0;
            }


            Debug.Log("111");
            AudioSource.PlayClipAtPoint(hitAudio, this.transform.position);//播放声音
           
            //销毁hit
            Destroy(hit.gameObject);
		}
	}

    void OnTriggerEnter(Collider other)
    {
        //碰撞获取金币
        if (other.gameObject.CompareTag("JB"))
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            count = count + 1;
            AudioSource.PlayClipAtPoint(coin, this.transform.position);//播放声音
            SetCountText();
        }

    }

    void SetCountText()
    {
        countText.text = "奖牌:" + count+"/"+ maxnum;
        if (count >= maxnum)
        {
            GameObject.Find("Canvas_new").transform.Find("bg").gameObject.SetActive(true);
            CancelInvoke("Atime");
            Time.timeScale = 0;
            WinUI.SetActive(true);
        }
    }

}