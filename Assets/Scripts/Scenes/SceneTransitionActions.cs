using System;

public sealed class SceneTransitionActions
{
    public Action[] ActionsBeforeSceneChange { get; set; }
    public Action[] ActionsAfterSceneChange { get; set; }
}