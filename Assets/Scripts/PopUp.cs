using UnityEngine;

public abstract class PopUp
{
    protected PopUp(GameObject go)
    {
        this.go = go;
    }

    public abstract void Update();
    
    public GameObject go { set; get; }
}

public class PopUpAnnoying : PopUp
{
    public PopUpAnnoying(GameObject go)
        : base(go)
    {
    }

    public override void Update()
    { }
}

public class PopUpDownload : PopUp
{
    public PopUpDownload(GameObject go)
        : base(go)
    {
    }

    public override void Update()
    { }
}
