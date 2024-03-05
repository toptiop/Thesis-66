using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    [SerializeField]
    private Stack<ICommand> commandQueue = new Stack<ICommand>();


    public List<ICommand> commands = new ();


    //public void AddCommand(ICommand command)
    //{
    //    commandQueue.Enqueue(command);
    //}

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        commandQueue.Push(command);
        
    }

    public void UndoLastCommand()
    {
        if (commandQueue.Count > 0)
        {
            ICommand lastCommand = commandQueue.Pop();
            // สำหรับกรณีที่ต้องการสร้างเมธอด Undo ใน Command แต่ในที่นี้ไม่ได้ให้ตัวอย่าง
            // lastCommand.Undo();
        }
    }

    private void OnDrawGizmos()
    {
        Debug.Log("Queue count: " + commandQueue.Count);
    }
}
