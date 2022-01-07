using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Threading;
using System.Threading.Tasks;


[ExecuteInEditMode]
public class GanerateListScript : GenerateBase
{
    public List<GenerateBase> generatesQueue;

    public override async void StartGenerate()
    {
        bool forcontinue = false;

        foreach (var gen in generatesQueue)
        {
            forcontinue = false;
            gen.Complite += ChangeBool;

            gen.StartGenerate();

            while (forcontinue)
                await Task.Delay(1000);

            gen.Complite -= ChangeBool;
        }
        Complite?.Invoke();
        return;
        void ChangeBool() => forcontinue = true;
    }

}
