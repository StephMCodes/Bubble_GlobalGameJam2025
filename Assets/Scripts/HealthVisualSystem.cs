using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthVisualSystem : MonoBehaviour
{
    public static HealthSystem heartHealthSysytemStatic;

    [SerializeField] private Sprite heart0Sprite;
    [SerializeField] private Sprite heart1Sprite;
    [SerializeField] private Sprite heart2Sprite;
    [SerializeField] private Sprite heart3Sprite;
    [SerializeField] private Sprite heart4Sprite;
    [SerializeField] private AnimationClip heartFullAnimationClip;


    private List<HeartImage> heartImageList;
    private HealthSystem heartHealthSystem;
    private bool isHealing;

    private void Awake()
    {
        //stores all hearts
        heartImageList = new List<HeartImage>();

    }
    private void Start()
    {
        //FunctionPeriodic.Create(HealingAnimatedPeriodic, 0.5f );
        HealthSystem heartHealthSystem = new HealthSystem(4); //call this function for hearts
        SetHeartsHealthSystem(heartHealthSystem);

        // CMDebug.ButtonUI(new Vector2(-50, -100), "Damage 1", () => heartHealthSystem.Damage(1));
        // CMDebug.ButtonUI(new Vector2(50, -100), "Damage 4", () => heartHealthSystem.Damage(4));

    }

    public void SetHeartsHealthSystem(HeartHealthSystem heartHealthSystem)
    {
        this.heartHealthSystem = heartHealthSystem;
        heartHealthSysytemStatic = heartHealthSystem;


        List<HealthSystem.Heart> heartList = heartHealthSystem.GetHeartList();
        Vector2 heartAnchoredPosition = new Vector2(725, 500);
        for (int i = 0; i < heartList.Count; i++)
        {
            HealthSystem.Heart heart = heartList[i];
            CreateHeartImage(heartAnchoredPosition).SetHeartFragments(heart.GetFragmentAmount());
            heartAnchoredPosition += new Vector2(60, 0);//30
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

    private void RefreshAllHearts()
    {
        List<HealthSystem.Heart> heartList = heartHealthSystem.GetHeartList();
        for (int i = 0; i < heartImageList.Count; i++)
        {
            HeartImage heartImage = heartImageList[i];
            HealthSystem.Heart heart = heartList[i];
            heartImage.SetHeartFragments(heart.GetFragmentAmount());
        }
    }

    private void HealingAnimatedPeriodic()
    {
        if (isHealing)
        {
            bool fullyHealed = true;
            List<HealthSystem.Heart> heartList = heartHealthSystem.GetHeartList();
            for (int i = 0; i < heartImageList.Count; i++)
            {
                HeartImage heartImage = heartImageList[i];
                HealthSystem.Heart heart = heartList[i];

                if (heartImage.GetFragmentAmount() != heart.GetFragmentAmount())
                { //Visiual is different from logic
                    heartImage.AddHeartVisualFragment();
                    if (heartImage.GetFragmentAmount() == HealthSystem.MAX_FRAGMENT_AMOUNT)
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
        heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);

        heartGameObject.GetComponent<Animation>().AddClip(heartFullAnimationClip, "HeartFull");

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
        private HealthVisual HealthVisual;
        private Animation animation;

        public HeartImage(HealthVisual heartHealthVisual, Image heartImage, Animation animation)
        {
            this.HealthVisual = heartHealthVisual;
            this.heartImage = heartImage;
            this.animation = animation;
        }

        public void SetHeartFragments(int fragments)
        {
            this.fragments = fragments;
            switch (fragments)
            {
                case 0:
                    heartImage.sprite = HealthVisual.heart0Sprite;


                    break;


                case 1:

                    heartImage.sprite = HealthVisual.heart1Sprite;


                    break;


                case 2:
                    heartImage.sprite = HealthVisual.heart2Sprite;



                    break;


                case 3:

                    heartImage.sprite = HealthVisual.heart3Sprite;


                    break;


                case 4:

                    heartImage.sprite = HealthVisual.heart4Sprite;


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