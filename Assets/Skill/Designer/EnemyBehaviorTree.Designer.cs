using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Skill.Framework.AI;

public abstract class EnemyBehaviorTree : Skill.Framework.AI.BehaviorTree
{

// Internal Enumurators
protected enum Actions
{
Fire = 0,
Delay = 1,
Escape = 2,
GoFriend = 3,
GoExit = 4,
}
protected enum Conditions
{
SeePoliceman = 0,
FriendsArround = 1,
NoFriendsArround = 2,
IsLookLikePossible = 3,
NoIsLookLikePossible = 4,
FriendsArroundFight = 5,
AlreadySeePoliceman = 6,
}

// Internal class
public static class AccessKeys
{

// Variables
private static Skill.Framework.AI.CounterLimitAccessKey _CanFire;

// Properties
public static  Skill.Framework.AI.CounterLimitAccessKey CanFire { get { return _CanFire; } }

// Methods
 static   AccessKeys()
{
_CanFire = new Skill.Framework.AI.CounterLimitAccessKey("CanFire",2);

}

}
public static class StateNames
{

// Variables
public static string _Root = "Root";

// Properties

// Methods

}

// Variables
private  Skill.Framework.AI.BehaviorTreeState _Root = null;
private  Skill.Framework.AI.SequenceSelector _FightWithFriends = null;
private  Skill.Framework.AI.Condition _SeePoliceman = null;
private  Skill.Framework.AI.Condition _FriendsArround = null;
private  Skill.Framework.AI.LoopSelector _FireLoop = null;
private  Skill.Framework.AI.Action _Fire = null;
private  Skill.Framework.AI.Action _Delay = null;
private  Skill.Framework.AI.SequenceSelector _FightAlone = null;
private  Skill.Framework.AI.Condition _NoFriendsArround = null;
private  Skill.Framework.AI.Condition _IsLookLikePossible = null;
private  Skill.Framework.AI.SequenceSelector _EscapeFight = null;
private  Skill.Framework.AI.Condition _NoIsLookLikePossible = null;
private  Skill.Framework.AI.Action _Escape = null;
private  Skill.Framework.AI.SequenceSelector _GoHelpFriend = null;
private  Skill.Framework.AI.Condition _FriendsArroundFight = null;
private  Skill.Framework.AI.Action _GoFriend = null;
private  Skill.Framework.AI.SequenceSelector _GoToExit = null;
private  Skill.Framework.AI.Condition _AlreadySeePoliceman = null;
private  Skill.Framework.AI.Action _GoExit = null;

// Properties
public  override string DefaultState { get { return "Root"; } }

// Methods
private   bool SeePoliceman_Condition(object sender, Skill.Framework.AI.BehaviorParameterCollection parameters)
{
return OnCondition( Conditions.SeePoliceman, sender ,  parameters);
}
private   bool FriendsArround_Condition(object sender, Skill.Framework.AI.BehaviorParameterCollection parameters)
{
return OnCondition( Conditions.FriendsArround, sender ,  parameters);
}
private   Skill.Framework.AI.BehaviorResult Fire_Action(object sender, Skill.Framework.AI.BehaviorParameterCollection parameters)
{
return OnAction( Actions.Fire , sender ,  parameters);
}
private   Skill.Framework.AI.BehaviorResult Delay_Action(object sender, Skill.Framework.AI.BehaviorParameterCollection parameters)
{
return OnAction( Actions.Delay , sender ,  parameters);
}
private   bool NoFriendsArround_Condition(object sender, Skill.Framework.AI.BehaviorParameterCollection parameters)
{
return OnCondition( Conditions.NoFriendsArround, sender ,  parameters);
}
private   bool IsLookLikePossible_Condition(object sender, Skill.Framework.AI.BehaviorParameterCollection parameters)
{
return OnCondition( Conditions.IsLookLikePossible, sender ,  parameters);
}
private   bool NoIsLookLikePossible_Condition(object sender, Skill.Framework.AI.BehaviorParameterCollection parameters)
{
return OnCondition( Conditions.NoIsLookLikePossible, sender ,  parameters);
}
private   Skill.Framework.AI.BehaviorResult Escape_Action(object sender, Skill.Framework.AI.BehaviorParameterCollection parameters)
{
return OnAction( Actions.Escape , sender ,  parameters);
}
private   bool FriendsArroundFight_Condition(object sender, Skill.Framework.AI.BehaviorParameterCollection parameters)
{
return OnCondition( Conditions.FriendsArroundFight, sender ,  parameters);
}
private   Skill.Framework.AI.BehaviorResult GoFriend_Action(object sender, Skill.Framework.AI.BehaviorParameterCollection parameters)
{
return OnAction( Actions.GoFriend , sender ,  parameters);
}
private   bool AlreadySeePoliceman_Condition(object sender, Skill.Framework.AI.BehaviorParameterCollection parameters)
{
return OnCondition( Conditions.AlreadySeePoliceman, sender ,  parameters);
}
private   Skill.Framework.AI.BehaviorResult GoExit_Action(object sender, Skill.Framework.AI.BehaviorParameterCollection parameters)
{
return OnAction( Actions.GoExit , sender ,  parameters);
}
protected  abstract Skill.Framework.AI.BehaviorResult OnAction(Actions action, object sender, Skill.Framework.AI.BehaviorParameterCollection parameters);
protected  abstract void OnActionReset(Actions action);
protected  abstract bool OnCondition(Conditions condition, object sender, Skill.Framework.AI.BehaviorParameterCollection parameters);
protected  override Skill.Framework.AI.BehaviorTreeState[] CreateTree()
{
this._Root = new Skill.Framework.AI.BehaviorTreeState("Root");
this._FightWithFriends = new Skill.Framework.AI.SequenceSelector("FightWithFriends");
this._SeePoliceman = new Skill.Framework.AI.Condition("SeePoliceman",SeePoliceman_Condition);
this._FriendsArround = new Skill.Framework.AI.Condition("FriendsArround",FriendsArround_Condition);
this._FireLoop = new Skill.Framework.AI.LoopSelector("FireLoop");
this._FireLoop.LoopCount = 0;
this._Fire = new Skill.Framework.AI.Action("Fire", Fire_Action, Skill.Framework.Posture.Unknown);
this._Delay = new Skill.Framework.AI.Action("Delay", Delay_Action, Skill.Framework.Posture.Unknown);
this._FightAlone = new Skill.Framework.AI.SequenceSelector("FightAlone");
this._NoFriendsArround = new Skill.Framework.AI.Condition("NoFriendsArround",NoFriendsArround_Condition);
this._IsLookLikePossible = new Skill.Framework.AI.Condition("IsLookLikePossible",IsLookLikePossible_Condition);
this._EscapeFight = new Skill.Framework.AI.SequenceSelector("EscapeFight");
this._NoIsLookLikePossible = new Skill.Framework.AI.Condition("NoIsLookLikePossible",NoIsLookLikePossible_Condition);
this._Escape = new Skill.Framework.AI.Action("Escape", Escape_Action, Skill.Framework.Posture.Unknown);
this._GoHelpFriend = new Skill.Framework.AI.SequenceSelector("GoHelpFriend");
this._FriendsArroundFight = new Skill.Framework.AI.Condition("FriendsArroundFight",FriendsArroundFight_Condition);
this._GoFriend = new Skill.Framework.AI.Action("GoFriend", GoFriend_Action, Skill.Framework.Posture.Unknown);
this._GoToExit = new Skill.Framework.AI.SequenceSelector("GoToExit");
this._AlreadySeePoliceman = new Skill.Framework.AI.Condition("AlreadySeePoliceman",AlreadySeePoliceman_Condition);
this._GoExit = new Skill.Framework.AI.Action("GoExit", GoExit_Action, Skill.Framework.Posture.Unknown);

this._Root.Add(_FightWithFriends,null);
this._Root.Add(_FightAlone,null);
this._Root.Add(_EscapeFight,null);
this._Root.Add(_GoHelpFriend,null);
this._Root.Add(_GoToExit,null);
this._FightWithFriends.Add(_SeePoliceman,null);
this._FightWithFriends.Add(_FriendsArround,null);
this._FightWithFriends.Add(_FireLoop,null);
this._FireLoop.Add(_Fire,null);
this._FireLoop.Add(_Delay,null);
this._FightAlone.Add(_SeePoliceman,null);
this._FightAlone.Add(_NoFriendsArround,null);
this._FightAlone.Add(_IsLookLikePossible,null);
this._FightAlone.Add(_FireLoop,null);
this._EscapeFight.Add(_SeePoliceman,null);
this._EscapeFight.Add(_NoFriendsArround,null);
this._EscapeFight.Add(_NoIsLookLikePossible,null);
this._EscapeFight.Add(_Escape,null);
this._GoHelpFriend.Add(_FriendsArroundFight,null);
this._GoHelpFriend.Add(_GoFriend,null);
this._GoToExit.Add(_AlreadySeePoliceman,null);
this._GoToExit.Add(_GoExit,null);
Skill.Framework.AI.BehaviorTreeState[] states = new Skill.Framework.AI.BehaviorTreeState[1];
states[0] = _Root;
return states;

}

}

