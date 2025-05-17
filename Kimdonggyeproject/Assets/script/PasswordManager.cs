using UnityEngine;
using TMPro;

public class PasswordManager : MonoBehaviour
{
    public TMP_Text passwordText;
    private string enteredPassword = "";
    private string correctPassword = "1234"; // ���ϴ� ��й�ȣ

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
            // 4�ڸ� �̻��̸� �Է����� ����
            if (enteredPassword.Length >= 4)
                return;

            enteredPassword += value;
        }

        passwordText.text = enteredPassword;
    }
}
