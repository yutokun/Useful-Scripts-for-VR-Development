using UnityEngine;

public class OculusEntitlementChecker : MonoBehaviour
{
    void Awake()
    {
        var logger = Debug.unityLogger;

#if UNITY_EDITOR
        logger.logEnabled = false;
#endif

        try
        {
            Oculus.Platform.Core.AsyncInitialize();
            Oculus.Platform.Entitlements.IsUserEntitledToApplication().OnComplete(message =>
            {
                if (message.IsError)
                {
                    logger.LogError("Failed", "You are NOT entitled to use this app.");
                    Application.Quit();
                }
                else
                {
                    logger.Log("You are entitled to use this app.");
                }
            });
        }
        catch (System.Exception ex)
        {
            logger.LogError("Failed", "Platform failed to initialize due to exception.");
            logger.LogException(ex);
            Application.Quit();
        }
    }
}