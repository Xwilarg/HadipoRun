using UnityEngine;

public abstract class PopUp
{
    protected PopUp(GameObject go)
    {
        isInstantiate = false;
        this.go = go;
    }

    public abstract void Update();

    public bool isInstantiate { set; get; }
    private GameObject go { set; get; }
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
