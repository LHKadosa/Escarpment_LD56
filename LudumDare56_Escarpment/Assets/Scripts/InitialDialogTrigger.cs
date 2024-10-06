using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialDialogTrigger : MonoBehaviour
{
    public DialogController dialogController;

    void Start()
    {
        List<string> startDialog = new List<string>
        {
            "Hey you! Why don't you come here and exterminate these tiny creatures, would ya?",
            "Trial by fire, hahahaaaaa.",
            "Oh and there's a scientist trapped somewhere around here, what an idiot...",
            "Good luck little machinist!"
        };

        dialogController.StartDialog(startDialog);
    }
}
