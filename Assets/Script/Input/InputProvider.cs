using UnityEngine;

public class InputProvider : IInputProvider
{
    public bool GetDash()
    {
        return Input.GetButton("Dash");
    }

    public Vector2 GetMoveDir()
    {
        //�Ƃ肠�������u���œ��͂��󂯎��
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        return new Vector2(h, v);
    }
}
