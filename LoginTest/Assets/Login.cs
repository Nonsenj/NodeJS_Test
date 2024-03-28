using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    public void OnloginClick()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        Debug.Log($"{username}:{password}");
    }
}
