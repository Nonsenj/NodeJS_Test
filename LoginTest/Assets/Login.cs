using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{

    [SerializeField] private string authenticationEndpoint = "http://127.0.0.1:3000/account";

    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private Button loginButton;
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    public void OnloginClick()
    {
        alertText.text = "Signning in...";
        loginButton.interactable = false;
        StartCoroutine(TryLogin());
    }

    private IEnumerator TryLogin()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        if ((username.Length < 3 || username.Length > 24) || (password.Length <3 || password.Length > 24))
        {
            alertText.text = "Invalid credentials";
            loginButton.interactable = true;
            yield break;
        }


        UnityWebRequest request = UnityWebRequest.Get($"{authenticationEndpoint}?rUsername={username}&rPassword={password}");
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;

            if (startTime > 10.0f) break;
            yield return null;
        }

        if (request.result == UnityWebRequest.Result.Success)
        {
            if (request.downloadHandler.text != "Invalid credentials")
            {
                alertText.text = "Welcome";
                loginButton.interactable = false;
                GameAccounts returnedAccount = JsonUtility.FromJson<GameAccounts>(request.downloadHandler.text);
                alertText.text = $"{returnedAccount._id} Welcome" + returnedAccount.username;
            }
            else
            {
                alertText.text = "Invalid credentials";
                loginButton.interactable = true;
            }
        }
        else
        {
            alertText.text = "Error conneting to the server...";
        }

        
        yield return null;
    }
}
