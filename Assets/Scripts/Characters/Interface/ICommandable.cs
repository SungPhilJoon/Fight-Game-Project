using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandable
{
    void LeftPunch();
    void RightPunch();
    void LeftKick();
    void RightKick();

    void OnExecuteLeftPunchAttack();
    void OnExecuteRightPunchAttack();
    void OnExecuteLeftKickAttack();
    void OnExecuteRightKickAttack();
}
