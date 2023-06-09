using UnityEngine;

public class InputProvider : IInputProvider
{
    public bool GetDash()
    {
        return Input.GetButton("Dash");
    }

    public Vector2 GetMoveDir()
    {
        //とりあえず仮置きで入力を受け取る
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        return new Vector2(h, v);
    }

    public bool GetJump()
    {
        return Input.GetButton("Jump");
    }

    public bool GetShoot()
    {
        return Input.GetButton("Fire1");
    }

    public bool GetAim()
    {
        return Input.GetButton("Fire2");
    }

    public bool GetChangeWindow()
    {
        return Input.GetButtonDown("Cancel");
    }
}
