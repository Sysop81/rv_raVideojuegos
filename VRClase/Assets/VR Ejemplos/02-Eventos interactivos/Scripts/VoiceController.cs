using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceController : MonoBehaviour
{

    [SerializeField] private GameObject luz;
    private KeywordRecognizer _keywordRecognizer;
    private Dictionary<string, Action> _wordToAction;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("script voice control");
        _wordToAction = new Dictionary<string, Action>();
        _wordToAction.Add("luz", TurrnLightOn);
        _wordToAction.Add("apagar",TurnOffLight);

        _keywordRecognizer = new KeywordRecognizer(_wordToAction.Keys.ToArray());
        _keywordRecognizer.OnPhraseRecognized += WordRecognizer;
        _keywordRecognizer.Start();
    }

    private void WordRecognizer(PhraseRecognizedEventArgs args)
    {
        Debug.Log("RECONOCIENDO --> " + args.text);
        _wordToAction[args.text].Invoke();
    }

    private void TurnOffLight()
    {
        luz.SetActive(false);
    }

    private void TurrnLightOn()
    {
        luz.SetActive(true);
    }
}
