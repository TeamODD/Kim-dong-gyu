using UnityEngine;
using TMPro;

public class PasswordManager : MonoBehaviour
{
    public TMP_Text passwordText;
    private string enteredPassword = "";
    private string correctPassword = "1234"; // 원하는 비밀번호

    public void OnButtonClick(string value)
    {
        if (value == "Del")
        {
            if (enteredPassword.Length > 0)
                enteredPassword = enteredPassword.Substring(0, enteredPassword.Length - 1);
        }
        else if (value == "Enter")
        {
            if (enteredPassword == correctPassword)
                passwordText.text = "Clear!";
            else
                passwordText.text = "Error!";

            enteredPassword = "";
            return;
        }
        else
        {
            // 4자리 이상이면 입력하지 않음
            if (enteredPassword.Length >= 4)
                return;

            enteredPassword += value;
        }

        passwordText.text = enteredPassword;
    }
}
