using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBehavior : EnemyBehaviorTree
{
    #region Conditions
    protected override bool OnCondition(Conditions condition, object sender, Skill.Framework.AI.BehaviorParameterCollection parameters)
    {
        switch(condition)
        {
            case Conditions.SeePoliceman:
                return SeePoliceman_Condition(parameters);
            case Conditions.FriendsArround:
                return FriendsArround_Condition(parameters);
            case Conditions.NoFriendsArround:
                return NoFriendsArround_Condition(parameters);
            case Conditions.IsLookLikePossible:
                return IsLookLikePossible_Condition(parameters);
            case Conditions.NoIsLookLikePossible:
                return NoIsLookLikePossible_Condition(parameters);
            case Conditions.FriendsArroundFight:
                return FriendsArroundFight_Condition(parameters);
            case Conditions.AlreadySeePoliceman:
                return AlreadySeePoliceman_Condition(parameters);
        }
        return false;
    }

    private bool SeePoliceman_Condition(Skill.Framework.AI.BehaviorParameterCollection parameters)
    {
        EnemyControllerScript ai = (EnemyControllerScript)UserData;
        if (ai.GetPolicemanVisible().Count == 0)
            return false;
        else
            return true;
    }

    private bool FriendsArround_Condition(Skill.Framework.AI.BehaviorParameterCollection parameters)
    {
        EnemyControllerScript ai = (EnemyControllerScript)UserData;
        if (ai.GetFriendsArround().Count == 0)
            return false;
        else
            return true;
    }

    private bool NoFriendsArround_Condition(Skill.Framework.AI.BehaviorParameterCollection parameters)
    {
        return !FriendsArround_Condition(parameters);
    }

    private bool IsLookLikePossible_Condition(Skill.Framework.AI.BehaviorParameterCollection parameters)
    {
        EnemyControllerScript ai = (EnemyControllerScript)UserData;
        List<PolicemanScript> policemans = ai.GetPolicemanVisible();
        int numberPVPoliceman = 0;
        for (int i = 0; i < policemans.Count; ++i)
        {
            numberPVPoliceman += policemans[i].GetRandomPV();
        }
        if (numberPVPoliceman > ai.Pv)
            return false;
        else
            return true;
    }

    private bool NoIsLookLikePossible_Condition(Skill.Framework.AI.BehaviorParameterCollection parameters)
    {
        return !IsLookLikePossible_Condition(parameters);
    }

    private bool FriendsArroundFight_Condition(Skill.Framework.AI.BehaviorParameterCollection parameters)
    {
        EnemyControllerScript ai = (EnemyControllerScript)UserData;
        List<EnemyControllerScript> friends = ai.GetFriendsArround();
        for (int i = 0; i < friends.Count; ++i)
        {
            if (friends[i].GetIsFighting())
                return true;
        }
        return false;
    }

    private bool AlreadySeePoliceman_Condition(Skill.Framework.AI.BehaviorParameterCollection parameters)
    {
        EnemyControllerScript ai = (EnemyControllerScript)UserData;
        return ai.GetAlreadySeePoliceman();
    }
    #endregion

    #region Actions
    protected override Skill.Framework.AI.BehaviorResult OnAction(Actions action, object sender, Skill.Framework.AI.BehaviorParameterCollection parameters)
    {
        switch (action)
        {
            case Actions.Fire:
                return Fire_Action(parameters);
            case Actions.Delay:
                return Delay_Action(parameters);
            case Actions.Escape:
                return Escape_Action(parameters);
            case Actions.GoFriend:
                return GoFriend_Action(parameters);
            case Actions.GoExit:
                return GoExit_Action(parameters);
        }
        return Skill.Framework.AI.BehaviorResult.Failure;
    }

    private Skill.Framework.AI.BehaviorResult Fire_Action(Skill.Framework.AI.BehaviorParameterCollection parameters)
    {
        EnemyControllerScript ai = (EnemyControllerScript)UserData;
        ai.Fire();
        return Skill.Framework.AI.BehaviorResult.Success;
    }

    Skill.Framework.TimeWatch _DelayTW;
    private Skill.Framework.AI.BehaviorResult Delay_Action(Skill.Framework.AI.BehaviorParameterCollection parameters)
    {
        if (_DelayTW.IsEnabled)
        {
            if (_DelayTW.IsOver)
            {
                _DelayTW.End();
                return Skill.Framework.AI.BehaviorResult.Success;
            }
        }
        else
        {
            EnemyControllerScript ai = (EnemyControllerScript)UserData;
            _DelayTW.Begin(ai.DelayBetweenTwoFires);
        }
        return Skill.Framework.AI.BehaviorResult.Running;
    }

    private Skill.Framework.AI.BehaviorResult Escape_Action(Skill.Framework.AI.BehaviorParameterCollection parameters)
    {
        EnemyControllerScript ai = (EnemyControllerScript)UserData;
        ai.Escape();
        return Skill.Framework.AI.BehaviorResult.Success;
    }

    private Skill.Framework.AI.BehaviorResult GoFriend_Action(Skill.Framework.AI.BehaviorParameterCollection parameters)
    {
        EnemyControllerScript ai = (EnemyControllerScript)UserData;
        List<EnemyControllerScript> friends = ai.GetFriendsArround();
        for (int i = 0; i < friends.Count; ++i)
        {
            if (friends[i].GetIsFighting())
            {
                ai.GoToHelpFriend(friends[i]);
                break;
            }
        }
        return Skill.Framework.AI.BehaviorResult.Success;
    }

    private Skill.Framework.AI.BehaviorResult GoExit_Action(Skill.Framework.AI.BehaviorParameterCollection parameters)
    {
        EnemyControllerScript ai = (EnemyControllerScript)UserData;
        ai.GoToExit();
        return Skill.Framework.AI.BehaviorResult.Success;
    }
    #endregion

    #region Reset Action
    protected override void OnActionReset(Actions action)
    {
    }
    #endregion
}
