﻿using Abstractions;
using UnityEngine;

namespace _Root.Scripts.Core.Unit
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        public override void ExecuteSpecificCommand(IAttackCommand command)
        {
            Debug.Log($"{name} attacked {command.Target}");
        }
    }
}