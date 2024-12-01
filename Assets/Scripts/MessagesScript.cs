using System.Collections.Generic;
using UnityEngine;

public class MessagesScript : MonoBehaviour
{
    private float timeout = 5.0f;
    private float leftTime;
    private GameObject content;
    private TMPro.TextMeshProUGUI messageTMP;
    private static Queue<Message> messageQueue = new Queue<Message>();

    void Start()
    {
        content = transform
            .Find("Content")
            .gameObject;

        messageTMP = transform
            .Find("Content/MessageText")
            .GetComponent<TMPro.TextMeshProUGUI>();

        leftTime = 0f;
    }

    void Update()
    {
        if (leftTime > 0)
        {
            leftTime -= Time.deltaTime;
            if (leftTime <= 0)
            {
                messageQueue.Dequeue();
                content.SetActive(false);
            }
        }
        else
        {
            if (messageQueue.Count > 0)
            {
                Message message = messageQueue.Peek();
                messageTMP.text = message.text;
                leftTime = message.timeout ?? this.timeout;
                content.SetActive(true);
            }
        }
    }

    public static void ShowMessage(string message, float? timeout = null)
    {
        if (messageQueue.Count > 0)
        {
            Message msg = messageQueue.Peek();
            if (msg.text == message)
            {
                Debug.Log($"Message '{message}' ignored");
                return;
            }
        }
        messageQueue.Enqueue(new Message
        {
            text = message, 
            timeout = timeout
        });
    }

    private class Message
    {
        public string text { get; set; }
        public float? timeout { get; set; }
    }
}
