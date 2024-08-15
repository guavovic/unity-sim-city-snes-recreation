using System;

public struct SceneTransitionActions
{
    public Action[] ActionsBeforeSceneChange { get; set; }
    public Action[] ActionsAfterSceneChange { get; set; }
}