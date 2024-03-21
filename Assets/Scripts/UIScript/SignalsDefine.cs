using deVoid.Utils;
using UnityEngine;

public class OpenUIGamePlay : ASignal
{
}

public class HideUIGamePlay : ASignal
{
}

public class OpenUIMainMenu : ASignal
{
}

public class HideUIMainMenu : ASignal
{
}

public class ShowDPSMenu : ASignal
{
}

public class HideDPSMenu : ASignal
{
}
//_______________
public class ShowUISetting : ASignal
{
}
public class HideUISetting : ASignal
{
}
public class ShowUINotificaltion : ASignal
{
}
public class HideUINotificaltion : ASignal
{
}
public class OpenUISoundSetting : ASignal
{
}
public class CloseUISoundSetting : ASignal
{
}
public class OpenSceneGamePlay : ASignal
{
}
public class OpenSceneSelectStage : ASignal 
{
}
public class HideStageSelectionUI : ASignal
{
}
//public class OpenStartGameScene : ASignal
//{ 
//}
public class OpenShop : ASignal
{
}
public class HideShop : ASignal
{
}
public class OpenTest : ASignal
{
}
public class OnLoginButtonClicked : ASignal <string,string>
{ 
}
public class SendMessageLoginRegister : ASignal <string,Color>
{ 
}
public class SendCurrency : ASignal <int>
{ 
}
public class SendMoneyLevelRequired : ASignal <int>
{ 
}


