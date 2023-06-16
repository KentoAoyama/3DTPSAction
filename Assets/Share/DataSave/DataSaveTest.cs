using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveTest : MonoBehaviour
{
    /// <summary>
    /// シングルトンのデータセーブクラスのセーブの処理を行う
    /// 使用する型をここで決めておいてもいいかも
    /// </summary>
    public void Save(ButtonsDataController.ButtonData saveObj, string fileName)
    {
        DataSaveManager.Instance.Save(saveObj, fileName);
    }

    /// <summary>
    /// シングルトンのデータセーブクラスのロード処理を行う
    /// </summary>
    public ButtonsDataController.ButtonData Load(string fileName)
    {
        return DataSaveManager.Instance.Load<ButtonsDataController.ButtonData>(fileName);   
    }
}
