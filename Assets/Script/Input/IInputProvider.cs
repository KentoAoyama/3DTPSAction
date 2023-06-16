using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputProvider
{
    Vector2 GetMoveDir();

    bool GetDash();

    bool GetJump();

    bool GetShoot();

    bool GetAim();

    bool GetChangeWindow();
}