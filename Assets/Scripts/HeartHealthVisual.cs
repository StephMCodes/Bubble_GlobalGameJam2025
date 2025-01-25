using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using JetBrains.Annotations;

public class HeartHealthVisual : MonoBehaviour
{
    public static HeartHealthSystem heartHealthSysytemStatic;

    [SerializeField] private Sprite heart0Sprite;
    //[SerializeField] private Sprite heart1Sprite;
    //[SerializeField] private Sprite heart2Sprite;
    //[SerializeField] private Sprite heart3Sprite;
    [SerializeField] private Sprite heart4Sprite;
    [SerializeField] private AnimationClip heartFullAnimationClip;

    private List<HeartImage> heartImageList;
    private HeartHealthSystem heartHealthSystem;
    private bool isHealing;

    private void Awake()
    {
        //stores all hearts
        heartImageList = new List<HeartImage>();

    }
    private void Start()
    {
        //FunctionPeriodic.Create(HealingAnimatedPeriodic, 0.5f );
        HeartHealthSystem heartHealthSystem = new HeartHealthSystem(3); //call this function for hearts
        SetHeartsHealthSystem(heartHealthSystem);

    }

    public void SetHeartsHealthSystem(HeartHealthSystem heartHealthSystem)
    {
        this.heartHealthSystem = heartHealthSystem;
        heartHealthSysytemStatic = heartHealthSystem;


        List<HeartHealthSystem.Heart> heartList = heartHealthSystem.GetHeartList();
        Vector2 heartAnchoredPosition = new Vector2(-900, 500);
        for (int i = 0; i < heartList.Count; i++)
        {
            HeartHealthSystem.Heart heart = heartList[i];
            CreateHeartImage(heartAnchoredPosition).SetHeartFragments(heart.GetFragmentAmount());
            heartAnchoredPosition += new Vector2(95, 0);//30
        }

        heartHealthSystem.OnDamaged += HeartHealthSystem_OnDamaged;
        heartHealthSystem.OnHealed += HeartHealthSystem_OnHealed;
        heartHealthSystem.OnDead += HeartHealthSystem_OnDead;

    }

    private void HeartHealthSystem_OnDead(object sender, System.EventArgs e)
    {
        Debug.Log("Dead!");
    }
    private void HeartHealthSystem_OnHealed(object sender, System.EventArgs e)
    {//Hearts health system was healed
       RefreshAllHearts();
        isHealing = true;
    }

    private void HeartHealthSystem_OnDamaged(object sender, System.EventArgs e)
    { //Hearts health system was damaged
        RefreshAllHearts();

    }

    public void test(int healAMT)
    {
        Debug.Log("i work");
        List<HeartHealthSystem.Heart> heartList = heartHealthSystem.GetHeartList();
        for (int i = 0; i < heartImageList.Count; i++)
        {
            HeartImage heartImage = heartImageList[i];
            HeartHealthSystem.Heart heart = heartList[i];

           
        }
    }

    public void RefreshAllHearts()
    {
        List<HeartHealthSystem.Heart> heartList = heartHealthSystem.GetHeartList();
        for (int i = 0; i < heartImageList.Count; i++)
        {
            HeartImage heartImage = heartImageList[i];
            HeartHealthSystem.Heart heart = heartList[i];
            heartImage.SetHeartFragments(heart.GetFragmentAmount());
        }
    }

    private void HealingAnimatedPeriodic()
    {
        if (isHealing)
        {
            bool fullyHealed = true;
            List<HeartHealthSystem.Heart> heartList = heartHealthSystem.GetHeartList();
            for (int i = 0; i < heartImageList.Count; i++)
            {
                HeartImage heartImage = heartImageList[i];
                HeartHealthSystem.Heart heart = heartList[i];

                if (heartImage.GetFragmentAmount() != heart.GetFragmentAmount())
                { //Visiual is different from logic
                    heartImage.AddHeartVisualFragment();
                    if (heartImage.GetFragmentAmount() == HeartHealthSystem.MAX_FRAGMENT_AMOUNT)
                    { //This heart was fully healed
                        heartImage.PlayHeartFullAnimation();
                    }
                    fullyHealed = false;
                    break;
                }
            }
            if (fullyHealed)
            {
                isHealing = false;
            }
        }
    }

    private HeartImage CreateHeartImage(Vector2 anchoredPosition)
    {
        // Create Game Object
        GameObject heartGameObject = new GameObject("Heart", typeof(Image), typeof(Animation));
        // Set as a child of transform
        heartGameObject.transform.parent = transform;
        // Set heart sprite 
        heartGameObject.transform.localPosition = Vector3.zero;

        // Locate and size heart
        heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(75, 75);

       // heartGameObject.GetComponent<Animation>().AddClip(heartFullAnimationClip, "HeartFull");

        // Set heart sprite 
        Image heartImageUI = heartGameObject.GetComponent<Image>();
        heartImageUI.sprite = heart4Sprite;

        HeartImage heartImage = new HeartImage(this, heartImageUI, heartGameObject.GetComponent<Animation>());
        heartImageList.Add(heartImage);

        return heartImage;
    }

    //represents a single heart
    public class HeartImage
    {
        private int fragments;
        private Image heartImage;
        private HeartHealthVisual heartHealthVisual;
        private Animation animation;

        public HeartImage(HeartHealthVisual heartHealthVisual, Image heartImage, Animation animation)
        {
            this.heartHealthVisual = heartHealthVisual;
            this.heartImage = heartImage;
            this.animation = animation;
        }

        public void SetHeartFragments(int fragments)
        {
            this.fragments = fragments;
            switch (fragments)
            {
                case 0:
                    heartImage.sprite = heartHealthVisual.heart0Sprite;


                    break;


                //case 1:

                //    heartImage.sprite = heartHealthVisual.heart1Sprite;


                //    break;


                //case 2:
                //    heartImage.sprite = heartHealthVisual.heart2Sprite;



                //    break;


                //case 3:

                //    heartImage.sprite = heartHealthVisual.heart3Sprite;


                //    break;


                case 4:

                    heartImage.sprite = heartHealthVisual.heart4Sprite;


                    break;
            }
        }
        public int GetFragmentAmount()
        {
            return fragments;
        }
        public void AddHeartVisualFragment()
        {
            SetHeartFragments(fragments + 1);
        }

        public void PlayHeartFullAnimation()
        {
            animation.Play("HeartFull", PlayMode.StopAll);
        }
    }

}
   
    

