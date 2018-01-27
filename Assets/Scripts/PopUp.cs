using UnityEngine;

public enum PopUpDownloadType
{

}

public abstract class PopUp
{
    protected PopUp(Vector2 pos)
    {
        isInstantiate = false;
    }

    public abstract void Update();

    public bool isInstantiate { set; get; }
}

public class PopUpDownload : PopUp
{
    public PopUpDownload(Vector2 pos)
        : base(pos)
    {
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
