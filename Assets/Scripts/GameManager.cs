using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Context newContext;
    public Animator animController;

    void Awake()
    {
        Singleton();
        SetUpSM();
    }

    public void Singleton()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public class Context { }

    void SetUpSM()
    {
        //Setup della state machine e del context
        animController = GetComponent<Animator>();

        Context context = new Context()
        {

        };
        foreach (StateBehaviourBase state in animController.GetBehaviours<StateBehaviourBase>())
        {
            state.Setup(context);
        }
    }

    public static Action GoToMainMenu;
    public static Action GoToPause;
    public static Action GoToFigthFase;
    public static Action GoToRepairFase;


    //events
    private void HandleOnMainMenu()
    {
        animController.SetTrigger("GoToMainMenu");
    }

    private void HandleOnFightFase()
    {
        animController.SetTrigger("GoToFightFase");
    }

    private void HandleOnRepairFase()
    {
        animController.SetTrigger("GoToRepairFase");
    }

    private void HandleOnPause()
    {
        animController.SetTrigger("GoToPause");
    }

    private void OnDisable()
    {
        if (instance == this)
        {
            GoToMainMenu -= instance.HandleOnMainMenu;
            GoToPause -= instance.HandleOnPause;
            GoToFigthFase -= instance.HandleOnFightFase;
            GoToRepairFase -= instance.HandleOnRepairFase;

        }
    }
}
